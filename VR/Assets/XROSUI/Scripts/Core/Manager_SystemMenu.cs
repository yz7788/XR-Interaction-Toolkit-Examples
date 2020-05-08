using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum XROSMenuTypes { Menu_None, Menu_General, Menu_Screenshot, Menu_Setting, Menu_Audio, Menu_Visual, Menu_User, Menu_Credit }

public class Manager_SystemMenu : MonoBehaviour
{
    public GameObject PF_SystemMenu;
    public GameObject GO_SystemMenu;
    public Controller_SystemMenu Module; 
    // Start is called before the first frame update
    void Start()
    {
        Module = GameObject.Find("PF_SystemMenu").GetComponent<Controller_SystemMenu>();
    }

    public void OpenMenu(XROSMenuTypes menu)
    {
        if(Module)
        {
            Module.OpenMenu(menu);
        }
        else
        {
            GO_SystemMenu = GameObject.Instantiate(PF_SystemMenu);
            Module = GO_SystemMenu.GetComponent<Controller_SystemMenu>();
            Dev.LogError("System Menu Controller doesn't exist");
        }
    }

    public void OpenMenu(string val)
    {
        XROSMenuTypes currentMenu;
        if (Enum.TryParse(val, true, out currentMenu))
        {
            if (Enum.IsDefined(typeof(XROSMenuTypes), currentMenu) | currentMenu.ToString().Contains(","))
            {
                Console.WriteLine("Converted '{0}' to {1}", val, currentMenu.ToString());

                OpenMenu(currentMenu);
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
    }

    // Update is called once per frame
    void Update()
    {
        DebugInput();
    }

    private void DebugInput()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            OpenMenu(XROSMenuTypes.Menu_None);
        }
        if (Input.GetKey(KeyCode.F2))
        {
            OpenMenu(XROSMenuTypes.Menu_General);
        }
        if (Input.GetKey(KeyCode.F3))
        {
            OpenMenu(XROSMenuTypes.Menu_Setting);
        }
        if (Input.GetKey(KeyCode.F4))
        {
            OpenMenu(XROSMenuTypes.Menu_Audio);
        }
        if (Input.GetKey(KeyCode.F5))
        {
            OpenMenu(XROSMenuTypes.Menu_Visual);
        }
        if (Input.GetKey(KeyCode.F6))
        {
            OpenMenu(XROSMenuTypes.Menu_User);
        }
        if (Input.GetKey(KeyCode.F7))
        {
            OpenMenu(XROSMenuTypes.Menu_Screenshot);
        }
        if (Input.GetKey(KeyCode.F8))
        {
            OpenMenu(XROSMenuTypes.Menu_Credit);
        }
    }
}
