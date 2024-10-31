using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition water { get { return uiCondition.water; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    private void Update()
    {
        // �⺻������ ����Ǵ� ü�°� ���¹̳�
        health.Add(health.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);


        // �⺻������ �ٴ� ���Ͱ���
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        water.Subtract(water.passiveValue * Time.deltaTime);


        if (hunger.curValue <= 0f || water.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0f)
        {
            Die();
        }

    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    // ������� ä��� �Լ�
    public void EatFood(float amount)
    {
        hunger.Add(amount);
    }

    // ������ ä��� �Լ�
    public void DrinkWater(float amount)
    {
        water.Add(amount);
    }




    public void Die()
    {
        Debug.Log("����");
    }
}
