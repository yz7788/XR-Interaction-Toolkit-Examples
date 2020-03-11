using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image myIcon;
    public void SetIcon(Sprite mySprite) 
    {
        myIcon.sprite = mySprite;
    }
}
