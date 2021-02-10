using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeatherCycler: MonoBehaviour
{


    public UnityEvent WeatherTransition;

    private void Start()
    {
        if (WeatherTransition == null)
        {
            WeatherTransition = new UnityEvent();
        }
        WeatherTransition.AddListener(AChangeInTheWeather);

    }

    private float timeSinceWeatherChangeAttempt = 0;
    public float weatherChangeAttemptFrequency;

    public bool weatherShouldChange = false;
    public float percentChanceOfWeatherChange = 5f;

  
    void weatherClock()
    {

        if (timeSinceWeatherChangeAttempt >= weatherChangeAttemptFrequency)
        {
            if (weatherShouldChange)
            {
                WeatherTransition.Invoke();
            }

            timeSinceWeatherChangeAttempt = 0;
        }

        if (Random.Range(0, 100) < percentChanceOfWeatherChange/100 )
        {
            weatherShouldChange = true;
        }
        else 
        { 
            weatherShouldChange = false; 
        }
        timeSinceWeatherChangeAttempt += Time.deltaTime;
    }
    public void AChangeInTheWeather()
    {
        Debug.Log("...hello?");
    }
  
    private void Update()
    {
        weatherClock();
    }
}
