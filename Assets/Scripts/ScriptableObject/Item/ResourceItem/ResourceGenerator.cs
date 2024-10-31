using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class ResourceGenerator : MonoBehaviour
{
    public GameObject[] resourcePrefab;
    private const float DELAY = 10f;
    private float delay = DELAY;
    private int count;
    private ResourceItemPool resourceItemPool;
    private int typeCount;

    private void Awake()
    {
        resourceItemPool = GetComponent<ResourceItemPool>();
        typeCount = System.Enum.GetValues(typeof(ResourceType)).Length;
    }

    private void Generate()
    {
        int resourceIndex = Random.Range(0, resourcePrefab.Length);
        int type = Random.Range(0, typeCount);
        GameObject newResource = resourceItemPool.SpawnFromPool((ResourceType)type);
        // TODO : width, height 구하여 전체 맵에 적절히 설정하기
        newResource.transform.position = new Vector2(Random.Range(0, 1920), Random.Range(0, 1080));
    }

    private void Update()
    {
        if (delay > 0) delay -= Time.deltaTime;
        else
        {
            delay = DELAY;
            Generate();
        }
    }
}