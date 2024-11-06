using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private Iinteractable curInteractable;

    public TextMeshProUGUI prompText;
    private Camera camera;

    public Inventory inventory;

    void Start()
    {
        camera = Camera.main;

        inventory = FindObjectOfType<Inventory>();
    }


    void Update()
    {
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<Iinteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                prompText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        prompText.gameObject.SetActive(true);
        prompText.text = curInteractable.GetInteractPrompt();
    }

    public void OninteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            inventory.AcquireItem(CharacterManager.Instance.Player.itemData);
            curInteractGameObject = null;
            curInteractable = null;
            prompText.gameObject.SetActive(false);
        }
    }

}
