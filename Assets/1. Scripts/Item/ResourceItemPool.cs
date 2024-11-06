using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public ResourceType tag;
    public GameObject parent;
    public List<GameObject> prefabs;
    public int size;
}

public class ResourceItemPool : MonoBehaviour
{
    public List<Pool> Pools;
    public Dictionary<ResourceType, Queue<Resource>> PoolDictionary;

    private void Awake()
    {
        PoolDictionary = new Dictionary<ResourceType, Queue<Resource>>();
        foreach (var pool in Pools)
        {
            Queue<Resource> objectPool = new Queue<Resource>();
            pool.size = pool.parent.transform.childCount;
            for (int i = 0; i < pool.size; i++)
            {
                pool.prefabs.Add(pool.parent.transform.GetChild(i).gameObject);
                //objectPool.Enqueue(pool.prefabs[i]);
            }
            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public Resource SpawnFromPool(ResourceType reaourceType)
    {
        if (!PoolDictionary.ContainsKey(reaourceType))
            return null;

        Resource resource = PoolDictionary[reaourceType].Dequeue();
        PoolDictionary[reaourceType].Enqueue(resource);
        resource.gameObject.SetActive(true);
        return resource;
    }
}