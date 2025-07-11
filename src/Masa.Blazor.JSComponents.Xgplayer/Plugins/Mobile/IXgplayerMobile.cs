namespace Masa.Blazor.Components.Xgplayer.Plugins.Mobile;

public interface IXgplayerMobile
{
    /// <summary>
    /// Disable gesture
    /// </summary>
    bool DisableGesture { get; set; }

    /// <summary>
    /// Whether to enable horizontal gesture processing
    /// </summary>
    bool GestureX { get; set; }

    /// <summary>
    /// Whether to enable vertical gesture processing
    /// </summary>
    bool GestureY { get; set; }

    /// <summary>
    /// Gesture range on the left, Ranges 0-1
    /// </summary>
    float ScopeL { get; set; }

    /// <summary>
    /// Gesture range on the right， Ranges 0-1
    /// </summary>
    float ScopeR { get; set; }

    /// <summary>
    /// Long press to fast forward and double speed
    /// </summary>
    double PressRate { get; set; }

    /// <summary>
    /// Whether to enable the dimming function on the right
    /// </summary>
    bool Darkness { get; set; }

    /// <summary>
    /// Dimming the maximum darkness
    /// </summary>
    double MaxDarkness { get; set; }

    /// <summary>
    /// Gesture trigger callback
    /// </summary>
    // Action? UpdateGesture { get; set; }

    /// <summary>
    /// Whether to enable upper and lower gradient shadows，value normal | none | top | bottom
    /// </summary>
    string Gradient { get; set; }

    /// <summary>
    /// Whether to update the currentTime of the player at the same time as touchMove, the default is false. During the gesture movement, the fast forward and rewind of the player will not be called directly, and then set when toucheEnd
    /// </summary>
    bool IsTouchingSeek { get; set; }

    /// <summary>
    /// touchemove triggers the pace, the default value is 5, used for throttling
    /// </summary>
    int MiniMoveStep { get; set; }

    /// <summary>
    /// Whether to disable the time progress bar
    /// </summary>
    bool DisableActive { get; set; }

    /// <summary>
    /// Whether to disable the time progress bar
    /// </summary>
    bool DisableTimeProgress { get; set; }

    /// <summary>
    /// Whether to hide the control bar when dragging
    /// </summary>
    bool HideControlsActive { get; set; }

    /// <summary>
    /// Hide the control bar when the gesture ends
    /// </summary>
    bool HideControlsEnd { get; set; }

    /// <summary>
    /// When sliding the player area to fast forward/rewind, the duration corresponding to the player area
    /// </summary>
    int MoveDuration { get; set; }

    /// <summary>
    /// Disable long press double speed adjustment
    /// </summary>
    bool DisablePress { get; set; }

    /// <summary>
    /// Whether to disable button prompts when fast forward/rewind
    /// </summary>
    bool DisableSeekIcon { get; set; }
}