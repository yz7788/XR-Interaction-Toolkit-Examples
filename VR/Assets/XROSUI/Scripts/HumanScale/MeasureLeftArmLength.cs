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
    int readingFile = 1;
    bool firstSetSkeletonPos = false;
    // float offset_x = 0.0f;     // This is for TEST UI ONLY
    // float offset_y = 0.0f;     // This is for TEST UI ONLY
    // float offset_z = -0.1f;     // This is for TEST UI ONLY
    // UI components
    public Button LeftArmMeasureButton;
    public Button UpdateFromFileButton;
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
    // Bone GO of Stick Skeleton
    GameObject Neck;
    GameObject Spine;
    GameObject RightHip;
    GameObject LeftHip;
    GameObject LeftUpperLeg;
    GameObject RightUpperLeg;
    GameObject LeftLowerLeg;
    GameObject RightLowerLeg;
    GameObject LeftFoot;
    GameObject RightFoot;
    GameObject LeftShoulder;
    GameObject RightShoulder;
    GameObject LeftLowerArm;
    GameObject LeftUpperArm;
    GameObject RightLowerArm;
    GameObject RightUpperArm;

    void Start()
    {
        // UI initialization
        Button measureBtn = LeftArmMeasureButton.GetComponent<Button>();
        measureBtn.onClick.AddListener(MeasureLeftArm);
        Button updateFromFileBtn = UpdateFromFileButton.GetComponent<Button>();
        updateFromFileBtn.onClick.AddListener(updateFromFile);
        LeftArmLengthText = GameObject.Find("LeftArmLengthText").GetComponent<Text>();
        LeftArmInstructionText = GameObject.Find("LeftArmInstructionText").GetComponent<Text>();
        Image workflowPoseImg = workflowPose.GetComponent<Image>();

        thisUIPanel = GameObject.Find("UIForUpdate");
        InReachFarthestPlane = GameObject.Find("InReachFarthestPlane");
        InReachProperPlane = GameObject.Find("InReachProperPlane");

        Neck = GameObject.Find("Neck");
        Spine = GameObject.Find("Spine");
        RightHip = GameObject.Find("RightHip");
        LeftHip = GameObject.Find("LeftHip");
        LeftUpperLeg = GameObject.Find("LeftUpperLeg");
        RightUpperLeg = GameObject.Find("RightUpperLeg");
        LeftLowerLeg = GameObject.Find("LeftLowerLeg");
        RightLowerLeg = GameObject.Find("RightLowerLeg");
        LeftFoot = GameObject.Find("LeftFoot");
        RightFoot = GameObject.Find("RightFoot");
        LeftShoulder = GameObject.Find("LeftShoulder");
        RightShoulder = GameObject.Find("RightShoulder");
        LeftLowerArm = GameObject.Find("LeftLowerArm");
        LeftUpperArm = GameObject.Find("LeftUpperArm");
        RightLowerArm = GameObject.Find("RightLowerArm");
        RightUpperArm = GameObject.Find("RightUpperArm");

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
            Core.Ins.ScenarioManager.SetFlag("AgreedCalibration", true);
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

    void updateFromFile()
    {
        // Read in skeleton
        if (readingFile == 1)
        {
            updateSkeletonFromFile("testSkeleton1");
            UpdateFromFileButton.GetComponentInChildren<Text>().text = "Update from File 2";
            readingFile = 2;
        }
        else
        {
            updateSkeletonFromFile("testSkeleton2");
            UpdateFromFileButton.GetComponentInChildren<Text>().text = "Update from File 1";
            readingFile = 1;
        }

        if (firstSetSkeletonPos == false)
        {
            stickSkeleton.transform.position = new Vector3(HMDPos.x + 0.8f, HMDPos.y - 0.70f, HMDPos.z);
            firstSetSkeletonPos = true;
        }
        
        // Set bone length in Game Object
        Neck.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.Neck) * 10.0f, 5.0f);
        Spine.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.Spine) * 10.0f, 5.0f);
        RightHip.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.RightHip) * 10.0f, 5.0f);
        LeftHip.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.LeftHip) * 10.0f, 5.0f);
        LeftUpperLeg.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.LeftUpperLeg) * 10.0f, 5.0f);
        RightUpperLeg.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.RightUpperLeg) * 10.0f, 5.0f);
        LeftLowerLeg.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.LeftLowerLeg) * 10.0f, 5.0f);
        RightLowerLeg.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.RightLowerLeg) * 10.0f, 5.0f);
        LeftFoot.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.LeftFoot) * 10.0f, 5.0f);
        RightFoot.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.RightFoot) * 10.0f, 5.0f);
        LeftShoulder.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.LeftShoulder) * 10.0f, 5.0f);
        RightShoulder.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.RightShoulder) * 10.0f, 5.0f);
        LeftLowerArm.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.LeftLowerArm) * 10.0f, 5.0f);
        LeftUpperArm.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.LeftUpperArm) * 10.0f, 5.0f);
        RightLowerArm.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.RightLowerArm) * 10.0f, 5.0f);
        RightUpperArm.transform.localScale = new Vector3(5.0f, Core.Ins.HumanScaleManager.getBoneLength((int)BoneIdx.RightUpperArm) * 10.0f, 5.0f);
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

    void updateSkeletonFromFile(string fileName)
    {
        StreamReader fi = new StreamReader(Application.dataPath + "/Data/" + fileName + ".txt");
        string[] axis = fi.ReadToEnd().Split(']');
        float[] x = axis[0].Replace("[", "").Replace("\r\n", "").Replace("\n", "").Split(' ').Where(s => s != "").Select(f => float.Parse(f)).ToArray();
        float[] y = axis[2].Replace("[", "").Replace("\r\n", "").Replace("\n", "").Split(' ').Where(s => s != "").Select(f => float.Parse(f)).ToArray();
        float[] z = axis[1].Replace("[", "").Replace("\r\n", "").Replace("\n", "").Split(' ').Where(s => s != "").Select(f => float.Parse(f)).ToArray();
        for (int i = 0; i < 17; i++)
        {
            Core.Ins.HumanScaleManager.setJointPosition(i, new Vector3(x[i], y[i], z[i]));
        }
        Core.Ins.HumanScaleManager.updateBoneLength();
    }
}
