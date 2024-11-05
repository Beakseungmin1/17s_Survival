using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemSO itemToGive; // 어떤 아이템을 주는지
    public int quantityPerHit = 1;
    public int capacity; // 몇 번 때려야 하는지

    public void Gether(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0) break;
            capacity--;
            Instantiate(itemToGive.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }

        if (capacity <= 0)
        {
            Destroy(gameObject);
        }
    }
}