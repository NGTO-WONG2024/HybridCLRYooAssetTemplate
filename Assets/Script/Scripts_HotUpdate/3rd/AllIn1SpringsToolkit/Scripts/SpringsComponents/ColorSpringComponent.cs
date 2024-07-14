using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Color Spring")]
	public class ColorSpringComponent : SpringComponent
	{
		public SpringColor colorSpring;
		[SerializeField] private bool autoUpdate;
		[SerializeField] private bool autoUpdatedObjectIsRenderer = true;
		[SerializeField] private Renderer autoUpdatedRenderer;
		[SerializeField] private Graphic autoUpdatedUiGraphic;

		private Color CurrentColor => GetCurrentValue();


		private Color GetDefaultColor()
		{
			Color res = Color.white;

			if (autoUpdate)
			{
				if (autoUpdatedObjectIsRenderer)
				{
					res = autoUpdatedRenderer.material.color;
				}
				else
				{
					res = autoUpdatedUiGraphic.material.color;
				}
			}

			return res;
		}

		protected override void SetCurrentValueByDefault()
		{
			Color defaultColor = GetDefaultColor();
			colorSpring.SetCurrentValue(defaultColor);
		}

		protected override void SetTargetByDefault()
		{
			Color defaultColor = GetDefaultColor();
			colorSpring.SetTarget(defaultColor);
		}

		public void Update()
		{
			if (autoUpdate)
			{
				if (autoUpdatedObjectIsRenderer)
				{
					autoUpdatedRenderer.material.color = CurrentColor;
				}
				else
				{
					autoUpdatedUiGraphic.color = CurrentColor;
				}
			}
		}

		protected override void RegisterSprings()
		{
			RegisterSpring(colorSpring);
		}

		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if (autoUpdate)
			{
				if (autoUpdatedObjectIsRenderer)
				{
					if (autoUpdatedRenderer == null)
					{
						AddErrorReason($"{gameObject.name} ColorSpringComponent Target Renderer is null but targetIsRenderer is enabled.");
						res = false;
					}
				}
				else if (autoUpdatedUiGraphic == null)
				{
					AddErrorReason($"{gameObject.name} ColorSpringComponent Target Image is null but autoUpdate is enabled.");
					res = false;
				}
			}

			return res;
		}

#if UNITY_EDITOR
		private void Reset()
		{
			if (autoUpdatedRenderer == null)
			{
				autoUpdatedRenderer = GetComponent<Renderer>();
			}
			if (autoUpdatedUiGraphic == null)
			{
				autoUpdatedUiGraphic = GetComponent<Graphic>();
			}
		}
#endif

		#region API PUBLIC METHODS
		public Color GetTarget()
		{
			return colorSpring.GetTargetColor();
		}

		public void SetTarget(Color targetColor)
		{
			colorSpring.SetTarget(targetColor);
		}

		public Vector4 GetVelocity()
		{
			return colorSpring.GetVelocity();
		}

		public void SetVelocity(Vector4 colorVelocity)
		{
			colorSpring.SetVelocity(colorVelocity);
		}

		//Usually we'll use this to make a color brighter
		public void AddVelocity(Vector4 colorVelocityDelta)
		{
			colorSpring.AddVelocity(colorVelocityDelta);
		}

		public void SetTargetAndReachEquilibrium(Color targetColor)
		{
			SetTarget(targetColor);
			ReachEquilibrium();
		}

		public void SetCurrentValue(Color currentColor)
		{
			colorSpring.SetCurrentValue(currentColor);
		}

		public Color GetCurrentValue()
		{
			Color res = colorSpring.GetCurrentValue();
			return res;
		}
		#endregion
	}
}