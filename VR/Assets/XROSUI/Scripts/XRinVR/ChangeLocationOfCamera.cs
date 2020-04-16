using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocationOfCamera : MonoBehaviour
{
    public GameObject Destination1;
    public GameObject Destination2;
    public GameObject Destination3;
    public GameObject Destination4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLocation(string message)
    {
        Debug.Log("change location!!");
        if (message.Equals("ChangeLocation"))
        {
            GameObject.Find("XRRig_XROS").transform.position = Destination1.transform.position;
            GameObject.Find("XRRig_XROS").transform.forward= -Destination1.transform.forward;
        }
    }
}
