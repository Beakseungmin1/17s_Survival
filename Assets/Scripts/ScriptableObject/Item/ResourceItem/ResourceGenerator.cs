using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class ResourceGenerator : MonoBehaviour
{
    private const float DELAY = 5f;
    private float delay = DELAY;
    private int count;
    private ResourceItemPool resourceItemPool;
    private int typeCount;

    private const float DESTROYTIME = 6f;
    private float destroyTime = DESTROYTIME;

    private void Awake()
    {
        resourceItemPool = GetComponent<ResourceItemPool>();
        typeCount = System.Enum.GetValues(typeof(ResourceType)).Length;
    }

    private void Generate()
    {
        int randomTag = Random.Range(0, typeCount);
        GameObject newResource = resourceItemPool.SpawnFromPool((ResourceType)randomTag);
        // TODO : width, height 구하여 전체 맵에 적절히 설정하기
        newResource.transform.position = new Vector3(Random.Range(0, 1000), 10, Random.Range(0, 1000));
    }

    private void Update()
    {
        if (delay > 0) delay -= Time.deltaTime;
        else
        {
            delay = DELAY;
            Generate();
        }

        if (destroyTime > 0) destroyTime -= Time.deltaTime;
        else
        {
            destroyTime = DESTROYTIME;
            int randomTag = Random.Range(0, typeCount);
            GameObject obj = resourceItemPool.PoolDictionary[(ResourceType)randomTag].Dequeue();
            obj.SetActive(false);
            resourceItemPool.PoolDictionary[(ResourceType)randomTag].Enqueue(obj);
        }
    }
}