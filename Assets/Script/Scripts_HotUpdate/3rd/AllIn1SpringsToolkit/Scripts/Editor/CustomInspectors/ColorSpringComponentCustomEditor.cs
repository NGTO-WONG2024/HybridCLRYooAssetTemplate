using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(ColorSpringComponent))]
	[CanEditMultipleObjects]
	public class ColorSpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SpringColorDrawer springColorDrawer;

		private SerializedProperty spAutoUpdate;
		private SerializedProperty spAutoUpdatedObjectIsRenderer;
		private SerializedProperty spAutoUpdatedRenderer;
		private SerializedProperty spAutoUpdatedUiGraphic;

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spAutoUpdate = serializedObject.FindProperty("autoUpdate");
			spAutoUpdatedObjectIsRenderer = serializedObject.FindProperty("autoUpdatedObjectIsRenderer");
			spAutoUpdatedRenderer = serializedObject.FindProperty("autoUpdatedRenderer");
			spAutoUpdatedUiGraphic = serializedObject.FindProperty("autoUpdatedUiGraphic");
		}

		protected override void CreateDrawers()
		{
			springColorDrawer = new SpringColorDrawer(serializedObject.FindProperty("colorSpring"), false, false);
		}

		protected override void DrawMainAreaUnfolded()
		{
			DrawSerializedProperty(spAutoUpdate, LABEL_WIDTH);

			if (spAutoUpdate.boolValue)
			{
				DrawSerializedProperty(spAutoUpdatedObjectIsRenderer, LABEL_WIDTH);

				if (spAutoUpdatedObjectIsRenderer.boolValue)
				{
					DrawSerializedProperty(spAutoUpdatedRenderer, LABEL_WIDTH);
				}
				else
				{
					DrawSerializedProperty(spAutoUpdatedUiGraphic, LABEL_WIDTH);
				}
			}
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring(
				labelInitialValues: "Initial Value",
				width: LABEL_WIDTH,
				springDrawer: springColorDrawer
				);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring(
				labelCustomTarget: "Target",
				width: LABEL_WIDTH,
				springDrawer: springColorDrawer
				);
		}

		protected override void DrawSprings()
		{
			DrawSpring(springColorDrawer);
		}
	}
}