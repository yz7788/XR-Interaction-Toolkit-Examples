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
        m_InteractableBase.onSelectEnter.AddListener(notifyCore);
    }
    void notifyCore(XRBaseInteractor obj){
        Core.Ins.ScenarioManager.SetFlag("GrabingKeyboard",true);//tell the Core user start keyboard successfully.
    }
    void DropKeyboard(XRBaseInteractor obj)
    {

    }

    void TriggerReleased(XRBaseInteractor obj)
    {
        if (kcc.active)
        {
            Core.Ins.ScenarioManager.SetFlag("TurnOffKeyboard",true);//tell the Core user start keyboard successfully.
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
        }
        else
        {
            Core.Ins.ScenarioManager.SetFlag("TurnOnKeyboard",true);//tell the Core user start keyboard successfully.
            kcc.CreateMirrorKeyboard(llamaPositon.position.x, llamaPositon.position.y, llamaPositon.position.z);
            kcc.active = true;

            leftRayController.GetComponent<XRRayInteractor>().maxRaycastDistance = 0;
            rightRayController.GetComponent<XRRayInteractor>().maxRaycastDistance = 0;
            leftDirectConroller.transform.localScale=new Vector3(ScaleNumber,ScaleNumber,ScaleNumber);
            rightDirectController.transform.localScale=new Vector3(ScaleNumber,ScaleNumber,ScaleNumber);
            leftRayController.transform.localScale=new Vector3(ScaleNumber,ScaleNumber,ScaleNumber);
            rightRayController.transform.localScale=new Vector3(ScaleNumber,ScaleNumber,ScaleNumber);
            LookAtCamera(obj);
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
                // print("change size "+controller.transform.GetChild(i).name);

                controller.transform.GetChild(i).transform.localScale = size;
                break;
            }
        }
    }
    private void LookAtCamera(XRBaseInteractor obj)
    {
        hemisphere.transform.RotateAround(obj.transform.position, transform.up, Camera.main.gameObject.transform.rotation.eulerAngles.y);

        GameObject charactorCreator = hemisphere.transform.GetChild(0).gameObject;
        int childCount = charactorCreator.transform.childCount;

        for (int i=0; i< childCount; i++)
        {
            GameObject key = charactorCreator.transform.GetChild(i).gameObject;
            var rotationVector = key.transform.rotation.eulerAngles;
            rotationVector.x = Camera.main.gameObject.transform.rotation.eulerAngles.x;
            key.transform.rotation = Quaternion.Euler(rotationVector);
        }

    }
}
