using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WebRequestManager : MonoBehaviour, IManager
{
    public Light DirectionalLight;

    private string _url = "https://api.open-meteo.com/v1/forecast?latitude=37.566&longitude=126.9784&current_weather=true";

    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public void Initialize()
    {
        _state = "Web Request Manager initialized..";
        Debug.Log(_state);

        StartCoroutine(FetchData());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    private IEnumerator FetchData()
    {
        UnityWebRequest request = UnityWebRequest.Get(_url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            WeatherData weather = JsonUtility.FromJson<WeatherData>(jsonResponse);
            Debug.Log($"Temp: {weather.current_weather.temperature}, Daytime: {weather.current_weather.is_day}");

            UpdateLighting(weather);
        }
        else
        {
            Debug.Log(request.error);
        }
    }

    private void UpdateLighting(WeatherData weather)
    {
        if (weather.current_weather.is_day == 1)
        {
            DirectionalLight.intensity = weather.current_weather.temperature / 4;
        }
        else
        {
            DirectionalLight.intensity = 0.0f;
        }
    }
}
