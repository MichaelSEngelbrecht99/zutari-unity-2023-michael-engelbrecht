using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using TMPro;

public class WeatherApp : MonoBehaviour
{
    [Header("Weather Block Prefab & Instantiate Transform")]
    [Header("====================")]
    public GameObject weatherBlock;
    public Transform weatherBlocksParent;
    public GameObject downloadDataText;

    // API KEY: c0327b4636ec2866cc7cbc56402d300c
    void Start()
    {
        // API gets calls based on Geo Coordinates

        // 1 - Calls details for Bhisho, Eastern Cape GEO COORDS [-32.8472, 27.4422]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-32.8472&lon=27.4422&appid=c0327b4636ec2866cc7cbc56402d300c"));

        // 2 - Calls details for Bloemfontein, Free State GEO COORDS [-29.1211, 26.214]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-29.1211&lon=26.214&appid=c0327b4636ec2866cc7cbc56402d300c"));

        // 3 - Calls details for Johannesburg, Gauteng GEO COORDS [-26.2023, 28.0436]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-26.2023&lon=28.0436&appid=c0327b4636ec2866cc7cbc56402d300c"));

        // 4 - Calls details for Pietermaritzburg, KwaZulu-Natal GEO COORDS [-29.6168, 30.3928]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-29.6168&lon=30.3928&appid=c0327b4636ec2866cc7cbc56402d300c"));

        // 5 - Calls details for Polokwane, Limpopo GEO COORDS [-23.9045, 29.4688]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-23.9045&lon=29.4688&appid=c0327b4636ec2866cc7cbc56402d300c"));

        // 6 - Calls details for Mbombela, Mpumalanga GEO COORDS [-25.4745, 30.9703]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-25.4745&lon=30.9703&appid=c0327b4636ec2866cc7cbc56402d300c"));

        // 7 - Calls details for Kimberley, Northern Cape GEO COORDS [-28.7323, 24.7623]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-28.7323&lon=24.7623&appid=c0327b4636ec2866cc7cbc56402d300c"));

        // 8 - Calls details for Mahikeng, North West GEO COORDS [-25.8652, 25.6442]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-25.8652&lon=25.6442&appid=c0327b4636ec2866cc7cbc56402d300c"));

        // 9 - Calls details for Cape Town, Western Cape GEO COORDS [-33.9258, 18.4232]
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=-33.9258&lon=18.4232&appid=c0327b4636ec2866cc7cbc56402d300c"));
        downloadDataText.SetActive(false);  
    }

    #region Gets And Sets Weather Information to a block
    // Requests API call through APPID and web request
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
                    StartCoroutine(CreateWeatherBlock(webRequest.downloadHandler.text));
                    break;
            }
        }
    }

    // Creates the Weather Block by getting the appropriate information from the API and sets the info upon instantiation
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



        // Converts from string to lowest integer, and converts from Kelvin to Celcius
        string _temperature = temperature.ToString().Substring(0, 3);
        int _temperatureInt = Convert.ToInt32(_temperature.ToString());
        _temperatureInt = _temperatureInt - 273;

        string _temperatureFeel = feelsLikeTemperature.ToString().Substring(0, 3);
        int _temperatureFeelInt = Convert.ToInt32(_temperatureFeel.ToString());
        _temperatureFeelInt = _temperatureFeelInt - 273;

        string _temperatureMin = minTemperature.ToString().Substring(0, 3);
        int _temperatureMinInt = Convert.ToInt32(_temperatureMin.ToString());
        _temperatureMinInt = _temperatureMinInt - 273;

        string _temperatureMax = maxTemperature.ToString().Substring(0, 3);
        int _temperatureMaxInt = Convert.ToInt32(_temperatureMax.ToString());
        _temperatureMaxInt = _temperatureMaxInt - 273;




        // Gets the icon code
        string iconCode = weatherIconId.ToString();

        // Constructs the URL for the weather icon
        string iconUrl = $"http://openweathermap.org/img/wn/{iconCode}.png";
        WWW www = new WWW(iconUrl);
        yield return www;
        Texture2D texture = new Texture2D(www.texture.width, www.texture.height);
        www.LoadImageIntoTexture(texture);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);




        // Instantiates block and places it in Scroll View Content
        GameObject cityWeatherBlock = Instantiate(weatherBlock, weatherBlocksParent);
        cityWeatherBlock.name = cityName.ToString();
        cityWeatherBlock.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cityName.ToString();
        cityWeatherBlock.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        cityWeatherBlock.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = String.Format("{0}{1}", _temperatureInt.ToString(), "°C");
        cityWeatherBlock.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = weatherDescription.ToString();
        cityWeatherBlock.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = String.Format("MIN {0}°C | MAX {1}°C", _temperatureMinInt.ToString(), _temperatureMaxInt.ToString());
        cityWeatherBlock.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = String.Format("Feels like: {0}{1}", _temperatureFeelInt.ToString(), "°C");
    }
    #endregion
}
