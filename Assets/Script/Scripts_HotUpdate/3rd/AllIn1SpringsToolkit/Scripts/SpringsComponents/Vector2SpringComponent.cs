using System.Collections.Generic;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Vector2 Spring")]
	public class Vector2SpringComponent : SpringComponent
	{
		public SpringVector2 springVector2; 

		protected override void RegisterSprings()
		{
			RegisterSpring(springVector2);
		}

		protected override void SetCurrentValueByDefault()
		{
			springVector2.SetCurrentValue(Vector2.zero);
		}

		protected override void SetTargetByDefault()
		{
			springVector2.SetTarget(Vector2.zero);
		}

		public Vector2 GetCurrentValue()
		{
			Vector3 res = springVector2.GetCurrentValue();
			return res;
		}

		public void SetTarget(Vector2 target)
		{
			springVector2.SetTarget(target);
		}

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}
	}
}