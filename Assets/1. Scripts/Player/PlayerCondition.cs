using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagalbe
{
    void TakePhysicalDamage(float damage);

}




public class PlayerCondition : MonoBehaviour, IDamagalbe
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition water { get { return uiCondition.water; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    private float lossStaminaWhileRun = 15f;

    public event Action onTakeDamage;

    private void Update()
    {
        // �⺻������ ����Ǵ� ü�°� ���¹̳�
        health.Add(health.passiveValue * Time.deltaTime);
        if (!CharacterManager.Instance.Player.controller.isRun)
        {
            stamina.Add(stamina.passiveValue * Time.deltaTime);
        }


        // �⺻������ �ٴ� ���Ͱ���
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        water.Subtract(water.passiveValue * Time.deltaTime);


        if (hunger.curValue <= 0f || water.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (CharacterManager.Instance.Player.controller.isRun)
        {
            stamina.Subtract(lossStaminaWhileRun * Time.deltaTime);
        }

        if (health.curValue <= 0f)
        {
            Die();
        }

    }

    public float Getstamina()
    {
        return stamina.curValue;
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

    public void TakePhysicalDamage(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

}
