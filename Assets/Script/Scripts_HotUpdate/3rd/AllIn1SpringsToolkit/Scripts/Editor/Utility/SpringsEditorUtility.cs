using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public static class SpringsEditorUtility
	{
		public const float FOLDOUT_HEIGHT = 16f;
		public const float LABEL_WIDTH = 250f;

		public const string FIELD_NAME_STOP_SPRING_ON_CURRENT_VALUE_CLAMP = "Stop Spring On Current Value Clamp";
		public const string FIELD_NAME_DRAG = "Drag";
		public const string FIELD_NAME_FORCE = "Force";
		public const string FIELD_NAME_UPDATE_AXIS = "Update Axis";
		public const string FIELD_NAME_VELOCITY = "Velocity";
		public const string FIELD_NAME_APPLY_CLAMPING = "Apply Clamping";
		public const string FIELD_NAME_CLAMP_TARGET = "Clamp Target";
		public const string FIELD_NAME_CLAMP_CURRENT_VALUE = "Clamp Current Value";
		public const string FIELD_NAME_MIN_VALUES = "Min Values";
		public const string FIELD_NAME_MAX_VALUES = "Max Values";
		public const string FIELD_NAME_OPERATION_VALUE = "Operation Value";

		public static void UpdateCurrentRect(ref Rect rect)
		{
			UpdateCurrentRect(ref rect, 1);
		}

		public static void UpdateCurrentRect(ref Rect rect, int numLines)
		{
			rect.y += EditorGUIUtility.singleLineHeight * numLines;
		}

		public static void UpdateCurrentRect(ref Rect rect, float yOffset)
		{
			rect.y += yOffset;
		}

		public static Vector4Bool DrawVector4Bool(Rect position, string label, string[] componentsLabel, float labelWidth, Vector4Bool vector4Bool)
		{
			Vector4Bool res = (Vector4Bool)DrawVectorBool(position, label, componentsLabel, labelWidth, vector4Bool);
			return res;
		}

		public static IVectorBool DrawVectorBool(Rect position, string label, string[] componentsLabels, float labelWidth, IVectorBool vectorBool)
		{
			Rect labelRect = new Rect(position);
			labelRect.width = labelWidth;
			EditorGUI.LabelField(labelRect, label);

			Rect toggleRect = new Rect(position);

			IVectorBool res = vectorBool;
			int size = vectorBool.GetSize();

			float widthEachComponent = (position.width - labelWidth) / size;
			float offset = widthEachComponent + 7f;


			toggleRect.width = widthEachComponent;
			toggleRect.x += labelRect.width;

			if (componentsLabels != null)
			{
				EditorGUIUtility.labelWidth = 14f;
				for (int i = 0; i < size; i++)
				{
					res[i] = DrawCustomToggle(toggleRect, componentsLabels[i], res[i]);
					toggleRect.x += offset;
				}
			}
			else
			{
				for (int i = 0; i < size; i++)
				{
					res[i] = DrawCustomToggle(toggleRect, res[i]);
					toggleRect.x += offset;
				}
			}

			return res;
		}

		public static Vector2Bool DrawVector2Bool(Rect position, string label, string[] componentsLabels, float labelWidth, Vector2Bool vector2Bool)
		{
			Vector2Bool res = (Vector2Bool)DrawVectorBool(position, label, componentsLabels, labelWidth, vector2Bool);
			return res;
		}

		public static Vector3Bool DrawVector3Bool(Rect position, string label, string[] componentsLabels, float labelWidth, Vector3Bool vector3Bool)
		{
			Vector3Bool res = (Vector3Bool)DrawVectorBool(position, label, componentsLabels, labelWidth, vector3Bool);
			return res;
		}

		private static bool DrawCustomToggle(Rect toggleRect, string label, bool value)
		{
			bool res = EditorGUI.Toggle(toggleRect, label, value);
			return res;
		}

		private static bool DrawCustomToggle(Rect toggleRect, bool value)
		{
			bool res = EditorGUI.Toggle(toggleRect, value);
			return res;
		}

		public static Vector4 DrawCustomVector4(Rect position, string label, float labelWidth, string[] componentsLabels, Vector4 vector4, bool threeDecimalsOnly = false)
		{
			return DrawCustomVector4(position, label, labelWidth, componentsLabels, Vector4Bool.AllTrue, vector4, threeDecimalsOnly);
		}

		public static Vector4 DrawCustomVector4(Rect position, string label, float labelWidth, string[] componentsLabels, Vector4Bool enableFields, Vector4 vector4, bool threeDecimalsOnly = false)
		{
			CustomVector4 customVector4 = new CustomVector4(vector4);
			customVector4 = (CustomVector4)DrawVector(position, label, labelWidth, componentsLabels, enableFields, customVector4, threeDecimalsOnly);
			Vector4 res = customVector4.vector4;

			return res;
		}

		public static Vector3 DrawCustomVector3(Rect position, string label, string[] componentsLabels, Vector3 vector3)
		{
			return DrawCustomVector3(position: position, label: label, labelWidth: 120f, componentsLabels: componentsLabels, enableFields: Vector3Bool.AllTrue, vector3: vector3);
		}

		public static Vector3 DrawCustomVector3(Rect position, string label, float labelWidth, string[] componentsLabels, Vector3 vector3, bool threeDecimalsOnly = false)
		{
			return DrawCustomVector3(position: position, label: label, labelWidth: labelWidth, componentsLabels: componentsLabels, enableFields: Vector3Bool.AllTrue, vector3: vector3, threeDecimalsOnly);
		}

		public static Vector3 DrawCustomVector3(Rect position, string label, float labelWidth, string[] componentsLabels, Vector3Bool enableFields, Vector3 vector3, bool threeDecimalsOnly = false)
		{
			CustomVector3 customVector3 = new CustomVector3(vector3);
			customVector3 = (CustomVector3)DrawVector(position, label, labelWidth, componentsLabels, enableFields, customVector3, threeDecimalsOnly);
			Vector3 res = customVector3.vector3;

			return res;
		}

		private static IVector DrawVector(Rect position, string label, float labelWidth, string[] componentsLabels, 
			IVectorBool enableFields, IVector vector3, bool threeDecimalsOnly)
		{
			Rect labelRect = new Rect(position);
			labelRect.width = labelWidth;
			EditorGUI.LabelField(labelRect, label);

			Rect rect = new Rect(position);

			IVector res = vector3;
			int size = res.GetSize();

			float widthEachComponent = (position.width - labelWidth) / size;
			float offset = widthEachComponent + 7f;

			EditorGUIUtility.labelWidth = 14f;

			rect.width = widthEachComponent;
			rect.x += labelRect.width;
			
			for (int i = 0; i < size; i++)
			{
				EditorGUI.BeginDisabledGroup(!enableFields[i]);
				
				float toPrint = res[i];
				if(threeDecimalsOnly)
				{
					toPrint = Mathf.Round(res[i] * 1000f) / 1000f;
				}
				res[i] = EditorGUI.FloatField(rect, componentsLabels[i], toPrint);
				EditorGUI.EndDisabledGroup();
				rect.x += offset;
			}

			EditorGUIUtility.labelWidth = 0f;

			return res;
		}

		public static float DrawCustomFloat(Rect position, string label, float floatValue, bool threeDecimalsOnly = false)
		{
			return DrawCustomFloatLogic(position, label, LABEL_WIDTH, floatValue, threeDecimalsOnly);
		}

		public static float DrawCustomFloatLogic(Rect position, string label, float labelWidth, float floatValue, bool threeDecimalsOnly = false)
		{
			float res = floatValue;
			if(threeDecimalsOnly)
			{
				res = Mathf.Round(floatValue * 1000f) / 1000f;
			}

			Rect labelRect = new Rect(position);
			labelRect.width = labelWidth;
			EditorGUI.LabelField(labelRect, label);

			Rect rect = new Rect(position);

			int size = 1;

			float widthEachComponent = (position.width - labelWidth) / size;
			
			EditorGUIUtility.labelWidth = 14f;

			rect.width = widthEachComponent;
			rect.x += labelRect.width;

			res = EditorGUI.FloatField(rect, string.Empty, res);

			EditorGUIUtility.labelWidth = 0f;

			return res;
		}

		public static Vector2 DrawCustomVector2(Rect position, string label, string[] componentsLabels, Vector2 vector2)
		{
			return DrawCustomVector2(position, label, 120f, componentsLabels, Vector2Bool.AllTrue, vector2);
		}

		public static Vector2 DrawCustomVector2(Rect position, string label, float labelWidth, string[] componentsLabels, Vector2 vector2, bool threeDecimalsOnly = false)
		{
			return DrawCustomVector2(position, label, labelWidth, componentsLabels, Vector2Bool.AllTrue, vector2, threeDecimalsOnly);
		}

		public static Vector2 DrawCustomVector2(Rect position, string label, float labelWidth, string[] componentsLabels, Vector2Bool enableFields, Vector2 vector2, bool threeDecimalsOnly = false)
		{
			CustomVector2 customVector2 = new CustomVector2(vector2);
			customVector2 = (CustomVector2)DrawVector(position, label, labelWidth, componentsLabels, enableFields, customVector2, threeDecimalsOnly);

			Vector2 res = customVector2.vector2;
			return res;
		}

		public static bool DrawToggleLayout(string label, float labelWidth, bool toggleValue)
		{
			EditorGUIUtility.labelWidth = labelWidth;

			bool res = EditorGUILayout.Toggle(label, toggleValue);

			EditorGUIUtility.labelWidth = 0f;

			return res;
		}

		public static bool DrawToggle(Rect rect, string label, bool toggleValue)
		{
			return DrawToggle(rect, label, LABEL_WIDTH, toggleValue);
		}

		public static bool DrawToggle(Rect rect, string label, float labelWidth, bool toggleValue)
		{
			EditorGUIUtility.labelWidth = labelWidth;
			bool res = EditorGUI.Toggle(rect, label, toggleValue);
			EditorGUIUtility.labelWidth = 0f;

			return res;
		}

		public static Color DrawColorField(Rect position, string label, float labelWidth, Color color)
		{
			Rect labelRect = new Rect(position);
			labelRect.width = labelWidth;
			EditorGUI.LabelField(labelRect, label);

			Rect rect = new Rect(position);
			rect.x += labelRect.width;

			Color res = EditorGUI.ColorField(rect, color);
			return res;
		}

		public static void DrawSerializedProperty(SerializedProperty serializedProperty, float labelWidth)
		{
			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUILayout.PropertyField(serializedProperty);
			EditorGUIUtility.labelWidth = 0f;
		}

		public static void DrawSerializedPropertyWithRect(Rect rect, float labelWidth, SerializedProperty serializedProperty)
		{
			DrawSerializedPropertyWithRect(rect, serializedProperty.displayName, labelWidth, serializedProperty);
		}

		public static void DrawSerializedPropertyWithRect(Rect rect, string label, float labelWidth, SerializedProperty serializedProperty)
		{
			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUI.PropertyField(rect, serializedProperty, new GUIContent(label));
			EditorGUIUtility.labelWidth = 0f;
		}

		public static void Space()
		{
			Space(1);
		}

		public static void Space(int numSpaces)
		{
			for(int i = 0; i < numSpaces; i++)
			{
				EditorGUILayout.Space();
			}
		}
	}
}
