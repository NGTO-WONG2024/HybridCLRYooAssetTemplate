#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	public class SpringFloatDrawer : SpringDrawer
	{
		private SpringFloatEditorObject springFloatEditorObject;

		public SpringFloatDrawer(SerializedProperty property, bool isFoldout, bool isDebugger) : 
			base(parentProperty: property, isFoldout: isFoldout, drawClampingArea: true, isDebugger) 
		{}

		public SpringFloatDrawer(bool isFoldout) : 
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, isDebugger: false)
		{}

		public SpringFloatDrawer(bool isFoldout, bool isDebugger) :
			base(parentProperty: null, isFoldout: isFoldout, drawClampingArea: true, isDebugger: isDebugger)
		{}

		protected override SpringEditorObject CreateSpringEditorObjectInstance(SerializedProperty parentProperty)
		{
			SpringEditorObject res = new SpringFloatEditorObject(parentProperty);
			return res;
		}

		public override void RefreshSerializedProperties(SerializedProperty parentProperty)
		{
			base.RefreshSerializedProperties(parentProperty);

			this.springFloatEditorObject = (SpringFloatEditorObject)springEditorObject;
		}

		protected override void DrawDrag(ref Rect currentRect)
		{
			springFloatEditorObject.Drag = DrawCustomFloat(currentRect, "Drag", springFloatEditorObject.Drag);
		}

		protected override void DrawForce(ref Rect currentRect)
		{
			springFloatEditorObject.Force = DrawCustomFloat(currentRect, "Force", springFloatEditorObject.Force);
		}

		protected override void DrawUpdate(ref Rect currentRect)
		{
			springFloatEditorObject.Update = DrawToggle(currentRect, "Update", LABEL_WIDTH, springFloatEditorObject.Update);
		}

		public override void DrawCurrentValue(ref Rect currentRect, string label, float labelWidth)
		{
			springFloatEditorObject.CurrentValue = DrawCustomFloatLogic(currentRect, label, labelWidth, springFloatEditorObject.CurrentValue, threeDecimalsOnly: true);
		}

		protected override void DrawVelocity(ref Rect currentRect)
		{
			springFloatEditorObject.Velocity = DrawCustomFloat(currentRect, "Velocity", springFloatEditorObject.Velocity, threeDecimalsOnly: true);
		}

		public override void DrawTarget(ref Rect currentRect, string label, float labelWidth)
		{
			springFloatEditorObject.Target = DrawCustomFloatLogic(currentRect, label, labelWidth, springFloatEditorObject.Target);
		}

		protected override void DrawClampTarget(ref Rect currentRect)
		{
			springFloatEditorObject.ClampTarget = DrawToggle(currentRect, FIELD_NAME_CLAMP_TARGET, springFloatEditorObject.ClampTarget);
		}

		protected override void DrawClampCurrentValue(ref Rect currentRect)
		{
			springFloatEditorObject.ClampCurrentValue = DrawToggle(currentRect, FIELD_NAME_CLAMP_CURRENT_VALUE, springFloatEditorObject.ClampCurrentValue);
		}

		protected override void DrawStopSpringOnCurrentValueClamp(ref Rect currentRect)
		{
			springFloatEditorObject.StopSpringOnCurrentValueClamp = DrawToggle(currentRect, FIELD_NAME_STOP_SPRING_ON_CURRENT_VALUE_CLAMP, springFloatEditorObject.StopSpringOnCurrentValueClamp);
		}

		protected override void DrawClampingRange(ref Rect currentRect)
		{
			springFloatEditorObject.MinValue = DrawCustomFloat(currentRect, "Min Values", springFloatEditorObject.MinValue);

			UpdateCurrentRect(ref currentRect);
			springFloatEditorObject.MaxValue = DrawCustomFloat(currentRect, "Max Values", springFloatEditorObject.MaxValue);
		}

		protected override void DrawNudgeOperationValues(ref Rect currentRect)
		{
			springFloatEditorObject.OperationValue = DrawCustomFloat(currentRect, "Operation Value", springFloatEditorObject.OperationValue);
		}
	}
}

#endif