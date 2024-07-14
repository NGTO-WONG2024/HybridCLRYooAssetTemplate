using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Anchored Position Spring")]
    public class AnchoredPositionSpringComponent : SpringComponent
    {
        public SpringVector2 anchoredPositionSpring;

        [SerializeField] private bool useTransformAsTarget = false;
        [SerializeField] private RectTransform followRectTransform;
		[SerializeField] private RectTransform targetRectTransform;
        [SerializeField] private Vector2 anchoredPositionTarget;
        
        protected override void RegisterSprings()
        {
	        RegisterSpring(anchoredPositionSpring);
        }

		protected override void SetCurrentValueByDefault()
		{
			anchoredPositionSpring.SetCurrentValue(followRectTransform.anchoredPosition);
		}

		protected override void SetTargetByDefault()
		{
			SetTarget(followRectTransform.anchoredPosition);
		}

		private void UpdateSpringSetTarget()
		{
			if (useTransformAsTarget)
			{
				anchoredPositionSpring.SetTarget(targetRectTransform.anchoredPosition);
			}
			else
			{
				anchoredPositionSpring.SetTarget(anchoredPositionTarget);
			}
		}

        public void Update()
        {
			RefreshAnchoredPositionTarget();
			UpdateSpringSetTarget();
            UpdateFollowerTransform();
        }

		private void RefreshAnchoredPositionTarget()
		{
			if (useTransformAsTarget)
			{
				anchoredPositionTarget = targetRectTransform.anchoredPosition;
			}
		}

        private void UpdateFollowerTransform()
        {
            followRectTransform.anchoredPosition = anchoredPositionSpring.GetCurrentValue();
        }
        
		public override bool IsValidSpringComponent()
        {
			bool res = true;

			if (useTransformAsTarget && targetRectTransform == null)
			{
				AddErrorReason($"{gameObject.name} useTransformAsTarget is enabled but targetRectTransform is null");
				res = false;
			}
			if (followRectTransform == null)
			{
				AddErrorReason($"{gameObject.name} followRectTransform cannot be null");
				res = false;
			}

            return res;
        }

        private void OnValidate()
        {
	        if(followRectTransform == null)
	        {
		        followRectTransform = GetComponent<RectTransform>();
	        }
        }

		#region API PUBLIC METHODS
		public Vector2 GetCurrentValue()
		{
			Vector2 res = anchoredPositionSpring.GetCurrentValue();
			return res;
		}

		public void SetCurrentValue(Vector2 anchoredPosition)
		{
			anchoredPositionSpring.SetCurrentValue(anchoredPosition);
			UpdateFollowerTransform();
		}

		public Vector2 GetTarget()
		{
			return anchoredPositionTarget;
		}

		public void SetTarget(Vector2 target)
		{
			if (useTransformAsTarget)
			{
				targetRectTransform.anchoredPosition = target;
			}
			else
			{
				anchoredPositionTarget = target;
			}

			anchoredPositionSpring.SetTarget(target);
		}

		public void SetTargetAndReachEquilibrium(Vector2 target)
		{
			SetTarget(target);
			anchoredPositionSpring.ReachEquilibrium();
			UpdateFollowerTransform();
		}

		public Vector2 GetVelocity()
		{
			return anchoredPositionSpring.GetVelocity();
		}

		public void SetVelocity(Vector2 velocity)
		{
			anchoredPositionSpring.SetVelocity(velocity);
		}

		public void AddVelocity(Vector2 deltaVelocity)
		{
			anchoredPositionSpring.AddVelocity(deltaVelocity);
		}

		public void SetForce(Vector2 force)
		{
			anchoredPositionSpring.SetForce(force);
		}

		public void SetDrag(Vector2 drag)
		{
			anchoredPositionSpring.SetDrag(drag);
		}

		public void SetUnifiedForce(float unifiedForce)
		{
			anchoredPositionSpring.SetUnifiedForce(unifiedForce);
		}

		public void SetUnifiedDrag(float unifiedDrag)
		{
			anchoredPositionSpring.SetUnifiedDrag(unifiedDrag);
		}

		public void SetClampingEnabled(bool value)
		{
			anchoredPositionSpring.SetClampingEnabled(value);
		}

		public void SetClampTarget(bool clampTargetX, bool clampTargetY)
		{
			anchoredPositionSpring.SetClampTarget(clampTargetX, clampTargetY);
		}

		public void SetClampCurrentValue(bool clampCurrentValueX, bool clampCurrentValueY)
		{
			anchoredPositionSpring.SetClampCurrentValue(clampCurrentValueX, clampCurrentValueY);
		}

		public void SetMinValues(Vector2 minValues)
		{
			anchoredPositionSpring.SetMinValues(minValues);
		}

		public void SetMaxValues(Vector2 maxValues)
		{
			anchoredPositionSpring.SetMaxValues(maxValues);
		}

		public void SetSetopSpringOnClamp(bool stopX, bool stopY)
		{
			anchoredPositionSpring.SetStopSpringOnClamp(stopX, stopY);
		}
		#endregion
	}
}