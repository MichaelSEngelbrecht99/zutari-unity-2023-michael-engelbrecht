using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using UnityEngine.UI;

public class WeatherApp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject weatherBlock;
    public Image weatherImage;

    // API KEY: c0327b4636ec2866cc7cbc56402d300c
    void Start()
    {
        // 1 - Calls details for Bhisho, Eastern Cape GEO COORDS [-32.8472, 27.4422]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-32.8472&lon=27.4422&appid=c0327b4636ec2866cc7cbc56402d300c"));

        //// 2 - Calls details for Bloemfontein, Free State GEO COORDS [-29.1211, 26.214]
        //StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-29.1211&lon=26.214&appid=c0327b4636ec2866cc7cbc56402d300c"));

        //// 3 - Calls details for Johannesburg, Gauteng GEO COORDS [-26.2023, 28.0436]
        //StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-26.2023&lon=28.0436&appid=c0327b4636ec2866cc7cbc56402d300c"));

        //// 4 - Calls details for Pietermaritzburg, KwaZulu-Natal GEO COORDS [-29.6168, 30.3928]
        //StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-29.6168&lon=30.3928&appid=c0327b4636ec2866cc7cbc56402d300c"));

        //// 5 - Calls details for Polokwane, Limpopo GEO COORDS [-23.9045, 29.4688]
        //StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-23.9045&lon=29.4688&appid=c0327b4636ec2866cc7cbc56402d300c"));

        //// 6 - Calls details for Mbombela, Mpumalanga GEO COORDS [-25.4745, 30.9703]
        //StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-25.4745&lon=30.9703&appid=c0327b4636ec2866cc7cbc56402d300c"));

        //// 7 - Calls details for Kimberley, Northern Cape GEO COORDS [-28.7323, 24.7623]
        //StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-28.7323&lon=24.7623&appid=c0327b4636ec2866cc7cbc56402d300c"));

        //// 8 - Calls details for Mahikeng, North West GEO COORDS [-25.8652, 25.6442]
        //StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-25.8652&lon=25.6442&appid=c0327b4636ec2866cc7cbc56402d300c"));

        //// 9 - Calls details for Cape Town, Western Cape GEO COORDS [-33.9258, 18.4232]
        //StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-33.9258&lon=18.4232&appid=c0327b4636ec2866cc7cbc56402d300c"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(webRequest.downloadHandler.text);
                    StartCoroutine(CreateWeatherBlock(webRequest.downloadHandler.text));
                    break;
            }
        }
    }

    public IEnumerator CreateWeatherBlock(string _weatherDetails)
    {
        // Parse the JSON response and extract the relevant information
        JObject weatherData = JObject.Parse(_weatherDetails);
        JToken weatherDescription = weatherData["weather"][0]["description"];
        JToken weatherIconId = weatherData["weather"][0]["icon"];
        JToken temperature = weatherData["main"]["temp"];
        JToken feelsLikeTemperature = weatherData["main"]["feels_like"];
        JToken minTemperature = weatherData["main"]["temp_min"];
        JToken maxTemperature = weatherData["main"]["temp_max"];
        JToken humidity = weatherData["main"]["humidity"];
        JToken windSpeed = weatherData["wind"]["speed"];
        JToken windDirection = weatherData["wind"]["deg"];
        JToken cityName = weatherData["name"];

        // Convert to double, rounds up to the nearest integer, and converts from Kelvin to Celcius
        double tempDouble = double.Parse(temperature.ToString().Replace(",", ".")); 
        int _temperature = ((int)Math.Ceiling(tempDouble) - 273); 

        //double tempFeelDouble = double.Parse(feelsLikeTemperature.ToString().Replace(",", "."));
        //int _temperatureFeel = ((int)Math.Ceiling(tempFeelDouble) - 273);

        //double tempMinDouble = double.Parse(minTemperature.ToString().Replace(",", "."));
        //int _temperatureMin = ((int)Math.Ceiling(tempMinDouble) - 273); 

        //double tempMaxDouble = double.Parse(maxTemperature.ToString().Replace(",", "."));
        //int _temperatureMax = ((int)Math.Ceiling(tempMaxDouble) - 273);

        // Gets the icon code
        string iconCode = weatherIconId.ToString();

        // Constructs the URL for the weather icon
        string iconUrl = $"http://openweathermap.org/img/wn/{iconCode}.png";
        WWW www = new WWW(iconUrl);
        yield return www;
        Texture2D texture = new Texture2D(www.texture.width, www.texture.height);
        www.LoadImageIntoTexture(texture);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        weatherImage.sprite = sprite;

        Debug.Log($"Weather in {cityName}:");
        Debug.Log($"Description: {weatherDescription}");
        Debug.Log($"Temperature: {_temperature}°C");
        //Debug.Log($"Feels like: {_temperatureFeel}°C");
        //Debug.Log($"Min: {_temperatureMin}°C");
        //Debug.Log($"Max: {_temperatureMax}°C");
        Debug.Log($"Humidity: {humidity}%");
        Debug.Log($"Wind speed: {windSpeed} m/s");
        Debug.Log($"Wind direction: {windDirection}°");
    }
}
