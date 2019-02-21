using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
	public void VibrateLight()
	{
		#if UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
		#endif
	} 

	public void VibrateMedium()
	{
		#if UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
		#endif
	}

	public void VibrateHeavy()
	{
		#if UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	} 
}