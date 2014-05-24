using UnityEngine;
using System.Collections;

public class Take : MonoBehaviour {

	public TakenParametrs sendparam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Controlled") 
		{
			sendparam.fstfinger = true;
			sendparam.fstfingerPos = transform.position;
			sendparam.X = transform.eulerAngles.x;
			sendparam.Y = transform.eulerAngles.y;
			sendparam.Z = transform.eulerAngles.z;

			Debug.Log("FSTFINGER");
			renderer.material.color = Color.red;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Controlled") 
		{
			sendparam.fstfinger = false;
			renderer.material.color = Color.white;

		}
	}
}
