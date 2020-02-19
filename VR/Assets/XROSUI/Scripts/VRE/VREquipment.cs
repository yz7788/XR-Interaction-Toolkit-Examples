using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VREquipment : MonoBehaviour
{
    protected XRGrabInteractable m_GrabInteractable;
    protected MeshRenderer m_MeshRenderer;

    protected static Color m_UnityMagenta = new Color(0.929f, 0.094f, 0.278f);
    protected static Color m_UnityCyan = new Color(0.019f, 0.733f, 0.827f);

    protected bool m_Held = false;

    public GameObject mainMenu;
    public GameObject audioMenu;
    public GameObject goggleMenu;


    float lastHeldTime;
    public float timeBeforeReturn = 0.5f;
    public GameObject socket;

    void OnEnable()
    {
        m_GrabInteractable = GetComponent<XRGrabInteractable>();
        m_MeshRenderer = GetComponent<MeshRenderer>();

        m_GrabInteractable.onFirstHoverEnter.AddListener(onFirstHoverEnter);
        m_GrabInteractable.onHoverEnter.AddListener(OnHoverEnter);
        m_GrabInteractable.onLastHoverExit.AddListener(OnHoverExit);
        m_GrabInteractable.onSelectEnter.AddListener(OnGrabbed);
        m_GrabInteractable.onSelectExit.AddListener(OnReleased);
    }


    private void OnDisable()
    {
        m_GrabInteractable.onFirstHoverEnter.RemoveListener(onFirstHoverEnter);
        m_GrabInteractable.onHoverEnter.RemoveListener(OnHoverEnter);
        m_GrabInteractable.onLastHoverExit.RemoveListener(OnHoverExit);
        m_GrabInteractable.onSelectEnter.RemoveListener(OnGrabbed);
        m_GrabInteractable.onSelectExit.RemoveListener(OnReleased);
    }

    private void OnGrabbed(XRBaseInteractor obj)
    {
        m_MeshRenderer.material.color = m_UnityCyan;
        //print("Grabbed: " + this.name);
        m_Held = true;
        this.transform.SetParent(null);
    }

    void OnReleased(XRBaseInteractor obj)
    {
        m_MeshRenderer.material.color = Color.white;
        m_Held = false;
    }

    void OnHoverExit(XRBaseInteractor obj)
    {
        if (!m_Held)
        {
            m_MeshRenderer.material.color = Color.white;
        }
    }

    void onFirstHoverEnter(XRBaseInteractor obj)
    {
        if (!m_Held)
        {
            //print("Hover: " + this.name);
            m_MeshRenderer.material.color = m_UnityMagenta;
        }
    }

    void OnHoverEnter(XRBaseInteractor obj)
    {
        if (!m_Held)
        {
            //Vibrate
            //foreach(XRBaseInteractor hi in this.m_GrabInteractable.hoveringInteractors)
            //{
               
            //}
        }
    }

    public virtual void AlternateFunction()
    {

    }

    void Update()
    {
        if (m_Held)
        {
            lastHeldTime = Time.time;
        }
        else if (!m_Held && Time.time > lastHeldTime + timeBeforeReturn)
        {
            this.transform.position = socket.transform.position;
            this.transform.SetParent(socket.transform);
        }
    }
}
