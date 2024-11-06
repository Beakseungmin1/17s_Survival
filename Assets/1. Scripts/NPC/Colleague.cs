using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleague : MonoBehaviour, Iinteractable
{
    public GameObject colleague;
    public bool isSaveColleague = false;

    public string GetInteractPrompt()
    {
        string str = $"[E] 동료 구출하기";
        return str;
    }

    public void OnInteract()
    {
        SaveColleague();
    }

    public void SaveColleague()
    {
        isSaveColleague = true;

        Destroy(colleague);
        Debug.Log("구출 완료");
    }
}
