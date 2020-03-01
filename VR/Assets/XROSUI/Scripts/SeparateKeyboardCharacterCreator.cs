using UnityEngine;
using UnityEngine.UI; //create public inputfield 

public class SeparateKeyboardCharacterCreator: KeyboardController
{

    public GameObject system;
    public int segments = 10;
    public float xradius = 0.01f;
    public float yradius = 0.01f;
    //Prefab for a 3D key
    public GameObject PF_Key;

    public float startingZ = 0f;
    public float startingY = 0f;
    private void Awake()
    {
        CreatePoints();
        GameObject go = CreateKey(-0.2f, -0.2f, 0, " ");
        Vector3 scale = go.transform.localScale;
        scale.x = 3 * scale.x;
        go.transform.localScale =  scale;

        go = CreateKey(0.2f, -0.2f, 0, " ");
        scale = go.transform.localScale;
        scale.x = 3 * scale.x;
        go.transform.localScale = scale;
    }

    private void Start()
    {
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
        CreateLine(-0.2f, 0.1f +startingY, startingZ, -20f, "qwert");
        CreateLine(0.2f, 0.1f + startingY, startingZ, 22f, "yuiop");
        CreateLine(-0.2f, 0f+ startingY, startingZ, -20f, "asdfg");
        CreateLine(0.2f, 0f + startingY, startingZ, +22f, "hjkl;");
        CreateLine(-0.2f, -0.1f + startingY, startingZ, -20f, "zxcv");
        CreateLine(0.2f, -0.1f + startingY, startingZ, 22f, "bnm,");
    }

    void CreateLine(float offsetX, float offsetY, float offsetZ, float angleOffset,string letters)
    {
        float x;
        float z;

        float angle = 300f - letters.Length * 2f + angleOffset;
        for (int i = 0; i < (letters.Length); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            CreateKey(x + offsetX, offsetY, z + offsetZ, "" + letters[i]);
            //GameObject go = Instantiate(PF_Key, new Vector3(x + offsetX, offsetY, z + offsetZ), new Quaternion(0, 0, 0, 0)); ;
            //go.transform.SetParent(this.transform);
            //XRKey key = go.GetComponent<XRKey>();
            //key.Setup("" + letters[i], this);
            //key.transform.LookAt(system.transform);
            angle += (180f / (letters.Length)) % 360;
        }
    }

    GameObject CreateKey(float x, float y, float z, string s)
    {
        GameObject go = Instantiate(PF_Key, new Vector3(x, y, z), new Quaternion(0, 0, 0, 0)); ;
        go.transform.SetParent(this.transform);
        XRKey key = go.GetComponent<XRKey>();
        key.Setup(s, this);

        return go;
    }
}

