using System.Collections.Generic;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Light Intensity Spring")]
	public class LightIntensitySpringComponent : SpringComponent
	{
		public SpringFloat lightIntensitySpring;
		[SerializeField] private Light autoUpdatedLight;

		protected override void RegisterSprings()
		{
			RegisterSpring(lightIntensitySpring);
		}

		protected override void SetCurrentValueByDefault()
		{
			lightIntensitySpring.SetCurrentValue(autoUpdatedLight.intensity);
		}

		protected override void SetTargetByDefault()
		{
			lightIntensitySpring.SetTarget(autoUpdatedLight.intensity);
		}

		public void Update()
		{
			UpdateLightIntensity();
		}

		public void UpdateLightIntensity()
		{
			autoUpdatedLight.intensity = lightIntensitySpring.GetCurrentValue();
		}

		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if (autoUpdatedLight == null)
			{
				AddErrorReason($"{gameObject.name} autoUpdatedLight is null.");
				res = false;
			}
			
			return res;
		}

#if UNITY_EDITOR
		private void Reset()
		{
			if(autoUpdatedLight == null)
			{
				autoUpdatedLight = GetComponent<Light>();
			}
		}
#endif
	}
}
