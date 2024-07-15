using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    private static GameSettings _instance;

    public static GameSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.FindObjectsOfTypeAll<GameSettings>().FirstOrDefault();
                if (_instance == null)
                {
                    _instance = CreateInstance<GameSettings>();
                    string assetPath = "Assets/Settings/GameSettings.asset";
                    AssetDatabase.CreateAsset(_instance, assetPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    Debug.LogWarning(
                        $"No instance of {typeof(GameSettings)} found. A new one was created at {assetPath}.");
                }
            }

            return _instance;
        }
    }

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.LogWarning(
                $"Another instance of {typeof(GameSettings)} already exists. This instance will be destroyed.");
            DestroyImmediate(this);
        }
    }

    public int updateVersionNumber = 0;
    public int packageVersionNumber = 1;
    public string PackageVersion => "v" + packageVersionNumber + "." + updateVersionNumber;
    public string AppRootVersion => "v" + packageVersionNumber + ".0";
}