using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace AllIn1SpringsToolkit
{
	[AddComponentMenu(SpringsToolkitConstants.ADD_COMPONENT_PATH + "Ui Slider Spring")]
	public class UiSliderSpringComponent : SpringComponent
	{
		public SpringFloat fillAmountSpring;

		[SerializeField] private Image autoUpdatedSliderImage;
		[SerializeField, Range(0f, 1f)] private float targetFillAmount;

		protected override void RegisterSprings()
		{
			RegisterSpring(fillAmountSpring);
		}

		protected override void SetCurrentValueByDefault()
		{
			fillAmountSpring.SetCurrentValue(autoUpdatedSliderImage.fillAmount);
		}

		protected override void SetTargetByDefault()
		{
			fillAmountSpring.SetTarget(autoUpdatedSliderImage.fillAmount);
		}

		public void Update()
		{
			UpdateTarget();
			UpdateFillAmount();
		}

		private void UpdateTarget()
		{
			fillAmountSpring.SetTarget(targetFillAmount);
		}

		private void UpdateFillAmount()
		{
			autoUpdatedSliderImage.fillAmount = fillAmountSpring.GetCurrentValue();
		}
		
		public void SetTargetImmediately(float currentHp)
		{
			targetFillAmount = currentHp;
			fillAmountSpring.SetTarget(currentHp);
			fillAmountSpring.ReachEquilibrium();
		}

		public void SetTarget(float currentHp)
		{
			targetFillAmount = currentHp;
			fillAmountSpring.SetTarget(currentHp);
		}

		public override bool IsValidSpringComponent()
		{
			bool res = true;

			if(autoUpdatedSliderImage == null)
			{
				AddErrorReason($"{gameObject.name} autoUpdatedSliderImage is null.");
				res = false;
			}

			return res;
		}

#if UNITY_EDITOR
		private void Reset()
		{
			if(autoUpdatedSliderImage == null)
			{
				autoUpdatedSliderImage = GetComponent<Image>();
			}
		}
#endif
	}
}
