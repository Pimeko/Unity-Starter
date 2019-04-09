/// <summary>
/// If you use the advanced mode you have to prepare the feedback generator you are about to use
/// This can decrease the latency but is barely noticable. If you do not use advanced mode
/// the feedback generator is prepared and triggered at the same time
/// </summary>
public class iOSHapticFeedbackAdvanced : iOSHapticFeedback
{

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Triggers one of the haptic feedbacks available on iOS.
    /// Warning: You are using the advanced mode. Make sure to prepare every feedback type before you trigger it.
    /// </summary>
    /// <param name="feedbackType">Feedback type.</param>
    public override void Trigger(iOSFeedbackType feedbackType)
    {
        TriggerFeedbackGenerator((int)feedbackType, true);
    }



    public void InstantiateFeedbackGenerator(iOSFeedbackType feedbackType)
    {
        base.InstantiateFeedbackGenerator((int)feedbackType);
    }

    public void PrepareFeedbackGenerator(iOSFeedbackType feedbackType)
    {
        base.PrepareFeedbackGenerator((int)feedbackType);
    }

    public void TriggerFeedbackGenerator(iOSFeedbackType feedbackType)
    {
        Trigger(feedbackType);
    }

    public void ReleaseFeedbackGenerator(iOSFeedbackType feedbackType)
    {
        base.ReleaseFeedbackGenerator((int)feedbackType);
    }
}