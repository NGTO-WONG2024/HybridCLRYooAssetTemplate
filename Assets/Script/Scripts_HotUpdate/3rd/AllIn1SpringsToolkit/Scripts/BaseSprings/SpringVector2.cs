using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringVector2 : Spring
	{
		public const int SPRING_SIZE = 2;

		private const int X = 0;
		private const int Y = 1;

		public SpringVector2() : base(SPRING_SIZE)
		{
				
		}

		public override bool HasValidSize()
		{
			bool res = springValues.Length == SPRING_SIZE;
			return res;
		}

		public override int GetSpringSize()
		{
			return SPRING_SIZE;
		}

		#region TARGET
		public Vector2 GetTarget()
		{
			Vector2 res = new Vector2(
				springValues[X].GetTarget(),
				springValues[Y].GetTarget());

			return res;
		}
		
		public void SetTarget(Vector2 target)
		{
			springValues[X].SetTarget(target.x);
			springValues[Y].SetTarget(target.y);
		}
		#endregion

		#region CURRENT VALUES
		public Vector2 GetCurrentValue()
		{
			Vector2 res = new Vector2(springValues[X].GetCurrentValue(), springValues[Y].GetCurrentValue());
			return res;
		}
		
		public void SetCurrentValue(Vector2 value)
		{
			springValues[X].SetCurrentValue(value.x);
			springValues[Y].SetCurrentValue(value.y);
		}
		#endregion

		#region VELOCITY
		public Vector2 GetVelocity()
		{
			Vector2 res = new Vector2(springValues[X].GetVelocity(), springValues[Y].GetVelocity());
			return res;
		}

		public void AddVelocity(Vector2 velocity)
		{
			springValues[X].AddVelocity(velocity.x);
			springValues[Y].AddVelocity(velocity.y);
		}

		public void SetVelocity(Vector2 velocity)
		{
			springValues[X].SetVelocity(velocity.x);
			springValues[Y].SetVelocity(velocity.y);
		}
		#endregion

		#region FORCE AND DRAG
		public void SetForce(Vector2 force)
		{
			SetForceByIndex(X, force.x);
			SetForceByIndex(Y, force.y);
		}

		public void SetDrag(Vector2 drag)
		{
			SetDragByIndex(X, drag.x);
			SetDragByIndex(Y, drag.y);
		}
		#endregion

		#region CLAMPING
		public void SetMinValues(Vector2 minValues)
		{
			SetMinValueByIndex(X, minValues.x);
			SetMinValueByIndex(Y, minValues.y);
		}

		public void SetMinValueX(float minValue)
		{
			SetMinValueByIndex(X, minValue);
		}

		public void SetMinValueY(float minValue)
		{
			SetMinValueByIndex(Y, minValue);
		}

		public void SetMaxValues(Vector2 maxValues)
		{
			SetMaxValueByIndex(X, maxValues.x);
			SetMaxValueByIndex(Y, maxValues.y);
		}

		public void SetMaxValueX(float maxValue)
		{
			SetMaxValueByIndex(X, maxValue);
		}

		public void SetMaxValueY(float maxValue)
		{
			SetMaxValueByIndex(Y, maxValue);
		}

		public void SetStopSpringOnClamp(bool stopX, bool stopY)
		{
			SetStopSpringOnCurrentValueClampByIndex(X, stopX);
			SetStopSpringOnCurrentValueClampByIndex(Y, stopY);
		}

		public void StopSpringOnClampX(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(X, stop);
		}

		public void StopSpringOnClampY(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(Y, stop);
		}

		public void SetClampTarget(bool clampTargetX, bool clampTargetY)
		{
			SetClampTargetByIndex(X, clampTargetX);
			SetClampTargetByIndex(Y, clampTargetY);
		}

		public void SetClampCurrentValue(bool clampCurrentValueX, bool clampCurrentValueY)
		{
			SetClampCurrentValueByIndex(X, clampCurrentValueX);
			SetClampCurrentValueByIndex(Y, clampCurrentValueY);
		}
		#endregion
	}
}