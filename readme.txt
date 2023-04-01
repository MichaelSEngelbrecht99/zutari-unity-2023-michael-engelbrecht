A build has been prepared, otherwise all the necassary project files and code are avaible in this repository.

I hope you enjoy this submission of my assessment.

Please see code details below
 ||
 ||	
 ||
 \/

======================================================================================================
Task 1:
======================================================================================================
Main Menu Scene
---------> LoadSceneManger.cs
		o LoadSceneWithIndex()
			- Loads scene based on Index
		o LoadSceneWithString()
			- Loads scene based on string
		o ExitApplication()
			- Closes app through Button or when pressing ESC

Level One Scene
---------> PlayerController.cs
		o UpdateRigidValues()
			- Updates text and calls Player Adjust Drag to adjust rigidbody drag
		o PlayerInput()
			- Handles Player input
		o ChangeMaterialColor()
			- Changes Cube color based on input direction
		o PlayerAdjustDrag()
			- Adjusts rigidbody drag
		o AdjustDrag()
			- Increases/Decreases Drag based on Unity event from button
		o AdjustVelocity()
			- Increases/Decreases Player Velocity based on Unity event from button
		o AdjustIncrementSize()
			- Increases/Decreases increment size based on Unity event from button
---------> ScreenWrapPlayer.cs
		o GetScreenConstraints()
			- Gets the constraints of the users position and sets boundries
		o RepositionPlayer()
			- Repositions player if they exceed boundries coordinates
---------> LoadSceneManger.cs
		o LoadSceneWithIndex()
			- Loads scene based on Index
		o LoadSceneWithString()
			- Loads scene based on string
		o ExitApplication()
			- Closes app through Button or when pressing ESC
======================================================================================================
Task 1 - Tested and completed
======================================================================================================



======================================================================================================
Task 2:
======================================================================================================
Weather App Scene
---------> WeatherApp.cs
		o Start()
			- Calls through the API to get information for all the cities and starts coroutine
		o GetRequest(string uri)
			- Gets request based on input string, whihc is the api's website + API key, if successful it will start creating the block
		o CreateWeatherBlock
			- Parses information with JObject and JTokens. Afterwards it convers the JTokens to string, then parse it to an int, 
			then converts from Kelvin to Celcius.
			- Gets the icon from the api and downloads it and sets it
			- Finally, instantiates the block and sets the specific information in the different place holders. 
---------> LoadSceneManger.cs
		o LoadSceneWithIndex()
			- Loads scene based on Index
		o LoadSceneWithString()
			- Loads scene based on string
		o ExitApplication()
			- Closes app through Button or when pressing ESC
======================================================================================================
Task 2 - Tested and completed
======================================================================================================

