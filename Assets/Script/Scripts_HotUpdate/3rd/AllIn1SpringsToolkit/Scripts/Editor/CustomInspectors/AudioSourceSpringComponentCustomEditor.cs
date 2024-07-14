using UnityEditor;
using static AllIn1SpringsToolkit.SpringsEditorUtility;

namespace AllIn1SpringsToolkit
{
	[CustomEditor(typeof(AudioSourceSpringComponent))]
	[CanEditMultipleObjects]
	public class AudioSourceSpringComponentCustomEditor : SpringComponentCustomEditor
	{
		private SerializedProperty spAutoUpdatedAudioSource;

		private SpringFloatDrawer volumeSpringDrawer;
		private SpringFloatDrawer pitchSpringDrawer;

		protected override void RefreshSerializedProperties()
		{
			base.RefreshSerializedProperties();

			spAutoUpdatedAudioSource = serializedObject.FindProperty("autoUpdatedAudioSource");
		}

		protected override void CreateDrawers()
		{
			volumeSpringDrawer = new SpringFloatDrawer(serializedObject.FindProperty("volumeSpring"), false, false);
			pitchSpringDrawer = new SpringFloatDrawer(serializedObject.FindProperty("pitchSpring"), false, false);
		}

		protected override void DrawCustomInitialTarget()
		{
			DrawCustomTargetBySpring("Target Volume", LABEL_WIDTH, volumeSpringDrawer);
			DrawCustomTargetBySpring("Target Pitch", LABEL_WIDTH, pitchSpringDrawer);
		}

		protected override void DrawCustomInitialValuesSection()
		{
			DrawInitialValuesBySpring("Initial Values Volume", LABEL_WIDTH, volumeSpringDrawer);
			DrawInitialValuesBySpring("Initial Values Pitch", LABEL_WIDTH, pitchSpringDrawer);
		}

		protected override void DrawMainAreaUnfolded()
		{
			DrawSerializedProperty(spAutoUpdatedAudioSource, LABEL_WIDTH);
		}

		protected override void DrawSprings()
		{
			DrawSpring(volumeSpringDrawer);
			DrawSpring(pitchSpringDrawer);
		}

		protected override void DrawInfoArea()
		{
			EditorGUILayout.Space(2);

			if (spAutoUpdatedAudioSource.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox("Auto Updated Audio Source is not assigned!", MessageType.Error);
			}
		}
	}
}