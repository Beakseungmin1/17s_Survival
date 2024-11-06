using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temperatureUI : MonoBehaviour
{
    public Image Hotimage;
    public Image Coldimage;
    public float FadeSpeed = 0.2f;

    float hotAlpha;
    float coldAlpha;

    private PlayerCondition condition;

    private Coroutine coroutine;

    void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
    }

    private void Update()
    {
        if (condition.Gettemperature() >= 70f)
        {
            FadeIn(Hotimage);
        }
        else if (condition.Gettemperature() <= 30f)
        {
            FadeIn(Coldimage);
        }
        else
        {
            FadeOut(Hotimage);
            FadeOut(Coldimage);
        }
    }

    public void FadeIn(Image image)
    {
        if (image == Hotimage && hotAlpha < 90f / 255f)
        {
            hotAlpha += FadeSpeed * Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, hotAlpha);
        }
        else if (image == Coldimage && coldAlpha < 90f / 255f)
        {
            coldAlpha += FadeSpeed * Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, coldAlpha);
        }
    }

    public void FadeOut(Image image)
    {
        if (image == Hotimage && hotAlpha > 0f)
        {
            hotAlpha -= FadeSpeed * Time.deltaTime;
            hotAlpha = Mathf.Clamp(hotAlpha, 0f, 90f / 255f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, hotAlpha);
        }
        else if (image == Coldimage && coldAlpha > 0f)
        {
            coldAlpha -= FadeSpeed * Time.deltaTime;
            coldAlpha = Mathf.Clamp(coldAlpha, 0f, 90f / 255f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, coldAlpha);
        }
    }
}
