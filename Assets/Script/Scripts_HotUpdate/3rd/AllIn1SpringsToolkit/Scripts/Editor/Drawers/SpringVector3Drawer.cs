#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public class SpringVector3Drawer : SpringDrawer
	{
		private SpringVector3EditorObject springVector3EditorObject;

		public SpringVector3Drawer(SerializedProperty property, bool isFoldout, bool isDebugger) : 
			base(parentProperty: property, isFoldout: isFoldout, drawClampingArea: true, isDebugger: isDebugger) {}

		public SpringVector3Drawer(bool isFoldout, bool isDebugger) :
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, isDebugger: isDebugger)	{ }

		public override void RefreshSerializedProperties(SerializedProperty parentProperty)
		{
			base.RefreshSerializedProperties(parentProperty);

			this.springVector3EditorObject = (SpringVector3EditorObject)this.springEditorObject;
		}

		protected override SpringEditorObject CreateSpringEditorObjectInstance(SerializedProperty parentProperty)
		{
			SpringEditorObject res = new SpringVector3EditorObject(parentProperty);
			return res;
		}

		protected override void DrawClampingRange(ref Rect currentRect)
		{
			springVector3EditorObject.MinValue = DrawCustomVector3(currentRect, "Min Values", LABEL_WIDTH, COMPONENTS_LABELS_XYZ, springVector3EditorObject.MinValue);

			UpdateCurrentRect(ref currentRect);
			springVector3EditorObject.MaxValue = DrawCustomVector3(currentRect, "Max Values", LABEL_WIDTH, COMPONENTS_LABELS_XYZ, springVector3EditorObject.MaxValue);
		}

		protected override void DrawClampTarget(ref Rect currentRect)
		{
			springVector3EditorObject.ClampTarget = DrawVector3Bool(currentRect, "Clamp Target", COMPONENTS_LABELS_XYZ, LABEL_WIDTH, springVector3EditorObject.ClampTarget);
		}

		protected override void DrawClampCurrentValue(ref Rect currentRect)
		{
			springVector3EditorObject.ClampCurrentValue = DrawVector3Bool(currentRect, "Clamp Current Value", COMPONENTS_LABELS_XYZ, LABEL_WIDTH, springVector3EditorObject.ClampCurrentValue);
		}

		protected override void DrawStopSpringOnCurrentValueClamp(ref Rect currentRect)
		{
			springVector3EditorObject.StopSpringOnCurrentValueClamp = DrawVector3Bool(currentRect, "Stop Spring On Current Value Clamp", COMPONENTS_LABELS_XYZ,
				LABEL_WIDTH, springVector3EditorObject.StopSpringOnCurrentValueClamp);
		}

		protected override void DrawUpdate(ref Rect currentRect)
		{
			springVector3EditorObject.Update = DrawVector3Bool(currentRect, "Update Axis", COMPONENTS_LABELS_XYZ, LABEL_WIDTH, springVector3EditorObject.Update);
		}

		protected override void DrawForce(ref Rect currentRect)
		{
			springVector3EditorObject.Force = DrawCustomVector3(currentRect, "Force", LABEL_WIDTH, COMPONENTS_LABELS_XYZ, springVector3EditorObject.Force);
		}

		protected override void DrawDrag(ref Rect currentRect)
		{
			springVector3EditorObject.Drag = DrawCustomVector3(currentRect, "Drag", LABEL_WIDTH, COMPONENTS_LABELS_XYZ, springVector3EditorObject.Drag);
		}

		public override void DrawCurrentValue(ref Rect currentRect, string label, float labelWidth)
		{
			springVector3EditorObject.CurrentValue = DrawCustomVector3(currentRect, label, labelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.CurrentValue, threeDecimalsOnly: true);
		}

		public override void DrawTarget(ref Rect currentRect, string label, float labelWidth)
		{
			springVector3EditorObject.Target = DrawCustomVector3(currentRect, label, labelWidth, COMPONENTS_LABELS_XYZ, springVector3EditorObject.Target);
		}

		protected override void DrawNudgeOperationValues(ref Rect currentRect)
		{
			springVector3EditorObject.OperationValue = DrawCustomVector3(currentRect, "Operation Value", LABEL_WIDTH, COMPONENTS_LABELS_XYZ, springVector3EditorObject.OperationValue);
		}

		protected override void DrawVelocity(ref Rect currentRect)
		{
			DrawCustomVector3(currentRect, "Velocity", LABEL_WIDTH, COMPONENTS_LABELS_XYZ, springVector3EditorObject.Velocity, threeDecimalsOnly: true);
		}
	}
}

#endif