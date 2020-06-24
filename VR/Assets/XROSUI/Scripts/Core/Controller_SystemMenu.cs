using System;
using System.Collections.Generic;
using UnityEngine;

public class Controller_SystemMenu : MonoBehaviour
{
    public GameObject Menu_None;
    public GameObject Menu_General;
    public GameObject Menu_Screenshot;
    public GameObject Menu_Setting;
    public GameObject Menu_Audio;
    public GameObject Menu_Visual;
    public GameObject Menu_User;
    public GameObject Menu_Credit;

    public IDictionary<XROSMenuTypes, GameObject> menus = new Dictionary<XROSMenuTypes, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        menus.Add(XROSMenuTypes.Menu_None, Menu_None);
        menus.Add(XROSMenuTypes.Menu_General, Menu_General);
        menus.Add(XROSMenuTypes.Menu_Screenshot, Menu_Screenshot);
        menus.Add(XROSMenuTypes.Menu_Setting, Menu_Setting);
        menus.Add(XROSMenuTypes.Menu_Audio, Menu_Audio);
        menus.Add(XROSMenuTypes.Menu_Visual, Menu_Visual);
        menus.Add(XROSMenuTypes.Menu_User, Menu_User);
        menus.Add(XROSMenuTypes.Menu_Credit, Menu_Credit);
    }

    public void OpenMenu(XROSMenuTypes menuTypes)
    {
        foreach (KeyValuePair<XROSMenuTypes, GameObject> item in menus)
        {
            Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            if (item.Value != null)
            {
                if (item.Key != menuTypes)
                {
                    item.Value.SetActive(false);
                }
                else
                {
                    item.Value.SetActive(true);
                }
            }
            else
            {
                Dev.LogError(item.Key + " does not exist");
            }
        }
    }
}
