using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeBoat : MonoBehaviour, Iinteractable
{
    public GameObject ending;
    public bool isEnd;

    private void Awake()
    {
        ending.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isEnd)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public string GetInteractPrompt()
    {

        string str = $"[E]≈ª√‚«œ±‚";
        return str;
    }

    public void OnInteract()
    {
        Escape();
    }

    public void Escape()
    {
        ending.SetActive(true);
        isEnd = true;
    }

}
