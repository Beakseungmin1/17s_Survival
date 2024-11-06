using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCampFire : MonoBehaviour
{
    public float damage;
    public float damageRate;
    public float temvalue;

    List<IDamagalbe> things = new List<IDamagalbe>();

    void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);

    }

    void DealDamage()
    {
        for(int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterManager.Instance.Player.condition.Addtemperature(temvalue);

        }

        if (other.TryGetComponent(out IDamagalbe damagalbe))
        {
            things.Add(damagalbe);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterManager.Instance.Player.condition.Subtracttemperature(temvalue);

        }

        if (other.TryGetComponent(out IDamagalbe damagable))
        {
            things.Remove(damagable);
        }
    }

}
