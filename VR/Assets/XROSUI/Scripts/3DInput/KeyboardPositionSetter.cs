using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class KeyboardPositionSetter : MonoBehaviour
{
    public SeparateKeyboardCharacterCreator kcc;
    XRGrabInteractable m_InteractableBase;

    const string k_AnimTriggerDown = "TriggerDown";
    const string k_AnimTriggerUp = "TriggerUp";


    Transform llamaPositon;
    void Start()
    {
        llamaPositon = gameObject.GetComponent<Transform>();
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onDeactivate.AddListener(TriggerReleased);
    }

    void TriggerReleased(XRBaseInteractor obj)
    {
        if (kcc.active)
        {
            kcc.SaveKeyPositions();
            kcc.DestroyPoints();
            kcc.active = false;
        }
        else
        {
            kcc.CreateMirrorKeyboard(llamaPositon.position.x, llamaPositon.position.y, llamaPositon.position.z);
            kcc.active = true;
        }

    }


    void Update()
    {
    }

}
