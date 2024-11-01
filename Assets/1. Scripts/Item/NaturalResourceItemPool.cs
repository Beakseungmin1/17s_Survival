using System.Collections.Generic;
using UnityEngine;

public class NaturalResourceItemPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public ResourceType tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> Pools;
    public Dictionary<ResourceType, Queue<GameObject>> PoolDictionary;
    public bool hasNaturalResource;

    private void Awake()
    {
        PoolDictionary = new Dictionary<ResourceType, Queue<GameObject>>();
        foreach (var pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(ResourceType tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = PoolDictionary[tag].Dequeue();
        PoolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);
        return obj;
    }
}