using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// GameObject and Vector3 variables to be used
	// for gameview initialization
	public Vector3 robotValues = new Vector3 (6.5f, 0.6f, 0f);
	public Vector3 startValues = new Vector3 (6.5f, 0f, 0f);
	public Vector3 finishValues = new Vector3 (6.5f, 0.3f, 0f);
	public GameObject robot;
	public GameObject startPoint;
	public GameObject finishPoint;
	public GameObject breakPoint;
	public GameObject youWin;

	public Vector3 numStartValues = new Vector3 (6.5f, -0.5f, 0f);
	public Vector3 numFinishValues = new Vector3 (6.5f, -0.5f, 0f);
	public GameObject numOne;
	public GameObject numTwo;
	public GameObject numThree;
	public GameObject numFour;
	public GameObject numFive;
	public GameObject numSix;
	public GameObject numSeven;
	public GameObject numEight;
	public GameObject numNine;
	public GameObject numZero;

	// GameObject and Vector3 variables to be used
	// for actionList initialization and updates
	public Vector3 listStartValues = new Vector3 (-8.5f, 3.5f, 0f);
	public Vector3 pressPlayValues = new Vector3 (-8f, -5f, 0f);
	public Vector3 pressStopValues = new Vector3 (-9f, -5f, 0f);

	public GameObject listBk50;
	public GameObject listBk10;
	public GameObject listBk05;
	public GameObject listBk01;
	public GameObject listFw01;
	public GameObject listFw05;
	public GameObject listFw10;
	public GameObject listFw50;
	public GameObject pressPlay;
	public GameObject pressStop;

	// GameObject and Rigidbody variables to be used for the robot gameplay
	private GameObject robotCl;

	// Int variables containing the start, finish and current points of the robot
	private int randStart;
	private int randFinish;
	private int currPoint;
	private int prevPoint;
	private float midPoint;
	// Variables to be used for real-time checking
	private int playMode;	// Not playing ~ 0, playing ~ 1
	private int jumping;	// Not jumping ~ 0, ascending ~ 1, mid-stop ~ 2, descending ~ 3
	private int direction;	// Left ~ -1, stopped ~ 0, right ~ 1
	private float dex;		// Distance from prev to curr point

	// Variables containing the actual x-values of line start, end and speed
	public float xMin = -6.5f;
	public float xMax = 6.5f;
	public float speed = 1;

	// Initialization
	// Generally the maximum startValue and finishValue shall be equal, x-value-wise
	// In the case where |startValue| = |finishValue| = 6.5, we will display numbers in [0,65] and
	// startPoint ~ [1,15], finishPoint ~ [40,65]
	void Start () {
		// Calculation of start and finish values
		randStart = Random.Range(0, 15);
		randFinish = Random.Range(40, 65);
		/*
		currPoint = randStart;
		prevPoint = randStart;
		playMode = 0;
		jumping = 0;
		direction = 0;
		*/
		// Initialize robot and start-finish lines
		InitializeRobot (randStart, currPoint, randFinish);
	}
	
	// Update is called once per frame
	// movs contains the sequence of moves entered -- q,w,e,r for left and a,s,d,f for right
	private string movs = "";
	private float nextClick = 0.0f;
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
					if ((hitTag == "left50") && (Time.time > nextClick)) {
						movs = movs + "q";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "left10") && (Time.time > nextClick)) {
						movs = movs + "w";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "left05") && (Time.time > nextClick)) {
						movs = movs + "e";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "left01") && (Time.time > nextClick)) {
						movs = movs + "r";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "right01") && (Time.time > nextClick)) {
						movs = movs + "a";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "right05") && (Time.time > nextClick)) {
						movs = movs + "s";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "right10") && (Time.time > nextClick)) {
						movs = movs + "d";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}
					if ((hitTag == "right50") && (Time.time > nextClick)) {
						movs = movs + "f";
						nextClick = Time.time + clickRate;
						EnqueueToActionList (movs);
					}

					// Handle click on the actionList -- to remove actions from the actionList
					if ((hitTag == "action01") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						// Calculate new actionList (movs) -- update actionList
						movs = DeleteFromActionList (movs, 1);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action02") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 2);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action03") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 3);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action04") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 4);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action05") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 5);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action06") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 6);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action07") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 7);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action08") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 8);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action09") && (Time.time > nextClick)) {
						nextClick = Time.time + clickRate;
						movs = DeleteFromActionList (movs, 9);
						EnqueueToActionList (movs);
					}
					if ((hitTag == "action10") && (Time.time > nextClick)) {
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
						for (int i = 0; i < prevList.Length; i++) {
							Destroy (prevList [i]);
						}
						// Delete x-value lines from previous plays
						prevList = GameObject.FindGameObjectsWithTag ("line");
						for (int i = 0; i < prevList.Length; i++) {
							Destroy (prevList [i]);
						}
						prevList = GameObject.FindGameObjectsWithTag ("pressdPlay");
						if (prevList.Length > 0) {
							InitializeRobot (randStart, randStart, randFinish);
						}
						// Actually enter play mode
						playMode = 1;
						print (movs);
						StartCoroutine (MoveRobot (movs));
					}
				}
				// Stop play mode -- click on Stop button
				// Stop button should work even when in play-mode
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
					// Re-initialize robot and start-finish lines
					playMode = 0;
					InitializeRobot (randStart, randStart, randFinish);
				}
			}
		}

		// ADDSTUFF -- Real-time gameplay
		if (playMode == 1) {
			if (jumping == 1) {
				print ("Jumpdafukup!");
				JumpRobot (prevPoint, currPoint, direction, dex, speed);
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
				JumpRobot (prevPoint, currPoint, direction, dex, speed);
				jumping = 3;
			}
			if (jumping == 3) {
				print ("Get back down!");
				JumpRobot (prevPoint, currPoint, direction, dex, speed);
				if (direction == 1) {
					if (nearlyEqual(robotCl.transform.position.x, ValueX(currPoint), 0.05f) || (robotCl.transform.position.x > ValueX(currPoint))) {
						jumping = 0;
					}
				}
				if (direction == -1) {
					if (nearlyEqual(robotCl.transform.position.x, ValueX(currPoint), 0.05f) || (robotCl.transform.position.x < ValueX(currPoint))) {
						jumping = 0;
					}
				}
			}
		}
	}

	// Initialize robot and start-finish lines
	void InitializeRobot (int randStart, int currPoint, int randFinish) {
		string robotTag = "player";

		currPoint = randStart;
		prevPoint = randStart;
		playMode = 0;
		jumping = 0;
		direction = 0;

		// Delete robot copies from previous plays
		// Must delete only if there is at least one pressdPlay-tagged object
		GameObject[] prevList;
		prevList = GameObject.FindGameObjectsWithTag ("pressdPlay");
		if (prevList.Length > 0) {
			GameObject[] prevList2;
			prevList2 = GameObject.FindGameObjectsWithTag ("player");
			for (int i = 0; i < prevList2.Length; i++) {
				Destroy (prevList2[i]);
			}
		}

		// Initialize poisition-rotation for the lines and the robot
		Vector3 startPosition = new Vector3 (ValueX(randStart), startValues.y, startValues.z);
		Vector3 robotStart = new Vector3 (startPosition.x, robotValues.y, robotValues.z);
		Vector3 finishPosition = new Vector3 (ValueX(randFinish), finishValues.y, finishValues.z);
		Quaternion startRotation = Quaternion.identity;
		Quaternion robotRotation = Quaternion.identity;
		Quaternion finishRotation = Quaternion.identity;

		// Instantiate poisition-rotation for the start-finish lines and the robot
		//Instantiate (robot, robotStart, robotRotation);
		Instantiate (startPoint, startPosition, startRotation);
		Instantiate (finishPoint, finishPosition, finishRotation);
		robotCl = Instantiate (robot, robotStart, robotRotation) as GameObject;
		robotCl.gameObject.tag = robotTag;
		//robotRb = robotCl.GetComponent<Rigidbody>();

		// Intantiate poisition-rotation for the numbers at the start -- Convert randStart to string
		string strStart = randStart.ToString();
		Vector3 numStartPosition = new Vector3 (ValueX(randStart), numStartValues.y, numStartValues.z);
		Quaternion numStartRotation = Quaternion.identity;

		// Check the digits one-by-one and instantiate the corresponding number
		for (int i = 0; i < strStart.Length; i++) 
		{
			char strCurr = strStart [i];
			switch (strCurr)
			{
			case '1':
				Instantiate(numOne, numStartPosition, numStartRotation);
				break;
			case '2':
				Instantiate(numTwo, numStartPosition, numStartRotation);
				break;
			case '3':
				Instantiate(numThree, numStartPosition, numStartRotation);
				break;
			case '4':
				Instantiate(numFour, numStartPosition, numStartRotation);
				break;
			case '5':
				Instantiate(numFive, numStartPosition, numStartRotation);
				break;
			case '6':
				Instantiate(numSix, numStartPosition, numStartRotation);
				break;
			case '7':
				Instantiate(numSeven, numStartPosition, numStartRotation);
				break;
			case '8':
				Instantiate(numEight, numStartPosition, numStartRotation);
				break;
			case '9':
				Instantiate(numNine, numStartPosition, numStartRotation);
				break;
			case '0':
				Instantiate(numZero, numStartPosition, numStartRotation);
				break;
			}
			numStartPosition.x = numStartPosition.x + 0.3f;
		}

		// Intantiate poisition-rotation for the numbers at the finish -- Convert randFinish to string
		string strFinish = randFinish.ToString();
		Vector3 numFinishPosition = new Vector3 (ValueX(randFinish), numFinishValues.y, numFinishValues.z);
		Quaternion numFinishRotation = Quaternion.identity;

		// Check the digits one-by-one and instantiate the corresponding number
		for (int i = 0; i < strFinish.Length; i++) 
		{
			char strCurr = strFinish [i];
			switch (strCurr)
			{
			case '1':
				Instantiate(numOne, numFinishPosition, numFinishRotation);
				break;
			case '2':
				Instantiate(numTwo, numFinishPosition, numFinishRotation);
				break;
			case '3':
				Instantiate(numThree, numFinishPosition, numFinishRotation);
				break;
			case '4':
				Instantiate(numFour, numFinishPosition, numFinishRotation);
				break;
			case '5':
				Instantiate(numFive, numFinishPosition, numFinishRotation);
				break;
			case '6':
				Instantiate(numSix, numFinishPosition, numFinishRotation);
				break;
			case '7':
				Instantiate(numSeven, numFinishPosition, numFinishRotation);
				break;
			case '8':
				Instantiate(numEight, numFinishPosition, numFinishRotation);
				break;
			case '9':
				Instantiate(numNine, numFinishPosition, numFinishRotation);
				break;
			case '0':
				Instantiate(numZero, numFinishPosition, numFinishRotation);
				break;
			}
			numFinishPosition.x = numFinishPosition.x + 0.3f;
		}
	}

	// Calculation of the robot's new position according to movs
	// Defined as a coroutine so Wait can be used
	IEnumerator MoveRobot (string movs) {
		char nextMov;
		float timish;
		string breakTag = "line";
		// Variables holding current point position-rotation to be used for instantiation -- Convert currPoint to string
		//string currStr = currPoint.ToString();
		Vector3 currPosition = new Vector3 ((-startValues.x+currPoint/10), startValues.y, startValues.z);
		Quaternion currRotation = Quaternion.identity;

		for (int i = 0; i < movs.Length; i++) 
		{
			// End if stop-button is pushed
			if (playMode == 0) {
				return false;
			}
			prevPoint = currPoint;
			nextMov = movs[i];
			switch(nextMov)
			{
				case 'q':
					currPoint = currPoint - 50;
					break;
				case 'w':
					currPoint = currPoint - 10;
					break;
				case 'e':
					currPoint = currPoint - 5;
					break;
				case 'r':
					currPoint = currPoint - 1;
					break;
				case 'a':
					currPoint = currPoint + 1;
					break;
				case 's':
					currPoint = currPoint + 5;
					break;
				case 'd':
					currPoint = currPoint + 10;
					break;
				case 'f':
					currPoint = currPoint + 50;
					break;
			}
			print (currPoint);
			midPoint = prevPoint + (currPoint - prevPoint) / 2;
			print (ValueX(midPoint));
			// Calculate new currPosition.x for line instantiation
			currPosition.x = ValueX (currPoint);
			// Calculate direction and set jumping
			if (prevPoint < currPoint) {
				direction = 1;
				timish = 0.25f + (currPoint - prevPoint)/10;
			} else {
				direction = -1;
				timish = 0.25f + (prevPoint - currPoint)/10;
			}
			jumping = 1;
			// Set current dex and timish values
			dex = Mathf.Abs (currPoint - prevPoint);
			timish = Mathf.Min (timish, 2.5f);
			JumpRobot (prevPoint, currPoint, direction, dex, speed);
			yield return new WaitForSeconds (timish);
			GameObject breakLine = Instantiate (breakPoint, currPosition, currRotation) as GameObject;
			breakLine.gameObject.tag = breakTag;
		}
		// Check the result
		if (currPoint == randFinish){
			Vector3 bravoPosition = new Vector3 (0, 0, 0);
			Quaternion bravoRotation = Quaternion.identity;
			Instantiate (youWin, bravoPosition, bravoRotation);
		}
		playMode = 0;
	}

	// Calculate the x-value of the robot or the start-finish lines
	// given the actual theoritical value
	// |startValue| = |finishValue| = 6.5, display numbers in [0,65]
	float ValueX (float cufPos) {
		float actPos;
		actPos = xMin + cufPos / 5;
		return (actPos);
	}

	// Calculate the theoritical value of the robot
	// given the actual x-value
	float PointX (float actPos) {
		float cufPoint;
		cufPoint = (actPos + 6.5f) * 5;
		return cufPoint;
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
	// For default numbering system -- displaying numbers in [0,65]
	// with |startValue| = |finishValue| = 6.5
	// dex = dx' = {1, 5, 10, 50}, dx = {0.2, 1, 2, 10}
	// for max(y-value) ~ 0.5, dy ~ {1, 0.2, 0.1, 0.02}
	// and mean(speed) ~ 2, speed ~ {0.2, 1, 2, 10}
	void JumpRobot (int prevPoint, int currPoint, int direction, float dex, float speed) {
		Vector3 vic = Vector3.zero;
		dex = Mathf.Min (dex, 5f);
		float dey = 10 / dex;
		// Check direction to find new transform.Translate
		if (direction == 1) {
			if ((dex == 1f) || (jumping == 2)) {
				vic = new Vector3 (dex, 0f, 0f);
			} else {
				if (jumping == 1) {
					// Works well-ish with 0.5f 0.25f for dx = 10
					vic = new Vector3 (dex, dey, 0f);
				} else {
					vic = new Vector3 (dex, -dey, 0f);
				}
				
			}
		}
		if (direction == -1) {
			if ((dex == 1f) || (jumping == 2)) {
				vic = new Vector3 (-dex, 0f, 0f);
			} else {
				if (jumping == 1) {
					vic = new Vector3(-dex, dey, 0f);
				} else {
					vic = new Vector3 (-dex, -dey, 0f);
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
				actionList[i] = Instantiate(listBk50, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'w':
				actionList[i] = Instantiate(listBk10, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'e':
				actionList[i] = Instantiate(listBk05, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'r':
				actionList[i] = Instantiate(listBk01, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'a':
				actionList[i] = Instantiate(listFw01, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 's':
				actionList[i] = Instantiate(listFw05, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'd':
				actionList[i] = Instantiate(listFw10, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'f':
				actionList[i] = Instantiate(listFw50, listPosition, listRotation) as GameObject;
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
	 // Update -- keyboard input
	// Get keyboard input -- Run when user hits 'return'/'enter'
		if((Input.GetKey ("q")) && (Time.time > nextClick)){
			movs = movs + "q";
			nextClick = Time.time + clickRate;
			print (movs);
		}
		if((Input.GetKey ("w")) && (Time.time > nextClick)){
			movs = movs + "w";
			nextClick = Time.time + clickRate;
			print (movs);
		}
		if((Input.GetKey ("e")) && (Time.time > nextClick)){
			movs = movs + "e";
			nextClick = Time.time + clickRate;
			print (movs);
		}
		if((Input.GetKey ("a")) && (Time.time > nextClick)){
			movs = movs + "a";
			nextClick = Time.time + clickRate;
			print (movs);
		}
		if((Input.GetKey ("s")) && (Time.time > nextClick)){
			movs = movs + "s";
			nextClick = Time.time + clickRate;
			print (movs);
		}
		if((Input.GetKey ("d")) && (Time.time > nextClick)){
			movs = movs + "d";
			nextClick = Time.time + clickRate;
			print (movs);
		}
		if ((Input.GetKey ("z")) && (Time.time > nextClick)) {
			PlayRobot(movs);
		}
		*/


	/*
	// Delete the first action from the actionList using keyboard input
	if ((Input.GetKey ("z")) && (Time.time > nextClick)) {
		nextClick = Time.time + clickRate;
		print ("DELETE ACTION01 BITCH!");
		GameObject[] prevList;
		prevList = GameObject.FindGameObjectsWithTag ("action01");
		for (int i = 0; i < prevList.Length; i++){
			Destroy (prevList[i]);
		}
		// Shift every other action by one
		char nextMov;
		string newMov = "";
		for (int i = 1; i < movs.Length; i++) {
			nextMov = movs[i];
			newMov = newMov + nextMov;
		}
		movs = newMov;
		EnqueueToActionList (movs);
	}
	*/
	
	/*
	// JumpRobot -- FAILED
	void JumpRobot (int prevPoint, int currPoint) {
		// Visualize the jump, part 1 -- Ascending
		if (robotCl.transform.position.x < midPoint) {
			//robotRb.velocity = new Vector3(speed, speed, 0);
			// Calculate the x-value of the mid-point
			// ADDSTUFF -- Currently directly moves robot to the mid-point
			//robotCl.transform.Translate (Vector3.right*(currPoint - prevPoint)/10);
			direction = 1;
			aniJump (prevPoint, currPoint, direction);
		}
		if (robotCl.transform.position.x > midPoint) {
			//robotRb.velocity = new Vector3(-speed, -speed, 0);
			//robotCl.transform.Translate (Vector3.right*(prevPoint - currPoint)/10);
			direction = -1;
			aniJump (prevPoint, currPoint, direction);
		}

		// Visualize the jump, part 2 -- Stop at the highest point
		if (robotRb.transform.position.x == midPoint) {
			robotRb.velocity = new Vector3(0, 0, 0);
		}
		// Visualize the jump, part 3 -- Descending
		// ADDSTUFF
	}

	*/
}
