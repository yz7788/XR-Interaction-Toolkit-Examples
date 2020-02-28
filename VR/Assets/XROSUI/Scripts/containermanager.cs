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
        GameObject Container_Cube;
        private float value=0;
        //Debug only
        GameObject CO;
        void Start()
        {
            //TODO for loop to generate layers
            for (int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(PF_layerObject, this.transform.position + Vector3.back * 0.2f * i, Quaternion.identity);
                value += 0.2f;
                print("The " + i + " layer value is " + value);
                go.transform.SetParent(this.transform);
                containerlayerlist.Add(go.GetComponent<containerlayer>());
            }
        }
        // Update is called once per frame
        void Update()
        {
            //CheckAngle();
            //CheckDistance();
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddContainerObject();
            } 
            //if (Input.GetKeyDown(KeyCode.M))
            //{
            //}
            if (Input.GetKeyDown(KeyCode.L))
            {
                print("Containerforward" + this.transform.forward);
            }
            CheckValue();
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
        //public void CheckDistance()
        //{
        //    GameObject controllerhand = GameObject.Find("righthand");
        //    float ContainerDepth;
        //    Container_Cube = GameObject.Find("Container_Cube");
        //    ContainerDepth = Container_Cube.GetComponent<Collider>().bounds.size.x;
        //    print("ContainerDepth is" + ContainerDepth);
        //    float dis2;
        //    //dis2 = Vector3.Distance(controllerhand.transform.position, );

        //    //for (float i=0;i<ContainerDepth;i++) 
        //    //{ 
        //    //}



        //    GameObject disappearlayer = GameObject.Find("Container_Cube");
        //    float dis;
        //    dis = Vector3.Distance(controllerhand.transform.position, disappearlayer.transform.position);
        //    // print("dis=" + dis);

        //    if (dis <= 0.2f)
        //    {
        //        containerlayerlist[0].gameObject.SetActive(false);
        //    }
        //    else if (dis > 1.0)
        //    {
        //        containerlayerlist[0].gameObject.SetActive(true);
        //    }
        //}
        #endregion

        #region check value
        public void CheckValue()
        {
            GameObject controllerhand = GameObject.Find("righthand");
            GameObject endpoint = GameObject.Find("EndPoint");
            float dis;
            dis = Vector3.Distance(controllerhand.transform.position, endpoint.transform.position);
            print("dis is" + dis);
            if (dis >= 0.4f && dis <= 0.45f)
            {
                containerlayerlist[0].gameObject.SetActive(false);
            }
            else if (dis > 0.5)
            {
                containerlayerlist[0].gameObject.SetActive(true);
            }
            if(dis >= 0.2f && dis <= 0.25f)
            {
                containerlayerlist[1].gameObject.SetActive(false);
            }
            else if (dis > 0.3)
            {
                containerlayerlist[1].gameObject.SetActive(true);
            }
            if (dis >= 0 && dis <= 0.12)
            {
                containerlayerlist[2].gameObject.SetActive(false);
            }
            else if (dis > 0.1)
            {
                containerlayerlist[2].gameObject.SetActive(true);
            } 
        }
            #endregion


            //public void CheckDistance()
            //{
            //    GameObject controllerhand = GameObject.Find("righthand");
            //    GameObject endpoint = GameObject.Find("EndPoint");
            //    float dis;

            //    dis = Vector3.Distance(controllerhand.transform.position, endpoint.transform.position);
            //    print("dis is" + dis);


            //    if (dis <= 0.5f)
            //    {

            //        containerlayerlist[2].gameObject.SetActive(false);
            //    }
            //    else if (dis > 0.8f)
            //    {
            //        containerlayerlist[2].gameObject.SetActive(true);
            //    }
            //for (int i = 0; i <= containerlayerlist.Count; i++)
            //{
            //    if (dis <= 0.1)
            //    {
            //        containerlayerlist[i].gameObject.SetActive(false);
            //    }
            //    else if (dis > 0.3)
            //    {
            //        containerlayerlist[i].gameObject.SetActive(true);
            //    }
            //}

            // }

            #region check angel between the cube and controller
            //public void CheckAngle() 
            //{ 
            //    GameObject controllerhand = GameObject.Find("righthand");
            //    //print("Containerforward" + this.transform.forward);
            //    print("test: " +Vector3.Dot(this.transform.forward, controllerhand.transform.forward));
            //}
            #endregion


        }
}
