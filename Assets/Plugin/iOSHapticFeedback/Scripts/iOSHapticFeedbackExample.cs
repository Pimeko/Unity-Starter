using UnityEngine;
using System.Collections;

public class iOSHapticFeedbackExample : MonoBehaviour {
    void Start ()
    {
        supported = iOSHapticFeedback.Instance.IsSupported();
        if (supported)
            Debug.Log("iOS Haptic Feedback supported");
        else
            Debug.Log("Your device does not support iOS haptic feedback");

    }

    bool supported = false;

    void OnGUI()
    {
        if (supported)
            GUI.Label(new Rect(0,0, 300, 50), "Your device supports haptic feedback.");
        else
            GUI.Label(new Rect(0,0, 300, 50), "Your device does not support haptic feedback.");

        for (int i = 0; i < 7; i++)
        {
            if (GUI.Button(new Rect(0,70 + i * 60, 300, 50), "Trigger "+(iOSHapticFeedback.iOSFeedbackType)i))
            {
                iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)i);
            }
        }

        if (GUI.Button(new Rect(0, 70 + 7 * 60, 300, 50), "Globally enabled: " + iOSHapticFeedback.Instance.IsEnabled))
        {
            iOSHapticFeedback.Instance.IsEnabled = !iOSHapticFeedback.Instance.IsEnabled;
        }
    }
}
