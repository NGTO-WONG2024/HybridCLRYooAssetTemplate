#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
    public class AllIn1SpringsDebuggerWindow : EditorWindow
    {
        [MenuItem("Tools/AllIn1/SpringsWindow")]
        public static void ShowAllIn1VfxToolkitWindowWindow()
        {
            GetWindow<AllIn1SpringsDebuggerWindow>("All In 1 Springs Debugger Window");
        }

		private const string Version = "1.0";
        private const string DEBUGGER_ALLIN1SPRINGSTOOLKIT = "ALLIN1SPRINGS_DEBUGGER";
        private const string ICON_TEXTURE_NAME = "AllIn1SpringsToolkitEditorIcon";
        private const string UP_ARROW = "\u2191";
        private const string DOWN_ARROW = "\u2193";

		const float configWidth = 43f + 10f;

		const float nameAndTypeMaxWidth = 300f;
		private float totalWidthOfLine;
        private Texture2D imageInspector;
        private bool debuggerEnabled;
        public Vector2 scrollPosition = Vector2.zero;
        private GUIStyle boxStyle, bigLabel, headerLabelStyle;
        private const int bigFontSize = 16;
        private string currentDefines;
        private int currentTab = 0;
        
		private SpringRotationDrawer springQuaternionDrawer;
		private SpringVector3Drawer springVector3Drawer;
		private SpringVector2Drawer springVector2Drawer;
		private SpringFloatDrawer springFloatDrawer;
		private SpringColorDrawer springColorDrawer;

		private void OnGUI()
		{
			InitializeGuiStylesIfNeeded();
			RefreshSpringDrawers();

			EditorGUILayout.ScrollViewScope scrollView = new EditorGUILayout.ScrollViewScope(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height));
			using (scrollView)
			{
				scrollPosition = scrollView.scrollPosition;
				RenderHeaderAndTabs();
				if (currentTab == 0)
				{
					EnableOrDisableFeature();
					if (debuggerEnabled) ShowDebugger();
				}
				else
				{
					AutoComponentIconChange();
					DocsAndContact();
				}
				CloseLineAndCurrentVersion();
			}
		}

		

		private void RefreshSpringDrawers()
		{
			if(springQuaternionDrawer == null)
			{
				springQuaternionDrawer = new SpringRotationDrawer(false, true);
			}
			if(springVector3Drawer == null)
			{
				springVector3Drawer = new SpringVector3Drawer(false, true);
			}
			if(springVector2Drawer == null)
			{
				springVector2Drawer = new SpringVector2Drawer(false, true);
			}
			if(springFloatDrawer == null)
			{
				springFloatDrawer = new SpringFloatDrawer(false, true);
			}
			if(springColorDrawer == null)
			{
				springColorDrawer = new SpringColorDrawer(false, true);
			}
		}

		private SpringDrawer GetSpringDrawerBySpring(Spring spring)
		{
			SpringDrawer res = null;

			if(spring is SpringRotation)
			{
				res = springQuaternionDrawer;
			}
			else if(spring is SpringVector3)
			{
				res = springVector3Drawer;
			}
			else if(spring is SpringVector2)
			{
				res = springVector2Drawer;
			}
			else if(spring is SpringFloat)
			{
				res = springFloatDrawer;
			}
			else if(spring is SpringColor)
			{
				res = springColorDrawer;
			}

			return res;
		}

        private void OnEnable() => GetImageInspectorIfNeeded();

        private void InitializeGuiStylesIfNeeded()
        {
            if(headerLabelStyle != null || boxStyle != null || bigLabel != null) return;
            headerLabelStyle = new GUIStyle(GUI.skin.label);
            headerLabelStyle.normal.textColor = Color.white;
            headerLabelStyle.fontStyle = FontStyle.Bold;
            headerLabelStyle.fontSize = 12;
            headerLabelStyle.alignment = TextAnchor.MiddleCenter;
            const float colorRgb = 0.0f;
            headerLabelStyle.normal.background = MakeTex(200, 30, new Color(colorRgb, colorRgb, colorRgb, 1f));
            headerLabelStyle.normal.textColor = Color.white;
            
            boxStyle = new GUIStyle(EditorStyles.helpBox);
            boxStyle.margin = new RectOffset(0, 0, 0, 0);
            boxStyle.alignment = TextAnchor.LowerLeft;
            boxStyle.normal.textColor = Color.white;
            
            bigLabel = new GUIStyle(EditorStyles.boldLabel);
            bigLabel.fontSize = bigFontSize;
            bigLabel.alignment = TextAnchor.LowerLeft;
            bigLabel.normal.textColor = Color.white;
        }

        private void GetImageInspectorIfNeeded()
        {
            if(imageInspector == null) imageInspector = Resources.Load<Texture2D>(SpringsToolkitConstants.CUSTOM_EDITOR_HEADER);
        }

        private void RenderHeaderAndTabs()
        {
            GetImageInspectorIfNeeded();
            if(imageInspector)
            {
                Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(50));
                GUI.DrawTexture(rect, imageInspector, ScaleMode.ScaleToFit, true);
            }
            else GUILayout.Label("AllIn1SpringsToolkit Debugger v" + Version, bigLabel);
            DrawLine(Color.grey, 1, 3);
            currentTab = GUILayout.Toolbar(currentTab, new string[] {"Springs Debugger", "Other"});
            DrawLine(Color.grey, 1, 3);
        }

        private void EnableOrDisableFeature()
        {
            currentDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            debuggerEnabled = HasDebuggerDefineSymbol();
            bool newDebuggerEnabled = EditorGUILayout.Toggle("Enable Spring Debugger?", debuggerEnabled);
            if(newDebuggerEnabled != debuggerEnabled)
            {
                if(newDebuggerEnabled) EnableDebuggerSymbol();
                else DisableDebuggerSymbol();
            }
        }

		private void ShowDebugger()
		{
			#if ALLIN1SPRINGS_DEBUGGER
			GUILayout.Space(10);
			DrawLine(Color.grey, 1, 3);
			if (!Application.isPlaying)
			{
				GUILayout.Label("Here you can visualize and edit your current springs");
				GUILayout.Label("Enter PLAY mode to use this feature", EditorStyles.boldLabel);
				return;
			}

			const float configWidth = 43f + 10f; //GetLabelWidth("Config") is 43
			totalWidthOfLine = configWidth + nameAndTypeMaxWidth + nameAndTypeMaxWidth + 73f + 73f;
			GUI.backgroundColor = Color.black;

			EditorGUILayout.BeginHorizontal("box");
			headerLabelStyle = new GUIStyle(EditorStyles.helpBox);
			headerLabelStyle.margin = new RectOffset(0, 0, 0, 0);
			headerLabelStyle.fontSize = 12;
			headerLabelStyle.fontStyle = FontStyle.Bold;
			GUILayout.Label("Config", headerLabelStyle, GUILayout.Width(configWidth));
			GUILayout.Label("GameObject Name", headerLabelStyle, GUILayout.MaxWidth(nameAndTypeMaxWidth));
			GUILayout.Label("Spring Component Type", headerLabelStyle, GUILayout.MaxWidth(nameAndTypeMaxWidth));
			GUILayout.Label("State", headerLabelStyle, GUILayout.Width(73f));
			GUILayout.Label("Ping", headerLabelStyle, GUILayout.Width(73f));
			EditorGUILayout.EndHorizontal();

			DrawLine(Color.grey, 1, 1, totalWidthOfLine);
			headerLabelStyle.fontSize = 11;
			headerLabelStyle.fontStyle = FontStyle.Normal;
			for (int i = 0; i < AllIn1DebuggerWindowData.springComponentsStates.Count; i++)
			{
				AllIn1DebuggerWindowData.SpringComponentState springComponentState = AllIn1DebuggerWindowData.springComponentsStates[i];

				Color backgroundColor = Color.white;
				if (i % 2 == 1) backgroundColor = Color.black;
				GUI.backgroundColor = backgroundColor;
				EditorGUILayout.BeginHorizontal("box");
				string upOrDownArrow = springComponentState.isExpanded ? UP_ARROW : DOWN_ARROW;
				if (GUILayout.Button(upOrDownArrow, GUILayout.Width(configWidth)))
				{
					if (Event.current.alt) //If alt is pressed we do the opposite for all the springs we didn't click
					{
						foreach(AllIn1DebuggerWindowData.SpringComponentState springState in AllIn1DebuggerWindowData.springComponentsStates)
						{
							if(springState != springComponentState)
							{
								springState.isExpanded = springComponentState.isExpanded;
							}
						}
					}
					springComponentState.isExpanded = !springComponentState.isExpanded;
				}

				GUILayout.Label(springComponentState.GetName(), headerLabelStyle, GUILayout.MaxWidth(nameAndTypeMaxWidth));
				GUILayout.Label(springComponentState.GetTypeName(), headerLabelStyle, GUILayout.MaxWidth(nameAndTypeMaxWidth));
				bool isActive = springComponentState.IsActiveInHierarchy();
				GUI.backgroundColor = Color.white;
				if (GUILayout.Button(isActive ? "Active" : "Inactive", GUILayout.Width(70)))
				{
					isActive = !isActive;
					springComponentState.SetActive(isActive);
				}
				if (GUILayout.Button("Ping", GUILayout.Width(70))) EditorGUIUtility.PingObject(springComponentState.springComponent);
				EditorGUILayout.EndHorizontal();

				if (springComponentState.isExpanded)
				{
					DrawSpringComponentState(springComponentState, backgroundColor);
				}
			}
			#endif
		}

		#if ALLIN1SPRINGS_DEBUGGER
		private void DrawSpringComponentState(AllIn1DebuggerWindowData.SpringComponentState springComponentState, Color backgroundColor)
		{
			for (int i = 0; i < springComponentState.springsStates.Count; i++)
			{
				GUI.backgroundColor = backgroundColor;
				AllIn1DebuggerWindowData.SpringState springState = springComponentState.springsStates[i];

				GUILayout.BeginHorizontal();
				GUILayout.Space(20);
				
				EditorGUILayout.BeginHorizontal("box");
				string upOrDownArrow = springState.isExpanded ? UP_ARROW : DOWN_ARROW;
				if (GUILayout.Button(upOrDownArrow, GUILayout.Width(configWidth)))
				{
					if (Event.current.alt) //If alt is pressed we do the opposite for all the springs we didn't click
					{
						foreach(AllIn1DebuggerWindowData.SpringState springS in springComponentState.springsStates)
						{
							if(springS != springState)
							{
								springS.isExpanded = springState.isExpanded;
							}
						}
					}
					springState.isExpanded = !springState.isExpanded;
				}

				EditorGUILayout.LabelField(springState.springName, GUILayout.Width(totalWidthOfLine - 70f));

				EditorGUILayout.EndHorizontal();
				
				GUILayout.EndHorizontal();

				if (springState.isExpanded)
				{
					GUI.backgroundColor = Color.white;

					SerializedObject so = new SerializedObject(springComponentState.springComponent);
					so.Update();
					SerializedProperty sp = so.FindProperty(springState.springName);

					SpringDrawer springDrawer = GetSpringDrawerBySpring(springState.spring);
					springDrawer.SetParentProperty(sp);

					Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: springDrawer.GetPropertyHeight());
					rect.x += 35f;
					rect.width -= 35f;
				
					springDrawer.OnGUI(rect, sp, GUIContent.none);
					
					so.ApplyModifiedProperties();
				}
			}
		}
		#endif

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i) pix[i] = col;
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        private void AutoComponentIconChange()
        {
			#if UNITY_2021_2_OR_NEWER
            if(!Application.isPlaying)
            {
                GUILayout.Label("Here you can automatically change all the icons of the custom spring components");
                if(GUILayout.Button("Update Custom Springs Component Icons")) UpdateCustomSpringComponentIcons();
                if(GUILayout.Button("Remove All Custom Springs Component Icons")) UpdateCustomSpringComponentIcons(remove: true);
				DrawLine(Color.grey, 1, 3);
            }
			#endif
        }
        
        private void DocsAndContact()
        {
            GUILayout.Label("If you need help with the asset please check the documentation pdf");
            GUILayout.Label("And if you have any questions or suggestions please contact me at: seasidegamestudios@gmail.com");
        }

        private void CloseLineAndCurrentVersion()
        {
            GUILayout.Space(10);
            DrawLine(Color.grey, 1, 3);
            GUILayout.Label("Current asset version is " + Version, EditorStyles.boldLabel);
        }

        #region DebuggerDefineSymbol
        private bool HasDebuggerDefineSymbol()
        {
			//AllIn1SpringsToolkit
			return currentDefines.Contains(DEBUGGER_ALLIN1SPRINGSTOOLKIT);
        }

        private void EnableDebuggerSymbol()
        {
            if(!debuggerEnabled) currentDefines += ";" + DEBUGGER_ALLIN1SPRINGSTOOLKIT;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, currentDefines);
            SceneViewNotificationAndLog("AllIn1SpringsToolkit Debugger Enabled");
        }

        private void DisableDebuggerSymbol()
        {
            currentDefines = currentDefines.Replace(DEBUGGER_ALLIN1SPRINGSTOOLKIT + ";", "");
            currentDefines = currentDefines.Replace(";" + DEBUGGER_ALLIN1SPRINGSTOOLKIT, "");
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, currentDefines);
            SceneViewNotificationAndLog("AllIn1SpringsToolkit Debugger Disabled");
        }
        #endregion


		private void DrawLine(Color color, float thickness = 2f, float padding = 10f, float width = -1f)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            if(width > 0f) r.width = width;
            r.height = thickness;
            r.y += (padding / 2f);
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

		private static void SceneViewNotificationAndLog(string message)
        {
            ShowSceneViewNotification(message);
        }

		private static void ShowSceneViewNotification(string message)
        {
            bool showNotification = EditorPrefs.GetInt("DisplaySceneViewNotifications", 1) == 1;
            if(!showNotification) return;
            
            GUIContent content = new GUIContent(message);
            #if UNITY_2019_1_OR_NEWER
            SceneView.lastActiveSceneView.ShowNotification(content, 1.5f);
            #else
            SceneView.lastActiveSceneView.ShowNotification(content);
            #endif
        }

		#if UNITY_2021_2_OR_NEWER
	    private void UpdateCustomSpringComponentIcons(bool remove = false)
        {
            Texture2D iconTexture = Resources.Load<Texture2D>(ICON_TEXTURE_NAME);
            if (iconTexture == null)
            {
                Debug.LogError($"No icon found with the name {ICON_TEXTURE_NAME}, did you delete it?");
                return;
            }

            string[] csFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories)
	            .Where(f => Path.GetFileName(f).Contains("Spring")).ToArray();
            for (int i = 0; i < csFiles.Length; i++)
            {
                string csFilePath = csFiles[i];
                string csFileContent = File.ReadAllText(csFilePath);
	            if(!csFileContent.Contains("AllIn1SpringsToolkit")) continue;
                MonoImporter monoImporter = AssetImporter.GetAtPath(GetRelativePathFromAssetsPath(csFilePath)) as MonoImporter;
                if(monoImporter == null) continue;
                monoImporter.SetIcon(null);
                if(!remove && csFileContent.Contains(": SpringComponent") && !csFileContent.Contains("AllIn1SpringsDebuggerWindow")) monoImporter.SetIcon(iconTexture);
                monoImporter.SaveAndReimport();
            }

            Debug.Log("AllIn1SpringsToolkit icon update completed");
        }
		#endif
        
        private string GetRelativePathFromAssetsPath(string absolutePath)
        {
            string projectPath = Application.dataPath;
            if (absolutePath.StartsWith(projectPath)) absolutePath = "Assets" + absolutePath.Substring(projectPath.Length);
            return absolutePath;
        }
    }
}
#endif