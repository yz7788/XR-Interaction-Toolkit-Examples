using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAlongLocalAxis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
