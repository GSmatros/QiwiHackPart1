using UnityEngine;
using System.Collections;

public class RightArm : MonoBehaviour {
	Directory direct;
	ImageIcon img ;
	PresentationIcon present;
	TakenParametrs TakeP;
	float lastFrame;
	float currentFrame = 0;
	float forceToSpeed;
	float defaulted;
	float defaultAngle;
	GameObject imge;

	private ImageIcon imageIcon;
	private Vector3 startPos;
	private Vector3 currentPos;
	private float resize;
	private bool isSizeChanged;
	private NetworkScript2 networkScript;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		currentFrame = transform.position.x;
		forceToSpeed = currentFrame - lastFrame;
		//Debug.Log(forceToSpeed);
		if (Input.GetKeyDown(KeyCode.L))
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,-90.0f,transform.eulerAngles.z); 
		}

	if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-0.3f);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+0.3f);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.position = new Vector3(transform.position.x-0.3f, transform.position.y, transform.position.z);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			transform.position = new Vector3(transform.position.x+0.3f, transform.position.y, transform.position.z);
		}

		lastFrame = currentFrame;
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Directory")
		{
			direct = other.GetComponent<Directory>();
			direct.RightArmIn = true;
		}
		if (other.tag == "Back")
		{
			other.gameObject.SendMessage("Backward");
		}
		if (other.tag == "Image")
		{
			img = other.GetComponent<ImageIcon>();
			img.RightArmIn = true;
			imge = other.gameObject;

		}
		if (other.tag == "ImageViewer")
		{
		imge.gameObject.SendMessage("Stop");
		}

		if (other.tag == "Presentation")
		{
			present = other.GetComponent<PresentationIcon>();
			present.RightArmIn = true;
		}


		if (other.tag == "Car Presentation")
		{
			networkScript = GameObject.Find("AppManager").GetComponent<NetworkScript2>();
			//networkScript.isLevelToLoadReceived = false;
			Application.LoadLevel(2);
		}

		if(other.tag == "Close"){
			imageIcon = GameObject.Find("Image Icon(Clone)").GetComponent<ImageIcon>();
			Invoke("InvokeClose", 1.0f);
		}

		if(other.tag == "Chart"){
			startPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			isSizeChanged = false;
			Debug.Log(startPos);
		}
		if (other.tag == "Controlled")
		{
			TakeP = other.GetComponent<TakenParametrs>();
			defaulted = transform.eulerAngles.y;
			defaultAngle = other.transform.eulerAngles.y;
			TakeP.fstfinger = true;
			renderer.material.color = Color.green;
		}

		if(other.tag == "ChartClose"){
			Debug.Log("CloseChart-----------------------------");
			Invoke("InvokeCloseChart", 1.0f);
		}

	}

	void InvokeClose(){
		imageIcon.Close1 ();
	}

	void InvokeCloseChart(){
		present.CloseChart();
	}



	void OnTriggerStay(Collider other)
	{
		if (other.tag == "ImageViewer")
		{
			//other.gameObject.SendMessage("Close");
			//Debug.Log("forceToSpeed " + forceToSpeed);
			imge.gameObject.SendMessage("Swipe", forceToSpeed);
			
			//other.rigidbody.isKinematic = false;
			//other.rigidbody.AddForce(forceToSpeed * 1000.0f,0,0);
		}

		if (other.tag == "Controlled")
		{

			TakeP.fstfingerPos = transform.position;
			float angle = defaulted;
			angle = defaultAngle + defaulted - transform.eulerAngles.y;
			TakeP.Y = angle;

			Debug.Log(angle);
		}

		if (other.tag == "Chart"){
			currentPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			resize = currentPos.z - startPos.z;
			//Debug.Log(resize);
			switch(other.name){
			case "Cube1":

				present.GetResize(resize, 1);
				startPos = currentPos;

				break;
			case "Cube2":
				present.GetResize(resize, 2);
				startPos = currentPos;

				break;
			case "Cube3":
				present.GetResize(resize, 3);
				startPos = currentPos;

				break;

					}
		}


	}




	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Directory")
		{
			direct.RightArmIn = false;
		}
		if (other.tag == "Presentation")
		{
			present.RightArmIn = false;
		}
		if (other.tag == "Image")
		{
			img.RightArmIn = false;
		}
		if (other.tag == "Close"){
			CancelInvoke("InvokeClose");
		}
		if (other.tag == "Controlled")
		{
			TakeP.fstfinger = false;
			renderer.material.color = Color.red;
		}

		if (other.tag == "ChartClose"){
			CancelInvoke("InvokeCloseChart");
		}

	}
}
