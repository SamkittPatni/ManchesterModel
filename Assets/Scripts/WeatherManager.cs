using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
public class WeatherManager : MonoBehaviour
{
    // API access variables
    public string apiKey = "55a826fd349fddb9c082b078d09d4e44";
    public string currentWeatherApi = "api.openweathermap.org/data/2.5/weather?";

    // UI elements
    [Header("UI")]
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI location;
    public TextMeshProUGUI mainWeather;
    public TextMeshProUGUI description;
    public TextMeshProUGUI temp;
    public TextMeshProUGUI feels_like;
    public TextMeshProUGUI temp_min;
    public TextMeshProUGUI temp_max;
    public TextMeshProUGUI pressure;
    public TextMeshProUGUI humidity;
    public TextMeshProUGUI windspeed;
    public TextMeshProUGUI winddirection;

    // Variable to access player location
    [SerializeField] private Camera user;

    // Location variables
    private float startlat = 53.46774f;
    private float startlong = -2.23147f;


    private void FixedUpdate()
    {
        StartCoroutine(FetchLocationData());
    }


    private IEnumerator FetchLocationData()
    {
        // Find player game coordinates
        float userX = user.transform.position.x;
        float userZ = user.transform.position.z;

        // Converting player coodinates into latitude and longitude
        float latitude = (float)(startlat + 0.0000025 * userX + 0.0000635 * userZ);
        float longitude = (float)(startlong + 0.0001125 * userX - 0.0000055 * userZ);

        // Call method to fetch weather data
        UpdateWeatherData(latitude, longitude);

        yield return new WaitForEndOfFrame();

    }


    private void UpdateWeatherData(float latitude, float longitude)
    {
        StartCoroutine(FetchWeatherDataFromApi(latitude, longitude));
    }


    private IEnumerator FetchWeatherDataFromApi(float latitude, float longitude)
    {
        // Creating API URL
        string url = currentWeatherApi + "lat=" + latitude + "&lon=" + longitude + "&appid=" + apiKey + "&units=metric";

        // Sending request to fetch weather data
        UnityWebRequest fetchWeatherRequest = UnityWebRequest.Get(url);
        yield return fetchWeatherRequest.SendWebRequest();

        if (fetchWeatherRequest.result == UnityWebRequest.Result.ConnectionError || fetchWeatherRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            //Check and print error
            statusText.text = fetchWeatherRequest.error;
        }
        else
        {
            // Parsing fetched data
            var response = JSON.Parse(fetchWeatherRequest.downloadHandler.text);

            // Setting UI elements to resepctive data
            location.text = response["name"];
            mainWeather.text = response["weather"][0]["main"];
            description.text = response["weather"][0]["description"];
            temp.text = response["main"]["temp"] + " C";
            feels_like.text = "Feels like:" + response["main"]["feels_like"] + " C";
            temp_min.text = "Min:" + response["main"]["temp_min"] + " C";
            temp_max.text = "Max:" + response["main"]["temp_max"] + " C";
            pressure.text = "Pressure:" + response["main"]["pressure"] + " Pa";
            humidity.text = "Humidity:" + response["main"]["humidity"] + " %";
            windspeed.text = "Wind:" + response["wind"]["speed"] + " Km/h";
            winddirection.text = "Wind Degrees:" + response["wind"]["deg"] + " degrees";
        }
    }
}