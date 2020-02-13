using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRKey : MonoBehaviour
{
    public CharacterCreatorScript characterCreator;

    public string myKey = "1";

    //Handle Collision here
    void OnCollisionEnter(Collision collision)
    {
        //Check for User's Input Device
        //if()
        characterCreator.RegisterInput(myKey);

        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //if (collision.relativeVelocity.magnitude > 2)
        //    audioSource.Play();
    }
}
