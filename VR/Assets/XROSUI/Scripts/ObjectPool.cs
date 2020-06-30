using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool;
    public int activeNum = 0;

    Vector3 initPosition = new Vector3(-3, 2, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    { 
        ObjectPoolerImplement.Ins.RegisterObjectPool(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAmount(int amount)
    {
        amountToPool = amount;
    }

    public void Init(GameObject objectToPool)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            obj.transform.position = initPosition;
            AssignAudio(obj, i);
            //obj.GetComponent<AudioSource>().Play();
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject(ref int objectID)
    {
        if (!IsEmpty())
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    pooledObjects[i].SetActive(true);
                    activeNum++;
                    objectID = i;
                    return pooledObjects[i];
                }
            }
        }
        return null;
    }

    public void AssignAudio(GameObject obj, int id)
    {
        AudioSource audioSource = obj.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Gun");
    }

     
    public void MoveForward(float v1, float v2, float v3)
    {
        if (activeNum != 0)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (pooledObjects[i].activeInHierarchy)
                {
                    pooledObjects[i].transform.Translate(v1, v2, v3);

                }
            }
        }
    }

    public void ReturnPooledObject(GameObject obj)
    {
        if (activeNum != 0)
        {
            obj.transform.position = initPosition;
            obj.SetActive(false);
            activeNum--;
        }
        else
        {
            Debug.Log("Pool is full. Cannot return object");
        }
        //pooledObjects.Add(obj);
    }

    public bool IsEmpty()
    {
        if (activeNum == pooledObjects.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsFull()
    {
        if (activeNum == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
