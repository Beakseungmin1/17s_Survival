using UnityEditor;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private int WeatherTypeCount;
    private static readonly float CHANGETIME = 30.0f;
    private float delay = CHANGETIME;
    private WeatherState state;

    [Header("Snow")]
    public GameObject Snow;
    public ParticleSystem SnowParticle;

    [Header("Rain")]
    public GameObject Rain;
    public ParticleSystem RainParticle;

    private void Awake()
    {
        WeatherTypeCount = System.Enum.GetValues(typeof(WeatherState)).Length;
    }

    private void Start()
    {
        state = GetRandomWeather();
        PlayWeather();
    }

    private void Update()
    {
        if (delay > 0) delay -= Time.deltaTime;
        else
        {
            delay = CHANGETIME;
            StopWeather();
            state = GetRandomWeather();
            PlayWeather();
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
                break;
            case WeatherState.Rain:
                Rain.gameObject.SetActive(true);
                RainParticle.Play();
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
                break;
            case WeatherState.Rain:
                RainParticle.Stop();
                Rain.gameObject.SetActive(false);
                break;
        }
    }
}