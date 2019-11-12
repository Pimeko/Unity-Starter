using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class VibrationController : MonoBehaviour
{
	[SerializeField]
	BoolVariable vibrationActive;
	[SerializeField]
	bool useBatch;
	[SerializeField, ShowIf("useBatch")]
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
		if (!useBatch || isDelaying)
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

		MMVibrationManager.Haptic(HapticTypes.LightImpact);
	}

	public void VibrateMedium()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		MMVibrationManager.Haptic(HapticTypes.MediumImpact);
	}

	public void VibrateHeavy()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
	}

	public void VibrateSuccess()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		MMVibrationManager.Haptic(HapticTypes.Success);
	}

	public void VibrateFailure()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		MMVibrationManager.Haptic(HapticTypes.Failure);
	}

	public void VibrateSelectionChange()
	{
		if (!isDelaying && vibrationActive != null && !vibrationActive.Value)
			return;

		UpdateBatch();

		MMVibrationManager.Haptic(HapticTypes.Selection);
	}
}