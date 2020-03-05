using UnityEngine;
using UnityEngine.UI; //create public inputfield 
public class CharacterCreatorScript : KeyboardController
{
    //Prefab for a 3D key
    public GameObject PF_Key; 
    public GameObject head;
    public GameObject row0 = null;
    public GameObject row1 = null;
    public GameObject row2 = null;
    public GameObject row3 = null;
    public GameObject row4 = null;
    private void Awake()
    {
        //These can be assigned in Inspector which is less prone to order changes
        GameObject keys = this.transform.GetChild(0).gameObject;
        row0 = keys.transform.GetChild(0).gameObject;
        row1 = keys.transform.GetChild(1).gameObject;
        row2 = keys.transform.GetChild(2).gameObject;
        row3 = keys.transform.GetChild(3).gameObject;
        row4 = keys.transform.GetChild(4).gameObject;

        for (int i = 0; i < 10; i++)
        {
            string s = "" + (i);
            CreateKey(s, row0, i);
        }
        char[] row1Keys = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };
        for (int i = 0; i < row1Keys.Length; i++)
        {
            string s = "" + row1Keys[i];
            CreateKey(s, row1, i);
        }

        char[] row2Keys = { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l' };
        for (int i = 0; i < row2Keys.Length; i++)
        {
            string s = "" + row2Keys[i];
            CreateKey(s, row2, i);
        }

        char[] row3Keys = { 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
        for (int i = 0; i < row3Keys.Length; i++)
        {
            string s = "" + row3Keys[i];
            CreateKey(s, row3, i);
        }

        //space
        CreateKey(" ", row4, 0);
    }

    private void Start()
    {
        //this.transform.position = head.transform.position + head.transform.forward * 0.3f;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            inputField.text = "";
        }
    }

    //Handle information from Key here
    public void RegisterInput(string s)
    {
        inputField.text += s;
    }

    private XRKey2 CreateKey(string s, GameObject parent, int position)
    {
        GameObject go = Instantiate(PF_Key, this.transform.position, Quaternion.identity);
        go.transform.SetParent(parent.transform);

        XRKey2 key = go.GetComponent<XRKey2>();
        key.Setup(s, this);
        
        return key;
    }
}
