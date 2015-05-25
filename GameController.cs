using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// GameObject and Vector3 variables to be used
	// for gameview initialization
	public Vector3 robotValues;
	public Vector3 startValues;
	public Vector3 finishValues;
	public GameObject robot;
	public GameObject startPoint;
	public GameObject finishPoint;
	public GameObject breakPoint;
	public GameObject youWin;

	public Vector3 numStartValues;
	public Vector3 numFinishValues;
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
	public Vector3 listStartValues; //Default: [-8.5,  3.5, 0]
	public Vector3 pressPlayValues; // Default: [-8, -5, 0]
	public Vector3 pressStopValues; // Default: [-9, -5, 0]

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
	private Rigidbody robotRb;

	// Int variables containing the start, finish and current points of the robot
	private int randStart;
	private int randFinish;
	private int currPoint;
	private int playMode; // Not playing ~ 0, playing ~ 1
	private int jumping; // Not jumping ~ 0, ascending ~ 1, mid-stop ~ 2, descending ~ 3

	// Float variables containing the actual x-values of line start, end and speed
	public float xMin; // Default: -6.5
	public float xMax; // Default: 6.5
	public float speed; // Default: 2

	// Initialization
	// Generally the maximum startValue and finishValue shall be equal, x-value-wise
	// In the case where |startValue| = |finishValue| = 6.5, we will display numbers in [0,65] and
	// startPoint ~ [1,15], finishPoint ~ [40,65]
	void Start () {
		// Calculation of start and finish values
		randStart = Random.Range(0, 15);
		randFinish = Random.Range(40, 65);
		currPoint = randStart;
		playMode = 0;
		jumping = 0;
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
					// Delete x-value lines from previous plays
					prevList = GameObject.FindGameObjectsWithTag ("line");
					for (int i = 0; i < prevList.Length; i++){
						Destroy (prevList[i]);
					}
					// Actually enter play mode
					playMode = 1;
					print (movs);
					StartCoroutine(MoveRobot(movs));
				}
				// Stop play mode -- click on Stop button
				if ((hitTag == "butStop") && (Time.time > nextClick)) {
					nextClick = Time.time + clickRate;
					// Update Stop-button
					pressTag = "pressdStop";
					Vector3 pressPosition = new Vector3 (pressStopValues.x, pressStopValues.y, pressStopValues.z);
					Quaternion pressRotation = Quaternion.identity;
					GameObject pressButton = Instantiate (pressStop, pressPosition, pressRotation) as GameObject;
					pressButton.gameObject.tag = pressTag;
					// ADDSTUFF -- Re-initialize robot without actually creating another copy
					// Re-initialize robot and start-finish lines
					// Must re-initialize only if there is at least one pressdPlay-tagged object
					/*GameObject[] prevList;
					prevList = GameObject.FindGameObjectsWithTag ("pressdPlay");
					if (prevList.Length > 0) {
						//robotCl.transform.Translate ((ValueX(currPoint)-ValueX(randStart)), 0, 0, Space.Self);
						robotCl.transform.Translate ((ValueX(currPoint-randStart)), 0, 0, Space.Self);
					}
					*/
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
			if (jumping != 0) {
				print ("Jumpdafukup!");
			}
		}
	}

	// Initialize robot and start-finish lines
	void InitializeRobot (int randStart, int currPoint, int randFinish) {
		string robotTag = "player";

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
		robotRb = robotCl.GetComponent<Rigidbody>();

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
		int prevPoint;
		char nextMov;
		string breakTag = "line";
		// Variables holding current point position-rotation to be used for instantiation -- Convert currPoint to string
		//string currStr = currPoint.ToString();
		Vector3 currPosition = new Vector3 ((-startValues.x+currPoint/10), startValues.y, startValues.z);
		Quaternion currRotation = Quaternion.identity;

		for (int i = 0; i < movs.Length; i++) 
		{
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
			// Calculate new currPosition.x for line instantiation
			currPosition.x = ValueX (currPoint);
			JumpRobot (prevPoint, currPoint);
			yield return new WaitForSeconds (1f);
			GameObject breakLine = Instantiate (breakPoint, currPosition, currRotation) as GameObject;
			breakLine.gameObject.tag = breakTag;
			// ADDSTUFF -- probably did the jump
			jumping = 0;
		}
		if (currPoint == randFinish){
			Vector3 bravoPosition = new Vector3 (0, 0, 0);
			Quaternion bravoRotation = Quaternion.identity;
			Instantiate (youWin, bravoPosition, bravoRotation);

		}
	}

	// Calculate the x-value of the robot or the start-finish lines
	// given the actual theoritical value
	// |startValue| = |finishValue| = 6.5, display numbers in [0,65]
	float ValueX (int currPos) {
		float actPos;
		actPos = xMin + currPos/5f;
		return (actPos);
	}

	// Display the movement from one point to another
	void JumpRobot (int prevPoint, int currPoint) {
		float midPoint = prevPoint + (currPoint - prevPoint) / 2;
		jumping = 1;

		//robotRb = robotCl.GetComponent<Rigidbody>();
		// Visualize the jump, part 1 -- Ascending
		if (robotCl.transform.position.x < midPoint) {
			//robotRb.velocity = new Vector3(speed, speed, 0);
			// Calculate the x-value of the mid-point
			// ADDSTUFF -- Currently directly moves robot to the mid-point
			robotCl.transform.Translate (Vector3.right*(currPoint - prevPoint)/10);
		}
		if (robotRb.transform.position.x > midPoint) {
			//robotRb.velocity = new Vector3(-speed, -speed, 0);
			robotCl.transform.Translate (Vector3.right*(prevPoint - currPoint)/10);
		}
		/*
		// Visualize the jump, part 2 -- Stop at the highest point
		if (robotRb.transform.position.x == midPoint) {
			robotRb.velocity = new Vector3(0, 0, 0);
		}
		// Visualize the jump, part 3 -- Descending
		// ADDSTUFF
		*/
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
	

}
