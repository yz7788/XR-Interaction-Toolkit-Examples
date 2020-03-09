using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureArea : MonoBehaviour
{
    public Audio_Type type;
    new Controller_Audio audio;
    public GameObject GestureCore;
    public GameObject Area;
    public GameObject GO_VE;
    public VREquipment VE;
    public float gestureDistance;
    public float DistanceY;
    public float DistanceZ;
    public float volume;
    public bool Y;
    public bool Z;
    public float coolDown = 0.5f;
    float lastAskTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject go = GameObject.Find("Headphone2");
        this.RegisterVREquipment(GO_VE.GetComponent<VREquipment>());
    }

    public void RegisterVREquipment(VREquipment vre)
    {
        this.VE = vre;
        this.GO_VE = vre.gameObject;
    }
    public void UnregisterVREquipment()
    {
        this.VE = null;
        this.GO_VE = null;
    }

    // Update is called once per frame
    void Update()
    {
        gestureDistance = Vector3.Distance(GestureCore.transform.position, GO_VE.transform.position);
        if (gestureDistance <= 0.5f * Area.transform.localScale.y)
        {
            if (lastAskTime + coolDown < Time.time)
            {
                MeasureDirect();
                lastAskTime = Time.time;
            }
        }

        /*
        if (Input.GetKey(KeyCode.Alpha7))
        {
            Dev.Log("[Debug] Register Equipment");
            GameObject go = GameObject.Find("Headphone2");
            this.RegisterVREquipment(go.GetComponent<VREquipment>());
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            Dev.Log("[Debug] Deregister Equipment");
            this.UnregisterVREquipment();
        }
        if (Input.GetKey(KeyCode.O))
        {
            Dev.Log("[Debug] Move Equipment Up");
            GO_VE.transform.position += Vector3.up;
        }
        if (Input.GetKey(KeyCode.L))
        {
            Dev.Log("[Debug] Move Equipment Down");
            Dev.Log(GO_VE.transform.position);
            GO_VE.transform.position += Vector3.down;
            Dev.Log(GO_VE.transform.position);
        }
        if (GO_VE)
        {
            if (Input.GetKey(KeyCode.Alpha9))
            {
                Dev.Log("test");
                MeasureDirect();
            }
        }
        */
    }

    public void MeasureDirect()
    {
        //up/down
        DistanceY = GO_VE.transform.position.y - GestureCore.transform.position.y ;
        //volume = DistanceY / (Area.transform.localScale.y / 2);
        //Dev.Log("distance is " + DistanceY);


        //forward/backward
        DistanceZ = GO_VE.transform.position.z - GestureCore.transform.position.z ;

        if (Mathf.Abs(DistanceY) >= Mathf.Abs(DistanceZ))
        {
            if (DistanceY > 0)
            {
                //Y = true;
                this.VE.HandleGesture(ENUM_XROS_Gesture.up);
                //Dev.Log("up");
            }
            else if (DistanceY < 0)
            {
                //Y = false;
                this.VE.HandleGesture(ENUM_XROS_Gesture.down);
                //Dev.Log("down");
            }
            //else Dev.Log("no change");
        }
        else
        {
            if (DistanceZ > 0)
            {
                //Z = true;
                this.VE.HandleGesture(ENUM_XROS_Gesture.left);
                //Dev.Log("left");
            }
            else if (DistanceZ < 0)
            {
                //Z = false;
                this.VE.HandleGesture(ENUM_XROS_Gesture.right);
                //Dev.Log("right");
            }
            //else Dev.Log("no change");
        }
    }
}
