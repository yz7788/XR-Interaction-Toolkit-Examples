using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_RayInteractorRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(transform.position, Vector3.up, -10f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(transform.position, Vector3.up, 10f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W))
        {//Moving forwards
            transform.Rotate(new Vector3(-10f * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(new Vector3(10f * Time.deltaTime, 0, 0));
        }
    }
}
