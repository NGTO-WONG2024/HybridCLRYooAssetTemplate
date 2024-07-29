using System.Collections.Generic;
using MoreMountains.Tools;
using Script.Scripts_HotUpdate;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Tutorial))]
public class TutorialEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Tutorial script = (Tutorial)target;

        if (GUILayout.Button("a"))
        {
            script.TutorialPlay(script.targetAreaName, "a");
        }
    }
}


[RequireComponent(typeof(UnityEngine.UI.Image))]
public class Tutorial : MonoBehaviour
{
    public RectTransform unmask;
    [MMReadOnly]
    public RectTransform targetArea;

    public string targetAreaName;
    public List<Sprite> frames;  
    public Image alice;
    public float frameRate = 1.0f;  

    private int currentFrame;
    private MMFollowTarget unmaskFollow;
    private float timer;

    void Start()
    {
        unmaskFollow = unmask.GetComponent<MMFollowTarget>();
        currentFrame = 0;  // 初始化当前帧索引
        timer = 0.0f;  // 初始化计时器
    }

    void Update()
    {
        AliceGif();
        CopySizeAndPosition();
    }


    void AliceGif()
    {
        if (frames.Count == 0) return;

        timer += Time.deltaTime;

        // 检查是否到达下一帧的时间
        if (timer >= (1.0f / frameRate))
        {
            timer -= (1.0f / frameRate);
            currentFrame = (currentFrame + 1) % frames.Count;
            alice.sprite = frames[currentFrame];
        }
    }
    
    // 调用这个函数来拷贝并对齐两个Image的大小和位置
    public void CopySizeAndPosition()
    {
        if (unmask == null || targetArea == null)
        {
            Debug.LogError("Source or target image is null!");
            return;
        }

        // 拷贝大小
        
        unmask.sizeDelta = targetArea.sizeDelta;
        unmaskFollow.Target = targetArea;
    }

    public void TutorialPlay(string gameObjectName, string desc)
    {
        targetArea = Game.Instance.transform.Find(gameObjectName).GetComponent<RectTransform>();
    }



}