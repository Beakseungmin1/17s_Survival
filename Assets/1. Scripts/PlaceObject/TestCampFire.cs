using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCampFire : MonoBehaviour
{
    public float damage;
    public float damageRate;

    List<IDamagalbe> things = new List<IDamagalbe>();

    void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);

    }

    void Update()
    {
        
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
        if(other.TryGetComponent(out IDamagalbe damagalbe))
        {
            things.Add(damagalbe);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IDamagalbe damagable))
        {
            things.Remove(damagable);
        }
    }


}
