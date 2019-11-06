using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!


public class BulletPoolManager : MonoBehaviour
{
 
    [System.Serializable]
    //Class that makes a list of pools so that we can have multiple different objects (factory pattern)
    public class Pool
    {
        //tag of pool
        public string tag;
        //Type of bullet in this pool
        public GameObject prefab;
        //Number of bullets contained in this pool
        public int size;
    }
//Creates the singleton
    #region Singleton

    public static BulletPoolManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion



    //TODO: create a structure to contain a collection of bullets
    //List of pools, list of Bullets made on start
    public List<Pool> pools;

    //Create a dictionary to help save all information of pools
    public Dictionary<string, Queue<GameObject>> poolDict;
    

    // Start is called before the first frame update
    void Start()
    {
        //assign poolDict to a dictionary that contains a name and a pool
        poolDict = new Dictionary<string, Queue<GameObject>>();

        //instansiates all objects from the pools in the game but they are all set as not active.
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject ob = Instantiate(pool.prefab);
                ob.SetActive(false);
                objectPool.Enqueue(ob);
            }

            poolDict.Add(pool.tag, objectPool);
            Debug.Log(pool.tag);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO: modify this function to return a bullet from the Pool
    public GameObject GetBullet(string tag, Vector3 pos, Quaternion rot)
    {
        //removes it from queue so it can't be used again until its readded, sets it to true and passes through a spawn position.
        GameObject bullet = poolDict[tag].Dequeue();   
        bullet.SetActive(true);
        bullet.transform.position = pos;
        bullet.transform.rotation = rot;
        bullet.tag = tag;
        
        return bullet;
    }

    //TODO: modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(GameObject bullet)
    {
        
        bullet.SetActive(false);
        poolDict[bullet.tag].Enqueue(bullet);

    }

}
