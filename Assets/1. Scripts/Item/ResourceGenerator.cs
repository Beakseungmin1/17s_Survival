using System.Collections;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceItemPool resourceItemPool;
    private Coroutine coroutine;
    public float DELAY;

    private void Awake()
    {
        resourceItemPool = GetComponent<ResourceItemPool>();
    }

    public void RespawnResource(Resource resource)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(RespawnResourceCoroutine(resource));
    }

    public IEnumerator RespawnResourceCoroutine(Resource resource)
    {
        Debug.Log("RespawnResource");
        yield return new WaitForSeconds(DELAY);
        Generate(resource);
        Debug.Log("10ÃÊ Áö³²");
    }
    public void Generate(Resource resource)
    {
        Debug.Log("Success Generate");
        resourceItemPool.PoolDictionary[resource.resourceType].Enqueue(resource);
        Resource newResource = resourceItemPool.SpawnFromPool(resource.resourceType);
        newResource.transform.position = resource.transform.position;
        newResource.resourceHP = resource.initialHP;
    }
}