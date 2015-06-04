using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	// GameObject and Vector3 variables to be used
	// for gameview initialization
	public Vector3 robotValues = new Vector3 (0f, 0.5f, 4f);
	public Vector3 startValues = Vector3.zero;
	public Vector3 obstacleValues = new Vector3 (0f, -1f, 2f);
	public Vector3 basketValues = new Vector3 (0f, -0.65f, 2f);
	public Vector3 appleValues = new Vector3 (3.2f, 1f, 2f);
	public GameObject robot;
	public GameObject obstacle;
	public GameObject basket;
	public GameObject apple;
	public GameObject youWin;
	public Vector3 numStartValues;

	// GameObject and Vector3 variables to be used
	// for actionList initialization and updates
	public Vector3 listStartValues = new Vector3 (-8.5f, 3.5f, 0f);
	public Vector3 pressPlayValues = new Vector3 (-8f, -5f, 0f);
	public Vector3 pressStopValues = new Vector3 (-9f, -5f, 0f);
	public int speed = 2;

	public GameObject listMoveLeft;
	public GameObject listMoveRight;
	public GameObject listJump;
	public GameObject listPickApple;
	public GameObject listPickNewApple;
	public GameObject listNotPickApple;
	public GameObject listAppleToBasket;
	public GameObject listThrowApple;
	public GameObject listEmptyBasket;
	public GameObject pressPlay;
	public GameObject pressStop;

	// GameObject to be used for the robot gameplay
	private GameObject robotCl;
	private GameObject obstaCl;
	private GameObject baskeCl;

	// Int variables containing the obstacle and start-current points of the robot
	private int randStart;
	private int randObstacle;
	private int randBasket;
	private int currPoint;
	private int prevPoint;
	private int nextPoint;
	private float midPoint;
	// Variables to be used for real-time checking
	private int playMode;	// Not playing ~ 0, playing ~ 1
	private int direction;	// Left ~ -1, stopped ~ 0, right ~ 1
	private int moving;		// Not moving ~ 0, moving ~ 1
	private int jumping;	// Not jumping ~ 0, ascending ~ 1, mid-stop ~ 2, descending ~ 3, on-spot ~ 4/5
	private int holding;	// Number of apples held
	private int throwing;	// Number of apples throwing
	private int basketed;	// Number of apples in the basket

	// Initialization
	// Generally the robot will start from a random point (block) on the left of the screen
	// Counting begins from the first complete block, so the whole image contains 19 blocks
	// with the tree being on the 16th and the obstacle between 10-12
	// Blocks in scene x-values: 1 ~ 6.4 | 2 ~ 5.7 | 3 ~ 5 | 4 ~ 4.3 | etc
	// Apples shall be instantiated in blocks 15, 16, 17
	void Start () {
		// Calculation of start and finish values
		randStart = Random.Range (9, 11);
		randObstacle = Random.Range (11, 13);
		randBasket = Random.Range (13, 15);
		currPoint = randStart;
		prevPoint = currPoint;
		nextPoint = currPoint;
		playMode = 0;
		direction = 0;
		moving = 0;
		jumping = 0;
		holding = 0;
		throwing = 0;
		basketed = 0;
		// Initialize robot and start-finish lines
		InitializeTutorial (randStart, currPoint, randObstacle);

	}

	// Update is called once per frame
	// movs contains the sequence of moves entered -- from left to right q,w,e,r,t,y,u,i
	public string movs = "";
	public float nextClick = 0.0f;
	public float clickRate = 0.25f;
	void Update () {
		string pressTag = "errPress";
		// Get mouse input
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				string hitTag = hit.transform.tag;
				// Clicks won't work when in play-mode
				if (playMode == 0) {
					// Handle click on the control panel icons -- to add actions on the actionList
					if ((hitTag == "stepleft") && (Time.time > nextClick)) {
						movs = movs + "q";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "stepright") && (Time.time > nextClick)) {
						movs = movs + "w";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "stepjump") && (Time.time > nextClick)) {
						movs = movs + "e";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "applepick") && (Time.time > nextClick)) {
						movs = movs + "r";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "applenotpick") && (Time.time > nextClick)) {
						movs = movs + "t";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "applethrow") && (Time.time > nextClick)) {
						movs = movs + "y";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "appletobasket") && (Time.time > nextClick)) {
						movs = movs + "u";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "appleemptybasket") && (Time.time > nextClick)) {
						movs = movs + "i";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}

					// Handle click on the actionList -- to remove actions from the actionList
					// ADDSTUFF
					if ((hitTag == "action01") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						// Calculate new actionList (movs) -- update actionList
						movs = DeleteFromActionList (movs, 1);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action02") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 2);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action03") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 3);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action04") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 4);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action05") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 5);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action06") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 6);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action07") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 7);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action08") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 8);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action09") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 9);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action10") && (Time.time > nextClick)){
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 10);
						EnqueueToActionList (movs);
					}

					// Enter play mode -- click on Play button
					if ((hitTag == "butPlay") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						currPoint = randStart;
						// Update Play-button
						pressTag = "pressdPlay";
						Vector3 pressPosition = new Vector3 (pressPlayValues.x, pressPlayValues.y, pressPlayValues.z);
						Quaternion pressRotation = Quaternion.identity;
						GameObject pressButton = Instantiate (pressPlay, pressPosition, pressRotation) as GameObject;
						pressButton.gameObject.tag = pressTag;
						// Update Stop-button -- in theory there should be only one item
						// tagged pressdStop, but search for multiple entries just in case
						GameObject[] prevList;
						prevList = GameObject.FindGameObjectsWithTag ("pressdStop");
						for (int i = 0; i < prevList.Length; i++){
							Destroy (prevList[i]);
						}
						/*// Delete apples from previous plays
					prevList = GameObject.FindGameObjectsWithTag ("apple");
					for (int i = 0; i < prevList.Length; i++) {
						Destroy (prevList [i]);
					}*/
						// Actually enter play mode
						playMode = 1;
						print (movs);
						StartCoroutine (MoveRobot (movs, currPoint, randObstacle));
					}
				}
				// Stop play mode -- click on Stop button
				// ADD: Clear previous scene-items -- on GameController of ExcSeven too
				if ((hitTag == "butStop") && (Time.time > nextClick)) {
					nextClick = Time.time + clickRate;
					// Update Stop-button
					pressTag = "pressdStop";
					Vector3 pressPosition = new Vector3 (pressStopValues.x, pressStopValues.y, pressStopValues.z);
					Quaternion pressRotation = Quaternion.identity;
					GameObject pressButton = Instantiate (pressStop, pressPosition, pressRotation) as GameObject;
					pressButton.gameObject.tag = pressTag;
					// Update Play-button -- in theory there should be only one item
					// tagged pressdPlay, but search for multiple entries just in case
					GameObject[] prevList;
					prevList = GameObject.FindGameObjectsWithTag ("pressdPlay");
					for (int i = 0; i < prevList.Length; i++){
						Destroy (prevList[i]);
					}
					// Re-initialize robot
					playMode = 0;
					InitializeTutorial (randStart, randStart, randObstacle);
				}
			}
		}
		// ADDSTUFF -- Real-time gameplay
		if (playMode == 1) {
			if (moving == 1) {
				// Not jumping
				if (jumping == 0) {
					print ("Moveit!");
					RunRobot (prevPoint, currPoint, direction, speed);
					if (direction == 1) {
						// For some reason checking with currPoint doesn't work -- currPoint keeps value from one step earlier in Update
						//if (nearlyEqual(robotCl.transform.position.x, ValueX(currPoint), 0.05f) || (robotCl.transform.position.x > ValueX(currPoint))) {
						if (nearlyEqual(robotCl.transform.position.x, ValueX(nextPoint), 0.05f) || (robotCl.transform.position.x > ValueX(nextPoint))) {
							moving = 0;
							jumping = 0;
							print (currPoint);
							print (robotCl.transform.position.x);
						}
					}
					if (direction == -1) {
						if (nearlyEqual(robotCl.transform.position.x, ValueX(nextPoint), 0.05f) || (robotCl.transform.position.x < ValueX(nextPoint))) {
							moving = 0;
							jumping = 0;
							print (currPoint);
							print (robotCl.transform.position.x);
						}
					}
				}
				// Jumping
				else {
					if (jumping == 1) {
						print ("Jumpdafukup!");
						RunRobot (prevPoint, currPoint, direction, speed);
						if (direction == 1) {
							if ((nearlyEqual(robotCl.transform.position.x, ValueX(midPoint), 0.05f)) || (robotCl.transform.position.x > ValueX(midPoint))) {
								print (robotCl.transform.position.x);
								jumping = 2;
							}
						}
						if (direction == -1) {
							if ((nearlyEqual(robotCl.transform.position.x, ValueX(midPoint), 0.05f)) || (robotCl.transform.position.x < ValueX(midPoint))) {
								print (robotCl.transform.position.x);
								jumping = 2;
							}
						}
					}
					if (jumping == 2) {
						print ("Hold it!");
						print (robotCl.transform.position.x);
						RunRobot (prevPoint, currPoint, direction, speed);
						jumping = 3;
					}
					if (jumping == 3) {
						print ("Get back down!");
						RunRobot (prevPoint, currPoint, direction, speed);
						if (direction == 1) {
							if (nearlyEqual(robotCl.transform.position.x, ValueX(nextPoint), 0.05f) || (robotCl.transform.position.x > ValueX(nextPoint))) {
								jumping = 0;
							}
						}
						if (direction == -1) {
							if (nearlyEqual(robotCl.transform.position.x, ValueX(nextPoint), 0.05f) || (robotCl.transform.position.x < ValueX(nextPoint))) {
								jumping = 0;
							}
						}
					}
				}
			}
		}
	}

	// Inititalization function
	void InitializeTutorial (int randStart, int currPoint, int randObstacle) {
		// Variables to be used for actual x-position
		float actualStart = ValueX (randStart);
		float actualObstacle = ValueX (randObstacle);
		float actualBasket = ValueX (randBasket);

		// Variables for tags and apple-objects
		GameObject appleCl;
		string robotTag = "player";
		string appleTag = "apple";
		string obstaTag = "obstacle";
		string baskeTag = "basket";

		// Delete robot, apple and obstacle copies from previous plays
		// Must delete only if there is at least one pressdPlay-tagged object
		GameObject[] prevList;
		prevList = GameObject.FindGameObjectsWithTag ("pressdPlay");
		if (prevList.Length > 0) {
			GameObject[] prevList2;
			prevList2 = GameObject.FindGameObjectsWithTag ("player");
			for (int i = 0; i < prevList2.Length; i++) {
				Destroy (prevList2[i]);
			}
			prevList2 = GameObject.FindGameObjectsWithTag ("obstacle");
			for (int i = 0; i < prevList2.Length; i++) {
				Destroy (prevList2[i]);
			}
			prevList2 = GameObject.FindGameObjectsWithTag ("apple");
			for (int i = 0; i < prevList2.Length; i++) {
				Destroy (prevList2[i]);
			}
		}

		// Initialize poisition-rotation for the obstacle and the robot
		Vector3 robotStart = new Vector3 (actualStart, robotValues.y, robotValues.z);
		Vector3 obstaclePosition = new Vector3 (actualObstacle, obstacleValues.y, obstacleValues.z);
		Vector3 basketPosition = new Vector3 (actualBasket, basketValues.y, basketValues.z);
		Quaternion robotRotation = Quaternion.identity;
		Quaternion obstacleRotation = Quaternion.identity;
		Quaternion basketRotation = Quaternion.identity;

		// Initialize and instantiate apples
		for (int i = 15; i <= 17; i++) {
			float appleStart = (i - 1)*0.7f - 6.4f;
			float randXNoise = Random.Range (-0.1f, 0.1f);
			float randYNoise = Random.Range (-0.2f, 0.2f);
			Vector3 applePosition = new Vector3 (appleStart + randXNoise, appleValues.y + randYNoise, appleValues.z);
			Quaternion appleRotation = Quaternion.identity;
			appleCl = Instantiate (apple, applePosition, appleRotation) as GameObject;
			appleCl.gameObject.tag = appleTag;
		}

		// Instantiate poisition-rotation for the obstacle and the robot
		robotCl = Instantiate (robot, robotStart, robotRotation) as GameObject;
		obstaCl = Instantiate (obstacle, obstaclePosition, obstacleRotation) as GameObject;
		baskeCl = Instantiate (basket, basketPosition, basketRotation) as GameObject;
		robotCl.gameObject.tag = robotTag;
		obstaCl.gameObject.tag = obstaTag;
		baskeCl.gameObject.tag = baskeTag;

	}

	// Calculation of the robot's new position-action according to movs
	// Blocks in scene x-values: 1 ~ 6.4 | 2 ~ 5.7 | 3 ~ 5 | 4 ~ 4.3 | etc
	// Tree trunk on block 16, branches from 14 to 18
	IEnumerator MoveRobot (string movs, int currPoint, int randObstacle) {
		char nextMov;
		float timish = 0.5f;

		for (int i = 0; i < movs.Length; i++) {
			// End if stop-button is pushed
			if (playMode == 0) {
				return false;
			}
			prevPoint = currPoint;
			nextMov = movs[i];
			switch (nextMov)
			{
			case 'q':
				if (currPoint != (randObstacle + 1)) {
					currPoint = currPoint - 1;
				}
				break;
			case 'w':
				if (currPoint != (randObstacle - 1)) {
					currPoint = currPoint + 1;
				}
				break;
			case 'e':
				if (currPoint != (randObstacle - 2)) {
					currPoint = currPoint + 2;
					jumping = 1;
				}
				break;
			case 'r':
				if ((currPoint == 15)||(currPoint ==16)||(currPoint ==17)) {
					// ADDSTUFF -- delete/ move apple from tree
					holding = 1;
				}
				break;
			case 't':
				// ADDSTUFF -- check if already holding, instantiate
				holding = 0;
				break;
			case 'y':
				// ADDSTUFF -- check if already holding, instantiate
				holding = 0;
				break;
			case 'u':
				if ((holding > 0)&&((currPoint >= randBasket - 1)&&(currPoint <= randBasket + 1))) {
					// ADDSTUFF -- move apple to basket
					holding = 0;
					basketed = basketed + 1;
				}
				break;
			case 'i':
				// ADDSTUFF -- check if empty, instantiate
				basketed = 0;
				break;
			}
			nextPoint = currPoint;
			midPoint = prevPoint + (currPoint - prevPoint) / 2;
			// Calculate direction
			if (prevPoint == currPoint) {
				direction = 0;
				moving = 0;
			} else {
				moving = 1;
				if (prevPoint < currPoint) {
					direction = 1;
					timish = 0.5f + (currPoint - prevPoint)/2;
				} else {
					direction = -1;
					timish = 0.5f + (prevPoint - currPoint)/2;
				}
			}
			yield return new WaitForSeconds (timish);
		}
		// Check the result
		if (basketed > 0) {
			Vector3 bravoPosition = new Vector3 (0, 0, 0);
			Quaternion bravoRotation = Quaternion.identity;
			Instantiate (youWin, bravoPosition, bravoRotation);
		}
		playMode = 0;
	}

	// Calculate the x-value of the robot or the obstacle
	// given the actual theoritical block
	// Blocks in scene x-values: 1 ~ 6.4 | 2 ~ 5.7 | 3 ~ 5 | 4 ~ 4.3 | etc
	float ValueX (float cufPos) {
		float actPos;
		actPos = (cufPos - 1)*0.7f - 6.4f;
		return actPos;
	}

	// Check for equality in floats
	bool nearlyEqual(float a, float b, float epsilon) {
		float absA = Mathf.Abs(a);
		float absB = Mathf.Abs(b);
		float absD = Mathf.Abs(absA - absB);

		if ( a * b < 0f) {
			return false;
		} else if (a == b) {
			return true;
		} else {
			return absD < epsilon;
		}
	}

	// Display the movement from one point to another
	void RunRobot (int prevPoint, int currPoint, int direction, float speed) {
		Vector3 vic = Vector3.zero;
		// Check direction to find new transform.Translate
		if (direction == 1) {
			if (jumping == 0) {
				vic = new Vector3 (1f, 0f, 0f);
			} else {
				if (jumping == 1) {
					// Works well-ish with 0.5f 0.25f for dx = 10
					vic = new Vector3 (1f, 1f, 0f);
				} else {
					vic = new Vector3 (1f, -1f, 0f);
				}

			}
		}
		if (direction == -1) {
			if (jumping == 0) {
				vic = new Vector3 (-1f, 0f, 0f);
			} else {
				if (jumping == 1) {
					vic = new Vector3(-1f, 1f, 0f);
				} else {
					vic = new Vector3 (-1f, -1f, 0f);
				}
			}
		}
		robotCl.transform.Translate (vic*speed*Time.deltaTime);
	}

	// Enqueue actions to the actionList
	void EnqueueToActionList(string movs){
		char nextMov;
		string nextTag = "errList";

		// Initialize poisition-rotation for the action list
		Vector3 listPosition = new Vector3 (listStartValues.x, listStartValues.y, listStartValues.z);
		Quaternion listRotation = Quaternion.identity;

		// List containing the actions -- To be used for tags
		GameObject[] actionList;
		actionList = new GameObject[10];

		// Delete items created from previous function calls
		GameObject[] prevList;
		if (movs.Length >= 0){
			// Delete copies of items tagged "action01"
			prevList = GameObject.FindGameObjectsWithTag ("action01");
			for (int i = 0; i < prevList.Length; i++){
				Destroy (prevList[i]);
			}
			// Delete copies of items tagged "action02"
			prevList = GameObject.FindGameObjectsWithTag ("action02");
			for (int i = 0; i < prevList.Length; i++){
				Destroy (prevList[i]);
			}
			// Delete copies of items tagged "action03"
			prevList = GameObject.FindGameObjectsWithTag ("action03");
			for (int i = 0; i < prevList.Length; i++){
				Destroy (prevList[i]);
			}
			// Delete copies of items tagged "action04"
			prevList = GameObject.FindGameObjectsWithTag ("action04");
			for (int i = 0; i < prevList.Length; i++){
				Destroy (prevList[i]);
			}
			// Delete copies of items tagged "action05"
			prevList = GameObject.FindGameObjectsWithTag ("action05");
			for (int i = 0; i < prevList.Length; i++){
				Destroy (prevList[i]);
			}
			// Delete copies of items tagged "action06"
			prevList = GameObject.FindGameObjectsWithTag ("action06");
			for (int i = 0; i < prevList.Length; i++){
				Destroy (prevList[i]);
			}
			// Delete copies of items tagged "action07"
			prevList = GameObject.FindGameObjectsWithTag ("action07");
			for (int i = 0; i < prevList.Length; i++){
				Destroy (prevList[i]);
			}
			// Delete copies of items tagged "action08"
			prevList = GameObject.FindGameObjectsWithTag ("action08");
			for (int i = 0; i < prevList.Length; i++){
				Destroy (prevList[i]);
			}
			// Delete copies of items tagged "action09"
			prevList = GameObject.FindGameObjectsWithTag ("action09");
			for (int i = 0; i < prevList.Length; i++){
				Destroy (prevList[i]);
			}
		}

		for (int i = 0; i < movs.Length; i++) {
			nextMov = movs [i];
			// Calculate current tag
			switch (i){
			case 0:
				nextTag = "action01";
				break;
			case 1:
				nextTag = "action02";
				break;
			case 2:
				nextTag = "action03";
				break;
			case 3:
				nextTag = "action04";
				break;
			case 4:
				nextTag = "action05";
				break;
			case 5:
				nextTag = "action06";
				break;
			case 6:
				nextTag = "action07";
				break;
			case 7:
				nextTag = "action08";
				break;
			case 8:
				nextTag = "action09";
				break;
			case 9:
				nextTag = "action10";
				break;
				//default:
				//	print("Unexpected error!");
				//	nextTag = "errorList";
				//	break;
			}

			// Get the next move from movs string and add it to the action list
			switch (nextMov) {
			case 'q':
				actionList[i] = Instantiate(listMoveLeft, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'w':
				actionList[i] = Instantiate(listMoveRight, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'e':
				actionList[i] = Instantiate(listJump, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'r':
				actionList[i] = Instantiate(listPickApple, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 't':
				actionList[i] = Instantiate(listNotPickApple, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'y':
				actionList[i] = Instantiate(listThrowApple, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'u':
				actionList[i] = Instantiate(listAppleToBasket, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'i':
				actionList[i] = Instantiate(listEmptyBasket, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			}
			// Change the y-axis value of listPosition -- To be used for the next action
			listPosition.y = listPosition.y - 0.75f;
		}
	}

	// Delete the m-th move from the actionList
	// Given the way this function is called it is impossible to try and delete an invalid move
	string DeleteFromActionList (string movs, int m) {
		char nextMov;
		string newMov = "";
		int acts = movs.Length;

		if (m == 1) {
			for (int i = 1; i < movs.Length; i++) {
				nextMov = movs [i];
				newMov = newMov + nextMov;
			}
		} else if (m == acts) {
			for (int i = 0; i < acts - 1; i++) {
				nextMov = movs [i];
				newMov = newMov + nextMov;
			}
		} else {
			for (int i = 0; i < m - 1; i++) {
				nextMov = movs [i];
				newMov = newMov + nextMov;
			}
			for (int i = m; i < acts; i++) {
				nextMov = movs [i];
				newMov = newMov + nextMov;
			}
		}
		return (newMov);
	}

	/*
	// Use movs to find the robot's next moves
	void HandleRobot (int randStart, string movs) {
		char nextMov;
		for (int i = 0; i < movs.Length; i++) 
		{
			nextMov = movs[i];
			switch(nextMov)
			{
			case 'q':
				// Move left
				currPoint = currPoint - 1;
				break;
			case 'w':
				// Move right
				currPoint = currPoint + 1;
				break;
			case 'e':
				// Jump
				currPoint = currPoint + 2;
				break;
			case 'r':
				// Pick apple
				if ((currPoint == 15)||(currPoint ==16)||(currPoint ==17)) {
					// ADDSTUFF -- delete/ move apple from tree
					holding = 1;
				}
				break;
			case 't':
				// Not pick apple
				holding = 0;
				break;
			case 'y':
				// Throw away apple
				if (holding == 1) {
					holding = 0;
					// ADDSTUFF -- instantiate apple copy on the ground
				}
				break;
			case 'u':
				// Put apple on basket
				if (holding == 1) {
					holding = 0;
					basketed = basketed + 1;
					// ADDSTUFF -- instantiate apple copy on the basket
				}
				break;
			case 'i':
				// Empty basket
				currPoint = currPoint + 50;
				if (basketed > 0) {
					basketed = 0;
					// ADDSTUFF -- instantiate apples around the basket
				}
				break;
			}
			print (currPoint);
		}
	}
	*/
}