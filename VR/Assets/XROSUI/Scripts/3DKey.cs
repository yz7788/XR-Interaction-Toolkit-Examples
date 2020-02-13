using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public CharacterCreatorScript cc;

    public string myKey = "test";

    //Handle Collision here
    void OnCollisionEnter(Collision collision)
    {
        //Check for User's Input Device
        //if()
        cc.RegisterInput(myKey);

        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //if (collision.relativeVelocity.magnitude > 2)
        //    audioSource.Play();
    }
}
