using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temperatureUI : MonoBehaviour
{
    public Image Hotimage;
    public Image Coldimage;
    public float flashSpeed;

    float totalAlpha;

    private PlayerCondition condition;

    private Coroutine coroutine;

    void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
    }

    private void Update()
    {
        if (condition.Gettemperature() >= 60f)
        {
            FadeIn(Hotimage);
        }
        else if (condition.Gettemperature() <= 40f)
        {
            FadeIn(Coldimage);
        }
    }

    public void FadeIn(Image image)
    {
        if (totalAlpha < 90f / 255f)
        {
            totalAlpha += 0.2f * Time.deltaTime;
            image.color = new Color(255f / 255f, 132f / 255f, 57f / 255f, totalAlpha);
        }
    }

    //public void Flash()
    //{
    //    if (coroutine != null)
    //    {
    //        StopCoroutine(coroutine);
    //    }


    //    image.enabled = true;
    //    image.color = new Color(1f, 100f / 255f, 100f / 255f);
    //    coroutine = StartCoroutine(FadeAway());
    //}

    //private IEnumerator FadeAway()
    //{
    //    float startAlpha = 0.3f;
    //    float a = startAlpha;

    //    while (a > 0)
    //    {
    //        a -= (startAlpha / flashSpeed) * Time.deltaTime;
    //        image.color = new Color(1f, 100f / 255f, 100f / 255f, a);
    //        yield return null;
    //    }

    //    image.enabled = false;
    //}
}
