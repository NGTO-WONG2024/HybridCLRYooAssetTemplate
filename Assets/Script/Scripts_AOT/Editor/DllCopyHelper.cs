using System.IO;
using HybridCLR.Editor.Commands;
using HybridCLR.Editor.Settings;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

namespace Script.Scripts_Aot.Editor
{
    public class DllCopyHelper
    {
        private static readonly string Platform = (EditorUserBuildSettings.activeBuildTarget).ToString();
        private static readonly string ProjectPath = Path.GetDirectoryName(Application.dataPath);
        private static readonly string DefaultPackage = "DefaultPackage";
        private static string MetaDataFolder => Path.Combine(ProjectPath, "HybridCLRData", "AssembliesPostIl2CppStrip", Platform);
        private static string HotUpdateFolder => Path.Combine(ProjectPath, "HybridCLRData", "HotUpdateDlls", Platform);
        private static string MetaDataResFolder => Path.Combine(Application.dataPath, "GameRes", "Dll", "Metadata");
        private static string HotUpdateResFolder => Path.Combine(Application.dataPath, "GameRes", "Dll", "HotUpdate");
        
        [MenuItem("HybridCLR/My/CopyDll", priority = 101)]
        public static void CopyDll()
        {
            foreach (var item in HybridCLRSettings.Instance.hotUpdateAssemblyDefinitions)
            {
                var sourceFilePath = Path.Combine(HotUpdateFolder, item.name + ".dll");
                CopyDllAndRename(sourceFilePath, HotUpdateResFolder);
            }

            foreach (var item in AOTGenericReferences.PatchedAOTAssemblyList)
            {
                var sourceFilePath = Path.Combine(MetaDataFolder, item);
                CopyDllAndRename(sourceFilePath, MetaDataResFolder);
            }
        }

        private static void CopyDllAndRename(string sourceFilePath, string targetFolder)
        {
            var destFileName = Path.Combine(targetFolder, Path.GetFileName(sourceFilePath) + ".bytes");
            File.Copy(sourceFileName:sourceFilePath, destFileName, true);
        }


        [MenuItem("HybridCLR/My/LogTest", priority = 200)]
        public static void LogTest()
        {
            GameSettings.Instance.updateVersionNumber++;
            Debug.Log(GameSettings.Instance.updateVersionNumber);
        }

        [MenuItem("HybridCLR/My/HotUpdate", priority = 121)]
        public static void HotUpdate()
        {
            AssetDatabase.Refresh();
            GameSettings.Instance.updateVersionNumber += 1;
            AssetDatabase.Refresh();
            
            CompileDllCommand.CompileDllIOS();
            AssetDatabase.Refresh();
            
            CopyDll();
            AssetDatabase.Refresh();
            
            YooIncrementBuild(BuildTarget.iOS, EBuildMode.IncrementalBuild, GameSettings.Instance.PackageVersion);
            AssetDatabase.Refresh();
            
            string destinationFolder = Path.Combine(AssetBundleBuilderHelper.GetDefaultBuildOutputRoot(), Platform,
                DefaultPackage, GameSettings.Instance.AppRootVersion);
            string sourceFolder = Path.Combine(AssetBundleBuilderHelper.GetDefaultBuildOutputRoot(), Platform,
                DefaultPackage, GameSettings.Instance.PackageVersion);
            CopyAssetBundleToCdnFolder(sourceFolder, destinationFolder);
            AssetDatabase.Refresh();
        }

        private static void CopyAssetBundleToCdnFolder(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(sourceFolder)) return;
            if (!Directory.Exists(destinationFolder)) Directory.CreateDirectory(destinationFolder);
            if (sourceFolder == destinationFolder) return;

            string[] files = Directory.GetFiles(sourceFolder);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationFolder, fileName);
                File.Copy(file, destFile, true);
            }
        }

        private static void YooIncrementBuild(BuildTarget buildTarget, EBuildMode eBuildMode, string packageVersion)
        {
            Debug.Log($"开始构建 : ");
            var buildOutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot();
            var streamingAssetsRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot();

            // 构建参数
            BuiltinBuildParameters buildParameters = new BuiltinBuildParameters
            {
                BuildOutputRoot = buildOutputRoot,
                BuildinFileRoot = streamingAssetsRoot,
                BuildPipeline = EBuildPipeline.BuiltinBuildPipeline.ToString(),
                BuildTarget = buildTarget,
                BuildMode = eBuildMode,
                PackageName = DefaultPackage,
                PackageVersion = packageVersion,
                VerifyBuildingResult = true,
                EnableSharePackRule = true, //启用共享资源构建模式，兼容1.5x版本
                FileNameStyle = EFileNameStyle.BundleName,
                BuildinFileCopyOption = EBuildinFileCopyOption.None,
                BuildinFileCopyParams = string.Empty,
                CompressOption = ECompressOption.LZ4
            };

            // 执行构建
            BuiltinBuildPipeline pipeline = new BuiltinBuildPipeline();
            var buildResult = pipeline.Run(buildParameters, true);
            if (buildResult.Success)
            {
                Debug.Log($"构建成功 : {buildResult.OutputPackageDirectory}");
            }
            else
            {
                Debug.LogError($"构建失败 : {buildResult.ErrorInfo}");
            }
        }

        // // 从构建命令里获取参数示例
        // private static string GetBuildPackageName()
        // {
        //     foreach (string arg in System.Environment.GetCommandLineArgs())
        //     {
        //         if (arg.StartsWith("buildPackage"))
        //             return arg.Split("="[0])[1];
        //     }
        //
        //     return string.Empty;
        // }
        
        
    }
}