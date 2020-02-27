using UnityEngine;
using UnityEngine.UI; //create public inputfield 
public class SeparateKeyboardCharacterCreator: KeyboardController
{
    public int segments = 10;
    public float xradius = 0.01f;
    public float yradius = 0.01f;
    //Prefab for a 3D key
    public GameObject PF_Key;


    private void Awake()
    {
    }

    private void Start()
    {
        CreatePoints();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            inputField.text = "";
        }
    }

    private XRKey CreateKey(string s, GameObject parent, int position)
    {
        GameObject go = Instantiate(PF_Key, this.transform.position, Quaternion.identity);
        go.transform.SetParent(parent.transform);

        XRKey key = go.GetComponent<XRKey>();
        key.Setup(s, this);
        
        return key;
    }

    void CreatePoints()
    {
        CreateLine(-0.25f, 0.3f +2f, -8f, "qwert");
        CreateLine(0.25f, 0.3f + 2f, -8f, "yuiop");
        CreateLine(-0.25f, 0.2f+2f, -8f, "asdfg");
        CreateLine(0.25f, 0.2f + 2f, -8f, "hjkl;");
        CreateLine(-0.25f, 0.1f + 2f, -8f, "zxcv");
        CreateLine(0.25f, 0.1f + 2f, -8f, "bnm,");
    }

    void CreateLine(float offsetX, float offsetY, float offsetZ, string letters)
    {
        float x;
        float z;

        float angle = 300f - letters.Length * 2f;
        float tempAngle = angle;
        for (int i = 0; i < (letters.Length); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * tempAngle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * tempAngle) * yradius;
            GameObject go = Instantiate(PF_Key, new Vector3(x + offsetX, offsetY, z + offsetZ), new Quaternion(0, 0, 0, 0)); ;
            go.transform.SetParent(this.transform);
            XRKey key = go.GetComponent<XRKey>();
            key.Setup("" + letters[i], this);
            tempAngle += (180f / (letters.Length)) % 360;
        }
    }
}
