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
	public Vector3 listStartValues;
	public Vector3 listCurrValues;

	public GameObject listBk50;
	public GameObject listBk10;
	public GameObject listBk05;
	public GameObject listBk01;
	public GameObject listFw01;
	public GameObject listFw05;
	public GameObject listFw10;
	public GameObject listFw50;

	// Int variables containing the start, finish and current points of the robot
	public int randStart;
	public int randFinish;
	public int currPoint;

	// Initialization
	// Generally the maximum startValue and finishValue shall be equal by value
	// In the case where |startValue| = |finishValue| = 6.5, we will display numbers in [0,65] and
	// startPoint ~ [1,15], finishPoint ~ [40,65]
	void Start () {
		// Calculation of start and finish values
		randStart = Random.Range(0, 15);
		randFinish = Random.Range(40, 65);
		currPoint = randStart;
		
		// Initialize poisition-rotation for the lines and the robot
		Vector3 startPosition = new Vector3 ((-startValues.x+randStart/10), startValues.y, startValues.z);
		Vector3 robotStart = new Vector3 (startPosition.x, robotValues.y, robotValues.z);
		Vector3 finishPosition = new Vector3 ((randFinish/10), finishValues.y, finishValues.z);
		Quaternion startRotation = Quaternion.identity;
		Quaternion robotRotation = Quaternion.identity;
		Quaternion finishRotation = Quaternion.identity;
		
		// Instantiate poisition-rotation for the start-finish lines and the robot
		Instantiate (robot, robotStart, robotRotation);
		Instantiate (startPoint, startPosition, startRotation);
		Instantiate (finishPoint, finishPosition, finishRotation);

		// Intantiate poisition-rotation for the numbers at the start -- Convert randStart to string
		string strStart = randStart.ToString();
		Vector3 numStartPosition = new Vector3 ((-numStartValues.x+randStart/10), numStartValues.y, numStartValues.z);
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
		Vector3 numFinishPosition = new Vector3 ((randFinish/10), numFinishValues.y, numFinishValues.z);
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
	
	// Update is called once per frame
	//movs contains the sequence of moves entered -- q,w,e,r for left and a,s,d,f for right
	public string movs = "";
	public float nextClick = 0.0f;
	public float clickRate = 2.5f;
	void Update () {
		// Get mouse input
		if (Input.GetMouseButton (0)) {
			// Handle click on the control panel icons
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				string hitTag = hit.transform.tag;

				if ((hitTag == "left50") && (Time.time > nextClick)) {
					movs = movs + "q";
					nextClick = Time.time + clickRate;
					print (movs);
					EnqueueToActionList (movs);
				}
				if ((hitTag == "left10") && (Time.time > nextClick)) {
					movs = movs + "w";
					nextClick = Time.time + clickRate;
					print (movs);
					EnqueueToActionList (movs);
				}
				if ((hitTag == "left05") && (Time.time > nextClick)) {
					movs = movs + "e";
					nextClick = Time.time + clickRate;
					print (movs);
					EnqueueToActionList (movs);
				}
				if ((hitTag == "left01") && (Time.time > nextClick)) {
					movs = movs + "r";
					nextClick = Time.time + clickRate;
					print (movs);
					EnqueueToActionList (movs);
				}
				if ((hitTag == "right01") && (Time.time > nextClick)) {
					movs = movs + "a";
					nextClick = Time.time + clickRate;
					print (movs);
					EnqueueToActionList (movs);
				}
				if ((hitTag == "right05") && (Time.time > nextClick)) {
					movs = movs + "s";
					nextClick = Time.time + clickRate;
					print (movs);
					EnqueueToActionList (movs);
				}
				if ((hitTag == "right10") && (Time.time > nextClick)) {
					movs = movs + "d";
					nextClick = Time.time + clickRate;
					print (movs);
					EnqueueToActionList (movs);
				}
				if ((hitTag == "right50") && (Time.time > nextClick)) {
					movs = movs + "f";
					nextClick = Time.time + clickRate;
					print (movs);
					EnqueueToActionList (movs);
				}

				// Enter play mode -- Click on Play button
				if ((hitTag == "butPlay") && (Time.time > nextClick)) {
					nextClick = Time.time + clickRate;
					currPoint = randStart;
					MoveRobot (movs);
				}
				// Stop play mode -- Click on Stop button
				if ((hitTag == "butStop") && (Time.time > nextClick)) {
					nextClick = Time.time + clickRate;
					// Do something
				}
			}
		}

	}

	// Calculation of the robot's new position according to movs
	void MoveRobot(string movs){
		char nextMov;
		for (int i = 0; i < movs.Length; i++) 
		{
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
		}
	}

	// Enque an action to the actionList
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
		if (movs.Length > 1){
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
				print (actionList[i].gameObject.tag);
				break;
			case 'w':
				actionList[i] = Instantiate(listBk10, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				print (actionList[i].gameObject.tag);
				break;
			case 'e':
				actionList[i] = Instantiate(listBk05, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				print (actionList[i].gameObject.tag);
				break;
			case 'r':
				actionList[i] = Instantiate(listBk01, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				print (actionList[i].gameObject.tag);
				break;
			case 'a':
				actionList[i] = Instantiate(listFw01, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				print (actionList[i].gameObject.tag);
				break;
			case 's':
				actionList[i] = Instantiate(listFw05, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				print (actionList[i].gameObject.tag);
				break;
			case 'd':
				actionList[i] = Instantiate(listFw10, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				print (actionList[i].gameObject.tag);
				break;
			case 'f':
				actionList[i] = Instantiate(listFw50, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				print (actionList[i].gameObject.tag);
				break;
			}
			// Change the y-axis value of listPosition -- To be used for the next action
			listPosition.y = listPosition.y - 0.75f;
		}
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
	

}
