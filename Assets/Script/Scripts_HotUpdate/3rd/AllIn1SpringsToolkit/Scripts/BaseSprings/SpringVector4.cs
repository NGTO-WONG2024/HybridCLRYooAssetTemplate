using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringVector4 : Spring
	{
		public const int SPRING_SIZE = 4;

		private const int X = 0;
		private const int Y = 1;
		private const int Z = 2;
		private const int W = 3;

		public SpringVector4() : base(SPRING_SIZE)
		{

		}

		public override int GetSpringSize()
		{
			return SPRING_SIZE;
		}

		public override bool HasValidSize()
		{
			bool res = springValues.Length == SPRING_SIZE;
			return res;
		}

		#region TARGET
		public Vector4 GetTarget()
		{
			Vector4 res = new Vector4(
				springValues[X].GetTarget(),
				springValues[Y].GetTarget(),
				springValues[Z].GetTarget(),
				springValues[W].GetTarget());

			return res;
		}
		
		public void SetTarget(Vector4 target)
		{
			springValues[X].SetTarget(target.x);
			springValues[Y].SetTarget(target.y);
			springValues[Z].SetTarget(target.z);
			springValues[W].SetTarget(target.w);
		}
		#endregion

		#region CURRENT VALUES
		public Vector4 GetCurrentValue()
		{
			Vector4 res = new Vector4(springValues[X].GetCurrentValue(), springValues[Y].GetCurrentValue(), springValues[Z].GetCurrentValue(), springValues[W].GetCurrentValue());
			return res;
		}

		public virtual void SetCurrentValue(Vector4 value)
		{
			springValues[X].SetCurrentValue(value.x);
			springValues[Y].SetCurrentValue(value.y);
			springValues[Z].SetCurrentValue(value.z);
			springValues[W].SetCurrentValue(value.w);
		}
		#endregion

		#region VELOCITY
		public Vector4 GetVelocity()
		{
			Vector4 res = new Vector4(springValues[X].GetVelocity(), springValues[Y].GetVelocity(), springValues[Z].GetVelocity(), springValues[W].GetVelocity());
			return res;
		}

		public void AddVelocity(Vector4 velocity)
		{
			springValues[X].AddVelocity(velocity.x);
			springValues[Y].AddVelocity(velocity.y);
			springValues[Z].AddVelocity(velocity.z);
			springValues[W].AddVelocity(velocity.w);
		}

		public void SetVelocity(Vector4 velocity)
		{
			springValues[X].SetVelocity(velocity.x);
			springValues[Y].SetVelocity(velocity.y);
			springValues[Z].SetVelocity(velocity.z);
			springValues[W].SetVelocity(velocity.w);
		}
		#endregion

		#region CLAMPING
		public void SetMinValues(Vector4 minValues)
		{
			SetMinValueByIndex(X, minValues.x);
			SetMinValueByIndex(Y, minValues.y);
			SetMinValueByIndex(Z, minValues.z);
			SetMinValueByIndex(W, minValues.w);
		}

		public void SetMinValueX(float minValue)
		{
			SetMinValueByIndex(X, minValue);
		}

		public void SetMinValueY(float minValue)
		{
			SetMinValueByIndex(Y, minValue);
		}

		public void SetMinValueZ(float minValue)
		{
			SetMinValueByIndex(Z, minValue);
		}

		public void SetMinValueW(float minValue)
		{
			SetMinValueByIndex(W, minValue);
		}

		public void SetMaxValues(Vector4 maxValues)
		{
			SetMaxValueByIndex(X, maxValues.x);
			SetMaxValueByIndex(Y, maxValues.y);
			SetMaxValueByIndex(Z, maxValues.z);
			SetMaxValueByIndex(W, maxValues.w);
		}

		public void SetMaxValueX(float maxValue)
		{
			SetMaxValueByIndex(X, maxValue);
		}

		public void SetMaxValueY(float maxValue)
		{
			SetMaxValueByIndex(Y, maxValue);
		}

		public void SetMaxValueZ(float maxValue)
		{
			SetMaxValueByIndex(Z, maxValue);
		}

		public void SetMaxValueW(float maxValue)
		{
			SetMaxValueByIndex(W, maxValue);
		}

		public void StopSpringOnClamp(bool stopX, bool stopY, bool stopZ, bool stopW)
		{
			SetStopSpringOnCurrentValueClampByIndex(X, stopX);
			SetStopSpringOnCurrentValueClampByIndex(Y, stopY);
			SetStopSpringOnCurrentValueClampByIndex(Z, stopZ);
			SetStopSpringOnCurrentValueClampByIndex(W, stopW);
		}

		public void StopSpringOnClampX(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(X, stop);
		}

		public void StopSpringOnClampY(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(Y, stop);
		}

		public void StopSpringOnClampZ(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(Z, stop);
		}

		public void StopSpringOnClampW(bool stop)
		{
			SetStopSpringOnCurrentValueClampByIndex(W, stop);
		}

		#endregion
	}
}