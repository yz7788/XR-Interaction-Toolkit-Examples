using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
[System.Serializable]
public class XRKey : MonoBehaviour
{
    private KeyboardController keyboardController;
    public Text myText;
    public float x;
    public float y;
    public float z;
    private Color transparent = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    private XRGrabInteractable m_GrabInteractable;
    private MeshRenderer m_MeshRenderer;
    private static Color m_UnityMagenta = new Color(0.929f, 0.094f, 0.278f);
    private static Color m_UnityCyan = new Color(0.019f, 0.733f, 0.827f);
    private bool m_Held = false;
    private float hover_timer = 0;
    private bool hover_start = false;
    private Button Button_Timer;

    public void Setup(string s, KeyboardController kc)
    {
        this.keyboardController = kc;
        this.name = "Key: " + s;
        myText.text = s;
    }

    public void Setup(string s, KeyboardController kc, Button button)
    {
        this.keyboardController = kc;
        this.name = "Key: " + s;
        this.Button_Timer = button;
        myText.text = s;
    }

    void OnEnable()
    {
        m_GrabInteractable = GetComponent<XRGrabInteractable>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_MeshRenderer.material.color = transparent;
        m_GrabInteractable.onFirstHoverEnter.AddListener(OnHoverEnter);
        m_GrabInteractable.onLastHoverExit.AddListener(OnHoverExit);
        m_GrabInteractable.onSelectEnter.AddListener(OnGrabbed);
        m_GrabInteractable.onSelectExit.AddListener(OnReleased);
    }


    private void OnDisable()
    {
        m_GrabInteractable.onFirstHoverEnter.RemoveListener(OnHoverEnter);
        m_GrabInteractable.onLastHoverExit.RemoveListener(OnHoverExit);
        m_GrabInteractable.onSelectEnter.RemoveListener(OnGrabbed);
        m_GrabInteractable.onSelectExit.RemoveListener(OnReleased);
    }

    private void OnGrabbed(XRBaseInteractor obj)
    {
        m_MeshRenderer.material.color = m_UnityCyan;
        m_Held = true;
    }

    void OnReleased(XRBaseInteractor obj)
    {
        m_MeshRenderer.material.color = Color.white;
        m_Held = false;
    }
    private void Update()
    {
        if (m_Held)
        {
        }
        if (hover_start)
        {
            hover_timer += Time.deltaTime;
        }

        if (hover_timer >= 2f)
        {
            hover_start = false;
            hover_timer = 0;
            Button_Timer.onClick.Invoke();
        }
    }

    void OnHoverExit(XRBaseInteractor obj)
    {
        if (!m_Held)
        {
            m_MeshRenderer.material.color = transparent;
            hover_start = false;
            hover_timer = 0;
        }

    }

    void OnHoverEnter(XRBaseInteractor obj)
    {
        if (myText.text == "start" && hover_start == false)
        {
            hover_start = true;
            return;
        }
        if (!m_Held & keyboardController.getWaiting() == false)
        {
            if (myText.text == "DEL")
            {
                XROSInput.Backspace();
                m_MeshRenderer.material.color = m_UnityMagenta;
                return;
            }
            keyboardController.wait();
            keyboardController.SetWaiting();
            keyboardController.RegisterInput(myText.text);
            XROSInput.AddInput(myText.text);
            m_MeshRenderer.material.color = m_UnityMagenta;
        }
    }

    public void OnKeyClicked()
    {
        keyboardController.RegisterInput(myText.text);
        XROSInput.AddInput(myText.text);
        m_MeshRenderer.material.color = m_UnityMagenta;

    }

    public override string ToString()
    {
        return "{\"key\": \"" + myText.text + "\"," + "\"x\": " + x + "," + "\"y\": " + y + "," + "\"z\": " + z + "}";
    }


}
