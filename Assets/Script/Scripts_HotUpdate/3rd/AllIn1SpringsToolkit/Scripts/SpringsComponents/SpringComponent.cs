using UnityEngine;
using System.Collections.Generic;

namespace AllIn1SpringsToolkit
{
	public abstract class SpringComponent : MonoBehaviour
	{
		private const float MAX_DELTA_TIME = 0.1f;
		public bool useScaledTime = true;

		private List<Spring> springs;
		
		protected bool isValidSpringComponent;
		[SerializeField] protected bool hasCustomInitialValues;
		[SerializeField] protected bool hasCustomTarget;
		protected string errorReason = string.Empty;
		
		[HideInInspector] public bool generalPropertiesUnfolded;
		[HideInInspector] public bool initialValuesUnfolded;

		private void Awake()
		{
			Initialize();
		}

		public virtual void OnDestroy()
		{
			UnregisterSprings();
		}

		protected virtual void Initialize()
		{
			springs = new List<Spring>();

			this.isValidSpringComponent = IsValidSpringComponent();
			if (!isValidSpringComponent)
			{
				SpringComponentNotValid();
			}
			else
			{
				RegisterSprings();
				CheckSpringSizes();

				SetInitialValues();

				for (int i = 0; i < springs.Count; i++)
				{
					springs[i].Initialize();
				}
			}
		}

		protected virtual void SetInitialValues()
		{
			if (!hasCustomInitialValues)
			{
				SetCurrentValueByDefault();
			}
			if (!hasCustomTarget)
			{
				SetTargetByDefault();
			}
		}

		protected virtual void SpringComponentNotValid()
		{
			string objectTypeName = GetType().ToString();
			objectTypeName = objectTypeName.Replace("AllIn1SpringsToolkit.", "");
			string fullErrorMessage = $"{gameObject.name} {objectTypeName} is not valid! Disabling Component";
			
			if (!string.IsNullOrEmpty(errorReason))
			{
				fullErrorMessage += $"\nReason: {errorReason}";
			}

			Debug.LogError(fullErrorMessage, gameObject);
			enabled = false;
		}

		protected void AddErrorReason(string reason)
		{
			if (!string.IsNullOrEmpty(errorReason))
			{
				errorReason += "\n";
			}

			errorReason += reason;
		}
		
		protected abstract void SetCurrentValueByDefault();
		protected abstract void SetTargetByDefault();

		protected abstract void RegisterSprings();

		public abstract bool IsValidSpringComponent();

		public void RegisterSpring(Spring spring)
		{
			this.springs.Add(spring);

#if ALLIN1SPRINGS_DEBUGGER && UNITY_EDITOR
			spring.AssignID();
			AllIn1DebuggerWindowData.RegisterSpring(this, spring);
#endif
		}

		public void UnregisterSprings()
		{
#if ALLIN1SPRINGS_DEBUGGER && UNITY_EDITOR
			AllIn1DebuggerWindowData.UnregisterSpring(this);
#endif
		}

		public virtual void LateUpdate()
		{
			this.isValidSpringComponent = IsValidSpringComponent();
			if (!isValidSpringComponent)
			{
				SpringComponentNotValid();
				return;
			}

			float deltaTime = useScaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
			deltaTime = Mathf.Min(deltaTime, MAX_DELTA_TIME);

			for (int i = 0; i < springs.Count; i++)
			{
				SpringLogic.UpdateSpring(deltaTime, springs[i]);
				
				springs[i].Update(deltaTime);
				springs[i].CheckEvents();

				springs[i].ProcessCandidateValue();
			}
		}

		public virtual void ReachEquilibrium()
		{
			for (int i = 0; i < springs.Count; i++)
			{
				springs[i].ReachEquilibrium();
			}
		}

		public void CheckSpringSizes()
		{
			for (int i = 0; i < springs.Count; i++)
			{
				bool hasValidSize = springs[i].CheckCorrectSize();

				if (!hasValidSize)
				{
					Debug.LogWarning($"Spring size not valid! Fix in the editor to avoid performance issues... [{gameObject.name}]", gameObject);
				}
			} 
		}

#if ALLIN1SPRINGS_DEBUGGER && UNITY_EDITOR
		public string GetSpringFieldName(Spring spring)
		{
			string res = string.Empty;

            System.Reflection.FieldInfo[] fields = this.GetType().GetFields();

			for(int i = 0; i < fields.Length; i++)
			{
				if (fields[i].FieldType == spring.GetType())
				{
					Spring springTmp = (Spring)fields[i].GetValue(this);
					if (springTmp.GetID() == spring.GetID())
					{
						res = fields[i].Name;
						break;
					}
				}
			}

			return res;
		}
#endif
	}
}