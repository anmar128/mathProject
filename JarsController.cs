using UnityEngine;
using System.Collections;

public class JarsController : MonoBehaviour {

	// GameObject and Vector3 variables to be used
	// for gameview initialization
	public Vector3 robotValues; //Default: [0, -6, 4]
	public Vector3 jarSValues; //Default: [5.5, 1, 0]
	public Vector3 jarMValues; //Default: [3, 1.3, 0]
	public Vector3 jarLValues; //Default: [-4, 1.5, 0]
	public GameObject robot;
	public GameObject jarS;
	public GameObject jarM;
	public GameObject jarL;
	public GameObject youWin;

	// GameObject and Vector3 variables to be used
	// for actionList initialization and updates
	public Vector3 listStartValues; //Default: [-8.5,  3.5, 0]
	public Vector3 pressPlayValues; // Default: [-8, -5, 0]
	public Vector3 pressStopValues; // Default: [-9, -5, 0]

	public GameObject listPickS;
	public GameObject listPickM;
	public GameObject listPickL;
	public GameObject listReleaseJar;
	public GameObject listFillJar;
	public GameObject listEmptyJar;
	public GameObject listPourS;
	public GameObject listPourM;
	public GameObject listPourL;
	public GameObject listSinkOn;
	public GameObject listSinkOff;
	public GameObject pressPlay;
	public GameObject pressStop;

	// Initialization
	void Start () {
		InitializeJars ();
	}
	
	// Update is called once per frame
	// movs contains the sequence of moves entered -- from left to right q,w,e,r,t,y,a,s,d,f,g
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
				// Handle click on the control panel icons -- to add actions on the actionList
				if ((hitTag == "picksmall") && (Time.time > nextClick)) {
					movs = movs + "q";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "pickmedium") && (Time.time > nextClick)) {
					movs = movs + "w";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "picklarge") && (Time.time > nextClick)) {
					movs = movs + "e";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "jarrelease") && (Time.time > nextClick)) {
					movs = movs + "r";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "jarfill") && (Time.time > nextClick)) {
					movs = movs + "t";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "jarempty") && (Time.time > nextClick)) {
					movs = movs + "y";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "pourtosmall") && (Time.time > nextClick)) {
					movs = movs + "a";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "pourtomedium") && (Time.time > nextClick)) {
					movs = movs + "s";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "pourtolarge") && (Time.time > nextClick)) {
					movs = movs + "d";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "sinkon") && (Time.time > nextClick)) {
					movs = movs + "f";
					nextClick = Time.time + clickRate;
					EnqueueToActionList (movs);
				}
				if ((hitTag == "sinkoff") && (Time.time > nextClick)) {
					movs = movs + "g";
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
					//currPoint = randStart;
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
					print (movs);
					//MoveRobot (movs);
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
					// Update Play-button -- in theory there should be only one item
					// tagged pressdPlay, but search for multiple entries just in case
					GameObject[] prevList;
					prevList = GameObject.FindGameObjectsWithTag ("pressdPlay");
					for (int i = 0; i < prevList.Length; i++){
						Destroy (prevList[i]);
					}
					// Re-initialize robot and jars
					InitializeJars ();
				}
			}
		}
	}

	// Initialize robot and jars
	void InitializeJars () {
		// Initialize poisition-rotation for the robot and the jars
		Vector3 robotPosition = new Vector3 (robotValues.x, robotValues.y, robotValues.z);
		Vector3 jarSPosition = new Vector3 (jarSValues.x, jarSValues.y, jarSValues.z);
		Vector3 jarMPosition = new Vector3 (jarMValues.x, jarMValues.y, jarMValues.z);
		Vector3 jarLPosition = new Vector3 (jarLValues.x, jarLValues.y, jarLValues.z);
		Quaternion robotRotation = Quaternion.identity;
		Quaternion jarSRotation = Quaternion.identity;
		Quaternion jarMRotation = Quaternion.identity;
		Quaternion jarLRotation = Quaternion.identity;

		// Instantiate poisition-rotation for the start-finish lines and the robot
		Instantiate (robot, robotPosition, robotRotation);
		Instantiate (jarS, jarSPosition, jarSRotation);
		Instantiate (jarM, jarMPosition, jarMRotation);
		Instantiate (jarL, jarLPosition, jarLRotation);
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
				actionList[i] = Instantiate(listPickS, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'w':
				actionList[i] = Instantiate(listPickM, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'e':
				actionList[i] = Instantiate(listPickL, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'r':
				actionList[i] = Instantiate(listReleaseJar, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 't':
				actionList[i] = Instantiate(listFillJar, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'y':
				actionList[i] = Instantiate(listEmptyJar, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'a':
				actionList[i] = Instantiate(listPourS, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 's':
				actionList[i] = Instantiate(listPourM, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'd':
				actionList[i] = Instantiate(listPourL, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'f':
				actionList[i] = Instantiate(listSinkOn, listPosition, listRotation) as GameObject;
				actionList[i].gameObject.tag = nextTag;
				actionList[i].gameObject.name = nextTag;
				break;
			case 'g':
				actionList[i] = Instantiate(listSinkOff, listPosition, listRotation) as GameObject;
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
}
