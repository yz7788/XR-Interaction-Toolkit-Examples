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
    // UI components
    public Button LeftArmMeasureButton;
    public Text LeftArmLengthText;
    public Text LeftArmInstructionText;
    // Generic (from systems)
    GameObject LeftController;
    GameObject RightController;
    GameObject HMD;
    Vector3 LeftControllerPos;
    Vector3 RightControllerPos;
    Vector3 HMDPos;
    float LHDistanceGeneric = 1.0f;

    void Start()
    {
        // UI initialization
        Button measureBtn = LeftArmMeasureButton.GetComponent<Button>();
        measureBtn.onClick.AddListener(MeasureLeftArm);
        LeftArmLengthText = GameObject.Find("LeftArmLengthText").GetComponent<Text>();
        LeftArmInstructionText = GameObject.Find("LeftArmInstructionText").GetComponent<Text>();

        // Function initialization
        LeftController = GameObject.Find("LeftController");
        RightController = GameObject.Find("RightController");
        HMD = GameObject.Find("HMD");
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

            stepCounter++;
        }
        else if (stepCounter == 1)
        {
            // Measure when left arm is straight down
            straightDownY = LeftControllerPos.y;
            // Update instruction
            LeftArmInstructionText.text = $"Step 2.Raise your left arm in parallel to groung while holding other body parts stationary. Again, use right controller to click \"Next\".";

            stepCounter++;
        }
        else if (stepCounter == 2)
        {
            // Measure when left arm is raised to horizontal
            horizontalY = LeftControllerPos.y;
            armLength = Mathf.Abs(straightDownY - horizontalY);
            LeftArmLengthText.text = $"Arm length: {armLength}";
            LeftArmMeasureButton.GetComponentInChildren<Text>().text = "Start";

            stepCounter = 0;
        }

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
    }

    Vector3 normalize(Vector3 vec)
    {
        float magnitude = vec.x * vec.x + vec.y * vec.y + vec.z * vec.z;
        return new Vector3(vec.x / magnitude, vec.y / magnitude, vec.z / magnitude);
    }
}
