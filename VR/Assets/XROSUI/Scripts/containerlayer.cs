using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityEngine.XR.Interaction.Toolkit
{

    public class containerlayer : MonoBehaviour
    {
        int max = 4;
        private float containerObjectRadius=0.05f;
        private int Rmax;
        private int Cmax;
        private int Rlast =0;
        private int Clast =0;
        private float buffery=0.2f;
        private float bufferz=0.3f;
        public List<containerobject> containerobjectlist = new List<containerobject>();
        public void AddObject(GameObject go)
        {
            containerobject co = go.GetComponent<containerobject>(); 
            containerobjectlist.Add(co);
            //co.transform.position = this.transform.position + Vector3.up * 0.5f * containerobjectlist.Count;
            //Place object
            int Ri;
            int Ci;
            print("set position:");
            Ri = Rlast;
            print("Ri: " + Ri);
            Ci = Clast;
            print("Ci: " + Ci); 
            float x = this.transform.position.x + (Ci * buffery) - 0.3f;
            float y = this.transform.position.y + (Ri * bufferz);
            float z = this.transform.position.z;
            co.transform.position = new Vector3(x, y, z);
            co.transform.SetParent(this.transform);
            //Calculate Next Position
            Ci++;
            print("calculate position");
            if (Ci >= Cmax)
            {
                Ci = 0;
                //print("Ci = "+Ci);
                Ri++;
                //print("Ri= " + Ri);
            }
            Rlast = Ri;
            print("Rlast is" + Rlast);
            Clast = Ci;
            print("Clast is" + Clast); 
            if (Clast >= Cmax && Rlast >= Rmax)
            {
                print("is full");
            }
        }
        public bool IsFull()
        {
            print("Is Full: " + (containerobjectlist.Count >= max));
            return containerobjectlist.Count >= max;
        }
        // Start is called before the first frame update
        void Start()
        {
         //x = this.GetComponent<Collider>().bounds.size.x;// PF_layerObject.collider.bounds.size.x;
         //z = this.GetComponent<Collider>().bounds.size.z; //PF_layerObject.collider.bounds.size.z;
         //y = this.GetComponent<Collider>().bounds.size.y;// PF_layerObject.collider.bounds.size.y;
         float a = this.transform.localScale.x;
         float b = this.transform.localScale.y;
         float c = this.transform.localScale.z;
         Cmax = (int)(c / (buffery + (containerObjectRadius * 2)));
            print("Cmax is "+ Cmax);
         Rmax = (int)(b / (bufferz + (containerObjectRadius * 2)));
            print("Rmax is "+ Rmax);
            print(containerObjectRadius);
            //IsFull();
        }
         // Update is called once per frame
        void Update()
        {
        }
    }
}