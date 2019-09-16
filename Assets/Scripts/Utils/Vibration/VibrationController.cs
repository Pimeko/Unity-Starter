using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
	[SerializeField]
	BoolVariable vibrationActive;
	[SerializeField]
	float maxBatch = 10, delayBetweenBatchReset = 2;
	[SerializeField]
	bool logOnAnyVibration;

	float nbInBatch;
	bool isDelaying;

	private void Start()
	{
		nbInBatch = 0;
		isDelaying = false;
	}

	void UpdateBatch()
	{
		if (isDelaying)
			return;
		if (nbInBatch < maxBatch)
		{
			nbInBatch++;
			if (logOnAnyVibration)
				print("[VIBRATION]");
		}
		else
		{
			isDelaying = true;
			DOVirtual.DelayedCall(delayBetweenBatchReset, () => {
				nbInBatch = 0;
				isDelaying = false;
			});
		}
	}

	public void VibrateLight()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
		#endif
	}

	public void VibrateMedium()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
		#endif
	}

	public void VibrateHeavy()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}

	public void VibrateSuccess()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}

	public void VibrateFailure()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Failure);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}

	public void VibrateSelectionChange()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		#if !UNITY_EDITOR && UNITY_IOS
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.SelectionChange);
		#elif UNITY_ANDROID
			Handheld.Vibrate();
		#endif
	}
}