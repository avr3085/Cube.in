using UnityEngine;
public class FloatingJoystick : MonoBehaviour
{
    [SerializeField] private RectTransform knob;
    public RectTransform Knob => knob;
    private RectTransform mRectTransform;
    public RectTransform JoystickRect => mRectTransform;

    private void Awake()
    {
        mRectTransform = GetComponent<RectTransform>();
    }
}