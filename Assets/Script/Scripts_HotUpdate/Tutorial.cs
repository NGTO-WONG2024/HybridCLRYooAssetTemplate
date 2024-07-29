using System.Collections.Generic;
using System.Threading.Tasks;
using MoreMountains.Tools;
using Script.Scripts_HotUpdate;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
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
            script.TutorialPlay(script.tutorialIndex);
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

    public List<string> clicks = new();
    [FormerlySerializedAs("currentIndex")] public int tutorialIndex = 0;
    
    private int currentFrame;
    private MMFollowTarget unmaskFollow;
    private float timer;

    void Start()
    {
        unmaskFollow = unmask.GetComponent<MMFollowTarget>();
        currentFrame = 0;  // 初始化当前帧索引
        timer = 0.0f;  // 初始化计时器
        TutorialPlay(0);
    }

    void Update()
    {
        AliceGif();
        CopySizeAndPosition();
        CheckClick();
    }

    void CheckClick()
    {
        bool clickInside = false;
        if (Input.GetMouseButtonDown(0))
        {
            clickInside = IsMouseOverImage(Input.mousePosition);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                clickInside=IsMouseOverImage(touch.position);
            }
        }

        if (clickInside)
        {
            tutorialIndex+=1;
            TutorialPlay(tutorialIndex);
        }
        
    }
    
    public bool IsMouseOverImage(Vector2 clickPos)
    {
        if (targetArea == null)
        {
            return false;
        }
        Vector2 localMousePosition = targetArea.InverseTransformPoint(clickPos);
        return targetArea.rect.Contains(localMousePosition);
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
    private void CopySizeAndPosition()
    {
        if (unmask == null || targetArea == null)
        {
            return;
        }

        // 拷贝大小
        
        unmask.sizeDelta = targetArea.sizeDelta;
        unmaskFollow.Target = targetArea;
    }

    public async void TutorialPlay(int index)
    {
        if (index >= clicks.Count)
        {
            gameObject.SetActive(false);
            return;
        }
        Transform t = null;
        for (int i = 0; i < 20; i++)
        {
            Debug.Log("finding");
            t = Game.Instance.transform.Find(clicks[index]);
            if (t != null)
            {
                break;
            }
            await Task.Delay(500);
        }
        targetArea = t.GetComponent<RectTransform>();

    }



}