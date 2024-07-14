using System.Reflection;
using UnityEngine;
using YooAsset;
using System.IO;

namespace Script.Scripts_AOT
{

    public class Root : MonoBehaviour
    {
        /// <summary>
        /// 是否是编辑器模式
        /// </summary>
        public EPlayMode playMode;

        /// <summary>
        /// 获取资源服务器地址
        /// </summary>
        private string GetHostServerURL()
        {
            string hostServerIP = "http://192.168.100.210:8080";
            string appVersion = "v1.0";

#if UNITY_EDITOR
            if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
                return $"{hostServerIP}/Android/DefaultPackage/{appVersion}";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
                return $"{hostServerIP}/iOS/DefaultPackage/{appVersion}";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
                return $"{hostServerIP}/WebGL/DefaultPackage/{appVersion}";
            else
                return $"{hostServerIP}/PC/DefaultPackage/{appVersion}";
#else
        if (Application.platform == RuntimePlatform.Android)
            return $"{hostServerIP}/Android/DefaultPackage/{appVersion}";
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            return $"{hostServerIP}/iOS/DefaultPackage/{appVersion}";
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
            return $"{hostServerIP}/WebGL/DefaultPackage/{appVersion}";
        else
            return $"{hostServerIP}/PC/DefaultPackage/{appVersion}";
#endif
        }

        private async void Start()
        {
            //初始化 YooAsset
            // 初始化资源系统
            YooAssets.Initialize();
            // 创建默认的资源包
            var package = YooAssets.CreatePackage("DefaultPackage");
            // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
            YooAssets.SetDefaultPackage(package);
            var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline.ToString();

            // 编辑器下的模拟模式
            InitializationOperation initializationOperation = null;
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                var createParameters = new EditorSimulateModeParameters();
                createParameters.SimulateManifestFilePath =
                    EditorSimulateModeHelper.SimulateBuild(buildPipeline, "DefaultPackage");
                initializationOperation = package.InitializeAsync(createParameters);
            }

            // 单机运行模式
            if (playMode == EPlayMode.OfflinePlayMode)
            {
                var createParameters = new OfflinePlayModeParameters();
                createParameters.DecryptionServices = new FileStreamDecryption();
                initializationOperation = package.InitializeAsync(createParameters);
            }

            // 联机运行模式
            if (playMode == EPlayMode.HostPlayMode)
            {
                string defaultHostServer = GetHostServerURL();
                string fallbackHostServer = GetHostServerURL();
                var createParameters = new HostPlayModeParameters();
                createParameters.DecryptionServices = new FileStreamDecryption();
                createParameters.BuildinQueryServices = new GameQueryServices();
                createParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                initializationOperation = package.InitializeAsync(createParameters);
            }

            // WebGL运行模式
            if (playMode == EPlayMode.WebPlayMode)
            {
                string defaultHostServer = GetHostServerURL();
                string fallbackHostServer = GetHostServerURL();
                var createParameters = new WebPlayModeParameters();
                createParameters.DecryptionServices = new FileStreamDecryption();
                createParameters.BuildinQueryServices = new GameQueryServices();
                createParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                initializationOperation = package.InitializeAsync(createParameters);
            }

            if (initializationOperation != null)
            {
                await initializationOperation.Task;

                // 如果初始化失败弹出提示界面
                if (initializationOperation.Status != EOperationStatus.Succeed)
                {
                    Debug.LogWarning($"{initializationOperation.Error}");
                }
                else
                {
                    var version = initializationOperation.PackageVersion;
                    Debug.Log($"Init resource package version : {version}");
                }
            }

            //3 获取资源版本
            var packageVersion = "";
            var operation = package.UpdatePackageVersionAsync();
            await operation.Task;
            if (operation.Status == EOperationStatus.Succeed)
            {
                //更新成功
                packageVersion = operation.PackageVersion;
                Debug.Log($"Updated package Version : {packageVersion}");
            }
            else
            {
                //更新失败
                Debug.LogError(operation.Error);
            }

            // 更新成功后自动保存版本号，作为下次初始化的版本。
            // 也可以通过operation.SavePackageVersion()方法保存。
            var operation2 = package.UpdatePackageManifestAsync(packageVersion);
            await operation2.Task;

            if (operation2.Status == EOperationStatus.Succeed)
            {
                //更新成功
            }
            else
            {
                //更新失败
                Debug.LogError(operation2.Error);
            }

            var downloader = package.CreateResourceDownloader(10, 3);

            if (downloader.TotalDownloadCount == 0)
            {
                Debug.Log("没有需要下载的资源");
            }

            //需要下载的文件总数和总大小
            int totalDownloadCount = downloader.TotalDownloadCount;
            long totalDownloadBytes = downloader.TotalDownloadBytes;

            //注册回调方法
            // downloader.OnDownloadErrorCallback = OnDownloadErrorFunction;
            // downloader.OnDownloadProgressCallback = OnDownloadProgressUpdateFunction;
            // downloader.OnDownloadOverCallback = OnDownloadOverFunction;
            // downloader.OnStartDownloadFileCallback = OnStartDownloadFileFunction;

            //开启下载
            downloader.BeginDownload();
            await downloader.Task;

            //检测下载结果
            Debug.Log(downloader.Status == EOperationStatus.Succeed);


            //7 补充元数据
            foreach (var assetInfo in package.GetAssetInfos("Metadata"))
            {
                Debug.Log("Metadata: " + assetInfo.AssetPath);
                AssetHandle handle = package.LoadAssetAsync<TextAsset>(assetInfo.AssetPath);
                await handle.Task;
                TextAsset textAsset = handle.AssetObject as TextAsset;
                if (textAsset == null) continue;
                byte[] dllBytes = textAsset.bytes;
                HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, HybridCLR.HomologousImageMode.SuperSet);
            }

            //8 华佗LoadDll
            foreach (var assetInfo in package.GetAssetInfos("HotUpdate"))
            {
                Debug.Log("HotUpdate: " + assetInfo.AssetPath);
                AssetHandle handle = package.LoadAssetAsync<TextAsset>(assetInfo.AssetPath);
                await handle.Task;
                TextAsset textAsset = handle.AssetObject as TextAsset;
                if (textAsset == null) continue;
                byte[] dllBytes = textAsset.bytes;
                Assembly.Load(dllBytes);
            }

            //9 加载结束 运行游戏
            string location = "Assets/GameRes/Scenes/GamePlay";
            var sceneMode = UnityEngine.SceneManagement.LoadSceneMode.Single;
            SceneHandle sceneHandle = package.LoadSceneAsync(location, sceneMode);
            await sceneHandle.Task;
        }
    }
    
    
    /// <summary>
    /// 远端资源地址查询服务类
    /// </summary>
    public class RemoteServices : IRemoteServices
    {
        private readonly string defaultHostServer;
        private readonly string fallbackHostServer;

        public RemoteServices(string defaultHostServer, string fallbackHostServer)
        {
            this.defaultHostServer = defaultHostServer;
            this.fallbackHostServer = fallbackHostServer;
        }

        string IRemoteServices.GetRemoteMainURL(string fileName)
        {
            return $"{defaultHostServer}/{fileName}";
        }

        string IRemoteServices.GetRemoteFallbackURL(string fileName)
        {
            return $"{fallbackHostServer}/{fileName}";
        }
    }

    /// <summary>
    /// 资源文件流加载解密类
    /// </summary>
    public class FileStreamDecryption : IDecryptionServices
    {
        /// <summary>
        /// 同步方式获取解密的资源包对象
        /// 注意：加载流对象在资源包对象释放的时候会自动释放
        /// </summary>
        AssetBundle IDecryptionServices.LoadAssetBundle(DecryptFileInfo fileInfo, out Stream managedStream)
        {
            BundleStream bundleStream =
                new BundleStream(fileInfo.FileLoadPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            managedStream = bundleStream;
            return AssetBundle.LoadFromStream(bundleStream, fileInfo.ConentCRC, GetManagedReadBufferSize());
        }

        /// <summary>
        /// 异步方式获取解密的资源包对象
        /// 注意：加载流对象在资源包对象释放的时候会自动释放
        /// </summary>
        AssetBundleCreateRequest IDecryptionServices.LoadAssetBundleAsync(DecryptFileInfo fileInfo,
            out Stream managedStream)
        {
            BundleStream bundleStream =
                new BundleStream(fileInfo.FileLoadPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            managedStream = bundleStream;
            return AssetBundle.LoadFromStreamAsync(bundleStream, fileInfo.ConentCRC, GetManagedReadBufferSize());
        }

        private static uint GetManagedReadBufferSize()
        {
            return 1024;
        }
    }

    /// <summary>
    /// 资源文件偏移加载解密类
    /// </summary>
    public class FileOffsetDecryption : IDecryptionServices
    {
        /// <summary>
        /// 同步方式获取解密的资源包对象
        /// 注意：加载流对象在资源包对象释放的时候会自动释放
        /// </summary>
        AssetBundle IDecryptionServices.LoadAssetBundle(DecryptFileInfo fileInfo, out Stream managedStream)
        {
            managedStream = null;
            return AssetBundle.LoadFromFile(fileInfo.FileLoadPath, fileInfo.ConentCRC, GetFileOffset());
        }

        /// <summary>
        /// 异步方式获取解密的资源包对象
        /// 注意：加载流对象在资源包对象释放的时候会自动释放
        /// </summary>
        AssetBundleCreateRequest IDecryptionServices.LoadAssetBundleAsync(DecryptFileInfo fileInfo,
            out Stream managedStream)
        {
            managedStream = null;
            return AssetBundle.LoadFromFileAsync(fileInfo.FileLoadPath, fileInfo.ConentCRC, GetFileOffset());
        }

        private static ulong GetFileOffset()
        {
            return 32;
        }
    }

    /// <summary>
    /// 资源文件解密流
    /// </summary>
    public class BundleStream : FileStream
    {
        private const byte Key = 64;

        public BundleStream(string path, FileMode mode, FileAccess access, FileShare share) : base(path, mode, access,
            share)
        {
        }

        public BundleStream(string path, FileMode mode) : base(path, mode)
        {
        }

        public override int Read(byte[] array, int offset, int count)
        {
            var index = base.Read(array, offset, count);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] ^= Key;
            }

            return index;
        }
    }

}