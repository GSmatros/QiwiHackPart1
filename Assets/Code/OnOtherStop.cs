using UnityEngine;
using System.Collections;

public class OnOtherStop : MonoBehaviour {

	GameObject imged;
	int number = 0;
	ImageIcon imgicon;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{

	   if (other.tag == "ImageViewer")
	   {

			if (other.transform.position.y > 4.0f)
			{
			//other.rigidbody.isKinematic = true;
				other.rigidbody.velocity = new Vector3(other.rigidbody.velocity.x, 0, other.rigidbody.velocity.z);
			}
			Debug.Log("AnotherStop");
			if (other.transform.position.y < 4.0f)
			{
				other.rigidbody.isKinematic = false;
				other.rigidbody.AddForce(0,3000.0f,0);
				other.gameObject.SendMessage("Stopped");
			}

	   }

	}

	void stop()
	{
		imgicon.Stop();

	}



	void OnTriggerExit(Collider other)
	{
		if (other.tag == "ImageViewer")
		{
			number++;
			imged = GameObject.FindWithTag("Image");
			imgicon = imged.gameObject.GetComponent<ImageIcon>();
			Debug.Log("insrt " + (number/2+1));
			
			if (number/2+1 == imgicon.imageList.Count)
			{
				number = 0;
				Invoke("stop", 0.5f);
			}
			if (other.transform.position.y > 4.0f)
			{
				other.rigidbody.isKinematic = false;
				other.rigidbody.AddForce(0,-1500.0f,0);
				other.gameObject.SendMessage("Stopped");
			}
		}
	}
}
