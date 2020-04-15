using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class KeyboardPositionSetter : MonoBehaviour
{
    public SeparateKeyboardCharacterCreator kcc;
    public GameObject hemisphere;
    XRGrabInteractable m_InteractableBase;

    const string k_AnimTriggerDown = "TriggerDown";
    const string k_AnimTriggerUp = "TriggerUp";
    public GameObject leftDirectConroller;
    public GameObject rightDirectController;
    public GameObject leftRayController;
    public GameObject rightRayController;
    public GameObject controllerPF;
    public float ScaleNumber;
    Transform llamaPositon;
    XRBaseInteractor controller;
    void Start()
    {
        llamaPositon = gameObject.GetComponent<Transform>();
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onDeactivate.AddListener(TriggerReleased);
        m_InteractableBase.onSelectExit.AddListener(DropKeyboard);
    }

    void DropKeyboard(XRBaseInteractor obj)
    {

    }

    void TriggerReleased(XRBaseInteractor obj)
    {
        if (kcc.active)
        {
            kcc.SaveKeyPositions();
            kcc.DestroyPoints();
            kcc.active = false;
            leftRayController.GetComponent<XRRayInteractor>().maxRaycastDistance = 10;
            rightRayController.GetComponent<XRRayInteractor>().maxRaycastDistance = 10;
            this.Transform(leftDirectConroller, true);
            this.Transform(rightDirectController, true);
            leftDirectConroller.transform.localScale = new Vector3(1, 1, 1);
            rightDirectController.transform.localScale = new Vector3(1, 1, 1);
            leftRayController.transform.localScale = new Vector3(1, 1, 1);
            rightRayController.transform.localScale = new Vector3(1, 1, 1);
            //hemisphere.transform.LookAt(Camera.main.transform);
        }
        else
        {
            kcc.CreateMirrorKeyboard(llamaPositon.position.x, llamaPositon.position.y, llamaPositon.position.z);
            //kcc.gameObject.transform.rotation = Camera.main.gameObject.transform.rotation;
            kcc.active = true;

            leftRayController.GetComponent<XRRayInteractor>().maxRaycastDistance = 0;
            rightRayController.GetComponent<XRRayInteractor>().maxRaycastDistance = 0;
            leftDirectConroller.transform.localScale=new Vector3(ScaleNumber,ScaleNumber,ScaleNumber);
            rightDirectController.transform.localScale=new Vector3(ScaleNumber,ScaleNumber,ScaleNumber);
            leftRayController.transform.localScale=new Vector3(ScaleNumber,ScaleNumber,ScaleNumber);
            rightRayController.transform.localScale=new Vector3(ScaleNumber,ScaleNumber,ScaleNumber);
            hemisphere.transform.RotateAround(obj.transform.position, transform.up, Camera.main.gameObject.transform.rotation.eulerAngles.y);
        }

    }

    void Transform(GameObject controller, bool large)
    {
        int count = controller.transform.childCount;
        Vector3 size;
        if (large)
            size = new Vector3(1f, 1f, 1f);
        else
            size = new Vector3(0.5f, 0.5f, 0.5f);
        print(size.ToString());
        for (int i = 0; i < count; i++)
        {
            if (controller.transform.GetChild(i).name.Contains(" Model"))
            {
                print("change size "+controller.transform.GetChild(i).name);

                controller.transform.GetChild(i).transform.localScale = size;
                break;
            }
        }

    }
    void Update()
    {
            

    }

}
