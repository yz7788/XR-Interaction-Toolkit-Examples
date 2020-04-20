using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class MeasureLeftArmLength : MonoBehaviour
{
    int stepCounter = 0;
    public float armLength = 0.0f;
    float straightDownY = 0.0f;
    float horizontalY = 0.0f;
    float offset_x = 0.0f;     // This is for TEST UI ONLY
    float offset_y = 0.0f;     // This is for TEST UI ONLY
    float offset_z = -0.1f;     // This is for TEST UI ONLY
    // UI components
    public Button LeftArmMeasureButton;
    public Text LeftArmLengthText;
    public Text LeftArmInstructionText;
    public RawImage workflowPose;
    public Texture workflowStep1;
    public Texture workflowStep2;
    public GameObject stickSkeleton;
    GameObject thisUIPanel;
    GameObject InReachFarthestPlane;
    GameObject InReachProperPlane;
    // Generic (from systems)
    GameObject LeftController;
    GameObject RightController;
    GameObject HMD;
    Vector3 LeftControllerPos;
    Vector3 RightControllerPos;
    Vector3 HMDPos;
    Vector3 LeftControllerRot;
    Vector3 RightControllerRot;
    Vector3 HMDRot;

    void Start()
    {
        // UI initialization
        Button measureBtn = LeftArmMeasureButton.GetComponent<Button>();
        measureBtn.onClick.AddListener(MeasureLeftArm);
        LeftArmLengthText = GameObject.Find("LeftArmLengthText").GetComponent<Text>();
        LeftArmInstructionText = GameObject.Find("LeftArmInstructionText").GetComponent<Text>();
        Image workflowPoseImg = workflowPose.GetComponent<Image>();

        thisUIPanel = GameObject.Find("UIForUpdate");
        InReachFarthestPlane = GameObject.Find("InReachFarthestPlane");
        InReachProperPlane = GameObject.Find("InReachProperPlane");

        // Function initialization
        LeftController = GameObject.Find("LeftController");
        RightController = GameObject.Find("RightController");
        HMD = GameObject.Find("HMD");

        stickSkeleton.transform.position = new Vector3(HMDPos.x + 1.0f, HMDPos.y - 0.5f, HMDPos.z);
    }

    void Update()
    {
        UpdateGenericPos();
    }

    public void MeasureLeftArm()
    {
        if (stepCounter == 0)
        {
            // Update instruction
            LeftArmInstructionText.text = $"Step 1. Stand still with your left arm straight down and use right controller to click \"Next\".";
            LeftArmMeasureButton.GetComponentInChildren<Text>().text = "Next";
            workflowPose.texture = workflowStep1;
            Core.Ins.ScenarioManager.SetFlag("AgreedCalibration",true);
            stepCounter++;
        }
        else if (stepCounter == 1)
        {
            // Measure when left arm is straight down
            straightDownY = LeftControllerPos.y;
            // Update instruction
            LeftArmInstructionText.text = $"Step 2.Raise your left arm in parallel to groung while holding other body parts stationary. Again, use right controller to click \"Next\".";
            workflowPose.texture = workflowStep2;

            stepCounter++;
        }
        else if (stepCounter == 2)
        {
            // Measure when left arm is raised to horizontal
            horizontalY = LeftControllerPos.y;
            armLength = Mathf.Abs(straightDownY - horizontalY);
            // Set length of bones
            Core.Ins.HumanScaleManager.setArmLength(armLength);
            // IN TEST
            SetBodyLength();
            LeftArmLengthText.text = $"Arm length: {armLength}";
            LeftArmMeasureButton.GetComponentInChildren<Text>().text = "Start";
            workflowPose.texture = null;
            // Change scale of stickSkeleton accordingly
            stickSkeleton.transform.localScale = new Vector3(armLength * 4.1f, armLength * 4.1f, armLength * 4.1f);
            stickSkeleton.transform.position = new Vector3(HMDPos.x + 0.8f, HMDPos.y - 0.70f, HMDPos.z);
            // Change position and scale of in-reach planes accordingly
            InReachFarthestPlane.transform.position = new Vector3(HMDPos.x, HMDPos.y - 0.3f, HMDPos.z);
            InReachProperPlane.transform.position = new Vector3(HMDPos.x, HMDPos.y - 0.3f, HMDPos.z);
            InReachFarthestPlane.transform.localScale = new Vector3(armLength * 700.0f, 1.0f, armLength * 700.0f);
            InReachProperPlane.transform.localScale = new Vector3(armLength * 420.0f, 1.5f, armLength * 420.0f);
            // Update UI position
            UpdateUIPos(thisUIPanel);
            LeftArmInstructionText.text = $"Measure the length of arm. Press \"Start\" to measure.";
            Core.Ins.ScenarioManager.SetFlag("FinishedCalibration",true);//tell the core your work is done
            stepCounter = 0;
        }

    }

    void SetBodyLength()
    {
        float singleRatio = armLength / 3.5f;
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.Neck, singleRatio * 0.5f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.Spine, singleRatio * 2.5f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.RightHip, singleRatio * 0.5f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.LeftHip, singleRatio * 0.5f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.LeftUpperLeg, singleRatio * 2.5f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.RightUpperLeg, singleRatio * 2.5f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.LeftLowerLeg, singleRatio * 2.0f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.RightLowerLeg, singleRatio * 2.0f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.LeftFoot, singleRatio * 1.0f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.RightFoot, singleRatio * 1.0f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.LeftShoulder, singleRatio * 1.2f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.RightShoulder, singleRatio * 1.2f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.LeftLowerArm, singleRatio * 2.0f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.LeftUpperArm, singleRatio * 1.5f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.RightLowerArm, singleRatio * 2.0f);
        Core.Ins.HumanScaleManager.setBoneLength((int)BoneIdx.RightUpperArm, singleRatio * 1.5f);
    }

    void UpdateUIPos(GameObject UIObject)
    {
        // Vector3 newPosition = new Vector3(UIObject.transform.position.x, UIObject.transform.position.y, UIObject.transform.position.z - armLength * 0.6f);
        float distance = armLength * 0.6f;
        UIObject.transform.position = new Vector3(HMDPos.x, HMDPos.y, HMDPos.z + distance);
    }

    float ComputeGeneric()
    {
        // Compute generic distance between HMD and left controller
        return ComputeDistance(LeftControllerPos, HMDPos);
    }

    float ComputeDistance(Vector3 pos1, Vector3 pos2)
    {

        float deltaX = pos1[0] - pos2[0];
        float deltaY = pos1[1] - pos2[1];
        float deltaZ = pos1[2] - pos2[2];
        return (float)Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
    }

    void UpdateGenericPos()
    {
        LeftControllerPos = LeftController.transform.position;
        RightControllerPos = RightController.transform.position;
        HMDPos = HMD.transform.position;
        LeftControllerRot = LeftController.transform.rotation.eulerAngles;
        RightControllerRot = RightController.transform.rotation.eulerAngles;
        HMDRot = HMD.transform.rotation.eulerAngles;
    }

    Vector3 normalize(Vector3 vec)
    {
        float magnitude = vec.x * vec.x + vec.y * vec.y + vec.z * vec.z;
        return new Vector3(vec.x / magnitude, vec.y / magnitude, vec.z / magnitude);
    }
}
