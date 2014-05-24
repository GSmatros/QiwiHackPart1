using UnityEngine;
using System.Collections;

public class NetworkScript : MonoBehaviour {

	private byte frame;
	private bool isMoved;

	private bool isChoosingFile;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		frame++;
		if (frame > 10 && Network.isServer && !isMoved) {
			networkView.RPC("MoveCube", RPCMode.All, null);
			frame = 0;
			isMoved = false;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			isChoosingFile = false;	
		}
	}
	
	void OnGUI(){
		if (!isChoosingFile) {
						if (GUI.Button (new Rect (300, 200, 200, 100), "Speaker")) {
								bool useNat = !Network.HavePublicAddress ();
								Network.InitializeServer (32, 25000, useNat);
								isChoosingFile = true;
			
						}
						if (GUI.Button (new Rect (300, 330, 200, 100), "Audience")) {
								Network.Connect ("172.16.16.243", 25000);
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
				}
		else {
			GUI.Label(new Rect(0,10,800,100), "Choose a presentation");
			if(GUI.Button(new Rect(300,100,200,100), "Test Presentation")){
				Application.LoadLevel(1);
			}
		}
	}
	[RPC]
	void MoveCube(){
		Vector3 transformCube = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		rigidbody.transform.position = transformCube;
		isMoved = true;
		//rigidbody.transform.Translate(Vector3.zero);
	}
}
