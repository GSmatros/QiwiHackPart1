using UnityEngine;
using System.Collections;

public class DeleteDirect : MonoBehaviour {
	public GameObject Hierarchy;
	public TextMesh text;
	public string NameofDirect;
	// Use this for initialization
	void Start () {
		Hierarchy = GameObject.Find("Hierarchy");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetName(string n)
	{
		NameofDirect = n;
		//Debug.Log(NameofDirect);
	}
	void SM()
	{
		Hierarchy.SendMessage("DeleteFolder", NameofDirect);
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
