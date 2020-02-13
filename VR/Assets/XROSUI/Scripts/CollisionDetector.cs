using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        print(col.gameObject.name + " printing collision");
    }

    void DisplayInput(InputField inputfield, char ch)
    {
        inputfield.text += ch;
        print("inputfield added " + inputfield.text);
    }
}
