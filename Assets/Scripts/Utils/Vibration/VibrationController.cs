using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
	[SerializeField]
	BoolVariable vibrationActive;

	public void VibrateLight()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;
			
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
		#endif
	}

	public void VibrateMedium()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;
			
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
		#endif
	}

	public void VibrateHeavy()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;
			
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}

	public void VibrateSuccess()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;
			
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}

	public void VibrateFailure()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;
			
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Failure);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}

	public void VibrateSelectionChange()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;
			
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.SelectionChange);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}
}