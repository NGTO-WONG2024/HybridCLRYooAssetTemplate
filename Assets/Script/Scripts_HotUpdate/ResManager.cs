using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public async Task<List<T>> LoadAll<T>(string path) where T : UnityEngine.Object
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var handle = package.LoadAllAssetsAsync<T>(path);
            await handle.Task;
            var t = handle.AllAssetObjects;
            var result = t.Cast<T>().ToList();
            return result;
        }
    }
}