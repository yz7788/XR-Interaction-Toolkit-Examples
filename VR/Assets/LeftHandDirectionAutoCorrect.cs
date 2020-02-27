using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class LeftHandDirectionAutoCorrect : MonoBehaviour
{
    Vector3 priorDirection;
    void onGrab(){
        GameObject LeftLaserEmitter=GameObject.Find("LeftLaserEmitter");
        GameObject LeftBaseController=GameObject.Find("LeftBaseController");
        priorDirection=LeftLaserEmitter.transform.forward;
        LeftLaserEmitter.transform.forward=LeftBaseController.transform.forward;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
