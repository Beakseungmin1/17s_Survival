using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemSO itemToGive; // 어떤 아이템을 주는지
    public int howManyGive; // 아이템 몇개주는지
    public float ResourceHP; // 리소스체력

    public void Gether(float Damage)
    {

        if (ResourceHP <= 0) return;
        ResourceHP -= Damage;

        if (ResourceHP <= 0)
        {
            for (int i = 0; i < howManyGive; i++)
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

                Instantiate(itemToGive.dropPrefab, transform.position + Vector3.up * (i + 2), randomRotation);
            }
            Destroy(gameObject);
        }
    }
}