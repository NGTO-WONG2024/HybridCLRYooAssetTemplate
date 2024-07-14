using System.Collections.Generic;
using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public class TransformSpringComponent : SpringComponent
	{
		public enum SpaceType
		{
			WorldSpace,
			LocalSpace,
		}
		
		public SpaceType spaceType;

		public bool springPositionEnabled = true;
		public SpringVector3 positionSpring;
		
		public bool springScaleEnabled = true;
		public SpringVector3 scaleSpring;

		public bool springRotationEnabled = true;
		public SpringRotation rotationSpring;

		public Transform followerTransform;

		public bool useTransformAsTarget;
		public Transform targetTransform;

		private Vector3 positionTarget;
		private Vector3 scaleTarget;
		private Quaternion rotationTarget;


		#region INIT

		protected override void RegisterSprings()
		{
			RegisterSpring(positionSpring);
			RegisterSpring(scaleSpring);
			RegisterSpring(rotationSpring);
		}

		protected override void SetInitialValues()
		{
			if (!hasCustomInitialValues)
			{
				SetCurrentValueByDefault();
			}
			else
			{
				if (!positionSpring.useInitialValues)
				{
					SetCurrentValuePositionByDefault();
				}
				if (!rotationSpring.useInitialValues)
				{
					SetCurrentValueRotationByDefault();
				}
				if (!scaleSpring.useInitialValues)
				{
					SetCurrentValueScaleByDefault();
				}
			}

			if (useTransformAsTarget)
			{
				UpdateTarget();
			}
			else
			{
				if (!hasCustomTarget)
				{
					SetTargetByDefault();
				}
				else
				{
					if (!positionSpring.useCustomTarget)
					{
						SetTargetPositionByDefault();
					}
					if (!rotationSpring.useCustomTarget)
					{
						SetTargetRotationByDefault();
					}
					if (!scaleSpring.useCustomTarget)
					{
						SetTargetScaleByDefault();
					}
				}
			}
		}

		protected override void SetCurrentValueByDefault()
		{
			SetCurrentValuePositionByDefault();
			SetCurrentValueRotationByDefault();
			SetCurrentValueScaleByDefault();
		}

		private void SetCurrentValuePositionByDefault()
		{
			if (spaceType == SpaceType.LocalSpace)
			{
				positionSpring.SetCurrentValue(followerTransform.localPosition);
			}
			else
			{
				positionSpring.SetCurrentValue(followerTransform.position);
			}
		}

		private void SetCurrentValueRotationByDefault()
		{
			if (spaceType == SpaceType.LocalSpace)
			{
				rotationSpring.SetCurrentValue(followerTransform.localRotation);
			}
			else
			{
				rotationSpring.SetCurrentValue(followerTransform.rotation);
			}
		}

		private void SetCurrentValueScaleByDefault()
		{
			scaleSpring.SetCurrentValue(followerTransform.localScale);
		}

		protected override void SetTargetByDefault()
		{
			SetTargetPositionByDefault();
			SetTargetRotationByDefault();
			SetTargetScaleByDefault();
		}

		private void SetTargetPositionByDefault()
		{
			if (spaceType == SpaceType.LocalSpace)
			{
				positionSpring.SetTarget(followerTransform.localPosition);
			}
			else
			{
				positionSpring.SetTarget(followerTransform.position);
			}
		}

		private void SetTargetRotationByDefault()
		{
			if (spaceType == SpaceType.LocalSpace)
			{
				rotationSpring.SetTarget(followerTransform.localRotation);
			}
			else
			{
				rotationSpring.SetTarget(followerTransform.rotation);
			}
		}

		private void SetTargetScaleByDefault()
		{
			scaleSpring.SetTarget(followerTransform.localScale);
		}

		#endregion

		private void Start()
		{
			UpdateTransform();
		}

		public void Update()
		{
			UpdateTransform();

			if (useTransformAsTarget)
			{
				UpdateTarget();
			}
		}

		#region UPDATE

		public void UpdateTarget()
		{
			if (spaceType == SpaceType.WorldSpace)
			{
				GetTargetsWorldSpace();
			}
			else if (spaceType == SpaceType.LocalSpace)
			{
				GetTargetsLocalSpace();
			}

			RefreshSpringsTargets();
		}

		private void UpdateTransform()
		{
			if (spaceType == SpaceType.WorldSpace)
			{
				UpdateTransformWorldSpace();
			}
			else if (spaceType == SpaceType.LocalSpace)
			{
				UpdateTransformLocalSpace();
			}
		}

		private void UpdateTransformWorldSpace()
		{
			if (springPositionEnabled)
			{
				followerTransform.position = positionSpring.GetCurrentValue();
			}

			if (springRotationEnabled)
			{
				followerTransform.rotation = rotationSpring.GetCurrentValue();
			}

			if (springScaleEnabled)
			{
				followerTransform.localScale = scaleSpring.GetCurrentValue();
			}
		}

		private void UpdateTransformLocalSpace()
		{
			if (springPositionEnabled)
			{
				followerTransform.localPosition = positionSpring.GetCurrentValue();
			}

			if (springRotationEnabled)
			{
				followerTransform.localRotation = rotationSpring.GetCurrentValue();
			}

			if (springScaleEnabled)
			{
				followerTransform.localScale = scaleSpring.GetCurrentValue();
			}
		}

		private void GetTargetsWorldSpace()
		{
			if (springPositionEnabled)
			{
				positionTarget = targetTransform.position;
			}

			if (springRotationEnabled)
			{
				rotationTarget = targetTransform.rotation;
			}

			if (springScaleEnabled)
			{
				scaleTarget = targetTransform.localScale;
			}
		}

		private void GetTargetsLocalSpace()
		{
			if (springPositionEnabled)
			{
				positionTarget = targetTransform.localPosition;
			}

			if (springRotationEnabled)
			{
				rotationTarget = targetTransform.localRotation;
			}

			if (springScaleEnabled)
			{
				scaleTarget = targetTransform.localScale;
			}
		}

		private void RefreshSpringsTargets()
		{
			positionSpring.SetTarget(positionTarget);

			if(spaceType == SpaceType.WorldSpace)
			{
				rotationSpring.SetTarget(rotationTarget);
			}
			else
			{
				rotationSpring.SetTarget(rotationTarget);
			}

			scaleSpring.SetTarget(scaleTarget);
		}

		#endregion

		#region SCALE SPRING
		public void SetScaleTarget(Vector3 newScaleTarget)
		{
			this.scaleTarget = newScaleTarget;

			scaleSpring.SetTarget(scaleTarget);
		}

		public void AddVelocityToScaleSpring(Vector3 velocity)
		{
			scaleSpring.AddVelocity(velocity);
		}

		public void SetValuesScaleSpring(Vector3 values)
		{
			scaleSpring.SetCurrentValue(values);
		}
		#endregion

		#region POSITION SPRING
		public void SetPositionTarget(Vector3 newPositionTarget)
		{
			this.positionTarget = newPositionTarget;
			positionSpring.SetTarget(positionTarget);
		}

		public void AddVelocityToPositionSpring(Vector3 velocity)
		{
			positionSpring.AddVelocity(velocity);
		}

		public void SetValuesPositionSpring(Vector3 values)
		{
			positionSpring.SetCurrentValue(values);
		}
		#endregion

		#region ROTATION SPRING

		public void SetRotationTarget(Vector3 newRotationTargetEuler)
		{
			rotationSpring.SetTarget(newRotationTargetEuler);
			this.rotationTarget = rotationSpring.GetTarget();
		}

		public void SetRotationTarget(Quaternion newRotationTarget)
		{
			this.rotationTarget = newRotationTarget;
			rotationSpring.SetTarget(rotationTarget);
		}

		public void AddVelocityToRotationSpring(Vector3 velocityEuler)
		{
			rotationSpring.AddVelocity(velocityEuler);
		}

		public void SetValuesRotationSpring(Vector3 valuesEuler)
		{
			rotationSpring.SetCurrentValue(valuesEuler);
		}
		#endregion


		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if(useTransformAsTarget && targetTransform == null)
			{
				AddErrorReason($"{gameObject.name} useTransformAsTarget is enabled but targetTransform is null");
				res = false;
			}
			if(followerTransform == null)
			{
				AddErrorReason($"{gameObject.name} followerTransform cannot be null");
				res = false;
			}
			
			return res;
		}

#if UNITY_EDITOR
		private void Reset()
		{
			followerTransform = transform;
		}
#endif
	}
}