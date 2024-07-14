using System;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public abstract class Spring
	{
#if UNITY_EDITOR && ALLIN1SPRINGS_DEBUGGER
		private static int LAST_SPRING_ID = int.MinValue;

		private int id;
#endif

		public event Action OnClampingApplied;

		public bool unifiedForceAndDrag;
		public float unifiedForce;
		public float unifiedDrag;

		public bool useInitialValues;
		public bool useCustomTarget;

		public bool springEnabled;
		public bool clampingEnabled;
		public SpringValues[] springValues;

		[HideInInspector] public bool showDebugFields;
		[HideInInspector] public bool unfolded;

		public Spring(int size)
		{
			springValues = new SpringValues[size];

			this.springEnabled = true;
			this.unifiedForceAndDrag = true;
			this.unifiedForce = 150f;
			this.unifiedDrag = 10f;

			for(int i = 0; i < size; i++)
			{
				springValues[i] = new SpringValues();
			}
		}

		public bool IsClampTargetEnabled()
		{
			bool res = false;

			for (int i = 0; i < springValues.Length; i++)
			{
				res = res || springValues[i].GetClampTarget();
			}

			return res;
		}

		public bool IsClampCurrentValueEnabled()
		{
			bool res = false;

			for (int i = 0; i < springValues.Length; i++)
			{
				res = res || springValues[i].GetClampCurrentValue();
			}

			return res;
		}

		public virtual void Update(float deltaTime)
		{
		
		}

		public void ReachEquilibrium()
		{
			for (int i = 0; i < springValues.Length; i++)
			{
				springValues[i].ReachEquilibrium();
			}
		}

		public void SetTargetToCurrentValue()
		{
			for (int i = 0; i < springValues.Length; i++)
			{
				springValues[i].SetTargetToCurrentValue();
			}
		}

		public abstract bool HasValidSize();

		public abstract int GetSpringSize();

		public bool CheckCorrectSize()
		{
			bool res = HasValidSize();
			if (!res)
			{
				int springSize = GetSpringSize();
				springValues = new SpringValues[springSize];
				for (int i = 0; i < springSize; i++)
				{
					springValues[i] = new SpringValues();
				}
			}

			return res;
		}

		public virtual void Initialize()
		{
			for(int i = 0; i < springValues.Length; i++)
			{
				springValues[i].Initialize();
			}
		}

		public virtual void ProcessCandidateValue()
		{
			for(int i = 0; i < springValues.Length; i++)
			{
				springValues[i].ApplyCandidateValue();
			}
		}

		public void CheckEvents()
		{
			bool isClamped = false;

			for (int i = 0; i < springValues.Length; i++)
			{
				isClamped = isClamped || springValues[i].IsClamped();
			}

			if (isClamped && OnClampingApplied != null)
			{
				OnClampingApplied();
			}
		}

#if UNITY_EDITOR && ALLIN1SPRINGS_DEBUGGER
		public void AssignID()
		{
			this.id = LAST_SPRING_ID;
			LAST_SPRING_ID++;
		}

		public int GetID()
		{
			return id;
		}
#endif

		#region FORCE AND DRAG
		public void SetUnifiedForceAndDragEnabled(bool value)
		{
			this.unifiedForceAndDrag = value;
		}

		public void SetUnifiedForce(float unifiedForce)
		{
			this.unifiedForce = unifiedForce;
		}

		public void SetUnifiedDrag(float unifiedDrag)
		{
			this.unifiedDrag = unifiedDrag;
		}

		public void SetForceByIndex(int index, float force)
		{
			springValues[index].SetForce(force);
		}

		public void SetDragByIndex(int index, float drag)
		{
			springValues[index].SetDrag(drag);
		}
		#endregion

		#region CLAMPING

		public void SetClampingEnabled(bool clampingEnabled)
		{
			this.clampingEnabled = clampingEnabled;
		}

		protected void SetMinValueByIndex(int index, float minValue)
		{
			springValues[index].SetMinValue(minValue);
		}

		protected void SetMaxValueByIndex(int index, float maxValue)
		{
			springValues[index].SetMaxValue(maxValue);
		}

		protected void SetStopSpringOnCurrentValueClampByIndex(int index, bool stop)
		{
			springValues[index].SetStopSpringOnCurrentValueClamp(stop);
		}

		protected void SetClampTargetByIndex(int index, bool clampTarget)
		{
			springValues[index].SetClampTarget(clampTarget);
		}

		protected void SetClampCurrentValueByIndex(int index, bool clampCurrentValue)
		{
			springValues[index].SetClampCurrentValue(clampCurrentValue);
		}

		#endregion
	}
}