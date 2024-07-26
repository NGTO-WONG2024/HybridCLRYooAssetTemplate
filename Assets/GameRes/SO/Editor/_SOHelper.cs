using System.Collections.Generic;
using System.IO;
using Script;
using UnityEngine;
using UnityEditor;

public class MyDataCreator
{
    
    public static List<Sprite> LoadAllSpritesInFolder(string folderPath)
    {
        List<Sprite> sprites = new List<Sprite>();
        
        // 获取指定路径下所有文件的相对路径
        string[] fileEntries = Directory.GetFiles(folderPath);

        foreach (string fileName in fileEntries)
        {
            string assetPath = "Assets/" + fileName.Substring(Application.dataPath.Length - 6);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

            if (sprite != null)
            {
                sprites.Add(sprite);
            }
        }

        return sprites;
    }
    
    [MenuItem("Assets/MyData Asset")]
    public static void _SOHelper()
    {
        // 获取目录下的所有 png 文件
        string[] files = Directory.GetFiles(Path.Combine(Application.dataPath, "GameRes","Art","ba","head"), "*.png");;

        List<Sprite> sprites=new ();    
        // 遍历所有文件
        foreach (string file in files)
        {
            // 由于AssetDatabase需要相对路径，我们把绝对路径转换为相对于Assets的路径
            string relativePath = file.Replace(Application.dataPath, "Assets");

            // 加载图片为Sprite
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(relativePath);

            if (sprite != null)
            {
                sprites.Add(sprite);
                Debug.Log("Loaded sprite: " + sprite.name);
            }
            else
            {
                Debug.LogError("Failed to load sprite at path: " + relativePath);
            }
        }


        foreach (var sprite in sprites)
        {
            // 创建ScriptableObject实例
            StudentData asset = ScriptableObject.CreateInstance<StudentData>();
            // 设置文件保存路径
            string path = Path.Combine("Assets", "GameRes", "SO", (sprite.name + ".asset"));
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path);
            asset.headIcon = sprite;
            asset.studentName = sprite.name;
            // 保存ScriptableObject到文件
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            
        }
        
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        
        
    }
    
    
    
    
    
}