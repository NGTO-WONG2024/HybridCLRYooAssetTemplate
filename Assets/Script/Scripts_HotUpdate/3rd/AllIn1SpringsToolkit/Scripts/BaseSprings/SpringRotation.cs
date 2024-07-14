using UnityEngine;

namespace AllIn1SpringsToolkit
{
	[System.Serializable]
	public class SpringRotation : Spring
	{
		public const int SPRING_SIZE = 10;

		private const int GLOBAL_AXIS_X = 0;
		private const int GLOBAL_AXIS_Y = 1;
		private const int GLOBAL_AXIS_Z = 2;

		private const int LOCAL_AXIS_X = 3;
		private const int LOCAL_AXIS_Y = 4;
		private const int LOCAL_AXIS_Z = 5;

		private const int ROTATION_AXIS_X = 6;
		private const int ROTATION_AXIS_Y = 7;
		private const int ROTATION_AXIS_Z = 8;
		private const int ANGLE = 9;
		
		private bool useQuaternionAsTarget;

		public SpringRotation() : base(SPRING_SIZE)
		{

		}

		public override int GetSpringSize()
		{
			return SPRING_SIZE;
		}

		public override bool HasValidSize()
		{
			return (springValues.Length == SPRING_SIZE);
		}

		#region CURRENT VALUES
		private Vector3 CurrentLocalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[LOCAL_AXIS_X].GetCurrentValue(),
					springValues[LOCAL_AXIS_Y].GetCurrentValue(),
					springValues[LOCAL_AXIS_Z].GetCurrentValue());

				return res;
			}
			set
			{
				springValues[LOCAL_AXIS_X].SetCurrentValue(value.x);
				springValues[LOCAL_AXIS_Y].SetCurrentValue(value.y);
				springValues[LOCAL_AXIS_Z].SetCurrentValue(value.z);
			}
		}

		private Vector3 CurrentGlobalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[GLOBAL_AXIS_X].GetCurrentValue(),
					springValues[GLOBAL_AXIS_Y].GetCurrentValue(),
					springValues[GLOBAL_AXIS_Z].GetCurrentValue());

				return res;
			}
			set
			{
				springValues[GLOBAL_AXIS_X].SetCurrentValue(value.x);
				springValues[GLOBAL_AXIS_Y].SetCurrentValue(value.y);
				springValues[GLOBAL_AXIS_Z].SetCurrentValue(value.z);
			}
		}

		private float CurrentAngle
		{
			get
			{
				float res = springValues[ANGLE].GetCurrentValue();
				return res;
			}
			set
			{
				springValues[ANGLE].SetCurrentValue(value);
			}
		}

		private Vector3 CurrentRotationAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[ROTATION_AXIS_X].GetCurrentValue(),
					springValues[ROTATION_AXIS_Y].GetCurrentValue(),
					springValues[ROTATION_AXIS_Z].GetCurrentValue());

				return res;
			}
			set
			{
				springValues[ROTATION_AXIS_X].SetCurrentValue(value.x);
				springValues[ROTATION_AXIS_Y].SetCurrentValue(value.y);
				springValues[ROTATION_AXIS_Z].SetCurrentValue(value.z);
			}
		}

		public Quaternion GetCurrentGlobalRotation()
		{
			Quaternion res = Quaternion.identity;
			if (useQuaternionAsTarget)
			{
				res = Quaternion.AngleAxis(CurrentAngle, CurrentRotationAxis);
			}
			else
			{
				res =
					Quaternion.AngleAxis(CurrentGlobalAxis.x, Vector3.right) *
					Quaternion.AngleAxis(CurrentGlobalAxis.y, Vector3.up) *
					Quaternion.AngleAxis(CurrentGlobalAxis.z, Vector3.forward);
			}

			return res;
		}

		public Quaternion GetCurrentValue()
		{
			Quaternion globalQuat = GetCurrentGlobalRotation();

			Vector3 forward = globalQuat * Vector3.forward;
			Vector3 up = globalQuat * Vector3.up;
			Vector3 right = globalQuat * Vector3.right;

			Quaternion res =
				Quaternion.AngleAxis(CurrentLocalAxis.x, right) *
				Quaternion.AngleAxis(CurrentLocalAxis.y, up) *
				Quaternion.AngleAxis(CurrentLocalAxis.z, forward) *
				globalQuat;

			return res;
		}

		public void SetCurrentValue(Quaternion currentQuaternion)
		{
			this.useQuaternionAsTarget = true;

			float angle;
			Vector3 axis;
			currentQuaternion.ToAngleAxis(out angle, out axis);
            
			if(Mathf.Approximately(Quaternion.Angle(currentQuaternion, Quaternion.identity), 0f) && Mathf.Approximately(angle, 0f))
			{
				axis = Vector3.forward;
				angle = 0f;
			}

			CurrentAngle = angle;
			CurrentRotationAxis = axis;
		}

		public void SetCurrentValue(Vector3 currentEuler)
		{
			this.useQuaternionAsTarget = false;
			CurrentGlobalAxis = currentEuler;
		}
		#endregion

		#region TARGET
		private Vector3 TargetLocalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[LOCAL_AXIS_X].GetTarget(),
					springValues[LOCAL_AXIS_Y].GetTarget(),
					springValues[LOCAL_AXIS_Z].GetTarget());

				return res;
			}
			set
			{
				springValues[LOCAL_AXIS_X].SetTarget(value.x);
				springValues[LOCAL_AXIS_Y].SetTarget(value.y);
				springValues[LOCAL_AXIS_Z].SetTarget(value.z);
			}
		}

		private Vector3 TargetGlobalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[GLOBAL_AXIS_X].GetTarget(),
					springValues[GLOBAL_AXIS_Y].GetTarget(),
					springValues[GLOBAL_AXIS_Z].GetTarget());

				return res;
			}
			set
			{
				springValues[GLOBAL_AXIS_X].SetTarget(value.x);
				springValues[GLOBAL_AXIS_Y].SetTarget(value.y);
				springValues[GLOBAL_AXIS_Z].SetTarget(value.z);
			}
		}

		private float TargetAngle
		{
			get
			{
				float res = springValues[ANGLE].GetTarget();
				return res;
			}
			set
			{
				springValues[ANGLE].SetTarget(value);
			}
		}

		private Vector3 TargetRotationAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[ROTATION_AXIS_X].GetTarget(),
					springValues[ROTATION_AXIS_Y].GetTarget(),
					springValues[ROTATION_AXIS_Z].GetTarget());

				return res;
			}
			set
			{
				springValues[ROTATION_AXIS_X].SetTarget(value.x);
				springValues[ROTATION_AXIS_Y].SetTarget(value.y);
				springValues[ROTATION_AXIS_Z].SetTarget(value.z);
			}
		}

		public void SetTarget(Quaternion target)
		{
			this.useQuaternionAsTarget = true;

			float angle;
			Vector3 axis;
			target.ToAngleAxis(out angle, out axis);
			
			
			if (Mathf.Approximately(Quaternion.Angle(target, Quaternion.identity), 0f) && Mathf.Approximately(angle, 0f))
			{
				axis = Vector3.forward;
				angle = 0f;
			}

			TargetRotationAxis = axis;
			TargetAngle = angle;
		}

		public void SetTarget(Vector3 targetValues)
		{
			this.useQuaternionAsTarget = false;
			TargetGlobalAxis = targetValues;
		}

		public Quaternion GetTarget()
		{
			Quaternion globalQuat = GetTargetGlobalRotation();


			Vector3 forward = globalQuat * Vector3.forward;
			Vector3 up = globalQuat * Vector3.up;
			Vector3 right = globalQuat * Vector3.right;

			Quaternion res =
				Quaternion.AngleAxis(TargetLocalAxis.x, right) *
				Quaternion.AngleAxis(TargetLocalAxis.y, up) *
				Quaternion.AngleAxis(TargetLocalAxis.z, forward) *
				globalQuat;

			return res;
		}

		private Quaternion GetTargetGlobalRotation()
		{
			Quaternion res = Quaternion.identity;
			if (useQuaternionAsTarget)
			{
				res = Quaternion.AngleAxis(TargetAngle, TargetRotationAxis);
			}
			else
			{
				res =
					Quaternion.AngleAxis(TargetGlobalAxis.x, Vector3.right) *
					Quaternion.AngleAxis(TargetGlobalAxis.y, Vector3.up) *
					Quaternion.AngleAxis(TargetGlobalAxis.z, Vector3.forward);
			}

			return res;
		}
		#endregion

		#region VELOCITY
		private Vector3 VelocityLocalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[LOCAL_AXIS_X].GetVelocity(),
					springValues[LOCAL_AXIS_Y].GetVelocity(),
					springValues[LOCAL_AXIS_Z].GetVelocity());

				return res;
			}
			set
			{
				springValues[LOCAL_AXIS_X].SetVelocity(value.x);
				springValues[LOCAL_AXIS_Y].SetVelocity(value.y);
				springValues[LOCAL_AXIS_Z].SetVelocity(value.z);
			}
		}

		private Vector3 VelocityGlobalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[GLOBAL_AXIS_X].GetVelocity(),
					springValues[GLOBAL_AXIS_Y].GetVelocity(),
					springValues[GLOBAL_AXIS_Z].GetVelocity());

				return res;
			}
			set
			{
				springValues[GLOBAL_AXIS_X].SetVelocity(value.x);
				springValues[GLOBAL_AXIS_Y].SetVelocity(value.y);
				springValues[GLOBAL_AXIS_Z].SetVelocity(value.z);
			}
		}

		private Vector3 VelocityRotationlAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[ROTATION_AXIS_X].GetVelocity(),
					springValues[ROTATION_AXIS_Y].GetVelocity(),
					springValues[ROTATION_AXIS_Z].GetVelocity());

				return res;
			}
			set
			{
				springValues[ROTATION_AXIS_X].SetVelocity(value.x);
				springValues[ROTATION_AXIS_Y].SetVelocity(value.y);
				springValues[ROTATION_AXIS_Z].SetVelocity(value.z);
			}
		}

		private float VelocityAngle
		{
			get
			{
				float res = springValues[ANGLE].GetVelocity();
				return res;
			}
			set
			{
				springValues[ANGLE].SetVelocity(value);
			}
		}

		public void AddVelocity(Vector3 eulerTarget)
		{
			const float velocityFactor = 150;
			VelocityLocalAxis += eulerTarget * velocityFactor;
		}

		public void SetVelocity(Vector3 eulerTarget)
		{
			VelocityLocalAxis = eulerTarget;
		}
		#endregion

		#region CANDIDATE VALUES
		private Vector3 CandidateLocalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[LOCAL_AXIS_X].GetCandidateValue(),
					springValues[LOCAL_AXIS_Y].GetCandidateValue(),
					springValues[LOCAL_AXIS_Z].GetCandidateValue());

				return res;
			}
			set
			{
				springValues[LOCAL_AXIS_X].SetCandidateValue(value.x);
				springValues[LOCAL_AXIS_Y].SetCandidateValue(value.y);
				springValues[LOCAL_AXIS_Z].SetCandidateValue(value.z);
			}
		}

		private Vector3 CandidateGlobalAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[GLOBAL_AXIS_X].GetCandidateValue(),
					springValues[GLOBAL_AXIS_Y].GetCandidateValue(),
					springValues[GLOBAL_AXIS_Z].GetCandidateValue());

				return res;
			}
			set
			{
				springValues[GLOBAL_AXIS_X].SetCandidateValue(value.x);
				springValues[GLOBAL_AXIS_Y].SetCandidateValue(value.y);
				springValues[GLOBAL_AXIS_Z].SetCandidateValue(value.z);
			}
		}

		private Vector3 CandidateRotationAxis
		{
			get
			{
				Vector3 res = new Vector3(
					springValues[ROTATION_AXIS_X].GetCandidateValue(),
					springValues[ROTATION_AXIS_Y].GetCandidateValue(),
					springValues[ROTATION_AXIS_Z].GetCandidateValue());

				return res;
			}
			set
			{
				springValues[ROTATION_AXIS_X].SetCandidateValue(value.x);
				springValues[ROTATION_AXIS_Y].SetCandidateValue(value.y);
				springValues[ROTATION_AXIS_Z].SetCandidateValue(value.z);
			}
		}

		private float CandidateAngle
		{
			get
			{
				float res = springValues[ANGLE].GetCandidateValue();
				return res;
			}
			set
			{
				springValues[ANGLE].SetCandidateValue(value);
			}
		}

		public override void ProcessCandidateValue()
		{
			Vector3 deltaLocalAxis = CandidateLocalAxis - CurrentLocalAxis;
			const float maxRotationPerFrame = 80f;
			deltaLocalAxis = Vector3.ClampMagnitude(deltaLocalAxis, maxRotationPerFrame);
			CandidateLocalAxis = deltaLocalAxis + CurrentLocalAxis;


			Vector3 deltaGlobalAxis = CandidateGlobalAxis - CurrentGlobalAxis;
			deltaGlobalAxis = Vector3.ClampMagnitude(deltaGlobalAxis, maxRotationPerFrame);
			CandidateGlobalAxis = deltaGlobalAxis + CurrentGlobalAxis;

			Vector3 candidateRotationAxis = CandidateRotationAxis;
			float dot = Vector3.Dot(candidateRotationAxis.normalized, CurrentRotationAxis.normalized);
			if (Mathf.Sign(dot) < 0)
			{
				CandidateAngle = -CandidateAngle;
				VelocityAngle = -VelocityAngle;
			}

			base.ProcessCandidateValue();
		}
		#endregion
	}
}