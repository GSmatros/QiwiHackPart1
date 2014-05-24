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
			Debug.Log("FSTFINGER");
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Controlled") 
		{
			sendparam.fstfinger = false;

		}
	}
}
