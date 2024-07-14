using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public abstract class SpringComponentCustomEditor : UnityEditor.Editor
	{
		protected static Color AREA_COLOR_01 = new Color(0.3f, 0.3f, 0.3f, 1.0f);
		protected static Color AREA_COLOR_02 = new Color(0.2f, 0.2f, 0.2f, 1.0f);

		//Serialized Properties
		protected SerializedProperty spHasCustomInitialValues;
		protected SerializedProperty spHasCustomTarget;
		protected SerializedProperty spUseScaledTime;

		protected SerializedProperty spGeneralPropertiesUnfolded;
		protected SerializedProperty spInitialValuesUnfolded;

		//Styles
		protected GUIStyle guiStyleLabelTitle;
		protected GUIStyle guiStyleToggle;
		protected GUIStyle guiStyleTitleBox;
		protected GUIStyle guiStyleButtonTittle;

		protected virtual void RefreshSerializedProperties()
		{
			spHasCustomInitialValues = serializedObject.FindProperty("hasCustomInitialValues");
			spHasCustomTarget = serializedObject.FindProperty("hasCustomTarget");
			spUseScaledTime = serializedObject.FindProperty("useScaledTime");

			spGeneralPropertiesUnfolded = serializedObject.FindProperty("generalPropertiesUnfolded");
			spInitialValuesUnfolded = serializedObject.FindProperty("initialValuesUnfolded");
		}

		protected void RefreshStyles()
		{
			guiStyleLabelTitle = new GUIStyle(EditorStyles.boldLabel); 
			guiStyleLabelTitle.normal.textColor = Color.white;

			guiStyleToggle = new GUIStyle(EditorStyles.toggle);

			guiStyleTitleBox = new GUIStyle(EditorStyles.helpBox); 
		}

		protected virtual void OnEnable()
		{
			RefreshSerializedProperties();
			CreateDrawers();
		}

		protected abstract void CreateDrawers();

		protected void DrawMainArea()
		{
			spGeneralPropertiesUnfolded.boolValue = DrawRectangleArea("General Properties", spGeneralPropertiesUnfolded.boolValue);
			serializedObject.ApplyModifiedProperties();

			if (spGeneralPropertiesUnfolded.boolValue)
			{
				DrawMainAreaUnfolded();

				DrawSerializedProperty(spUseScaledTime, LABEL_WIDTH);
			}
		}

		protected virtual void DrawMainAreaUnfolded()
		{

		}

		protected void DrawInitiaValues()
		{
			spInitialValuesUnfolded.boolValue = DrawRectangleArea("Initial Values", spInitialValuesUnfolded.boolValue);
			serializedObject.ApplyModifiedProperties();

			if (spInitialValuesUnfolded.boolValue)
			{
				DrawInitialValuesSection();
			}
		}

		protected virtual void DrawInitialValuesSection()
		{
			EditorGUILayout.BeginVertical();

			spHasCustomInitialValues.boolValue = DrawToggleLayout("Has Custom Initial Values", LABEL_WIDTH, spHasCustomInitialValues.boolValue);

			if (spHasCustomInitialValues.boolValue)
			{
				DrawCustomInitialValuesSection();
				SpringsEditorUtility.Space();
			}

			if (!spHasCustomInitialValues.boolValue && spHasCustomTarget.boolValue)
			{
				SpringsEditorUtility.Space();
			}
			DrawSerializedProperty(spHasCustomTarget, LABEL_WIDTH);
		
			if (spHasCustomTarget.boolValue)
			{
				DrawCustomInitialTarget();
			}

			EditorGUILayout.EndVertical();
		}

		protected abstract void DrawSprings();

		protected abstract void DrawCustomInitialValuesSection();

		protected abstract void DrawCustomInitialTarget();

		protected bool DrawRectangleArea(string areaName, bool foldout)
		{
			return DrawRectangleArea(height: GetTitleAreaHeight(), areaName: areaName, spToggle: null, areaEnabled: true, foldout: foldout);
		}

		protected bool DrawRectangleArea(string areaName, SerializedProperty spToggle, bool foldout)
		{
			return DrawRectangleArea(height: GetTitleAreaHeight(), areaName: areaName, spToggle: spToggle, areaEnabled: true, foldout: foldout);
		}

		protected bool DrawRectangleArea(float height, string areaName, bool foldout)
		{
			return DrawRectangleArea(height: height, areaName: areaName, spToggle: null, areaEnabled: true, foldout: foldout);
		}

		protected bool DrawRectangleArea(float height, string areaName, SerializedProperty spToggle, bool areaEnabled, bool foldout)
		{
			bool res = foldout;

			bool toggleHasChanged = false;

			EditorGUILayout.BeginHorizontal();

			Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: height);
			EditorGUI.DrawRect(rect, AREA_COLOR_01);

			Rect labelRect = new Rect(rect);
			labelRect.x = 25f;
			EditorGUIUtility.labelWidth = 100f;


			if (spToggle != null)
			{
				Rect toggleRect = new Rect(labelRect);
				toggleRect.width = 25f;

				EditorGUI.BeginChangeCheck();
				spToggle.boolValue = EditorGUI.Toggle(toggleRect, spToggle.boolValue);
				toggleHasChanged = EditorGUI.EndChangeCheck();
			}

			labelRect.width = areaName.Length * 10f;
			labelRect.x += 20f;

			EditorGUI.LabelField(labelRect, areaName, guiStyleLabelTitle);

			EditorGUIUtility.labelWidth = 0f;

			EditorGUILayout.EndHorizontal();

			if (Event.current.type == EventType.MouseDown)
			{
				if (rect.Contains(Event.current.mousePosition))
				{
					res = !res;
				}
			}

			if (toggleHasChanged)
			{
				res = true;
			}

			if (spToggle != null)
			{
				res = res && spToggle.boolValue;
			}

			return res;
		}

		protected void DrawSimpleRectangleArea(float height, string areaName)
		{
			EditorGUILayout.BeginHorizontal();

			Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: height);
			EditorGUI.DrawRect(rect, AREA_COLOR_01);

			Rect labelRect = new Rect(rect);
			labelRect.x = rect.width * 0.5f;
			EditorGUIUtility.labelWidth = 100f;
			labelRect.width = EditorGUIUtility.labelWidth;
			EditorGUI.LabelField(labelRect, areaName, guiStyleLabelTitle);

			EditorGUIUtility.labelWidth = 0f;

			EditorGUILayout.EndHorizontal();
		}

		protected float GetTitleAreaHeight()
		{
			float res = EditorGUIUtility.singleLineHeight * 1.5f;
			return res;
		}

		protected void DrawInitialValuesBySpring(string labelUseInitialValues, string labelInitialValues, float width, bool springEnabled, SpringDrawer springDrawer)
		{
			if (springEnabled)
			{
				Rect rectUseInitialValues = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
				springDrawer.DrawUseInitialValues(ref rectUseInitialValues, labelUseInitialValues, width);

				if (springDrawer.springEditorObject.UseInitialValues)
				{
					Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);

					Space();
				}
			}
		}

		protected void DrawInitialValuesBySpring(string labelInitialValues, float width, SpringDrawer springDrawer)
		{
			Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
			springDrawer.DrawCurrentValue(ref rect, labelInitialValues, width);
		}

		protected void DrawCustomTargetBySpring(string labelUseCustomTarget, string labelCustomTarget, float width, bool springEnabled, SpringDrawer springDrawer)
		{
			if (springEnabled)
			{
				Rect rectUseInitialValues = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
				springDrawer.DrawUseCustomTarget(ref rectUseInitialValues, labelUseCustomTarget, width);

				if (springDrawer.springEditorObject.UseCustomTarget)
				{
					Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
					springDrawer.DrawTarget(ref rect, labelCustomTarget, width);

					Space();
				}
			}
		}

		protected void DrawCustomTargetBySpring(string labelCustomTarget, float width, SpringDrawer springDrawer)
		{
			Rect rect = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight);
			springDrawer.DrawTarget(ref rect, labelCustomTarget, width);
		}

		protected void DrawSpring(SpringDrawer springDrawer)
		{
			DrawSpring(springDrawer.springEditorObject.spParentroperty.displayName, springDrawer, null);
		}

		protected void DrawSpringWithEnableToggle(SpringDrawer springDrawer)
		{
			DrawSpring(springDrawer.springEditorObject.spParentroperty.displayName, springDrawer, springDrawer.springEditorObject.spSpringEnabled);
		}

		protected void DrawSpring(string springName, SpringDrawer springDrawer, SerializedProperty spToggle)
		{
			springDrawer.springEditorObject.Unfolded = DrawRectangleArea(
				height: GetTitleAreaHeight(),
				areaName: springName,
				areaEnabled: true,
				spToggle: spToggle,
				foldout: springDrawer.springEditorObject.Unfolded);

			serializedObject.ApplyModifiedProperties();

			if (springDrawer.springEditorObject.Unfolded)
			{
				Rect propertyRect = EditorGUILayout.GetControlRect(hasLabel: false, height: springDrawer.GetPropertyHeight());
				springDrawer.OnGUI(propertyRect);
			}
		}

		protected virtual void DrawInfoArea()
		{

		}

		public override void OnInspectorGUI()
		{
			RefreshStyles();

			serializedObject.Update();

			DrawMainArea();
			DrawInitiaValues();
			DrawSprings();

			DrawInfoArea();

			serializedObject.ApplyModifiedProperties();
		}
	}
}