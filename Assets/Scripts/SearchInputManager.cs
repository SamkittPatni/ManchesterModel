using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class SearchInputManager : MonoBehaviour
{
    // API access variables
    string accesstoken = "pk.eyJ1Ijoic2Fta2l0dCIsImEiOiJjbHl4NTczaDAxeGpyMnFzNnVsOHE3dHViIn0.k6sNwb7VLjZGDtHTO47tSQ";
    string searchapi = "https://api.mapbox.com/search/searchbox/v1";

    // Latitude and logitude at game coordinates (0,0,0)
    private float startlat = 53.46774f;
    private float startlong = -2.23147f;

    private Camera player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Camera>();
    }


    // Method called to update camera location on search
    public void UpdateCameraLocation(string input)
    { 
        StartCoroutine(FetchSearchResults(input));
    }

    private IEnumerator FetchSearchResults(string input)
    {
        // Creating API URL
        string searchURL = searchapi + "/forward?q=" + UnityWebRequest.EscapeURL(input) + "&limit=1&country=GB&access_token=" + accesstoken;

        // Sending request to fetch location data
        UnityWebRequest searchRequest = UnityWebRequest.Get(searchURL);
        yield return searchRequest.SendWebRequest();

        // Parsing fetched data
        var response = JSON.Parse(searchRequest.downloadHandler.text);
        
        // Extracting latitude and logitude of searched location
        float latitude = response["features"][0]["properties"]["coordinates"]["latitude"];
        float longitude = response["features"][0]["properties"]["coordinates"]["longitude"];

        // Calculating game coordinates based on latitude and longitude
        float cameraX = 768.424729305f * latitude + 8871.81278379f * longitude - 21288.7495634f;
        float cameraZ = 15717.778554f * latitude - 349.283967866f * longitude - 841173.513797f;

        // Moving player to the location
        player.transform.position = new Vector3(cameraX, 10f, cameraZ);

    }
}
