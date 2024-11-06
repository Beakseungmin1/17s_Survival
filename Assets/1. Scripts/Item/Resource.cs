using System;
using System.Collections;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemSO itemToGive; // 어떤 아이템을 주는지
    public ResourceType resourceType;
    public int howManyGive; // 아이템 몇개주는지
    public float resourceHP; // 리소스체력
    public float initialHP;
    private ResourceGenerator ResourceGenerator;


    private void Awake()
    {
        ResourceGenerator = GetComponentInParent<ResourceGenerator>();
        resourceHP = initialHP;
    }

    public void Gether(float Damage)
    {

        if (resourceHP <= 0) return;
        resourceHP -= Damage;

        if (resourceHP <= 0)
        {
            for (int i = 0; i < howManyGive; i++)
            {
                Quaternion randomRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 0);

                Instantiate(itemToGive.dropPrefab, transform.position + Vector3.up * (i + 2), randomRotation);
            }
            ResourceGenerator.RespawnResource(this);
            gameObject.SetActive(false);
        }
    }
}