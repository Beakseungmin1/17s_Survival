using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Condition temperature { get { return uiCondition.temperature; } }

    public bool isDead = false;

    public float noHungerHealthDecay;

    public float temperatureHealthDecay;

    private float lossStaminaWhileRun = 15f;

    public event Action onTakeDamage;

    public GameObject GameOver;

    private void Start()
    {

        GameOver.SetActive(false);
    }

    private void Update()
    {
        // 기본적으로 재생되는 체력과 스태미나
        health.Add(health.passiveValue * Time.deltaTime);
        if (!CharacterManager.Instance.Player.controller.isRun)
        {
            stamina.Add(stamina.passiveValue * Time.deltaTime);
        }


        // 기본적으로 다는 허기와갈증
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

        if (health.curValue <= 0.5f && isDead == false)
        {
            Invoke("Die", 0);
        }

        if (temperature.curValue <= 30f || temperature.curValue >= 70f)
        {
            health.Subtract(temperatureHealthDecay * Time.deltaTime);
        }
    }

    public float Getstamina()
    {
        return stamina.curValue;
    }

    public float Gettemperature()
    {
        return temperature.curValue;
    }

    public void Addtemperature(float value)
    {
        temperature.Add(value);
    }

    public void Subtracttemperature(float value)
    {
        temperature.Subtract(value);
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    // 배고픔을 채우는 함수
    public void EatFood(float amount)
    {
        hunger.Add(amount);
    }

    // 갈증을 채우는 함수
    public void DrinkWater(float amount)
    {
        water.Add(amount);
    }

    public void HealStamina(float amount)
    {
        stamina.Add(amount);
    }

    public void Die()
    {
        GameOver.SetActive(true);
        isDead = true;
    }

    public void TakePhysicalDamage(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

}
