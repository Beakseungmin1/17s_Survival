using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemSO itemToGive; // � �������� �ִ���
    public int quantityPerHit = 1;
    public int capacity; // �� �� ������ �ϴ���

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