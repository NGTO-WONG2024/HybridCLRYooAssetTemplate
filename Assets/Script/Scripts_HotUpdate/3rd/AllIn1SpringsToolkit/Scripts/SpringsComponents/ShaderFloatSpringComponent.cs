using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpringsToolkit
{
	public class ShaderFloatSpringComponent : SpringComponent
	{
		public SpringFloat shaderValueSpring;

		[SerializeField] private string shaderPropertyName;
		
		[SerializeField] private bool targetIsRenderer = true;
		[SerializeField] private Renderer targetRenderer;
		[SerializeField] private Graphic targetGraphic;

		[SerializeField] private bool getAutoUpdatedMaterialFromTarget;
		[SerializeField] private Material autoUpdatedMaterial;        

        private int shaderPropertyID;
        private float initialShaderValue;

        protected override void Initialize()
        {
	        shaderPropertyID = Shader.PropertyToID(shaderPropertyName);
	        if(getAutoUpdatedMaterialFromTarget)
	        {
		        if(targetIsRenderer)
		        {
			        autoUpdatedMaterial = targetRenderer.material;
		        }
		        else
		        {
			        autoUpdatedMaterial = new Material(targetGraphic.material);
			        targetGraphic.material = autoUpdatedMaterial;
		        }
	        }
	        
			base.Initialize();
        }

		private float GetDefaultShaderValue()
		{
			float res = autoUpdatedMaterial.GetFloat(shaderPropertyID);
			return res;
		}

		protected override void SetCurrentValueByDefault()
		{
			float defaultShaderValue = GetDefaultShaderValue();
			shaderValueSpring.SetCurrentValue(defaultShaderValue);
		}

		protected override void SetTargetByDefault()
		{
			float defaultShaderValue = GetDefaultShaderValue();
			shaderValueSpring.SetTarget(defaultShaderValue);
		}


		public void Update()
		{
			autoUpdatedMaterial.SetFloat(shaderPropertyID, shaderValueSpring.GetCurrentValue());
		}
        
        public void ChangeTargetProperty(string newPropertyName)
        {
            shaderPropertyName = newPropertyName;
            shaderPropertyID = Shader.PropertyToID(shaderPropertyName);
        }

        private void Reset()
        {
            if(targetRenderer == null) targetRenderer = GetComponent<Renderer>();
            if(targetGraphic == null) targetGraphic = GetComponent<Graphic>();
            if(targetRenderer == null && targetGraphic != null) targetIsRenderer = false;
            if(targetRenderer != null && targetGraphic == null) targetIsRenderer = true;
            if(targetIsRenderer) targetGraphic = null;
            else targetRenderer = null;   
		}
		protected override void RegisterSprings()
		{
			RegisterSpring(shaderValueSpring);
		}

		public override bool IsValidSpringComponent()
		{
			if(targetIsRenderer && targetRenderer == null)
			{
				AddErrorReason($"{gameObject.name} ShaderFloatSpringComponent targetRenderer is null.");
				return false;
			}
			
			if(!targetIsRenderer && targetGraphic == null)
			{
				AddErrorReason($"{gameObject.name} ShaderFloatSpringComponent targetGraphic is null.");
				return false;
			}
			
			if(getAutoUpdatedMaterialFromTarget)
			{
				if(targetIsRenderer)
				{
					autoUpdatedMaterial = targetRenderer.material;
				}
				else
				{
					autoUpdatedMaterial = new Material(targetGraphic.material);
					targetGraphic.material = autoUpdatedMaterial;
				}
			}

			if(autoUpdatedMaterial == null)
			{
				AddErrorReason($"{gameObject.name} ShaderFloatSpringComponent autoUpdatedMaterial is null.");
				return false;
			}

			return true;
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			if(!getAutoUpdatedMaterialFromTarget)
			{
				autoUpdatedMaterial.SetFloat(shaderPropertyID, initialShaderValue);	
			}
		}
		
		public void SetTarget(float target)
		{
			shaderValueSpring.SetTarget(target);
		}
		
		public void SetCurrentValue(float value)
		{
			shaderValueSpring.SetCurrentValue(value);
		}

		public void AddVelocity(float velocity)
		{
			shaderValueSpring.AddVelocity(velocity);
		}
	}
}