using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Camera movement variables
    private float _speed = 20f;
    private float _turnSpeed = 2f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;

    // Rainfall particle system variables
    private bool isParticleOn;
    [SerializeField] ParticleSystem rainfall;

    // Skybox Materials
    [SerializeField] Material rainSkybox;
    [SerializeField] Material clearSkybox;

    // Player mode
    private int mode;

    // Camera position variables for car mode
    private Vector3 carCameraPosition = new Vector3(0f, 3.65f, -7.32f);
    private Quaternion carCameraRotation = new Quaternion(0f, 0f, 0f, 1f);

    // Control Keys
    private KeyCode carMode = KeyCode.Alpha1;
    private KeyCode flyMode = KeyCode.Alpha2;
    private KeyCode rainKey = KeyCode.R;


    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        rainfall.Stop();
        isParticleOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Changing mode
        if (Input.GetKey(carMode))
        {
            mode = 0;
        }
        if (Input.GetKey(flyMode))
        {
            mode = 1;
        }

        // Opening camera controls if in camera mode
        if (mode == 1)
        {
            // Getting cmaera control inputs
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            yaw += _turnSpeed * Input.GetAxis("Mouse X");
            pitch -= _turnSpeed * Input.GetAxis("Mouse Y");

            // Moving and turning camera
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.forward * verticalInput * _speed * Time.deltaTime);
        }
        // Setting camera to follow car and stopping movement
        else
        {
            transform.SetLocalPositionAndRotation(carCameraPosition, carCameraRotation);
        }
        
        // Rainfall Toggle
        if (Input.GetKeyDown(rainKey))
        {
            RainControl();
        }
    }

    // Rainfall toggling method
    public void RainControl()
    {
        if (isParticleOn)
        {
            rainfall.Stop();
            isParticleOn = false;
            RenderSettings.skybox = clearSkybox;

        }
        else
        {
            rainfall.Play();
            isParticleOn = true;
            RenderSettings.skybox = rainSkybox;
        }
    }
}
