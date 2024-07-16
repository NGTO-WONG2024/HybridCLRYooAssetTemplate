using System;
using System.Reflection;
using UnityEngine;
using YooAsset;
using System.Threading.Tasks;
using UnityEditor;

namespace Script.Scripts_AOT
{
    public class Root : MonoBehaviour
    {
        public EPlayMode playMode;
        public string hostServerIP = "http://192.168.100.210:8080";


        private async void Start()
        {
            // 初始化
            var package = await InitYooAsset();
            // 更新资源
            await UpdateAsset(package);
            // 华佗补充元数据+loadDll
            await HybridCLRLoad(package);
            //更新结束 进入GamePlay场景
            string location = "Assets/GameRes/Scenes/GamePlay";
            SceneHandle sceneHandle = package.LoadSceneAsync(location);
            await sceneHandle.Task;
        }

        private async Task<ResourcePackage> InitYooAsset()
        {
            YooAssets.Initialize();
            var package = YooAssets.CreatePackage("DefaultPackage");
            YooAssets.SetDefaultPackage(package);
            string hostServer = "";
#if UNITY_EDITOR
            string rootVersion = PlayerSettings.bundleVersion.Split('.')[0] + ".0";
            hostServer = EditorUserBuildSettings.activeBuildTarget switch
            {
                BuildTarget.Android => $"{hostServerIP}/Android/DefaultPackage/{rootVersion}",
                BuildTarget.iOS => $"{hostServerIP}/iOS/DefaultPackage/{rootVersion}",
                BuildTarget.WebGL => $"{hostServerIP}/WebGL/DefaultPackage/{rootVersion}",
                _ => $"{hostServerIP}/PC/DefaultPackage/{rootVersion}"
            };
#else
            string rootVersion = Application.version.Split('.')[0] + ".0";
            hostServer = Application.platform switch
            {
                RuntimePlatform.Android => $"{hostServerIP}/Android/DefaultPackage/{rootVersion}",
                RuntimePlatform.IPhonePlayer => $"{hostServerIP}/iOS/DefaultPackage/{rootVersion}",
                RuntimePlatform.WebGLPlayer => $"{hostServerIP}/WebGL/DefaultPackage/{rootVersion}",
                _ => $"{hostServerIP}/PC/DefaultPackage/{rootVersion}"
            };
#endif
            var fallBackHostServer = hostServer;
            InitializationOperation initializationOperation = null;
            switch (playMode)
            {
                case EPlayMode.EditorSimulateMode:
                    var editorSimulateModeParameters = new EditorSimulateModeParameters();
                    editorSimulateModeParameters.SimulateManifestFilePath =
                        EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(),
                            "DefaultPackage");
                    initializationOperation = package.InitializeAsync(editorSimulateModeParameters);
                    break;
                // 单机运行模式
                case EPlayMode.OfflinePlayMode:
                    var offlinePlayModeParameters = new OfflinePlayModeParameters();
                    offlinePlayModeParameters.DecryptionServices = new FileStreamDecryption();
                    initializationOperation = package.InitializeAsync(offlinePlayModeParameters);
                    break;
                // 联机运行模式
                case EPlayMode.HostPlayMode:
                    var hostPlayModeParameters = new HostPlayModeParameters();
                    hostPlayModeParameters.DecryptionServices = new FileStreamDecryption();
                    hostPlayModeParameters.BuildinQueryServices = new GameQueryServices();
                    hostPlayModeParameters.RemoteServices = new RemoteServices(hostServer, fallBackHostServer);
                    initializationOperation = package.InitializeAsync(hostPlayModeParameters);
                    break;
                // Web模式
                case EPlayMode.WebPlayMode:
                    var webPlayModeParameters = new WebPlayModeParameters();
                    webPlayModeParameters.DecryptionServices = new FileStreamDecryption();
                    webPlayModeParameters.BuildinQueryServices = new GameQueryServices();
                    webPlayModeParameters.RemoteServices = new RemoteServices(hostServer, fallBackHostServer);
                    initializationOperation = package.InitializeAsync(webPlayModeParameters);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (initializationOperation == null) return package;
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

            return package;
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

            var manifestOp = package.UpdatePackageManifestAsync(packageVersion, timeout: 30);
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
                Debug.Log("文件总数: " + downloader.TotalDownloadCount);
                Debug.Log("文件总大小: " + downloader.TotalDownloadBytes);
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

        private static async Task HybridCLRLoad(ResourcePackage package)
        {
            //补充元数据
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

            //华佗LoadDll
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
        }
    }
}