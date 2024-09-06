using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMovementScript : MonoBehaviour
{
    // Bus starting location
    float latitude = 53.464398f;
    float longitude = -2.231818f;

    // Bus ending location
    float busEndlat = 53.476146f;
    float busEndlong = -2.242516f;

    // Variables to hold ingame bus locations 
    float busX, busZ;
    float busEndX, busEndZ;
    Vector3 startPosition;
    Vector3 endPosition;

    // Bus movement time control variables
    float duration = 150f;
    float elapsedTime;


    void Start()
    {
        // Calculating bus world location and converting into a vector3
        busX = 768.424729305f * latitude + 8871.81278379f * longitude - 21288.7495634f;
        busZ = 15717.778554f * latitude - 349.283967866f * longitude - 841173.513797f;
        startPosition = new Vector3(-25.07894f, 0.5f, -14.18474f);

        // Calculating bus end world location and converting into a vector3
        busEndX = 768.424729305f * busEndlat + 8871.81278379f * busEndlong - 21288.7495634f;
        busEndZ = 15717.778554f * busEndlat - 349.283967866f * busEndlong - 841173.513797f;
        endPosition = new Vector3(-106.0143f, 0.5f, 136.7282f);
    }

    
    void Update()
    {
        // Interpolate the bus position between start and end positions
        elapsedTime += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / duration));
    }
}
