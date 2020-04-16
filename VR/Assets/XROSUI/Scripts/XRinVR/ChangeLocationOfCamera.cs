using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocationOfCamera : MonoBehaviour
{
    public List<GameObject> DestinationList = new List<GameObject>();
    public Animator anim;
    public int currentLocationId = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DebugInput();
        if (Core.Ins.ScenarioManager.GetFlag("FinishedCalibration") && currentLocationId == 1)
        {
            anim.SetBool("fadeOut", true);
        }
        //if (Core.Ins.ScenarioManager.GetFlag("Keyboard Flag?") && currentLocationId == 2)
        //{
        //    anim.SetBool("fadeOut", true);
        //}
    }

    //Track debug inputs here
    //https://docs.google.com/spreadsheets/d/1NMH43LMlbs5lggdhq4Pa4qQ569U1lr_O7HSHESEantU/edit#gid=0
    void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MoveToLocation(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MoveToLocation(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MoveToLocation(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            MoveToLocation(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            anim.SetBool("fadeOut", true);
        }
    }

    public void ChangeLocation(int i)
    {
        // Debug.Log("change location!!");
        //if (message.Equals("ChangeLocation"))
        {
            currentLocationId++;
            MoveToLocation(this.currentLocationId);
        }
    }

    private void MoveToLocation(int locationID)
    {
        Core.Ins.XRManager.GetXRRig().transform.position = DestinationList[locationID].transform.position;
        Core.Ins.XRManager.GetXRRig().transform.forward = DestinationList[locationID].transform.forward;
        //GameObject.Find("XRRig_XROS").transform.position = Destination1.transform.position;
        //GameObject.Find("XRRig_XROS").transform.forward = -Destination1.transform.forward;
        anim.SetBool("fadeOut", false);
    }
}
