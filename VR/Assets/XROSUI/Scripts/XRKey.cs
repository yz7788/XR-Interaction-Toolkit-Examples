using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XRKey : MonoBehaviour
{
    public KeyboardController keyboardController;

    public string myKey = "1";
    public Text myText;
    public void Setup(string s, KeyboardController kc)
    {
        this.myKey = s;
        this.keyboardController = kc;
        this.name = "Key: " + s;
        myText.text = s;
    }
    //Handle Collision here
    void OnTriggerEnter(Collider other)
    {
        //Check for User's Input Device
        //if()
        print("triggered");
        keyboardController.RegisterInput(myKey);

        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //if (collision.relativeVelocity.magnitude > 2)
        //    audioSource.Play();
    }
}
