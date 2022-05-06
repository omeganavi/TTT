using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;


public class AppManager : MonoBehaviour
{
    public float appRefresh;
    private float timer;
    public Text currentWeatherText, tempText, currentTimeText;
    public WeatherStates weatherController;
    public IPGrabber ip;
    private void Start()
    {
        timer = appRefresh;
        
    }

    public IEnumerator GetWeather()
    {
        var weatherAPI = new UnityWebRequest("https://api.openweathermap.org/data/2.5/weather?q="+ip.ipCity+","+ip.ipCountry+"&units=metric&APPID=747e931eefd6573cdaa373526addaa81")
        {
            downloadHandler = new DownloadHandlerBuffer()
        };
        yield return weatherAPI.SendWebRequest();

        if(weatherAPI.isNetworkError || weatherAPI.isHttpError)
        {
            print("Fail getting data");
            yield break;
        }

        JSONNode weatherInfo = JSON.Parse(weatherAPI.downloadHandler.text);
        
        currentWeatherText.text = "Temperatura: " + weatherInfo["weather"][0]["description"];
        tempText.text = "Tiempo: " + Mathf.Floor(weatherInfo["main"][0]) + "°C";
        


        if (weatherInfo["weather"][0]["icon"] == "01d")
        {
            weatherController.ClearDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "01n")
        {
            weatherController.ClearDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "02d")
        {
            weatherController.CloudsDayBroken();
            weatherController.ClearDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "02n")
        {
            weatherController.CloudsDayBroken();
        }
        else if (weatherInfo["weather"][0]["icon"] == "03d")
        {
            weatherController.CloudsDayBroken();
        }
        else if (weatherInfo["weather"][0]["icon"] == "03n")
        {
            weatherController.CloudsDayBroken();
        }
        else if (weatherInfo["weather"][0]["icon"] == "10d")
        {
            weatherController.RainDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "10n")
        {
            weatherController.RainDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "09n")
        {
            weatherController.CloudsDayBroken();
            weatherController.RainDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "09d")
        {
            weatherController.CloudsDayBroken();
            weatherController.RainDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "50d")
        {
            
            weatherController.MistDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "50n")
        {
            weatherController.MistDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "13d")
        {
            weatherController.SnowDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "13n")
        {
            weatherController.SnowDay();
        }
        else if (weatherInfo["weather"][0]["icon"] == "02d")
        {
            weatherController.CloudsDayBroken();
        }
        else if (weatherInfo["weather"][0]["icon"] == "02n")
        {
            weatherController.CloudsDayBroken();
        }
        else if (weatherInfo["weather"][0]["icon"] == "04d")
        {
            weatherController.CloudsDayBroken();
        }
        else if (weatherInfo["weather"][0]["icon"] == "04n")
        {
            weatherController.CloudsDayBroken();
        }

        print(weatherInfo["weather"][0]["description"]);
        print(weatherInfo["weather"][0]["icon"]);
        print(weatherInfo["weather"][0]["main"]);


    }

    private void Update()
    {
        currentTimeText.text = "Current time: "+DateTime.Now.ToString(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
        timer -= Time.deltaTime;


        if(timer <= 0)
        {
            StopCoroutine("GetWeather");
            StartCoroutine("GetWeather");
            print("App Refresh");
            
            timer = appRefresh;
        }
    }
}
