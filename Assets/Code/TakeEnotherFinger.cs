using UnityEngine;
using System.Collections;

public class TakeEnotherFinger : MonoBehaviour {
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
			sendparam.scfinger = true;
			sendparam.scfingerPos = transform.position;
			Debug.Log("SECFINGER");
			renderer.material.color = Color.red;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Controlled") 
		{
			sendparam.scfinger = false;
			renderer.material.color = Color.white;
		}
	}
}
