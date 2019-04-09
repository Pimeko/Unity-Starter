using UnityEngine;
using System.Collections;
#if (UNITY_IOS && !UNITY_EDITOR)
using System.Runtime.InteropServices;
#endif

public class iOSHapticFeedback : MonoBehaviour {

    static iOSHapticFeedback _instance;

    public static iOSHapticFeedback Instance
    {
        get
        {
            if (!_instance)
            {
                Debug.LogWarning("No iOS Haptic Feedback instance available. Creating one.");
                GameObject obj = new GameObject("iOS Haptic Feedback");
                _instance = obj.AddComponent<iOSHapticFeedback>();
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class iOSFeedbackTypeSettings
    {
        public bool SelectionChange = true, ImpactLight = true, ImpactMedium = true, ImpactHeavy = true;
        public bool NotificationSuccess = true, NotificationWarning = true, NotificationFailure = true;

        public bool Notifications
        {
            get
            {
                return NotificationSuccess || NotificationWarning || NotificationFailure;
            }
        }
    }

    /// <summary>
    /// Defines which feedback generators will be used
    /// This prevents the instantiation of feedback generators that are not used.
    /// </summary>
    public iOSFeedbackTypeSettings usedFeedbackTypes = new iOSFeedbackTypeSettings();


    /// <summary>
    /// Instantiate all feedback generators at start.
    /// </summary>
    protected virtual void Awake()
    {
        // Prevent having more than one instance in a scene
        if (_instance)
        { 
            Debug.LogWarning("There is already an instance of iOSHapticFeedback.");
            Destroy(gameObject);
            return;
        }

        // The iOSHapticFeedback instance should be persistent between scenes
        DontDestroyOnLoad(gameObject);

        _instance = this;

        // Initiate the feedback generators
        for (int i = 0; i < 5; i++)
        {
            if (FeedbackIdSet(i))
             {
                InstantiateFeedbackGenerator(i);
             }
        }
        feedbackGeneratorsSetUp = true;
    }

    bool feedbackGeneratorsSetUp = false;

    /// <summary>
    /// Release all feedback generators when the object is destroyed
    /// </summary>
    protected void OnDestroy()
    {
        // On destruction the feedback generators are released if they have been set up in the first place
        if (!feedbackGeneratorsSetUp)
            return;
        
        for (int i = 0; i < 5; i++)
        {
            if (FeedbackIdSet(i))
            {
                ReleaseFeedbackGenerator(i);
            }
        }
    }


    protected bool FeedbackIdSet(int id)
    {
        return ((id == 0 && usedFeedbackTypes.SelectionChange)
             || (id == 1 && usedFeedbackTypes.ImpactLight)
             || (id == 2 && usedFeedbackTypes.ImpactMedium)
             || (id == 3 && usedFeedbackTypes.ImpactHeavy)
            || ((id == 4 || id == 5 || id == 6) && usedFeedbackTypes.Notifications));
    }

    // Link to native functions
    #if UNITY_IOS && !UNITY_EDITOR
    [DllImport ("__Internal")]
    private static extern void _instantiateFeedbackGenerator(int id);

    [DllImport ("__Internal")]
    private static extern void _prepareFeedbackGenerator(int id);

    [DllImport ("__Internal")]
    private static extern void _triggerFeedbackGenerator(int id, bool advanced);

    [DllImport ("__Internal")]
    private static extern void _releaseFeedbackGenerator(int id);

    #else
    // Instantiate placeholders that do nothing for other platforms than iOS
    private void _instantiateFeedbackGenerator(int id){}
    private void _prepareFeedbackGenerator(int id){}
    private void _triggerFeedbackGenerator(int id, bool advanced){}
    private void _releaseFeedbackGenerator(int id){}

    #endif

    protected void InstantiateFeedbackGenerator(int id)
    {
        if (debug)
            Debug.Log("Instantiate iOS feedback generator "+(iOSFeedbackType)id);
        _instantiateFeedbackGenerator(id);
    }

    protected void PrepareFeedbackGenerator(int id)
    {
        if (debug)
            Debug.Log("Prepare iOS feedback generator "+(iOSFeedbackType)id);
        _prepareFeedbackGenerator(id);
    }

    protected void TriggerFeedbackGenerator(int id, bool advanced)
    {
        if (debug)
            Debug.Log("Trigger iOS feedback generator "+(iOSFeedbackType)id + ", advanced mode: "+advanced);
        _triggerFeedbackGenerator(id, advanced);
    }

    protected void ReleaseFeedbackGenerator(int id)
    {
        if (debug)
            Debug.Log("Release iOS feedback generator "+(iOSFeedbackType)id);
        _releaseFeedbackGenerator(id);
    }




    public bool debug = false;

    public enum iOSFeedbackType { SelectionChange, ImpactLight, ImpactMedium, ImpactHeavy, Success, Warning, Failure, None};


    bool _isEnabled = true;

    // Disable to globally turn off haptics feedback. Can be changed at runtime as well.
    public bool IsEnabled
    {
        set
        {
            _isEnabled = value;
            if (debug)
                Debug.Log("iOSHapticFeedback globally enabled: " + value);
        }
        get
        {
            return _isEnabled;
        }
    }


    /// <summary>
    /// Triggers one of the haptic feedbacks available on iOS.
    /// </summary>
    public virtual void Trigger(iOSFeedbackType feedbackType)
    {
        if (_isEnabled)
        {
            if (FeedbackIdSet((int)feedbackType))
                TriggerFeedbackGenerator((int)feedbackType, false);
            else
                Debug.LogError("You cannot trigger a feedback generator without instantiating it first");
        }
        else if (debug)
        {
            Debug.Log("Haptic Feedback not triggered because the property 'IsEnabled' of the iOSHapticFeedback component has beeen disabled.");
        }
    }


    /// <summary>
    /// Returns true if the current device supports haptic feedback by checking the device model.
    /// Cannot detect if haptic feedback is enabled in the settings.
    /// </summary>
    public bool IsSupported ()
    {
#if UNITY_IOS
        return (SystemInfo.supportsVibration &&
            SystemInfo.deviceModel.Contains("iPhone") &&
            UnityEngine.iOS.Device.generation >= UnityEngine.iOS.DeviceGeneration.iPhone7);
#else
        return false;
#endif
    }
}

