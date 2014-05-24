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
	private GameObject mainCube;
	private bool isMainCubeLoaded = false;

	private int a;
	int a1;
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
			target = GameObject.Find("OnlineCube") as GameObject;
			isTargetLoaded = true;
		}
		if (Application.loadedLevel == 1 && !isMainCubeLoaded) {
			mainCube = GameObject.Find("Cube1") as GameObject;
			isMainCubeLoaded = true;
		}
	}
	
	void OnGUI(){
		GUI.skin = appSkin;

		if (Application.loadedLevel == 1) {
			if(GUI.Button(new Rect(700, 430, 100, 50), "    Back")){
				Application.LoadLevel (0);
			}	
			if(mainCube != null ){
			GUI.Label(new Rect(0,0,400,50), mainCube.transform.position.ToString());
				Debug.Log(mainCube.transform.position);
			}
			if(target != null){
			GUI.Label(new Rect(0,0,400,100), target.transform.position.ToString());
				Debug.Log(target.transform.position);
			}

			if(Network.isServer){
				if(GUI.Button(new Rect(0, 0, 100, 50), "send 1")){
					networkView.RPC("SendOne", RPCMode.All, null);
				}	
			}

			if(Network.isClient){
				GUI.Label(new Rect(0,0,100,100), a1.ToString());
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
		Vector3 transformCube = new Vector3(mainCube.transform.position.x, mainCube.transform.position.y, mainCube.transform.position.z);
		target.rigidbody.transform.position = transformCube;
		isMoved = true;
		//rigidbody.transform.Translate(Vector3.zero);
	}

	[RPC]
	void SendOne(){
		a1 = 1;
	}
}
