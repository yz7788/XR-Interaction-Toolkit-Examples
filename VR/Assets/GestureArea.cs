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
    public float DistanceY;
    public float DistanceZ;
    public float volume;
    public bool Y;
    public bool Z;


    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKey(KeyCode.Alpha7))
        {
            print("[Debug] Register Equipment");
            GameObject go = GameObject.Find("Headphone2");
            this.RegisterVREquipment(go.GetComponent<VREquipment>());
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            print("[Debug] Deregister Equipment");
            this.UnregisterVREquipment();
        }
        if (Input.GetKey(KeyCode.O))
        {
            print("[Debug] Move Equipment Up");
            GO_VE.transform.position += Vector3.up;
        }
        if (Input.GetKey(KeyCode.L))
        {
            print("[Debug] Move Equipment Down");
            print(GO_VE.transform.position);
            GO_VE.transform.position += Vector3.down;
            print(GO_VE.transform.position);
        }
        if (GO_VE)
        {
            if (Input.GetKey(KeyCode.Alpha9))
            {
                print("test");
                MeasureDirect();
            }
        }
    }

    public void MeasureDirect()
    {
        //up/down
        DistanceY = GO_VE.transform.position.y - GestureCore.transform.position.y ;
        //volume = DistanceY / (Area.transform.localScale.y / 2);
        Debug.Log("distance is " + DistanceY);


        //forward/backward
        DistanceZ = GO_VE.transform.position.z - GestureCore.transform.position.z ;

        if (Mathf.Abs(DistanceY) >= Mathf.Abs(DistanceZ))
        {
            if (DistanceY > 0)
            {
                //Y = true;
                this.VE.HandleGesture(ENUM_XROS_Gesture.up);
                Debug.Log("up");
            }
            else if (DistanceY < 0)
            {
                //Y = false;
                this.VE.HandleGesture(ENUM_XROS_Gesture.down);
                Debug.Log("down");
            }
            else Debug.Log("no change");
        }

        else
        {
            if (DistanceZ > 0)
            {
                Z = true;
                Debug.Log("backward");
            }
            else if (DistanceZ < 0)
            {
                Z = false;
                Debug.Log("forward");
            }
        }

    }
}
