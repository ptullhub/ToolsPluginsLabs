using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Networking;
using static UnityEditor.Progress;

public class WeatherManager: MonoBehaviour
{
    [HideInInspector]
    public int arrayIdx = 0;
    [NonSerialized]
    public string[] cityName = new string[] { "Orlando", "Sacramento", "Madrid", "Moscow", "Liverpool" };

    private string xmlApi;
    private XmlDocument xmlDoc;

    public Material sunnySkybox;
    public Material rainySkybox;
    public Material sunriseSkybox;
    public Material sunsetSkybox;

    public Light sun;
    private void Start()
    {
        xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=" + cityName[arrayIdx] + ",us&mode=xml&appid=36c052901f0f0112c65e2757fdf98793";
        Debug.Log(xmlApi);

        StartCoroutine(GetWeatherXML(OnXMLDataLoaded));
    }
    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=" + cityName[arrayIdx] + ",us&mode=xml&appid=36c052901f0f0112c65e2757fdf98793";
        Debug.Log(xmlApi);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"Network problem: {request.error}");
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Response error: {request.responseCode}");
            }
            else
            {
                callback(request.downloadHandler.text);
                string xmlData = request.downloadHandler.text;

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlData);

                int cloudCoverage = int.Parse(xmlDoc.SelectSingleNode("//clouds").Attributes["value"].Value);
                float lightIntensity = Mathf.Lerp(0.2f, 1.0f, 1 - cloudCoverage / 100f);

                sun.intensity = lightIntensity;

                Debug.Log($"Cloud Coverage: {cloudCoverage}% - Setting light intensity to: {lightIntensity}");

                string sunriseStr = xmlDoc.SelectSingleNode("//sun").Attributes["rise"].Value;
                string sunsetStr = xmlDoc.SelectSingleNode("//sun").Attributes["set"].Value;

                DateTime sunrise = DateTime.Parse(sunriseStr);
                DateTime sunset = DateTime.Parse(sunsetStr);
                DateTime currentTime = DateTime.UtcNow;

                if (currentTime >= sunrise.AddHours(-1) && currentTime <= sunrise.AddHours(1))
                {
                    Debug.Log("It's around sunrise.");
                    SetSkybox(sunriseSkybox);
                }
                else if (currentTime >= sunset.AddHours(-1) && currentTime <= sunset.AddHours(1))
                {
                    Debug.Log("It's around sunset.");
                    SetSkybox(sunsetSkybox);
                }
                else
                {
                    string weather = xmlDoc.SelectSingleNode("//weather").Attributes["value"].Value;
                    if (weather.ToLower().Contains("rain"))
                    {
                        Debug.Log("It's rainy.");
                        SetSkybox(rainySkybox);
                    }
                    else
                    {
                        Debug.Log("It's not rainy.");
                        SetSkybox(sunnySkybox);
                    }
                }
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, callback);
    }

    public void OnXMLDataLoaded(string data)
    {
        Debug.Log(data);
    }

    private void ParseXMLDocument()
    {
        string cityName = xmlDoc.SelectSingleNode("//city").Attributes["name"].Value;
        string country = xmlDoc.SelectSingleNode("//country").InnerText;
        string timezone = xmlDoc.SelectSingleNode("//timezone").InnerText;

        string temperature = xmlDoc.SelectSingleNode("//temperature").Attributes["value"].Value;
        string minTemp = xmlDoc.SelectSingleNode("//temperature").Attributes["min"].Value;
        string maxTemp = xmlDoc.SelectSingleNode("//temperature").Attributes["max"].Value;

        string weather = xmlDoc.SelectSingleNode("//weather").Attributes["value"].Value;
        string weatherIcon = xmlDoc.SelectSingleNode("//weather").Attributes["icon"].Value;

        string sunrise = xmlDoc.SelectSingleNode("//sun").Attributes["rise"].Value;
        string sunset = xmlDoc.SelectSingleNode("//sun").Attributes["set"].Value;
    }
    private void SetSkybox(Material skybox)
    {
        UnityEngine.RenderSettings.skybox = skybox;
    }

}