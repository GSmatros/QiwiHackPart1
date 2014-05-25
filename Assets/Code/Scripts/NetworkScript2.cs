using UnityEngine;
using System.Collections;

public class NetworkScript2 : MonoBehaviour {
	private GUIStyle menuStyle;
	private GUISkin appSkin;

	private GameObject mainCube;
	private bool isMainCubeLoaded;
	private GameObject clientCube;

	private Vector3 movePos;
	float a;
	float  b;
	float c;
	float  a1;
	float  b1;
	float  c1;
	// Use this for initialization
	void Start () {
		menuStyle = new GUIStyle ();
		menuStyle.normal.textColor = Color.black;
		menuStyle.alignment = TextAnchor.MiddleCenter;
		appSkin = Resources.Load ("AppSkin") as GUISkin;
		isMainCubeLoaded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel == 0) {
		
		}

		if (Application.loadedLevel == 1) {
			if(Network.isServer){
				if(!isMainCubeLoaded){
					mainCube = GameObject.Find("Cube1") as GameObject;
					clientCube = GameObject.Find("Cube2") as GameObject;
					isMainCubeLoaded = true;
				}
				Debug.Log(mainCube);
				Debug.Log(mainCube.transform.position);
				Debug.Log(mainCube.transform.position.x);
				Debug.Log(mainCube.transform.position.y);
				Debug.Log(mainCube.transform.position.z);
				Debug.Log(a);
				Debug.Log(b);
				Debug.Log(c);
				if(isMainCubeLoaded){
					a = mainCube.transform.position.x;
					b = mainCube.transform.position.y;
					c = mainCube.transform.position.z;
					NetworkViewID viewID = Network.AllocateViewID();
					networkView.RPC("SendMessagePos", RPCMode.All, mainCube.transform.position);
				}
			}
			if(Network.isClient){
				clientCube.transform.position = movePos;	
			}
		}
	}

	void OnGUI(){
		GUI.skin = appSkin;
		if (Application.loadedLevel == 0) {
			if (GUI.Button (new Rect (300, 330, 200, 50), "      Speaker")) {
				bool useNat = !Network.HavePublicAddress ();
				Network.InitializeServer (32, 25000, useNat);
				Application.LoadLevel(1);			
			}
			if (GUI.Button (new Rect (300, 410, 200, 50), "      Audience")) {
				Network.Connect ("172.16.16.243", 25000);
				Application.LoadLevel(1);
			}
			if (GUI.Button (new Rect (10, 410, 200, 50), "      Quit")) {
				Application.Quit();
			}
		}

		if (Application.loadedLevel == 1) {
			if(GUI.Button(new Rect(700, 430, 100, 50), "    Back")){
				Application.LoadLevel (0);
			}
			if(Network.isServer){
				GUI.Label(new Rect(0,0,400,50), "Server");	
				GUI.Label(new Rect(0,50,400,50), mainCube.transform.position.x.ToString()
				          +  mainCube.transform.position.y.ToString()
				          +  mainCube.transform.position.z.ToString());
			}
			if(Network.isClient){
				GUI.Label(new Rect(0,0,400,50), "Client");	
				//GUI.Label(new Rect(0,50,400,50), a1.ToString() + b1.ToString() + c1.ToString());
				GUI.Label(new Rect(0,50,400,50),movePos.ToString());
				Debug.Log(movePos);
			}
		}

	}

	[RPC]
	void SendMessagePos( Vector3 baget){
		movePos = baget;
	}





}
