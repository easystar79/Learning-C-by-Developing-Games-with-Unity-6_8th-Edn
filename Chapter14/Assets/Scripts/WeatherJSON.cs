using UnityEngine;
using System;

[Serializable]
public class WeatherData
{
    public CurrentWeather current_weather;
}

[Serializable]
public class CurrentWeather
{
    public float temperature;
    public int is_day;
}
