using UnityEngine;
using System.Collections;

public class RightArm : MonoBehaviour {
	Directory direct;
	ImageIcon img ;
	float lastFrame;
	float currentFrame = 0;
	float forceToSpeed;
	GameObject imge;

	private ImageIcon imageIcon;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		currentFrame = transform.position.x;
		forceToSpeed = currentFrame - lastFrame;
		Debug.Log(forceToSpeed);

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




		if (other.tag == "Car Presentation")
		{
			Application.LoadLevel(2);
		}

		if(other.tag == "Close"){
			Debug.Log("CLOSED-----------------------------------------------");
			imageIcon = GameObject.Find("Image Icon(Clone)").GetComponent<ImageIcon>();
			Invoke("InvokeClose", 1.0f);
		}
	}

	void InvokeClose(){
		imageIcon.Close1 ();
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
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Directory")
		{
			direct.RightArmIn = false;
		}
		if (other.tag == "Image")
		{
			img.RightArmIn = false;
		}
		if (other.tag == "Close"){
			Debug.Log ("CANCEL INVOKE++++++++++++++++++++++++++++++++++++++++++");
			CancelInvoke("InvokeClose");
		}
	}
}
