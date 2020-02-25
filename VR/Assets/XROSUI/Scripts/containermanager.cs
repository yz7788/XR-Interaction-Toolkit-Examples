using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityEngine.XR.Interaction.Toolkit
{
    public class containermanager : MonoBehaviour
    {
        public List<containerobject> containerobjectlist = new List<containerobject>();
        public List<containerlayer> containerlayerlist = new List<containerlayer>();
        public GameObject PF_containerObject;
        public GameObject PF_layerObject;
        //Debug only
        GameObject CO;
        void Start()
        {
            //TODO for loop to generate layers
            for (int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(PF_layerObject, this.transform.position + Vector3.forward * 0.5f * i, Quaternion.identity);
                go.transform.SetParent(this.transform);
                containerlayerlist.Add(go.GetComponent<containerlayer>());
            }
        }
        // Update is called once per frame
        void Update()
        {
            CheckDistance();
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddContainerObject();
            }
            //if (Input.GetKeyDown(KeyCode.M))
            //{
            //GameObject controllerhand = GameObject.Find("righthand");
            //print("success find object right hand.");
            //}
        }
        private void Awake()
        {

        }
        public void AddContainerObject()
        {
            //print("hi");
            for (int i = 0; i < containerlayerlist.Count; i++)
            {
                // print("hi2");
                if (!containerlayerlist[i].IsFull())
                {
                    //print("hi3");
                    CO = Instantiate(PF_containerObject, transform.position + transform.forward * 2, Quaternion.identity);
                    containerlayerlist[i].AddObject(CO);
                    return;
                }
            }
        }
        #region check distance between the controller and layer, if<1 disappear 
        public void CheckDistance()
        {
            GameObject controllerhand = GameObject.Find("righthand");
            GameObject disappearlayer = GameObject.Find("ComplexGrabCube");
            float dis;
            dis = Vector3.Distance(controllerhand.transform.position, disappearlayer.transform.position);
            print("dis=" + dis);

            if (dis <= 0.2f)
            {
                containerlayerlist[0].gameObject.SetActive(false);
            }
            else if (dis > 1.0)
            {
                containerlayerlist[0].gameObject.SetActive(true);
            }
        }
        #endregion




    }
}
