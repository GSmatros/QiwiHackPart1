using UnityEngine;
using System.Collections;

public class NetworkScript2 : MonoBehaviour {
	private GUIStyle menuStyle;
	private GUISkin appSkin;

	private GameObject mainCube;
	private bool isMainCubeLoaded;
	private GameObject clientCube;
	private bool isClientCubeLoaded;
	private GameObject car;
	private bool isCarLoaded;

	private Vector3 movePos;
	private float angleRotation;
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
		isClientCubeLoaded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel == 0) {
		
		}

		if (Application.loadedLevel == 1) {
			Debug.Log(car);
			if(!isCarLoaded){
				car = GameObject.Find("bok_lewy03") as GameObject;
				isCarLoaded = true;
			}
			if(Network.isServer){
				if(!isMainCubeLoaded){
					mainCube = GameObject.Find("Cube1") as GameObject;

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
					networkView.RPC("SendRotation", RPCMode.All, mainCube.transform.eulerAngles.y);
				}
			}
			if(Network.isClient){
				if(!isClientCubeLoaded){
					clientCube = GameObject.Find("Cube2") as GameObject;
					isClientCubeLoaded = true;
				}

				clientCube.transform.position = movePos;
				clientCube.transform.rotation = Quaternion.Euler(0, angleRotation,0);
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
			/*if(GUI.Button(new Rect(700, 430, 100, 50), "    Back")){
				Application.LoadLevel (0);
			}*/

			if(GUI.Button(new Rect(0,150,200,100), "Red")){
				car.renderer.material.color = Color.red;
			}
			if(GUI.Button(new Rect(0,270,200,100), "Green")){
				car.renderer.material.color = Color.green;
			}
			if(GUI.Button(new Rect(0,380,200,100), "White")){
				car.renderer.material.color = Color.white;
			}
			if(Network.isServer){
				if(GUI.Button(new Rect(0,0,200,100), "Reset")){
					mainCube.transform.localPosition = new Vector3(0,0.7f,-5.0f);
					mainCube.transform.rotation = Quaternion.Euler(0,0,0);
				}
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

	[RPC]
	void SendRotation(float angle){
		angleRotation = angle;
	}



}
