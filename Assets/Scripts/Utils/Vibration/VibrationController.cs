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
	bool logOnAnyVibration;

	public enum PeriodicType
	{
		VERY_LIGHT,
		LIGHT,
		MEDIUM,
		HEAVY
	}
	Tween currentPeriodicTween;

	void LogVibration(HapticTypes type)
	{
		if (logOnAnyVibration)
			print("[VIBRATION] " + type);
	}

	public void VibrateLight()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;

		MMVibrationManager.Haptic(HapticTypes.LightImpact);
		LogVibration(HapticTypes.LightImpact);
	}

	public void VibrateMedium()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;

		MMVibrationManager.Haptic(HapticTypes.MediumImpact);
		LogVibration(HapticTypes.MediumImpact);
	}

	public void VibrateHeavy()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;

		MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
		LogVibration(HapticTypes.HeavyImpact);
	}

	public void VibrateSuccess()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;

		MMVibrationManager.Haptic(HapticTypes.Success);
		LogVibration(HapticTypes.Success);
	}

	public void VibrateFailure()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;

		MMVibrationManager.Haptic(HapticTypes.Failure);
		LogVibration(HapticTypes.Failure);
	}

	public void VibrateSelectionChange()
	{
		if (vibrationActive != null && !vibrationActive.Value)
			return;

		MMVibrationManager.Haptic(HapticTypes.Selection);
		LogVibration(HapticTypes.Selection);
	}

	public void BeginPeriodic(PeriodicType type, float period = .25f)
	{
        DOTweenUtils.KillTween(ref currentPeriodicTween);
		switch (type)
		{
			case PeriodicType.VERY_LIGHT:
				VibrateSelectionChange();
				break;
			case PeriodicType.MEDIUM:
				VibrateMedium();
				break;
			case PeriodicType.HEAVY:
				VibrateHeavy();
				break;
			case PeriodicType.LIGHT:
			default:
				VibrateLight();
				break;
		}
        DOTweenUtils.KillTween(ref currentPeriodicTween);
        currentPeriodicTween = DOVirtual.DelayedCall(period, () =>
        {
            BeginPeriodic(type, period);
        });
	}

	public void StopPeriodic()
	{
		DOTweenUtils.KillTween(ref currentPeriodicTween);
	}
}