#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public class SpringVector2Drawer : SpringDrawer
	{
		private SpringVector2EditorObject springVector2EditorObject;

		public SpringVector2Drawer(SerializedProperty property, bool isFoldout, bool isDebugger) : 
			base(parentProperty: property, isFoldout: isFoldout, drawClampingArea: true, isDebugger: isDebugger) 
		{}

		public SpringVector2Drawer(bool isFoldout) : 
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, isDebugger: false)
		{}

		public SpringVector2Drawer(bool isFoldout, bool isDebugger) :
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, isDebugger: isDebugger)
		{}

		protected override SpringEditorObject CreateSpringEditorObjectInstance(SerializedProperty parentProperty)
		{
			SpringEditorObject res = new SpringVector2EditorObject(parentProperty);
			return res;
		}

		public override void RefreshSerializedProperties(SerializedProperty parentProperty)
		{
			base.RefreshSerializedProperties(parentProperty);

			this.springVector2EditorObject = (SpringVector2EditorObject)this.springEditorObject;
		}

		protected override void DrawDrag(ref Rect currentRect)
		{
			springVector2EditorObject.Drag = DrawCustomVector2(currentRect, "Drag", LABEL_WIDTH, COMPONENTS_LABELS_XY, springVector2EditorObject.Drag);
		}

		protected override void DrawForce(ref Rect currentRect)
		{
			springVector2EditorObject.Force = DrawCustomVector2(currentRect, "Force", LABEL_WIDTH, COMPONENTS_LABELS_XY, springVector2EditorObject.Force);
		}

		protected override void DrawUpdate(ref Rect currentRect)
		{
			springVector2EditorObject.Update = DrawVector2Bool(currentRect, "Update Axis", COMPONENTS_LABELS_XY, LABEL_WIDTH, springVector2EditorObject.Update);
		}

		public override void DrawCurrentValue(ref Rect currentRect, string label, float labelWidth)
		{
			springVector2EditorObject.CurrentValue = DrawCustomVector2(
				position: currentRect, 
				label: label, 
				labelWidth: labelWidth,
				componentsLabels: COMPONENTS_LABELS_XY, 
				vector2: springVector2EditorObject.CurrentValue, threeDecimalsOnly: true);
		}

		protected override void DrawVelocity(ref Rect currentRect)
		{
			springVector2EditorObject.Velocity = DrawCustomVector2(currentRect, "Velocity", LABEL_WIDTH, COMPONENTS_LABELS_XY, springVector2EditorObject.Velocity, threeDecimalsOnly: true);
		}

		public override void DrawTarget(ref Rect currentRect, string label, float labelWidth)
		{
			springVector2EditorObject.Target = DrawCustomVector2(
				position: currentRect, 
				label: label, 
				labelWidth: labelWidth, 
				componentsLabels: COMPONENTS_LABELS_XY, 
				vector2: springVector2EditorObject.Target);
		}

		protected override void DrawClampTarget(ref Rect currentRect)
		{
			springVector2EditorObject.ClampTarget = DrawVector2Bool(currentRect, "Clamp Target", COMPONENTS_LABELS_XY, LABEL_WIDTH, springVector2EditorObject.ClampTarget);
		}

		protected override void DrawClampCurrentValue(ref Rect currentRect)
		{
			springVector2EditorObject.ClampCurrentValue = DrawVector2Bool(currentRect, "Clamp Current Value", COMPONENTS_LABELS_XY, LABEL_WIDTH, springVector2EditorObject.ClampCurrentValue);
		}

		protected override void DrawStopSpringOnCurrentValueClamp(ref Rect currentRect)
		{
			springVector2EditorObject.StopSpringOnCurrentValueClamp = DrawVector2Bool(currentRect, FIELD_NAME_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, COMPONENTS_LABELS_XY, LABEL_WIDTH, springVector2EditorObject.StopSpringOnCurrentValueClamp);
		}

		protected override void DrawClampingRange(ref Rect currentRect)
		{
			springVector2EditorObject.MinValue = DrawCustomVector2(currentRect, "Min Values", LABEL_WIDTH, COMPONENTS_LABELS_XY, springVector2EditorObject.MinValue);

			UpdateCurrentRect(ref currentRect);
			springVector2EditorObject.MaxValue = DrawCustomVector2(currentRect, "Max Values", LABEL_WIDTH, COMPONENTS_LABELS_XY, springVector2EditorObject.MaxValue);
		}

		protected override void DrawNudgeOperationValues(ref Rect currentRect)
		{
			springVector2EditorObject.OperationValue = DrawCustomVector2(currentRect, "Operation Value", LABEL_WIDTH, COMPONENTS_LABELS_XY, springVector2EditorObject.OperationValue);
		}
	}
}

#endif