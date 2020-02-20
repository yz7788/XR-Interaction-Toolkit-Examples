using System;
using System.Collections.Generic;
using UnityEngine;
public class Controller_GameMenu : MonoBehaviour
{
    public GameObject XRRig;

    public GameObject Menu_General;
    public GameObject Menu_Setting;
    public GameObject Menu_Audio;
    public GameObject Menu_Visual;

    public enum menuTypes { Menu_General, Menu_Setting, Menu_Audio, Menu_Visual }

    public IDictionary<string, GameObject> menus = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = XRRig.transform.position + XRRig.transform.forward * 2.5f;
        this.transform.LookAt(XRRig.transform);
        this.transform.Rotate(Vector3.up, 180);

        print(menuTypes.Menu_General.ToString());
        menus.Add(menuTypes.Menu_General.ToString(), Menu_General);
        menus.Add(menuTypes.Menu_Setting.ToString(), Menu_Setting);
        menus.Add(menuTypes.Menu_Audio.ToString(), Menu_Audio);
        menus.Add(menuTypes.Menu_Visual.ToString(), Menu_Visual);
    }

    public void OpenMenu(string val)
    {
        menuTypes currentMenu;
        if (Enum.TryParse(val, true, out currentMenu))
        {
            if (Enum.IsDefined(typeof(menuTypes), currentMenu) | currentMenu.ToString().Contains(","))
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
        //GameObject currentMenuToOpen;


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
        if(Input.GetKey(KeyCode.N))
        {
            OpenMenu("Menu_General");
        }
        if (Input.GetKey(KeyCode.M))
        {
            OpenMenu("Menu_Setting");
        }
        if (Input.GetKey(KeyCode.H))
        {
            OpenMenu("Menu_Audio");
        }
        if (Input.GetKey(KeyCode.J))
        {
            OpenMenu("Menu_Visual");
        }

    }
}
