using System.Collections.Generic;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Vector4 Spring")]
	public class Vector4SpringComponent : SpringComponent
	{
		public SpringVector4 springVector4;

		protected override void RegisterSprings()
		{
			RegisterSpring(springVector4);
		}

		protected override void SetCurrentValueByDefault()
		{
			springVector4.SetCurrentValue(Vector4.zero);
		}

		protected override void SetTargetByDefault()
		{
			springVector4.SetTarget(Vector4.zero);
		}

		public Vector4 GetCurrentValue()
		{
			Vector4 res = springVector4.GetCurrentValue();
			return res;
		}

		public void SetTarget(Vector4 target)
		{
			springVector4.SetTarget(target);
		}

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}
	}
}