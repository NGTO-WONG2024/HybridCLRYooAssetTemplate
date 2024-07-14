using System.Collections.Generic;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Rotation Spring")]
	public class RotationSpringComponent : SpringComponent
	{
		public SpringRotation springRotation;

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}

		protected override void SetCurrentValueByDefault()
		{
			springRotation.SetCurrentValue(Quaternion.identity);
		}

		protected override void SetTargetByDefault()
		{
			springRotation.SetTarget(Quaternion.identity);
		}

		protected override void RegisterSprings()
		{
			RegisterSpring(springRotation);
		}
	}
}