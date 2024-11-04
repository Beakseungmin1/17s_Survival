using UnityEngine;

public class NaturalResourceGenerator : MonoBehaviour
{
    private const float DELAY = 5f;
    private float delay = DELAY;
    private int count;
    private NaturalResourceItemPool resourceItemPool;
    private int typeCount;

    private void Awake()
    {
        resourceItemPool = GetComponent<NaturalResourceItemPool>();
        typeCount = System.Enum.GetValues(typeof(ResourceType)).Length;
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

    private void Generate()
    {
        for(int i = 0;i < tag.Length; i++)
        {
            if (!resourceItemPool.pool.hasNaturalResource)
            {
                GameObject newResource = resourceItemPool.SpawnFromPool((ResourceType)i);

                // todo : position change
                newResource.transform.position = new Vector3(Random.Range(0, 1000), 10, Random.Range(0, 1000));
            }
        }
    }
}