using UnityEngine;
using UnityEngine.UI; //create public inputfield 
public class SeparateKeyboardCharacterCreator: KeyboardController
{
    public GameObject system;
    public int segments = 10;
    public float xradius;
    public float yradius;
    public float smallXradius; //radius for first and last rows of keyboard
    public float smallYradius;
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
        CreateCurvedLine(smallXradius, smallYradius, -0.2f, 0.3f + 2f, -0.05f + -8f, -20f, "qwert");
        CreateCurvedLine(smallXradius, smallYradius, 0.2f, 0.3f + 2f, -0.05f + -8f, 22f, "yuiop");
        CreateCurvedLine(xradius, yradius,-0.2f, 0.2f+2f, -8f, -20f, "asdfg");
        CreateCurvedLine(xradius, yradius, 0.2f, 0.2f + 2f, -8f,+22f, "hjkl;");
        CreateCurvedLine(smallXradius, smallYradius, -0.2f, 0.1f + 2f, -0.05f + -8f, -20f, "zxcv");
        CreateCurvedLine(smallXradius, smallYradius, 0.2f, 0.1f + 2f, -0.05f + -8f, 22f, "bnm,");
    }


    void CreateCurvedLine(float xradius, float yradius, float offsetX, float offsetY, float offsetZ, float angleOffset,string letters)
    {
        float x;
        float z;

        float angle = 300f - letters.Length * 2f + angleOffset;
        for (int i = 0; i < (letters.Length); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            print("x, y, z: " + (x + offsetX) + " " + offsetY + " " + (z + offsetZ));
            GameObject go = Instantiate(PF_Key, new Vector3(x + offsetX, offsetY, z + offsetZ), new Quaternion(0, 0, 0, 0)); ;
            go.transform.SetParent(this.transform);
            XRKey key = go.GetComponent<XRKey>();
            key.Setup("" + letters[i], this);
            //key.transform.LookAt(system.transform);
            angle += (180f / (letters.Length)) % 360;
        }
    }
}
