# Project Description
ManchesterModel is a Unity project that aims to create a digital twin of the city of Manchester, United Kingdom. The project's current goal is to test what functionality can be implemented in this model ranging from live data to interactability within the model. The project is currently in a prototype phase with unpolished features. The idea is to use these features in the future and create more robust, focused, and polished products within the environment. Future goals for the project include porting it to AR/VR.

Current implemented features are:
- 3D model of Manchester with accurate building height and terrain elevation data.
- Live weather data based on location.
- Location search and teleport.
- 2 modes of movement:
 - Driving around in a car.
 - Fly around as a Camera.
- Dynamic weather conditions and associated skyboxes:
 - Clear
 - Cloudy
 - Rain
 - Snow
- Day/Night Cycle.

# Project Structure
The project has a relatively simple structure with one main scene and a few objects and assets working in tandem to create the functionality.
## Dependencies
The project makes use of several different SDKs, APIs, packages and plugins to implement the functionality.
### Mapbox Unity SDK
The Mapbox Unity SDK is used to generate the 3D map of Manchester. It is downloaded from the Mapbox website at https://www.mapbox.com/unity and imported into the project as a custom asset. The use of the SDK requires a Mapbox account to be created and an access token generated. This is then submitted into the access token field of the Mapbox setup menu.

---
### Mapbox Search Box API
The Mapbox Search Box API is used to get location data given a text input using search requests (see *SearchInputManager* script). The use of this API requires the same access token as the one created for the Mapbox Unity SDK.

---
### OpenWeatherMap API
The OpenWeatherMap API is used to get live weather data from the player's location within the scene (see *WeatherManager* script). This requires an OpenWeatherMap account along with an API key that is generated from the account page.

---
### PROMETEO: Car Controller Package
The PROMETEO package provides car prefabs that are used in the project for driving functionality. It is imported into the project from the package manager after downloading from the Unity Asset store at https://assetstore.unity.com/packages/tools/physics/prometeo-car-controller-209444.

---
### Skyseries Skybox Free Package
The Skyseries package is used for its different skyboxes in the different weather modes. It is imported into the project from the package manager after downloading from the Unity Asset Store at https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633.

---
### SimpleJSON Plugin
The SimpleJSON plugin is used to make parsing of JSON results from the various APIs easier. To use the SimpleJSON script needs to be added to the plugins folder within our asset folder.

## Scenes
There are two scenes in the project:
- PrototypeScene
- EngineeringBuildingAScene
### EngineeringBuildingAScene
This is a placeholder scene used to demonstrate the functionality of "entering" a building. In addition to the default camera and directional light, the scene contains a **ReturnToMapInteract** empty gameobject with a *LeaveToMapInteraction* script component that allows the user to return to the main scene. This is currently a dummy with the idea of being a 3D representation of the building. The functionality of this can be extended to other buildings for future augmentations of the project.

---
### PrototypeScene
This is the main scene of the project. It is made up of a number of different gameobejcts that together incorporate all the functionality of the project.

---
#### IconicBuildingMap
The **IconicBuildingMap** gameobject is one of the major objects in the scene. This is a customizable prefab that is a part of the Mapbox Unity SDK which represents the 3D model of the city of Manchester. The prefab is attached with a *AbstractMap* script which allows for a number of values to be changed to alter the displayed map. 

The centre of the map is set to the coordinates of the Alan Turing building for the purpose of this project and a range of 2 km around this point is displayed in the scene at runtime. *Mapbox Streets* is used as the base image for the map and *Mapbox Terrain* is used as the main terrain source. The Elevation Layer Type is set to *Terrain with Elevation* to give accurate elevation data for the map and a collider is added to this to allow for other objects to interact with the terrain.

A number of different Map Layers are used to add other features to the map. This includes buildings, roads, and life traffic data extracted from several different tilesets, namely *Mapbox Streets v8* and *Mapbox Traffic v1*. The Roads are turned off due to their redundancy for the project, but can easily be turned on from the menu. The different traffic conditions are also turned off due to inconsistencies with live data as well as the uncertainty of its correct functioning. Still, they can be similarly activated from the menu.

The gameobject is also attached with a *SpawnOnMap* script that allows for specified prefabs to be spawned on the map at specified coordinates. Currently inactive, it can be used for future extensions of the project, say for example, in a scavager hunt type game if required.

---
#### PlayerCar
The **PlayerCar** gameobject is a prefab that is a part of the PROMETEO package. It is a player-controllable car that is used to traverse the 3D model at runtime. The prefab is also attached with a *CarController* script which includes the logic for the car movement when in the Drive Mode (accessed by pressing '1'), as well as the logic for switching between modes.

The car object also has an additional **MainCamera** attached to it. This is attached with a *PlayerController* script which lets us move the camera when in the Camera Fly mode (accessed by pressing '2'). The MainCamera includes a **Rainfall** particle system as a child object. This lets us simulate rainfall when activated (done by pressing 'R' or the Rainfall button).

---
#### Canvas
The **Canvas** holds all the UI elements of the scene. It is made up of a number of different panels.
##### Weather Display
The **Weather Display** panel displays live weather data. It includes a number of *TextMeshPro-Text* elements that display different information in real-time.
##### Search Panel
The **Search Panel** allows for user input for the search function. It has a subpanel which holds a Text label, and an Input Field that allows the user the enter the location they want to travel to. The Input field is attached with a *SearchInputManager* script which contains the functionality for searching, which is initiated when the submit key is pressed. 
##### Interact Text
The **Interact Text** is a *TextMeshPro-Text* element that holds the text that the user sees to prompt interaction with elements in the scene.
##### Rainfall Button
The **Rainfall Button** allows us to toggle the rainfall particles on or off. It calls a function on button press that performs this action.

---
#### WeatherManager
The **WeatherManager** is an empty gameobejct that holds the *WeatherManager* script. This script is used to fetch live weather data and controls the Weather Display of the Canvas.

---
#### EventSystem
The **EventSystem** is a UI gameobject that allows the user to interact with the UI elements.

---
#### EngineeringBuildingPin
The **EngineeringBuildingPin** is a gameobject used to mark the Nancy Rothwell Building. This object has two essential child objects. The **EngineeringText** which is a simple text identifier for the building and the **EngineeringBuildingEnterTrigger** which is an empty gameobject with an *InteractionController* script attached to it that allows the user to interact with it to "enter" the building (which simply loads the EngineeringBuildingAScene). To allow for interaction, this object is attached with a collider and a rigidbody component.

---
#### Directional Light
The **Directional Light** is used in the scene to simulate a day/night cycle. It is attached with a *DayCycleManager* script that controls this behaviour.

---
#### Bus
The **Bus** is a 3D cuboid gameobject used as a dummy representation of a bus in the city. It is attached with a *BusMovement* script that controls its motion by interpolating its position between two 3D points. It can be used in a later version of the project with live data to accurately simulate the movement of public transport in the city.

---
#### Plane
The **Plane** is a gameobject used to provide a "ground" for the **PlayerCar** to land on at the beginning of the game. This was used as a fix due to the terrain from the map not loading in time and can be removed if this issue is fixed.

## Scripts
### PlayerController
#### Overview
This Unity C# script manages the player's camera control and environment effects (such as toggling rainfall) for a simulation. The player can switch between two modes: a camera movement mode and a car-following mode. In addition, the script allows toggling of rainfall particle effects and skybox materials.

---

#### Variables

##### Camera Movement Variables
- **_speed**: (Private `float`) The movement speed of the camera in "fly mode". Defaults to 20f.
- **_turnSpeed**: (Private `float`) The speed of camera rotation when the player moves the mouse. Defaults to 2f.
- **yaw**: (Private `float`) The horizontal rotation of the camera, updated by the mouse's X-axis movement.
- **pitch**: (Private `float`) The vertical rotation of the camera, updated by the mouse's Y-axis movement.
- **horizontalInput**: (Private `float`) Input for horizontal movement from the player (A/D or arrow keys).
- **verticalInput**: (Private `float`) Input for vertical movement from the player (W/S or arrow keys).

##### Rainfall Particle System Variables
- **isParticleOn**: (Private `bool`) Tracks whether the rainfall particle system is active or not.
- **rainfall**: (Serialized `ParticleSystem`) The particle system component representing rainfall. Set via the Unity editor.

##### Skybox Materials
- **rainSkybox**: (Serialized `Material`) Skybox material used when it is raining. Set via the Unity editor.
- **clearSkybox**: (Serialized `Material`) Skybox material used when there is no rain. Set via the Unity editor.

##### Player Mode
- **mode**: (Private `int`) Determines the player's control mode:
  - `0`: Car mode (camera follows the car and does not move freely).
  - `1`: Fly mode (camera can be moved freely with inputs).

##### Camera Position Variables for Car Mode
- **carCameraPosition**: (Private `Vector3`) The fixed position of the camera when in car-following mode.
- **carCameraRotation**: (Private `Quaternion`) The fixed rotation of the camera when in car-following mode.

##### Control Keys
- **carMode**: (Private `KeyCode`) Key used to switch to "car mode" (default: `Alpha1`).
- **flyMode**: (Private `KeyCode`) Key used to switch to "fly mode" (default: `Alpha2`).
- **rainKey**: (Private `KeyCode`) Key used to toggle rainfall (default: `R`).

---

#### Methods

##### `Start()`
- This method is called once when the script is initialized.
- **Actions**:
  - Sets the default mode to "car mode" (`mode = 0`).
  - Stops the rainfall particle system at the start.
  - Initializes `isParticleOn` to `false` to indicate that the rain is initially off.

##### `Update()`
- This method is called once per frame.
- **Actions**:
  - **Mode switching**: 
    - If the player presses the `Alpha1` key, it switches to "car mode" (`mode = 0`).
    - If the player presses the `Alpha2` key, it switches to "fly mode" (`mode = 1`).
  
  - **Fly mode camera control**: 
    - If in fly mode (`mode == 1`), the script updates the camera's yaw (horizontal rotation) and pitch (vertical rotation) based on mouse movement.
    - Camera moves horizontally (A/D or arrow keys) and forward/backward (W/S or arrow keys) based on player input.

  - **Car mode camera control**:
    - If in car mode (`mode == 0`), the camera is set to follow the car at a fixed position and rotation (`carCameraPosition` and `carCameraRotation`).

  - **Rainfall toggle**:
    - If the player presses the `R` key, the `RainControl()` method is invoked to toggle the rain effect.

##### `RainControl()`
- This method handles toggling the rainfall particle system and changing the skybox material.
- **Actions**:
  - If rain is currently active (`isParticleOn == true`), it stops the rainfall particle system, sets `isParticleOn` to `false`, and switches to the `clearSkybox`.
  - If rain is inactive (`isParticleOn == false`), it plays the rainfall particle system, sets `isParticleOn` to `true`, and switches to the `rainSkybox`.

---

#### How to Use
1. **Assigning Rain Particle System and Skybox Materials**:
   - In the Unity editor, drag and drop the required particle system for rainfall into the `rainfall` field of the script.
   - Assign the appropriate skybox materials (one for rain and one for clear skies) into the `rainSkybox` and `clearSkybox` fields.

2. **Switching Modes**:
   - Press `1` to enter car-following mode (default).
   - Press `2` to enter camera fly mode, which allows free movement and rotation using keyboard inputs and the mouse.

3. **Toggling Rain**:
   - Press the `R` key to toggle rainfall and switch between the rain and clear skybox.

---

#### Customization
- **Camera speed and turn rate**: Adjust the `_speed` and `_turnSpeed` variables to control the fly mode camera's movement speed and rotation sensitivity.
- **Camera position and rotation in car mode**: Modify the `carCameraPosition` and `carCameraRotation` variables to change the camera's fixed position and rotation when following the car.
- **Control keys**: You can change the `KeyCode` values for switching modes or toggling rain by modifying the `carMode`, `flyMode`, and `rainKey` variables.

### SearchInputManager

#### Overview
This Unity C# script enables searching for a location using the Mapbox Search API and updates the camera position in the game world based on the search results. The script converts real-world latitude and longitude coordinates into game world coordinates and moves the camera (or player) to the searched location.

---

#### Variables

##### API Access Variables
- **accesstoken**: (Private `string`) The access token required for authenticating with the Mapbox Search API. Replace this with your own Mapbox API token.
- **searchapi**: (Private `string`) The base URL for the Mapbox Search API used to retrieve search results.

##### Coordinates Variables
- **startlat**: (Private `float`) The latitude of the starting location at game coordinates (0,0,0). Defaults to `53.46774` (corresponding to a location in Manchester, UK).
- **startlong**: (Private `float`) The longitude of the starting location. Defaults to `-2.23147`.

##### Camera Variable
- **player**: (Private `Camera`) Reference to the main camera object, which is used to update the camera's position based on the search result.

---

#### Methods

##### `Start()`
- **Description**: Called once when the script is initialized.
- **Actions**:
  - Finds and stores the reference to the main camera in the scene by calling `GameObject.FindObjectOfType<Camera>()`.

##### `UpdateCameraLocation(string input)`
- **Description**: Public method that triggers the process of searching for a location and updating the camera's position based on the search input.
- **Parameters**:
  - `input`: (`string`) The search query (e.g., a city name, address, or landmark) entered by the user.
- **Actions**:
  - Calls `StartCoroutine(FetchSearchResults(input))` to perform the search asynchronously and update the camera location based on the search result.

##### `FetchSearchResults(string input)`
- **Description**: A private coroutine that sends a request to the Mapbox Search API and processes the returned data.
- **Parameters**:
  - `input`: (`string`) The search query for which the API request is made.
- **Actions**:
  - Constructs the search URL using the `searchapi` base URL, the `input` string (which is escaped to be URL-safe), and the `accesstoken`.
  - Sends a GET request to the Mapbox Search API and waits for a response.
  - Parses the response data using the SimpleJSON library to extract the latitude and longitude of the searched location.
  - Converts the latitude and longitude into game world coordinates using a linear transformation formula and moves the camera (or player) to the new position.

---

#### Game Coordinate Calculation

In this script, latitude and longitude are mapped to game coordinates using specific linear transformation formulas:

- **Game X-coordinate (cameraX)**:
  - Formula: `cameraX = 768.424729305 * latitude + 8871.81278379 * longitude - 21288.7495634`
  
- **Game Z-coordinate (cameraZ)**:
  - Formula: `cameraZ = 15717.778554 * latitude - 349.283967866 * longitude - 841173.513797`

These formulas allow conversion from real-world coordinates to game world positions.

---

#### How to Use

1. **Assign API Token**:
   - Replace the `accesstoken` variable with your own Mapbox access token to authenticate API requests.

2. **Search and Update Camera**:
   - Call the `UpdateCameraLocation(input)` method, passing a string with the desired location (e.g., "London", "Oxford Street"). The camera will move to the corresponding location in the game world.

3. **Customization**:
   - You can modify the latitude and longitude conversion formula to fit your game’s coordinate system if necessary.

---

#### Dependencies

- **Mapbox Search API**: Used to retrieve geolocation data (latitude and longitude) based on a search query.
- **SimpleJSON**: A lightweight JSON parsing library used to process the response from the API. Make sure to include this library in your Unity project for parsing API responses.

---

#### Error Handling

- **API Request Failure**: The script currently assumes a successful response from the API. Consider adding error handling to manage cases where the API request fails, such as network errors or invalid queries.
- **Location Not Found**: The script assumes the API returns a valid location. You may want to check for cases where the API does not return any results and handle that gracefully.

### WeatherManager

#### Overview
This Unity C# script fetches real-time weather data using the OpenWeatherMap API based on the player's in-game location. It then updates the UI elements to display weather information such as temperature, humidity, pressure, wind speed, and more. The player's game coordinates are converted into latitude and longitude, which are used to query the weather data from the API.

---

#### Variables

##### API Access Variables
- **apiKey**: (Public `string`) The OpenWeatherMap API key used to authenticate and fetch the weather data.
- **currentWeatherApi**: (Public `string`) The base URL for the OpenWeatherMap API, used to build the full request URL for current weather data.

##### UI Elements
- **statusText**: (Public `TextMeshProUGUI`) Displays the status of the API request, such as error messages.
- **location**: (Public `TextMeshProUGUI`) Displays the name of the location corresponding to the fetched weather data.
- **mainWeather**: (Public `TextMeshProUGUI`) Displays the main weather condition (e.g., "Clear", "Clouds").
- **description**: (Public `TextMeshProUGUI`) Displays a short description of the weather condition (e.g., "few clouds", "light rain").
- **temp**: (Public `TextMeshProUGUI`) Displays the current temperature in degrees Celsius.
- **feels_like**: (Public `TextMeshProUGUI`) Displays the perceived temperature (feels like).
- **temp_min**: (Public `TextMeshProUGUI`) Displays the minimum temperature.
- **temp_max**: (Public `TextMeshProUGUI`) Displays the maximum temperature.
- **pressure**: (Public `TextMeshProUGUI`) Displays the atmospheric pressure in Pascals (Pa).
- **humidity**: (Public `TextMeshProUGUI`) Displays the humidity percentage.
- **windspeed**: (Public `TextMeshProUGUI`) Displays the wind speed in kilometers per hour (Km/h).
- **winddirection**: (Public `TextMeshProUGUI`) Displays the wind direction in degrees.

##### Location Variables
- **user**: (Serialized `Camera`) Reference to the player camera, used to determine the player's current position in the game world.
- **startlat**: (Private `float`) The reference latitude of the starting point in the game. Defaults to `53.46774f`.
- **startlong**: (Private `float`) The reference longitude of the starting point in the game. Defaults to `-2.23147f`.

---

#### Methods

##### `FixedUpdate()`
- **Description**: This method is called at fixed intervals and continuously updates the player's location by fetching the corresponding weather data.
- **Actions**:
  - Calls the `FetchLocationData()` method to calculate the player’s latitude and longitude in the game world and fetch weather data based on that location.

##### `FetchLocationData()`
- **Description**: Coroutine that calculates the player's latitude and longitude based on their in-game coordinates.
- **Actions**:
  - Converts the player's in-game X and Z coordinates to real-world latitude and longitude using a linear transformation formula.
  - Calls `UpdateWeatherData(latitude, longitude)` to request weather data based on the calculated latitude and longitude.

##### `UpdateWeatherData(float latitude, float longitude)`
- **Description**: This method initiates the fetching of weather data for the given latitude and longitude.
- **Parameters**:
  - `latitude`: (`float`) The latitude of the player's location.
  - `longitude`: (`float`) The longitude of the player's location.
- **Actions**:
  - Starts the `FetchWeatherDataFromApi(latitude, longitude)` coroutine to fetch weather information from the OpenWeatherMap API.

##### `FetchWeatherDataFromApi(float latitude, float longitude)`
- **Description**: Coroutine that sends an HTTP request to the OpenWeatherMap API to fetch weather data for the given coordinates.
- **Parameters**:
  - `latitude`: (`float`) The latitude of the player's location.
  - `longitude`: (`float`) The longitude of the player's location.
- **Actions**:
  - Constructs the API request URL using the `latitude`, `longitude`, and `apiKey`.
  - Sends the request and waits for the response.
  - If the request fails, it displays the error message in `statusText`.
  - If successful, it parses the response JSON and updates the corresponding UI elements with the weather data.

---

#### Game Coordinate Calculation

The player's in-game X and Z coordinates are converted to real-world latitude and longitude using these linear transformation formulas:
- **Latitude**: `latitude = startlat + 0.0000025 * userX + 0.0000635 * userZ`
- **Longitude**: `longitude = startlong + 0.0001125 * userX - 0.0000055 * userZ`

---

#### How to Use

1. **Assign API Key**:
   - Set the `apiKey` variable with your OpenWeatherMap API key.

2. **UI Setup**:
   - Link the `TextMeshProUGUI` UI elements in the Unity Editor to the corresponding fields (e.g., `statusText`, `location`, `temp`).

3. **Camera Assignment**:
   - Assign the player’s camera to the `user` field in the Unity Editor or via script.

4. **Running the Script**:
   - The script continuously updates weather data based on the player's location every frame (via `FixedUpdate()`), showing the weather data in the UI.

---

#### Dependencies

- **OpenWeatherMap API**: Provides weather data based on geographic coordinates.
- **SimpleJSON**: A lightweight JSON parsing library used to process the response from the API. Ensure the SimpleJSON package is included in your Unity project.

---

#### Error Handling

- **Connection Errors**: If there is a connection error or an issue with the API request, the error message is displayed in the `statusText` UI element.
- **Invalid Coordinates**: The script assumes that valid latitude and longitude coordinates are provided.


### InteractionController

#### Overview
This Unity C# script allows the player to interact with specific objects or areas in the game. When the player is within range of an interactive object, a UI prompt is displayed, and they can press the interaction key (`E` by default) to trigger an action, such as loading a new scene.

---

#### Variables

##### Interaction Control
- **canInteract**: (Private `bool`) A flag that determines whether the player is within the interaction range and can trigger the interaction.

##### UI Elements
- **interactText**: (Public `TextMeshProUGUI`) A UI text element that displays interaction instructions when the player is close to an interactive object.

##### Control Keys
- **interactKey**: (Private `KeyCode`) The key used to trigger the interaction. Set to `KeyCode.E` by default.

---

#### Methods

##### `Update()`
- **Description**: This method is called every frame and checks if the player can interact with an object and if they press the interaction key (`E`). If both conditions are true, the script loads the next scene (scene index `1`).
- **Actions**:
  - If `canInteract` is `true` and the player presses the `interactKey`, it triggers `SceneManager.LoadScene(1)` to load the scene with index `1`.

##### `OnTriggerEnter(Collider other)`
- **Description**: This method is triggered when the player enters the interaction range of an object or area (marked with a trigger collider).
- **Parameters**:
  - `other`: (`Collider`) The collider object that enters the trigger area.
- **Actions**:
  - If the collider belongs to an object tagged "Player", the method sets `canInteract` to `true`, allowing interaction, and displays the interaction prompt by calling `SetInteractText()`.

##### `OnTriggerExit(Collider other)`
- **Description**: This method is triggered when the player exits the interaction range.
- **Parameters**:
  - `other`: (`Collider`) The collider object that exits the trigger area.
- **Actions**:
  - If the collider belongs to an object tagged "Player", the method sets `canInteract` to `false`, disallowing interaction, and hides the interaction prompt by calling `RemoveInteractText()`.

##### `SetInteractText()`
- **Description**: Updates the `interactText` UI element to show the interaction prompt.
- **Actions**:
  - Sets the `interactText` to "Press [E] to Enter" when the player is within range and can interact.

##### `RemoveInteractText()`
- **Description**: Clears the `interactText` UI element to hide the interaction prompt.
- **Actions**:
  - Sets the `interactText` to an empty string, removing the prompt when the player is out of range.

---

#### How to Use

1. **Setup in Unity**:
   - Attach the `InteractionController` script to any object that the player can interact with.
   - Ensure the object has a collider marked as "Is Trigger".
   - The player object must have the tag "Player" to trigger the interactions.
   
2. **UI Setup**:
   - Assign a `TextMeshProUGUI` object to the `interactText` field in the Unity Editor to display the interaction prompt.

3. **Scene Management**:
   - Make sure that the target scene (loaded when the player interacts) is set to index `1` in the Build Settings. You can change the index as needed.

---

#### Notes
- This script assumes the interactive object or area has a trigger collider attached.
- It uses `SceneManager.LoadScene(1)` to load the next scene. If your game requires loading different scenes, modify the scene index as needed.
- The interaction prompt text can be customized in the `SetInteractText()` method.

### LeaveToMapInteraction

#### Overview
The `LeaveToMapInteraction` script allows the player to return to the main map or initial scene by pressing the "Escape" key. When the player presses the `Escape` key, the script triggers a scene change, loading the scene with index `0`.

---

#### Variables

##### Control Key
- **exitInteract**: (Private `KeyCode`) The key used to trigger the scene change. It is set to `KeyCode.Escape` by default, which corresponds to the `Escape` key on the keyboard.

---

#### Methods

##### `Update()`
- **Description**: This method is called every frame and checks if the `Escape` key is pressed. If the key is pressed, it loads the scene with index `0`.
- **Actions**:
  - Detects if the `escapeInteract` key (`Escape`) is pressed using `Input.GetKeyDown(exitInteract)`.
  - If the key is pressed, the method calls `SceneManager.LoadScene(0)` to load the scene with index `0`.

---

#### How to Use

1. **Assign Scene Index**:
   - Ensure that the scene you want to return to (typically the main map or main menu) is set as the first scene in the Build Settings (index `0`).

2. **Setup in Unity**:
   - Attach the `LeaveToMapInteraction` script to any GameObject in the scene where you want the player to be able to return to the main map.
   - The script will automatically listen for the `Escape` key press and load the scene with index `0` when pressed.

---

#### Notes
- The scene index is hardcoded to `0`. If you want to return to a different scene, update the index in `SceneManager.LoadScene(0)` to the appropriate scene index.
- Make sure the target scene (main map or menu) is included in the Unity Build Settings with the correct index.

### DayCycleManager

#### Overview
The `DayCycleManager` script simulates a day-night cycle in a Unity scene by rotating a GameObject (typically a directional light representing the sun) around the X-axis. The rotation speed is determined by the length of a full day in minutes, allowing you to control how fast a day passes in the game.

---

#### Variables

##### Public
- **dayLengthMinutes**: (Public `float`) Specifies the length of a full in-game day in real-world minutes. The default is set to `1440` minutes, which corresponds to 24 real-world hours.

##### Private
- **rotationSpeed**: (Private `float`) The speed at which the object will rotate, calculated in degrees per second based on the `dayLengthMinutes`.

---

#### Methods

##### `Start()`
- **Description**: This method is called once when the script starts. It calculates the `rotationSpeed` of the object based on the length of a full day, in-game.
- **Actions**:
  - The formula for rotation speed is `360 degrees` (a full rotation) divided by the number of seconds in the specified day length (`dayLengthMinutes * 60`).
  - The result is stored in the `rotationSpeed` variable to control how fast the GameObject rotates.

##### `Update()`
- **Description**: This method is called every frame and rotates the GameObject around the X-axis based on the calculated `rotationSpeed`.
- **Actions**:
  - Rotates the GameObject by multiplying the `rotationSpeed` with `Time.deltaTime` to ensure smooth rotation per frame.
  - The rotation occurs along the X-axis (`new Vector3(1, 0, 0)`), simulating the rising and setting of a sun.

---

#### How to Use

1. **Assign to Sun or Light Object**:
   - Attach this script to a directional light (or another GameObject) in your scene that represents the sun or other celestial body.
   
2. **Set Day Length**:
   - Adjust the `dayLengthMinutes` in the Unity Editor to control how long a full day lasts in-game. For example, setting it to `1440` represents a full 24-hour cycle, while a lower number (e.g., `10`) will speed up the day-night cycle.

---

#### Notes
- The script uses a simple rotation mechanism and rotates the GameObject only along the X-axis. This works well for simulating the rising and setting of the sun.
- If you want to adjust the direction of the light (e.g., the sun's path) or add more complex day-night cycles, you may need to modify the axis or rotation logic.

### BusMovement

#### Overview
The `BusMovementScript` simulates the movement of a bus from one location to another within a Unity scene. The bus moves along a path between two world positions, based on latitude and longitude values, and the movement occurs over a specified duration using linear interpolation (`Vector3.Lerp`).

---

#### Variables

##### Location Coordinates
- **latitude**: (Private `float`) Starting latitude of the bus in the real world.
- **longitude**: (Private `float`) Starting longitude of the bus in the real world.
- **busEndlat**: (Private `float`) Ending latitude of the bus in the real world.
- **busEndlong**: (Private `float`) Ending longitude of the bus in the real world.

##### World Coordinates
- **busX**: (Private `float`) The in-game world X coordinate for the starting latitude and longitude of the bus.
- **busZ**: (Private `float`) The in-game world Z coordinate for the starting latitude and longitude of the bus.
- **busEndX**: (Private `float`) The in-game world X coordinate for the ending latitude and longitude of the bus.
- **busEndZ**: (Private `float`) The in-game world Z coordinate for the ending latitude and longitude of the bus.
- **startPosition**: (Private `Vector3`) The starting position of the bus in the game world.
- **endPosition**: (Private `Vector3`) The destination position of the bus in the game world.

##### Time Variables
- **duration**: (Private `float`) The total time (in seconds) for the bus to travel from the start position to the end position. Default is 150 seconds.
- **elapsedTime**: (Private `float`) Tracks the amount of time that has passed since the movement started.

---

#### Methods

##### `Start()`
- **Description**: Called when the script is first executed. It calculates the in-game positions of the bus using latitude and longitude values and converts them into `Vector3` world coordinates.
- **Actions**:
  - The starting and ending bus positions are calculated based on a custom mathematical conversion from latitude and longitude to Unity's world coordinates (`busX`, `busZ` for start and `busEndX`, `busEndZ` for end).
  - The `startPosition` and `endPosition` are initialized as `Vector3` values, which represent the in-game world positions of the bus.

##### `Update()`
- **Description**: This method is called once per frame. It smoothly interpolates the bus's position from its start location to its end location using linear interpolation (`Vector3.Lerp`), over the duration specified.
- **Actions**:
  - Updates the `elapsedTime` using `Time.deltaTime` to track how much time has passed since the bus started moving.
  - Uses `Vector3.Lerp()` to smoothly move the bus between `startPosition` and `endPosition` over the course of `duration` seconds.
  - The bus moves progressively based on the ratio of `elapsedTime / duration`.

---

#### How to Use

1. **Setup in Unity**:
   - Attach this script to a GameObject (representing the bus) in the Unity scene.
   - The script will automatically calculate the starting and ending positions based on real-world latitude and longitude values.

2. **Adjust Movement Speed**:
   - Modify the `duration` variable to control how long it takes for the bus to reach its destination. For example, increasing `duration` will make the bus move more slowly, while decreasing it will make the bus move faster.

---

#### Notes
- The conversion from latitude and longitude to Unity world coordinates uses custom formulas specific to the game setup. If working in a different environment or scale, you may need to adjust these calculations.
- `Vector3.Lerp()` is used for linear interpolation, meaning the bus moves in a straight line between the start and end positions at a constant speed.
- Ensure that the start and end positions (in real-world latitude and longitude) correspond to valid locations in your game world.
