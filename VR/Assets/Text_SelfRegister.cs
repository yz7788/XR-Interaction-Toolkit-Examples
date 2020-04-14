using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Text_SelfRegister : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextDisplayType type;

    // Start is called before the first frame update
    void Start()
    {
        Core.Ins.ScenarioManager.Register(text, type);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
