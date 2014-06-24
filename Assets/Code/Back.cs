using UnityEngine;
using System.Collections;

public class Back : MonoBehaviour {

	public GameObject Hierarchy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Backward()
	{
		Hierarchy.SendMessage("BackDirectory");
	}
}
