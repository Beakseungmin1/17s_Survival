using UnityEditor;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private int WeatherTypeCount;
    public float changeTime;
    private float delay;
    private WeatherState state;

    private PlayerCondition condition;

    [Header("Snow")]
    public GameObject Snow;
    public ParticleSystem SnowParticle;

    [Header("Rain")]
    public GameObject Rain;
    public ParticleSystem RainParticle;

    private void Awake()
    {
        WeatherTypeCount = System.Enum.GetValues(typeof(WeatherState)).Length;
        delay = changeTime;
    }

    private void Start()
    {
        condition = CharacterManager.Instance.Player.condition;

        state = GetRandomWeather();
        PlayWeather();
    }

    private void Update()
    {
        if (delay > 0) delay -= Time.deltaTime;
        else
        {
            delay = changeTime;
            StopWeather();
            state = GetRandomWeather();
            PlayWeather();
        }

        switch (state)
        {
            case WeatherState.Snow:
                condition.Subtracttemperature(1f * Time.deltaTime);
                break;
            case WeatherState.Rain:
                condition.Subtracttemperature(1f * Time.deltaTime);
                break;
        }
    }

    private WeatherState GetRandomWeather()
    {
        return (WeatherState)Random.Range(0, WeatherTypeCount);
    }

    private void PlayWeather()
    {
        switch (state)
        {
            case WeatherState.Snow:
                Snow.gameObject.SetActive(true);
                SnowParticle.Play();
                AudioManager.Instance.PlayMusic(Music.Snow);
                break;
            case WeatherState.Rain:
                Rain.gameObject.SetActive(true);
                RainParticle.Play();
                AudioManager.Instance.PlayMusic(Music.Rain);
                break;
        }
    }

    private void StopWeather()
    {
        switch (state)
        {
            case WeatherState.Snow:
                SnowParticle.Stop();
                Snow.gameObject.SetActive(false);
                AudioManager.Instance.StopMusic(Music.Snow);
                break;
            case WeatherState.Rain:
                RainParticle.Stop();
                Rain.gameObject.SetActive(false);
                AudioManager.Instance.StopMusic(Music.Rain);
                break;
        }
    }
}