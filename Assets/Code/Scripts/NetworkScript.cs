using UnityEngine;
using System.Collections;

public class NetworkScript : MonoBehaviour {

	private byte frame;
	private bool isMoved;

	private bool isChoosingFile;

	private GUIStyle menuStyle;
	private GUISkin appSkin;

	private GameObject target;
	private bool isTargetLoaded = false;
	// Use this for initialization
	void Start () {
		menuStyle = new GUIStyle ();
		menuStyle.normal.textColor = Color.black;
		menuStyle.alignment = TextAnchor.MiddleCenter;
		appSkin = Resources.Load ("AppSkin") as GUISkin;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (target);
		frame++;
		if (frame > 10 && Network.isServer && !isMoved && isTargetLoaded) {
			networkView.RPC("MoveCube", RPCMode.All, null);
			frame = 0;
			isMoved = false;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
						isChoosingFile = false;	
				}
		if (Application.loadedLevel == 1 && !isTargetLoaded) {
			target = GameObject.Find("Cube1") as GameObject;
			isTargetLoaded = true;
		}
	}
	
	void OnGUI(){
		GUI.skin = appSkin;
		if (Network.isServer) {
			GUI.Label (new Rect (10, 10, 400, 100), "Server");
			
			/*if(GUI.Button(new Rect(250, 450, 100, 100), "MoveCubeRight")){

				transform.position = new Vector3(transform.position.x + 0.1f, 
				                                 transform.position.y, 
				                                 transform.position.z);  
			}
			if(GUI.Button(new Rect(50, 450, 100, 100), "MoveCubeLeft")){
				transform.position = new Vector3(transform.position.x - 0.1f, 
				                                 transform.position.y, 
				                                 transform.position.z);
			}*/
		}
		if (Network.isClient) {
			GUI.Label (new Rect (10, 10, 400, 200), "Client");
		}
		if (Application.loadedLevel == 1) {
			if(GUI.Button(new Rect(700, 430, 100, 50), "    Back")){
				Application.LoadLevel (0);
			}	
		}

		if (Application.loadedLevel == 0) {
						if (!isChoosingFile) {
								if (GUI.Button (new Rect (300, 330, 200, 50), "      Speaker")) {
										bool useNat = !Network.HavePublicAddress ();
										Network.InitializeServer (32, 25000, useNat);
										isChoosingFile = true;
			
								}
								if (GUI.Button (new Rect (300, 410, 200, 50), "      Audience")) {
										Network.Connect ("172.16.16.243", 25000);
										Application.LoadLevel(1);
								}
								if (GUI.Button (new Rect (10, 410, 200, 50), "      Quit")) {
									Application.Quit();
								}

						} else {
								GUI.Label (new Rect (0, 10, 800, 50), "Choose a presentation", menuStyle);
								if (GUI.Button (new Rect (275, 100, 250, 50), "              Test Presentation")) {
										GUI.Label (new Rect (0, 200, 800, 50), "Loading...", menuStyle);
										Application.LoadLevel (1);
								}
						}
				}
	}

	[RPC]
	void MoveCube(){
		Vector3 transformCube = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		target.rigidbody.transform.position = transformCube;
		isMoved = true;
		//rigidbody.transform.Translate(Vector3.zero);
	}
}
