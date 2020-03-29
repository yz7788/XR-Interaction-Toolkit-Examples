using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class KeyboardPositionSetter : MonoBehaviour
{
    public SeparateKeyboardCharacterCreator kcc;
    XRGrabInteractable m_InteractableBase;

    [SerializeField] ParticleSystem m_BubbleParticleSystem = null;

    const string k_AnimTriggerDown = "TriggerDown";
    const string k_AnimTriggerUp = "TriggerUp";

    bool m_TriggerDown;

    Transform llamaPositon;
    void Start()
    {
        llamaPositon = gameObject.GetComponent<Transform>();
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onActivate.AddListener(TriggerPulled);
        m_InteractableBase.onDeactivate.AddListener(TriggerReleased);
    }

    void TriggerReleased(XRBaseInteractor obj)
    {
        m_TriggerDown = false;
        if (kcc.active)
        {
            kcc.DestroyPoints();
            kcc.active = false;
        }
        else
        {
            kcc.CreatePoints(llamaPositon.position.x, llamaPositon.position.y, llamaPositon.position.z);
            kcc.active = true;
        }

    }

    void TriggerPulled(XRBaseInteractor obj)
    {
        m_TriggerDown = true;
    }


    void Update()
    {
    }

}
