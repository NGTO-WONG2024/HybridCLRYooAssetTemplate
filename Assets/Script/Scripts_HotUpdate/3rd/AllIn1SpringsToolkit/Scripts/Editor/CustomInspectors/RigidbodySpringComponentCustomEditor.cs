using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(RigidbodySpringComponent))]
	[CanEditMultipleObjects]
	public class RigidbodySpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SpringVector3Drawer springPositionDrawer;
		private SpringRotationDrawer springRotationDrawer;

		private SerializedProperty spUseTransformAsTarget;
		private SerializedProperty spRigidBodyFollower;
		private SerializedProperty spTarget;

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spUseTransformAsTarget = serializedObject.FindProperty("useTransformAsTarget");
			spRigidBodyFollower = serializedObject.FindProperty("rigidBodyFollower");
			spTarget = serializedObject.FindProperty("target");
		}

		protected override void CreateDrawers()
		{
			springPositionDrawer = new SpringVector3Drawer(serializedObject.FindProperty("positionSpring"), false, false);
			springRotationDrawer = new SpringRotationDrawer(serializedObject.FindProperty("rotationSpring"), false, false);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring(
				labelUseCustomTarget: "Use Custom Target Position",
				labelCustomTarget: "Target Position",
				width: LABEL_WIDTH,
				springEnabled: springPositionDrawer.springEditorObject.spSpringEnabled.boolValue,
				springDrawer: springPositionDrawer);

			DrawCustomTargetBySpring(
				labelUseCustomTarget: "Use Custom Target Rotation",
				labelCustomTarget: "Target Rotation",
				springEnabled: springRotationDrawer.springEditorObject.spSpringEnabled.boolValue,
				width: LABEL_WIDTH,
				springDrawer: springRotationDrawer);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring(
				labelUseInitialValues: "Use Initial Position",
				labelInitialValues: "Initial Position",
				width: LABEL_WIDTH,
				springEnabled: springPositionDrawer.springEditorObject.spSpringEnabled.boolValue,
				springDrawer: springPositionDrawer);

			DrawInitialValuesBySpring(
				labelUseInitialValues: "Use Initial Rotation",
				labelInitialValues: "Initial Rotation",
				width: LABEL_WIDTH,
				springEnabled: springRotationDrawer.springEditorObject.spSpringEnabled.boolValue,
				springDrawer: springRotationDrawer);
		}

		protected override void DrawSprings()
		{
			DrawSpringWithEnableToggle(springPositionDrawer);
			DrawSpringWithEnableToggle(springRotationDrawer);
		}

		protected override void DrawMainAreaUnfolded()
		{
			DrawSerializedProperty(spRigidBodyFollower, LABEL_WIDTH);
			DrawSerializedProperty(spUseTransformAsTarget, LABEL_WIDTH);

			if (spUseTransformAsTarget.boolValue)
			{
				DrawSerializedProperty(spTarget, LABEL_WIDTH);
			}
		}
		
		protected override void DrawInfoArea()
		{
			EditorGUILayout.Space(2);

			if (spRigidBodyFollower.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox("RigidBodyFollower is not assigned!", MessageType.Error);
			}
		}
	}
}