using UnityEngine;

namespace AllIn1SpringsToolkit
{
	//Broad purpose spring class that can be used to animate any float value, we can then use this spring values to feed them to Reactor components
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Float Spring")]
	public class FloatSpringComponent : SpringComponent
	{
		public SpringFloat springFloat;
		
		protected override void RegisterSprings()
		{
			RegisterSpring(springFloat);
		}

		protected override void SetCurrentValueByDefault()
		{
			springFloat.SetCurrentValue(0f);
		}

		protected override void SetTargetByDefault()
		{
			springFloat.SetTarget(0f);
		}

		#region API PUBLIC METHODS
		public float GetTarget()
		{
			return springFloat.GetTarget();
		}

		public void SetTarget(float target)
		{
			springFloat.SetTarget(target);
		}

		public float GetCurrentValue()
		{
			float res = springFloat.GetCurrentValue();
			return res;
		}

		public void SetCurrentValue(float value)
		{
			springFloat.SetCurrentValue(value);
		}

		public override bool IsValidSpringComponent()
		{
			//No direct dependencies
			return true;
		}
		#endregion
	}
}