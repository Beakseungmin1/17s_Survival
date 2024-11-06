using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NearFire : MonoBehaviour
{
    public float temvalue;
    public float sectemvalue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterManager.Instance.Player.condition.Addtemperature(temvalue);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    CharacterManager.Instance.Player.condition.Subtracttemperature(temvalue);

        //}

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterManager.Instance.Player.condition.Addtemperature(sectemvalue * Time.deltaTime);

        }
    }
}
