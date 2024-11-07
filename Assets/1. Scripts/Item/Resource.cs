using System;
using System.Collections;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemSO itemToGive; // � �������� �ִ���
    public ResourceType resourceType;
    public int howManyGive; // ������ ��ִ���
    public float resourceHP; // ���ҽ�ü��
    public float initialHP;
    public float delay;

    private Renderer[] resourceRenderer;
    private Collider resourceCollider;

    private void Awake()
    {
        resourceHP = initialHP;
        resourceRenderer = gameObject.GetComponentsInChildren<Renderer>();
        if(gameObject.TryGetComponent<Collider>(out Collider collider)) resourceCollider = collider;
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
            ResourceDeactivation();
            RespawnResource();
        }
    }

    private void RespawnResource()
    {
        StartCoroutine(RespawnResourceCoroutine());
    }

    private IEnumerator RespawnResourceCoroutine()
    {
        yield return new WaitForSeconds(delay);
        ResourceActivation();
    }

    private void ResourceDeactivation()
    {
        foreach (Renderer renderer in resourceRenderer)
        {
            renderer.enabled = false;
        }
        resourceCollider.enabled = false;
    }

    private void ResourceActivation()
    {
        resourceHP = initialHP;
        foreach (Renderer renderer in resourceRenderer)
        {
            renderer.enabled = true;
        }
        resourceCollider.enabled = true;
    }
}