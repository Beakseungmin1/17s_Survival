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

    void Update()
    {
        float temperature = condition.Gettemperature();

        if (temperature >= 60f)
        {
            hotAlpha = Mathf.Clamp((temperature - 60f) / 30f * (120f / 255f), 0f, 120f / 255f);
        }
        else if (temperature <= 40f)
        {
            coldAlpha = Mathf.Clamp((40f - temperature) / 30f * (120f / 255f), 0f, 120f / 255f);
        }
        else
        {
            hotAlpha = 0f;
            coldAlpha = 0f;
        }

        UpdateImageAlpha(Hotimage, hotAlpha);
        UpdateImageAlpha(Coldimage, coldAlpha);
    }

    private void UpdateImageAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
