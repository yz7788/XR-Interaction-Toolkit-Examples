using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMoving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            transform.RotateAround(transform.position,Vector3.up,-10f*Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.D)){
            transform.RotateAround(transform.position,Vector3.up,10f*Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.W)){//Moving forwards
            transform.Rotate(new Vector3(-10f*Time.deltaTime,0,0));
        }
        else if(Input.GetKey(KeyCode.S)){
            transform.Rotate(new Vector3(10f*Time.deltaTime,0,0));
        }
        else if (Input.GetKey(KeyCode.Q)){
            print("Q pressed");
            GameObject target= GameObject.Find("Sphere_6");
            // Quaternion rotation = Quaternion.LookRotation((target.transform.position - source).normalized);
            // print("target tranform: "+target.transform);
            transform.LookAt(target.transform);
        }
    }
}
