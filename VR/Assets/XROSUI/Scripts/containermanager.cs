using System;
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
        public GameObject startpoint;
        public GameObject controllerhand_Left;
        public GameObject controllerhand_Right;
        public float lefthandvalue;
        public float righthandvalue;
        //GameObject Container_Cube;
        private float value = 0;
        //Debug only
        GameObject CO;
        //
        //
        public Transform planeTarget;
        public Transform target;
        void Start()
        {
            controllerhand_Right = GameObject.Find("RightDirectController");
            controllerhand_Left = GameObject.Find("LeftDirectController");
            //TODO for loop to generate layers
            for (int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(PF_layerObject, this.transform.position + Vector3.back * 0.15f * i, Quaternion.identity);
                value += 0.2f;
                //print("The " + i + " layer value is " + value);
                go.transform.SetParent(this.transform);
                containerlayer co = go.GetComponent<containerlayer>();
                co.layervalue = value;
                containerlayerlist.Add(co);
            }
        }
        // Update is called once per frame
        void Update()
        {  
            AddContainerObject();
            CheckDistanceForEach();
            //CheckDistanceForRight();
            //GetDistanceToPoint();
            //CheckValue();
            //CheckAngle();
            //CheckDistance();
            if (Input.GetKeyDown(KeyCode.K))
            {
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                print("Containerforward" + this.transform.forward);
            }  
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
        #region  CheckDistanceForEach
        public void CheckDistanceForEach()
        {
            lefthandvalue = Vector3.Distance(controllerhand_Left.transform.position, startpoint.transform.position);
            righthandvalue = Vector3.Distance(controllerhand_Right.transform.position, startpoint.transform.position);
            //Dev.Log("righthandvalue"+ righthandvalue);
            float totalvalueleft;
            float totalvalueright;
            totalvalueleft = lefthandvalue - 0.1f;
            //Dev.Log("total value" + totalvalue);
            totalvalueright = righthandvalue - 0.1f;
            //Dev.Log("total value" + totalvalue);
            for (int i = 0; i <containerlayerlist.Count; i++)
            {
                if (totalvalueleft <= containerlayerlist[i].layervalue)
                {
                    containerlayerlist[i].gameObject.SetActive(false);
                }
                else if (totalvalueright <= containerlayerlist[i].layervalue)
                {
                    containerlayerlist[i].gameObject.SetActive(false);
                }
                else if (totalvalueleft > containerlayerlist[i].layervalue)
                {
                    containerlayerlist[i].gameObject.SetActive(true);
                }
                else if (totalvalueright > containerlayerlist[i].layervalue)
                {
                    containerlayerlist[i].gameObject.SetActive(true);
                }
            }
            //slidervalue from 0 to 0.8
            //layervalue 0.2 0.4 0.6
        }
        //public float GetDistanceToPoint()
        //{
        //    Plane plane= new Plane(planeTarget.up, planeTarget.position);
        //    float distance = plane.GetDistanceToPoint(target.position);
        //    print("distance:" + distance);
        //    //return distance;
        //}
        #endregion
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
            if (dis >= 0.2f && dis <= 0.25f)
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

