using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpenMenu : MonoBehaviour
{
    public XROSMenuTypes AssociatedMenuType;
    public Button button;
    private void Start()
    {
        if (button == null)
        {
            button = this.GetComponent<Button>();
            Dev.LogWarning("Button is not assigned in " + this.name);
        }
        if (button)
        {
            button.onClick.AddListener(OpenAssociatedMenu);
        }
    }
    public void OpenAssociatedMenu()
    {
        Core.Ins.SystemMenu.OpenMenu(AssociatedMenuType);
    }
}
