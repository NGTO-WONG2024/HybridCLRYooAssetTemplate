using System;
using System.Reflection;
using UnityEngine;
using YooAsset;
using System.IO;
using System.Threading.Tasks;

namespace Script.Scripts_AOT
{
    public class Root : MonoBehaviour
    {
        public EPlayMode playMode;
        public string hostServerIP = "http://192.168.100.210:8080";
        public string appVersion = "v1.0";

        private async void Start()
        {
            //1 初始化
            var package = await InitYooAsset();
            //2 更新资源
            await UpdateAsset(package);

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

        private static async Task UpdateAsset(ResourcePackage package)
        {
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
            var manifestOp = package.UpdatePackageManifestAsync(packageVersion);
            await manifestOp.Task;
            if (manifestOp.Status == EOperationStatus.Succeed)
            {
                //更新成功
            }
            else
            {
                //更新失败
                Debug.LogError(manifestOp.Error);
            }

            var downloader = package.CreateResourceDownloader(10, 3);
            if (downloader.TotalDownloadCount == 0)
            {
                Debug.Log("没有需要下载的资源");
            }
            else
            {
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
            }
        }

        private async Task<ResourcePackage> InitYooAsset()
        {
            //初始化 YooAsset
            YooAssets.Initialize();
            // 创建默认的资源包
            var package = YooAssets.CreatePackage("DefaultPackage");
            // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
            YooAssets.SetDefaultPackage(package);
            var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline.ToString();
            var hostServer = $"{hostServerIP}/{Application.platform.ToString()}/DefaultPackage/{appVersion}";
            var fallBackHostServer = hostServer; //测试 用同一个host 
            // 编辑器下的模拟模式
            InitializationOperation initializationOperation = null;
            switch (playMode)
            {
                case EPlayMode.EditorSimulateMode:
                {
                    var createParameters = new EditorSimulateModeParameters
                    {
                        SimulateManifestFilePath =
                            EditorSimulateModeHelper.SimulateBuild(buildPipeline, "DefaultPackage")
                    };
                    initializationOperation = package.InitializeAsync(createParameters);
                    break;
                }
                // 单机运行模式
                case EPlayMode.OfflinePlayMode:
                {
                    var createParameters = new OfflinePlayModeParameters
                    {
                        DecryptionServices = new FileStreamDecryption()
                    };
                    initializationOperation = package.InitializeAsync(createParameters);
                    break;
                }
                // 联机运行模式
                case EPlayMode.HostPlayMode:
                {
                    var createParameters = new HostPlayModeParameters
                    {
                        DecryptionServices = new FileStreamDecryption(),
                        BuildinQueryServices = new GameQueryServices(),
                        RemoteServices = new RemoteServices(hostServer, fallBackHostServer)
                    };
                    initializationOperation = package.InitializeAsync(createParameters);
                    break;
                }
                // Web模式
                case EPlayMode.WebPlayMode:
                {
                    var createParameters = new WebPlayModeParameters
                    {
                        DecryptionServices = new FileStreamDecryption(),
                        BuildinQueryServices = new GameQueryServices(),
                        RemoteServices = new RemoteServices(hostServer, fallBackHostServer)
                    };
                    initializationOperation = package.InitializeAsync(createParameters);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
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

            return package;
        }
    }
}