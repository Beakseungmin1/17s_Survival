using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemSO itemToGive; // � �������� �ִ���
    public int howManyGive; // ������ ��ִ���
    public float ResourceHP; // ���ҽ�ü��

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