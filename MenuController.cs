using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	// GameObject and Vector3 variables to be used
	// for updating
	public Vector3 pressStart1Values = new Vector3 (-5.5f, -1.3f, 0f);
	public Vector3 pressInfo1Values = new Vector3 (-5.5f, -2f, 0f);
	public Vector3 pressInfo7Values = new Vector3 (5.5f, 2f, 0f);
	public Vector3 pressStart7Values = new Vector3 (5.5f, 1.3f, 0f);
	public Vector3 pressShowInfo1Values = new Vector3 (-105f, -135f, 0f);
	public Vector3 pressShowInfo7Values = new Vector3 (125f, 45f, 0f);
	public Vector3 pressShowStartffs1Values = new Vector3 (-145f, -65f, 0f);
	public Vector3 pressShowStart7Values = new Vector3 (185f, 50f, 0f);
	// For some reason when the ShowStartValue for exc 1 is named pressShowStart1Values
	// it takes the value of pressShowInfo1Values

	public GameObject pressInfo;
	public GameObject pressStart;
	public GameObject showInfo1;
	public GameObject showInfo7;
	public GameObject showStart;

	// Canvas variable
	private Canvas canvasComponent;

	// Variables to be used for updating and time
	private int clickdStart1;
	private int clickdStart7;
	private int clickdInfo1;
	private int clickdInfo7;
	private float nextClick = 0.0f;
	private float clickRate = 0.25f;

	// No initialization needed on the menu
	void Start () {
		canvasComponent = GameObject.Find("Canvas").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject pressButton;
		GameObject tempTextBox;
		Vector3 butPosition = new Vector3 (0f, 0f, 0f);
		Vector3 shoPosition = new Vector3 (0f, 0f, 0f);
		Quaternion butRotation = Quaternion.identity;
		Quaternion shoRotation = Quaternion.identity;
		string binfo1Tag = "pressdmeninf1";
		string binfo7Tag = "pressdmeninf7";
		string bstart1Tag = "pressdmenstart1";
		string bstart7Tag = "pressdmenstart7";
		string tinfo1Tag = "texmeninf1";
		string tinfo7Tag = "texmeninf7";
		string tstart1Tag = "texmenstart1";
		string tstart7Tag = "texmenstart7";

		// Get mouse input
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				string hitTag = hit.transform.tag;
				// Show information
				if ((hitTag == "butmeninf1") && (Time.time > nextClick)) {
					clickdInfo1 = clickdInfo1 + 1;
					nextClick = Time.time + clickRate;
					if (clickdInfo1 > 1) {
						GameObject[] prevList;
						prevList = GameObject.FindGameObjectsWithTag ("pressdmeninf1");
						for (int i = 0; i < prevList.Length; i++) {
							Destroy (prevList [i]);
						}
						prevList = GameObject.FindGameObjectsWithTag ("texmeninf1");
						for (int i = 0; i < prevList.Length; i++) {
							Destroy (prevList [i]);
						}
						clickdInfo1 = 0;
					} else {
						butPosition = pressInfo1Values;
						shoPosition = pressShowInfo1Values;
						pressButton = Instantiate (pressInfo, butPosition, butRotation) as GameObject;
						tempTextBox = Instantiate (showInfo1, shoPosition, shoRotation) as GameObject;
						tempTextBox.transform.SetParent (canvasComponent.gameObject.transform, false);
						pressButton.gameObject.tag = binfo1Tag;
						tempTextBox.gameObject.tag = tinfo1Tag;
					}
				}
				if ((hitTag == "butmeninf7") && (Time.time > nextClick)) {
					clickdInfo7 = clickdInfo7 + 1;
					nextClick = Time.time + clickRate;
					if (clickdInfo7 > 1) {
						GameObject[] prevList;
						prevList = GameObject.FindGameObjectsWithTag ("pressdmeninf7");
						for (int i = 0; i < prevList.Length; i++) {
							Destroy (prevList [i]);
						}
						prevList = GameObject.FindGameObjectsWithTag ("texmeninf7");
						for (int i = 0; i < prevList.Length; i++) {
							Destroy (prevList [i]);
						}
						clickdInfo7 = 0;
					} else {
						butPosition = pressInfo7Values;
						shoPosition = pressShowInfo7Values;
						pressButton = Instantiate (pressInfo, butPosition, butRotation) as GameObject;
						tempTextBox = Instantiate (showInfo7, shoPosition, shoRotation) as GameObject;
						tempTextBox.transform.SetParent (canvasComponent.gameObject.transform, false);
						pressButton.gameObject.tag = binfo7Tag;
						tempTextBox.gameObject.tag = tinfo7Tag;
					}
				}
				if ((hitTag == "butmenstart1") && (Time.time > nextClick)) {
					clickdStart1 = clickdStart1 + 1;
					nextClick = Time.time + clickRate;
					if (clickdStart1 > 1) {
						GameObject[] prevList;
						prevList = GameObject.FindGameObjectsWithTag ("pressdmenstart1");
						for (int i = 0; i < prevList.Length; i++) {
							Destroy (prevList [i]);
						}
						Application.LoadLevel ("ExcOne");
						clickdStart1 = 0;
					} else {
						butPosition = pressStart1Values;
						shoPosition = pressShowStartffs1Values;
						Instantiate (pressStart, butPosition, butRotation);
						tempTextBox = Instantiate (showStart, shoPosition, shoRotation) as GameObject;
						tempTextBox.transform.SetParent (canvasComponent.gameObject.transform, false);
					}
				}
				if ((hitTag == "butmenstart7") && (Time.time > nextClick)) {
					clickdStart7 = clickdStart7 + 1;
					nextClick = Time.time + clickRate;
					if (clickdStart7 > 1) {
						GameObject[] prevList;
						prevList = GameObject.FindGameObjectsWithTag ("pressdmenstart7");
						for (int i = 0; i < prevList.Length; i++) {
							Destroy (prevList [i]);
						}
						Application.LoadLevel ("ExcSeven");
						clickdStart7 = 0;
					} else {
						butPosition = pressStart7Values;
						shoPosition = pressShowStart7Values;
						Instantiate (pressStart, butPosition, butRotation);
						tempTextBox = Instantiate (showStart, shoPosition, shoRotation) as GameObject;
						tempTextBox.transform.SetParent (canvasComponent.gameObject.transform, false);
					}
				}
				/*
				if ((hitTag == "butex1") && (Time.time > nextClick)) {
					nextClick = Time.time + clickRate;
					Application.LoadLevel ("ExcOne");
				}
				if ((hitTag == "butex2") && (Time.time > nextClick)) {
					nextClick = Time.time + clickRate;
					Application.LoadLevel ("ExcSeven");
				}
				*/
			}
		}
	}

	void InitializeMenu () {
		clickdStart1 = 0;
		clickdStart7 = 0;
		clickdInfo1 = 0;
		clickdInfo7 = 0;
	}
}
