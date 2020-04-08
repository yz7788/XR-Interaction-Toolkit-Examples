using System;
using System.Collections.Generic;
using UnityEngine;
public enum XROSMenuTypes { Menu_None, Menu_General, Menu_Setting, Menu_Audio, Menu_Visual }

public class Controller_GameMenu : MonoBehaviour
{
    public GameObject XRRig;

    //public GameObject UICanvas;
    public GameObject Menu_None;
    public GameObject Menu_General;
    public GameObject Menu_Setting;
    public GameObject Menu_Audio;
    public GameObject Menu_Visual;


    public IDictionary<string, GameObject> menus = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        XROSMenu.EVENT_NewMenu += OpenMenu;
        //print(menuTypes.Menu_General.ToString());
        menus.Add(XROSMenuTypes.Menu_None.ToString(), Menu_None);
        menus.Add(XROSMenuTypes.Menu_General.ToString(), Menu_General);
        menus.Add(XROSMenuTypes.Menu_Setting.ToString(), Menu_Setting);
        menus.Add(XROSMenuTypes.Menu_Audio.ToString(), Menu_Audio);
        menus.Add(XROSMenuTypes.Menu_Visual.ToString(), Menu_Visual);
    }

    //public void OpenMenu(XROSMenuTypes menuTypes)
    //{
    //    this.OpenMenu(menuTypes.ToString());
    //}
    public void OpenMenu(string val)
    {
        XROSMenuTypes currentMenu;
        if (Enum.TryParse(val, true, out currentMenu))
        {
            if (Enum.IsDefined(typeof(XROSMenuTypes), currentMenu) | currentMenu.ToString().Contains(","))
            {
                Console.WriteLine("Converted '{0}' to {1}", val, currentMenu.ToString());
            }
            else
            {
                Console.WriteLine("{0} is not a value of the enum", val);
            }
        }
        else
        {
            Console.WriteLine("{0} is not a member of the enum", val);
        }       

        foreach (KeyValuePair<string, GameObject> item in menus)
        {
            Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            if (item.Key != val)
            {
                item.Value.SetActive(false);
            }
            else
            {
                item.Value.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            OpenMenu("Menu_None");
        }
        if (Input.GetKey(KeyCode.F2))
        {
            OpenMenu("Menu_General");
        }
        if (Input.GetKey(KeyCode.F3))
        {
            OpenMenu("Menu_Setting");
        }
        if (Input.GetKey(KeyCode.F4))
        {
            OpenMenu("Menu_Audio");
        }
        if (Input.GetKey(KeyCode.F5))
        {
            OpenMenu("Menu_Visual");
        }
        //this.transform.position = XRRig.transform.position + XRRig.transform.forward * 2.5f;
        //this.transform.LookAt(XRRig.transform);
        //this.transform.Rotate(Vector3.up, 180);
    }
}
