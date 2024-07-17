using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;


//unity窗口额外显示当前工程的路径 用来快速区分窗口是哪个分支的unity
[InitializeOnLoad]
public class UnityEditorTitleName
{
    static UnityEditorTitleName()
    {
        EditorApplication.playModeStateChanged += HandlePlayModeStateChanged;
        HandlePlayModeStateChanged(PlayModeStateChange.ExitingPlayMode);
    }

    private static void HandlePlayModeStateChanged(PlayModeStateChange obj)
    {
        async Task DelayCall()
        {
            await Task.Delay(200);
            Type tEditorApplication = typeof(EditorApplication);
            Type tApplicationTitleDescriptor = tEditorApplication.Assembly.GetTypes()
            
                .First(x => x.FullName == "UnityEditor.ApplicationTitleDescriptor");

            EventInfo eiUpdateMainWindowTitle = tEditorApplication.GetEvent("updateMainWindowTitle", BindingFlags.Static | BindingFlags.NonPublic);
            MethodInfo miUpdateMainWindowTitle = tEditorApplication.GetMethod("UpdateMainWindowTitle", BindingFlags.Static | BindingFlags.NonPublic);

            Type delegateType = typeof(Action<>).MakeGenericType(tApplicationTitleDescriptor);
            MethodInfo methodInfo = ((Action<object>)UpdateMainWindowTitle).Method;
            Delegate del = Delegate.CreateDelegate(delegateType, null, methodInfo);

            if (eiUpdateMainWindowTitle == null) return;
            eiUpdateMainWindowTitle.GetAddMethod(true).Invoke(null, new object[] { del });
            if (miUpdateMainWindowTitle != null) miUpdateMainWindowTitle.Invoke(null, Array.Empty<object>());
            eiUpdateMainWindowTitle.GetRemoveMethod(true).Invoke(null, new object[] { del });
        }
        _ = DelayCall();
    }

    static void UpdateMainWindowTitle(object desc)
    {
        var oldValue = 
            (string)typeof(EditorApplication).Assembly.GetTypes()
                .First(x => x.FullName == "UnityEditor.ApplicationTitleDescriptor")
                .GetField("title", BindingFlags.Instance | BindingFlags.Public)
                ?.GetValue(desc);
        typeof(EditorApplication).Assembly.GetTypes()
            .First(x => x.FullName == "UnityEditor.ApplicationTitleDescriptor")
            .GetField("title", BindingFlags.Instance | BindingFlags.Public)
            ?.SetValue(desc, oldValue+" "+Application.dataPath);
    }
}

[InitializeOnLoad]
public class ToolBarEditor
{
    static ToolBarEditor()
    {
        //场景切换
        ToolbarExtender.RightToolbarGUI.Add(OnRightToolbarGUI);
        ToolbarExtender.RightToolbarGUI.Add(OpenScene);
        ToolbarExtender.RightToolbarGUI.Add(LockFrameRate);
    }

    static void OnRightToolbarGUI()
    {
        GUILayout.FlexibleSpace();

        GUIContent buttonContent = EditorGUIUtility.IconContent("d_PlayButton");
        buttonContent.text = "开始游戏";
        if (Application.isPlaying)
        {
            buttonContent = EditorGUIUtility.IconContent("d_Refresh");
            buttonContent.text = "快速重开";
        }

        if (GUILayout.Button(buttonContent))
        {
            SceneHelper.StartScene("GamePlay");
        }
    }

    private static string[] sceneNames = null;
    private static GUILayoutOption width = GUILayout.Width(200);
    private static GUILayoutOption height = GUILayout.Height(40);
    private static int currentSceneIndex = -1;

    private static void OpenScene()
    {
        try
        {
            sceneNames ??= SceneHelper.GetAllSceneNames().ToArray();
            if (currentSceneIndex == -1)
            {
                currentSceneIndex = sceneNames.ToList().IndexOf(SceneManager.GetActiveScene().name);
            }
            var oldIndex = currentSceneIndex;
            currentSceneIndex = EditorGUILayout.Popup(oldIndex, sceneNames, width, height);
            if (currentSceneIndex == oldIndex) return;
            SceneHelper.ChangeScene(sceneNames[currentSceneIndex]);
        }
        catch
        {
            //ignore
            Debug.Log("场景切换加载失败");
        }
    }
    
    private static void LockFrameRate()
    {
        bool isFrameRateLocked = EditorPrefs.GetBool("IsFrameRateLocked", false);

        GUILayout.Label("锁帧");
        isFrameRateLocked = EditorGUILayout.Toggle(isFrameRateLocked, GUILayout.Width(15));
        EditorPrefs.SetBool("IsFrameRateLocked", isFrameRateLocked);

        if (isFrameRateLocked)
        {
            int currentFrameRate = EditorPrefs.GetInt("LockFrameRate", 60);
            int newFrameRate = EditorGUILayout.IntSlider(currentFrameRate, 30, 240);

            if (newFrameRate != currentFrameRate)
            {
                EditorPrefs.SetInt("LockFrameRate", newFrameRate);
            }

            if (Application.isPlaying)
            {
                Application.targetFrameRate = newFrameRate;
            }
        }
    }

}

#region history

[InitializeOnLoad]
public class SelectionLogger
{
    static SelectionLogger()
    {
        Selection.selectionChanged += LogSelection;
    }

    private static void LogSelection()
    {
        if (Selection.activeObject != null)
        {
            Debug.Log("Selected: " + Selection.activeObject.name);
        }
    }
}

#endregion

#region 场景切换

static class SceneHelper
{
    static string _sceneToOpen;
    private static bool _isRun;

    public static void ChangeScene(string sceneName)
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }

        _sceneToOpen = sceneName;
        _isRun = false;
        EditorApplication.update += OnUpdate;
    }

    public static void StartScene(string sceneName)
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }

        _sceneToOpen = sceneName;
        _isRun = true;
        EditorApplication.update += OnUpdate;
    }


    public static List<string> GetSceneNames(string path)
    {
        List<string> sceneNames = new List<string>();
        string[] files = Directory.GetFiles(path, "*.unity", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            sceneNames.Add(name);
        }

        return sceneNames;
    }
    
    public static List<string> GetAllSceneNames()
    {        
        List<string> sceneNames = new List<string>();
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in allAssetPaths)
        {
            if (assetPath.EndsWith(".unity") && (assetPath.Contains("GameRes") || assetPath.Contains("Root") ))
            {
                string name = Path.GetFileNameWithoutExtension(assetPath);
                sceneNames.Add(name);
            }
        }

        return sceneNames;
    }

    static void OnUpdate()
    {
        if (_sceneToOpen == null ||
            EditorApplication.isPlaying || EditorApplication.isPaused ||
            EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }

        EditorApplication.update -= OnUpdate;

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            // need to get scene via search because the path to the scene
            // file contains the package version so it'll change over time
            string[] guids = AssetDatabase.FindAssets("t:scene " + _sceneToOpen, null);
            if (guids.Length == 0)
            {
                Debug.LogWarning("Couldn't find scene file");
            }
            else
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                EditorSceneManager.OpenScene(scenePath);
                EditorApplication.isPlaying = _isRun;
            }
        }

        _sceneToOpen = null;
    }
}

#endregion

#region ToolbarExtender

[InitializeOnLoad]
public static class ToolbarExtender
{
    //static int m_toolCount;
    //static GUIStyle m_commandStyle = null;

    public static readonly List<Action> LeftToolbarGUI = new List<Action>();
    public static readonly List<Action> RightToolbarGUI = new List<Action>();

    static ToolbarExtender()
    {
        ToolbarCallback.OnToolbarGUILeft = GUILeft;
        ToolbarCallback.OnToolbarGUIRight = GUIRight;
    }

#if UNITY_2019_1_OR_NEWER
    public const float playPauseStopWidth = 140;
#else
    public const float playPauseStopWidth = 100;
#endif

    private static void GUILeft()
    {
        var center = EditorGUIUtility.currentViewWidth / 2 - 100;
        GUILayout.BeginHorizontal();
        GUILayout.Space(center - 100 * LeftToolbarGUI.Count - playPauseStopWidth);
        for (int i = 0; i < LeftToolbarGUI.Count; i++)
        {
            LeftToolbarGUI[i].Invoke();
        }

        GUILayout.EndHorizontal();
    }

    private static void GUIRight()
    {
        GUILayout.BeginHorizontal();
        foreach (var handler in RightToolbarGUI)
        {
            handler();
        }

        GUILayout.EndHorizontal();
    }
}

public static class ToolbarCallback
{
    static Type m_toolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");
    static Type m_guiViewType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GUIView");

#if UNITY_2020_1_OR_NEWER
    static Type m_iWindowBackendType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.IWindowBackend");

    static PropertyInfo m_windowBackend = m_guiViewType.GetProperty("windowBackend",
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

    static PropertyInfo m_viewVisualTree = m_iWindowBackendType.GetProperty("visualTree",
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
#else
		static PropertyInfo m_viewVisualTree = m_guiViewType.GetProperty("visualTree",
			BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
#endif

    static FieldInfo m_imguiContainerOnGui = typeof(IMGUIContainer).GetField("m_OnGUIHandler",
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

    static ScriptableObject m_currentToolbar;

    /// <summary>
    /// Callback for toolbar OnGUI method.
    /// </summary>
    public static Action OnToolbarGUI;


    public static Action OnToolbarGUILeft;
    public static Action OnToolbarGUIRight;

    static ToolbarCallback()
    {
        EditorApplication.update -= OnUpdate;
        EditorApplication.update += OnUpdate;
    }

    static void OnUpdate()
    {
        // Relying on the fact that the toolbar is a ScriptableObject and gets deleted when layout changes
        if (m_currentToolbar == null)
        {
            // Find the toolbar
            var toolbars = UnityEngine.Object.FindObjectsOfTypeAll(m_toolbarType);
            m_currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
            if (m_currentToolbar != null)
            {
#if UNITY_2021_1_OR_NEWER
                var root = m_currentToolbar.GetType()
                    .GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
                var rawRoot = root.GetValue(m_currentToolbar);
                var mRoot = rawRoot as VisualElement;
                RegisterCallback("ToolbarZoneLeftAlign", OnToolbarGUILeft);
                RegisterCallback("ToolbarZoneRightAlign", OnToolbarGUIRight);

                void RegisterCallback(string root, Action cb)
                {
                    var toolbarZone = mRoot.Q(root);

                    var parent = new VisualElement()
                    {
                        style =
                        {
                            flexGrow = 1,
                            flexDirection = FlexDirection.Row,
                        }
                    };
                    var container = new IMGUIContainer();
                    container.onGUIHandler += () => { cb?.Invoke(); };
                    parent.Add(container);
                    toolbarZone.Add(parent);
                }
#else
#if UNITY_2020_1_OR_NEWER
					var windowBackend = m_windowBackend.GetValue(m_currentToolbar);

					// Get its visual tree
					var visualTree = (VisualElement) m_viewVisualTree.GetValue(windowBackend, null);
#else
					// Get its visual tree
					var visualTree = (VisualElement) m_viewVisualTree.GetValue(m_currentToolbar, null);
#endif

					// Get the first child which 'happens' to be the toolbar IMGUIContainer
					var container = (IMGUIContainer) visualTree[0];

					// (Re)attach handler
					var handler = (Action) m_imguiContainerOnGui.GetValue(container);
					handler -= OnGUI;
					handler += OnGUI;
					m_imguiContainerOnGui.SetValue(container, handler);

#endif
            }
        }
    }

    static void OnGUI()
    {
        var handler = OnToolbarGUI;
        if (handler != null)
            handler();
    }
}

#endregion