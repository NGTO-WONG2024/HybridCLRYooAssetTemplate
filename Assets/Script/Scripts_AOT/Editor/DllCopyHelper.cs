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
        static readonly string platform = (EditorUserBuildSettings.activeBuildTarget).ToString();
        static readonly string projectPath = System.IO.Path.GetDirectoryName(Application.dataPath);

        private static string MetaDataFolder =>
            Path.Combine(projectPath, "HybridCLRData", "AssembliesPostIl2CppStrip", platform);

        private static string HotUpdateFolder => Path.Combine(projectPath, "HybridCLRData", "HotUpdateDlls", platform);

        [MenuItem("HybridCLR/My/CopyDll", priority = 101)]
        public static void CopyDll()
        {
            foreach (var item in HybridCLRSettings.Instance.hotUpdateAssemblyDefinitions)
            {
                var filePath = Path.Combine(HotUpdateFolder, item.name + ".dll");
                var folder = Path.Combine(Application.dataPath, "GameRes", "Dll", "HotUpdate");
                CopyDllAndRename(filePath, folder);
            }

            foreach (var item in AOTGenericReferences.PatchedAOTAssemblyList)
            {
                var filePath = Path.Combine(MetaDataFolder, item);
                var folder = Path.Combine(Application.dataPath, "GameRes", "Dll", "Metadata");
                CopyDllAndRename(filePath, folder);
            }
        }

        private static void CopyDllAndRename(string dllFilePath, string targetFolder)
        {
            File.Copy(dllFilePath, Path.Combine(targetFolder, Path.GetFileName(dllFilePath) + ".bytes"), true);
        }

        [MenuItem("HybridCLR/My/HotUpdateIOS", priority = 121)]
        public static void HotUpdateIOS()
        {
            AssetDatabase.Refresh();
            CompileDllCommand.CompileDllIOS();
            AssetDatabase.Refresh();
            CopyDll();
            AssetDatabase.Refresh();
            var appVersionNumber = EditorPrefs.GetInt("appVersion", 1); //大版本
            var updateVersionNumber = EditorPrefs.GetInt("updateVersion", 1); //小版本
            var appVersion = "v" + appVersionNumber + "." + 0;
            var packageVersion = "v" + appVersionNumber + "." + updateVersionNumber; //v1.0 v1.1
            YooIncrementBuild(EBuildMode.IncrementalBuild, packageVersion);
            AssetDatabase.Refresh();
            string destinationFolder = Path.Combine(AssetBundleBuilderHelper.GetDefaultBuildOutputRoot(), "iOS",
                "DefaultPackage", appVersion);
            string sourceFolder = Path.Combine(AssetBundleBuilderHelper.GetDefaultBuildOutputRoot(), "iOS",
                "DefaultPackage", packageVersion);
            CopyAssetToCdn(sourceFolder, destinationFolder);
            AssetDatabase.Refresh();
            EditorPrefs.SetInt("updateVersion", updateVersionNumber + 1);
        }

        static void CopyAssetToCdn(string sourceFolder, string destinationFolder)
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

        public static void YooIncrementBuild(EBuildMode eBuildMode, string packageVersion)
        {
            Debug.Log($"开始构建 : ");
            var buildoutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot();
            var streamingAssetsRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot();

            // 构建参数
            BuiltinBuildParameters buildParameters = new BuiltinBuildParameters();
            buildParameters.BuildOutputRoot = buildoutputRoot;
            buildParameters.BuildinFileRoot = streamingAssetsRoot;
            buildParameters.BuildPipeline = EBuildPipeline.BuiltinBuildPipeline.ToString();
            buildParameters.BuildTarget = BuildTarget.iOS;
            buildParameters.BuildMode = eBuildMode;
            buildParameters.PackageName = "DefaultPackage";
            buildParameters.PackageVersion = packageVersion;
            buildParameters.VerifyBuildingResult = true;
            buildParameters.EnableSharePackRule = true; //启用共享资源构建模式，兼容1.5x版本
            buildParameters.FileNameStyle = EFileNameStyle.BundleName;
            buildParameters.BuildinFileCopyOption = EBuildinFileCopyOption.None;
            buildParameters.BuildinFileCopyParams = string.Empty;
            buildParameters.CompressOption = ECompressOption.LZ4;

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

// 从构建命令里获取参数示例
        private static string GetBuildPackageName()
        {
            foreach (string arg in System.Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("buildPackage"))
                    return arg.Split("="[0])[1];
            }

            return string.Empty;
        }
    }
}