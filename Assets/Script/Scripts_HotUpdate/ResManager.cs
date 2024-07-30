using System.Threading.Tasks;
using Script.Scripts_HotUpdate;
using UnityEditor;
using UnityEngine;
using YooAsset;

namespace Script
{
    public class ResManager : Singleton<ResManager>
    {
        public async Task<T> Load<T>(string path) where T : UnityEngine.Object
        {
            if (!Application.isPlaying)
            {
                return AssetDatabase.LoadAssetAtPath<T>(path);
            }
            else
            {
                var package = YooAssets.GetPackage("DefaultPackage");
                var handle = package.LoadAssetAsync<T>(path);
                await handle.Task;
                var t = (T)handle.AssetObject;
                return t;
            }
        }
        
    }
}