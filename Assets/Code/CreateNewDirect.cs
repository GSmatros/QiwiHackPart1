using UnityEngine;
using System.Collections;

public class CreateNewDirect : MonoBehaviour {
	public GameObject Hierarchy;
	public TextMesh text;
	// Use this for initialization
	void Start () {
		Hierarchy = GameObject.Find("Hierarchy");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SM()
	{
		Hierarchy.SendMessage("CreateNewFolder");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "RightArm")
		{		
			text.color = Color.red;
			Invoke("SM" , 1.0f);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "RightArm")
		{
			text.color = Color.white;
			CancelInvoke("SM");
		}
	}
}
