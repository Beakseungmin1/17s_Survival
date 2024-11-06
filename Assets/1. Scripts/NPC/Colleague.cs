using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleague : MonoBehaviour, Iinteractable
{
    public GameObject colleague;
    public bool isSaveColleague = false;
    public Transform escapePosition;

    public GameObject boat;

    private void Start()
    {
        boat.SetActive(false);
    }

    public string GetInteractPrompt()
    {
        if (isSaveColleague)
        {
            string str = $"�������� ���������� �󸥳����ڰ�";
            return str;
        }
        else
        {
            string str = $"[E] ���� �����ϱ�";
            return str;
        }
    }

    public void OnInteract()
    {
        SaveColleague();
    }

    public void SaveColleague()
    {
        isSaveColleague = true;

        transform.position = escapePosition.position;
        transform.rotation = Quaternion.Euler(0, 270f, 0);
        boat.SetActive(true);
        //Destroy(colleague);
        Debug.Log("���� �Ϸ�");
    }
}
