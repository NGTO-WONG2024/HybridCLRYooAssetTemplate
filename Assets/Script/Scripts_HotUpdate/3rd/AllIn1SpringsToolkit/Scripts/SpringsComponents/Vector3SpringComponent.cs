using System.Collections.Generic;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Vector3 Spring")]
	public class Vector3SpringComponent : SpringComponent
	{
		public SpringVector3 springVector3;

		protected override void RegisterSprings()
		{
			RegisterSpring(springVector3);
		}

		protected override void SetCurrentValueByDefault()
		{
			springVector3.SetCurrentValue(Vector3.zero);
		}

		protected override void SetTargetByDefault()
		{
			springVector3.SetTarget(Vector3.zero);
		}

		public Vector3 GetCurrentValue()
		{
			Vector3 res = springVector3.GetCurrentValue();
			return res;
		}

		public void SetTarget(Vector3 target)
		{
			springVector3.SetTarget(target);
		}

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}
	}
}