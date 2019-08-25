using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
	public void VibrateLight()
	{
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
		#endif
	}

	public void VibrateMedium()
	{
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
		#endif
	}

	public void VibrateHeavy()
	{
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}

	public void VibrateSuccess()
	{
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}

	public void VibrateFailure()
	{
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Failure);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}

	public void VibrateSelectionChange()
	{
		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.SelectionChange);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}
}