using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{
    public Equip curEquip;
    public Transform equipParent;

    private PlayerController controller;
    private PlayerCondition condition;

    Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    public void EquipNew(ItemSO data)
    {
        UnEquip();
        curEquip = Instantiate(data.EquipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;

        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curEquip != null && controller.canLook)
        {
            curEquip.OnAttackInput();

            if (curEquip is EquipTool equip)
            {
                if (equip.doesItFood)
                {
                    condition.EatFood(equip.increasedFoodValue);
                    condition.DrinkWater(equip.increasedWaterValue);
                    //������ ������ ������� �͵� �߰��� ����
                }
                else if (equip.doesItBuilding)
                {
                    //���ش��� �������ּž��� �޼ҵ�
                }
            }
        }
    }
}
