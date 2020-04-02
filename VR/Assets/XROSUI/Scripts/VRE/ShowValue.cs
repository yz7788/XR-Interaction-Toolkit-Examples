using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValue : MonoBehaviour
{
    //public GameObject GO_VE;
    //public VREquipment VE;
    Text m_Text;
    float value;
    //float coolDown = 0.5f;
    //float lastAskTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<Text>();
    }
    

    void CheckVRE()
    {
        /*
        if (VRE == headphone)
        {
            IfVolume();
        }
        if (VRE == headphone)
        {
            IfBrightness();
        }
        */
    }
    void IfVolume()
    {
        value = Core.Ins.AudioManager.GetVolume(Audio_Type.master);
        m_Text.text = "Volume:" + ((int)(Mathf.Pow(10f, value / 20f) * 100f)).ToString() + "%";
    }
    void IfBrightness()
    {
        value = Core.Ins.VisualManager.GetBrightness();
        m_Text.text = "Brightness:" + ((int)(value * 100f)).ToString() + "%";
    }
    // Update is called once per frame
    void Update()
    {
        IfVolume();
        //if a certain amount of time has passed,, hide myText
    }
}
