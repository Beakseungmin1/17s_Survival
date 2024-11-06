using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iinteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, Iinteractable
{
    public ItemSO Data;

    private Camera _camera;

    private Dictionary<WeaponType, ResourceType> matchingTypeDict = new Dictionary<WeaponType, ResourceType>
    {
        { WeaponType.Axe, ResourceType.Tree },
        { WeaponType.Pickax, ResourceType.Rock }
    };


    private void Start()
    {
        if (Data is WeaponSO weapon)
        {
            _camera = Camera.main;
        }
    }

    public string GetInteractPrompt()
    {
        string str = $"{Data.itemName}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = Data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }

    public void OnHit()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Damage and range are given to each weapon
        // Damage and resource damage are different values
        if (Data is WeaponSO weapon)
        {

            if (Physics.Raycast(ray, out hit, weapon.AttackDistance))
            {
                if (weapon.itemType == ItemType.Tool && hit.collider.TryGetComponent(out Resource resource))
                {
                    if (matchingTypeDict[weapon.WeaponType] == resource.resourceType)
                    {
                        resource.Gether(weapon.ResourceDamage);
                    }
                }

                if (hit.collider.TryGetComponent(out NPC npc))
                {
                    npc.TakePhysicalDamage(weapon.Damage);
                }
            }
        }
    }
}
