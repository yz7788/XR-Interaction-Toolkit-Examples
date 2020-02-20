using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSeducing : MonoBehaviour
{
  XRGrabInteractable m_GrabInteractable;
  MeshRenderer m_MeshRenderer;

  static Color m_UnityMagenta = new Color(0.929f, 0.094f, 0.278f);
  static Color m_UnityCyan = new Color(0.019f, 0.733f, 0.827f);

  GameObject laser=GameObject.Find("RightBaseController");//Parent node of right hand controller
  GameObject target= GameObject.Find("LaserSeducer");//The cube supposed to be locked on

  bool m_Held = false;

  void OnEnable()
  {
      m_GrabInteractable = GetComponent<XRGrabInteractable>();
      m_MeshRenderer = GetComponent<MeshRenderer>();

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

  void OnHoverExit(XRBaseInteractor obj)
  {
      if (!m_Held)
      {
          m_MeshRenderer.material.color = Color.white;
      }
  }

  void OnHoverEnter(XRBaseInteractor obj)
  {
      if (!m_Held)
      {
          m_MeshRenderer.material.color = m_UnityMagenta;
      }
  }

    // Start is called before the first frame update
  void Start()
  {

  }

    // Update is called once per frame
  void Update()
  {
    if(m_Held){
      Vector3 direction = (target.transform.position - laser.transform.position).normalized;
      Vector3 NormalVector=Vector3.Cross(laser.transform.forward,direction);
      Quaternion quaternion=Quaternion.FromToRotation(laser.transform.forward,direction);
      transform.RotateAround(laser.transform.position,NormalVector,Vector3.Angle(laser.transform.forward,direction));
    }
  }
}
