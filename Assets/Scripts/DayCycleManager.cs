using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float dayLengthMinutes = 1440f;

    float rotationSpeed;
    void Start()
    {
        rotationSpeed = 360 / dayLengthMinutes / 60;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
    }
}
