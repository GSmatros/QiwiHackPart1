using UnityEngine;
using System.Collections;

public class OnStop : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Image" || other.tag == "Directory")
		{
			//Debug.Log("stop");
			other.rigidbody.isKinematic = true;
		}
	}


}
