using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public class AudioSourceSpringComponent : SpringComponent
	{
        [SerializeField] private AudioSource autoUpdatedAudioSource;


		public SpringFloat volumeSpring;
		public SpringFloat pitchSpring;

		protected override void RegisterSprings()
		{
			RegisterSpring(volumeSpring);
			RegisterSpring(pitchSpring);
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValue(volumeValue: autoUpdatedAudioSource.volume, pitchValue: autoUpdatedAudioSource.pitch);
		}

		protected override void SetTargetByDefault()
		{
			SetTarget(targetVolume: autoUpdatedAudioSource.volume, targetPitch: autoUpdatedAudioSource.pitch);
		}

		public void Update()
		{
			UpdateAudioSource();
		}

		private void UpdateAudioSource()
		{
			autoUpdatedAudioSource.volume = volumeSpring.GetCurrentValue();
			autoUpdatedAudioSource.pitch = pitchSpring.GetCurrentValue();
		}

		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if (autoUpdatedAudioSource == null)
			{
				AddErrorReason($"{gameObject.name} autoUpdatedAudioSource is null.");
				res = false;
			}

			return res;
		}

		#region API PUBLIC METHODS

		#region VOLUME
		public float GetCurrentVolume()
		{
			return volumeSpring.GetCurrentValue();
		}
		#endregion

		public float GetCurrentPitch()
		{
			return pitchSpring.GetCurrentValue();
		}

		public void SetCurrentVolumeValue(float value)
		{
			volumeSpring.SetCurrentValue(value);
		}

		public void SetCurrentPitchValue(float value)
		{
			pitchSpring.SetCurrentValue(value);
		}

		public void SetCurrentValue(float volumeValue, float pitchValue)
		{
			SetCurrentVolumeValue(volumeValue);
			SetCurrentPitchValue(pitchValue);
		}

		public float GetTargetVolume()
		{
			return volumeSpring.GetTarget();
		}

		public float GetTargetPitch()
		{
			return pitchSpring.GetTarget();
		}

		public void SetTarget(float targetVolume, float targetPitch)
		{
			SetTargetVolume(targetVolume);
			SetTargetPitch(targetPitch);
		}

		public void SetTargetVolume(float target)
		{
			volumeSpring.SetTarget(target);
		}

		public void SetTargetPitch(float target)
		{
			pitchSpring.SetTarget(target);
		}

		public float GetVolumeVelocity()
		{
			return volumeSpring.GetVelocity();		
		}

		public float GetPitchVelocity()
		{
			return pitchSpring.GetVelocity();
		}

		public void SetVolumeVelocity(float velocity)
		{
			volumeSpring.SetVelocity(velocity);
		}

		public void SetPitchVelocity(float velocity)
		{
			pitchSpring.SetVelocity(velocity);
		}

		public void SetVelocity(float volumeVelocity, float pitchVelocity)
		{
			SetVolumeVelocity(volumeVelocity);
			SetPitchVelocity(pitchVelocity);
		}

		public void AddVolumeVelocity(float deltaVelocity)
		{
			volumeSpring.AddVelocity(deltaVelocity);
		}

		public void AddPitchVelocity(float deltaVelocity)
		{
			pitchSpring.AddVelocity(deltaVelocity);
		}

		public void AddVelocity(float deltaVolumeVelocity, float deltaPitchVelocity)
		{
			AddVolumeVelocity(deltaVolumeVelocity);
			AddPitchVelocity(deltaPitchVelocity);
		}

		public void ReachEqulibriumVolume()
		{
			volumeSpring.ReachEquilibrium();
		}

		public void ReachEquilibriumPitch()
		{
			pitchSpring.ReachEquilibrium();
		}

		public void SetPitchClampingEnabled(bool value)
		{
			pitchSpring.SetClampingEnabled(value);
		}

		public void SetVolumeClampingEnabled(bool value)
		{
			volumeSpring.SetClampingEnabled(value);
		}

		public void SetClampingEnabled(bool pitchClampingEnabled, bool volumeClampingEnabled)
		{
			SetPitchClampingEnabled(pitchClampingEnabled);
			SetVolumeClampingEnabled(volumeClampingEnabled);
		}

		public void SetClampTargetPitch(bool value)
		{
			pitchSpring.SetClampTarget(value);
		}

		public void SetClampTargetVolume(bool value)
		{
			volumeSpring.SetClampTarget(value);
		}

		public void SetClampTarget(bool clampTargetPitch, bool clampTargetVolume)
		{
			SetClampTargetPitch(clampTargetPitch);
			SetClampTargetVolume(clampTargetVolume);
		}

		public void SetClampCurrentValuePitch(bool value)
		{
			pitchSpring.SetClampCurrentValue(value);
		}

		public void SetClampCurrentValueVolume(bool value)
		{
			volumeSpring.SetClampCurrentValue(value);
		}

		public void SetClampCurrentValue(bool clampCurrentValuePitch, bool clampCurrentValueVolume)
		{
			SetClampCurrentValuePitch(clampCurrentValuePitch);
			SetClampCurrentValueVolume(clampCurrentValueVolume);
		}

		public void SetStopSpringOnClampPitch(bool value)
		{
			pitchSpring.StopSpringOnClamp(value);
		}

		public void SetStopSpringOnClampVolume(bool value)
		{
			volumeSpring.StopSpringOnClamp(value);
		}

		public void SetStopSpringOnClamp(bool stopSpringOnClampPitch, bool stopSpringOnClampVolume)
		{
			SetStopSpringOnClampPitch(stopSpringOnClampPitch);
			SetStopSpringOnClampVolume(stopSpringOnClampVolume);
		}
		#endregion

#if UNITY_EDITOR
		private void Reset()
		{
			if(autoUpdatedAudioSource == null)
			{
				autoUpdatedAudioSource = GetComponent<AudioSource>();
			}
		}
#endif
	}
}
