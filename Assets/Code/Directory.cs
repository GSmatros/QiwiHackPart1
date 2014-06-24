 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Directory : MonoBehaviour {

	public bool RightArmIn = false;
	public bool LeftArmIn = false;
	public GameObject DirectoryChoose;
	public TextMesh DirectoryNameText;
	GameObject Hierarchy;
	public string directoryName = "New Directory(1)";


	// Use this for initialization
	void Start () {
		DirectoryNameText.text = directoryName;

		//transform.localPosition = new Vector3(-0.3f,0.1f,0.2f);
		Hierarchy = GameObject.Find("Hierarchy");
	}
	
	// Update is called once per frame
	void Update () {

		if (RightArmIn)
		{
			DirectoryChoose.renderer.enabled = true;
			Invoke("OpenDirectory", 1.0f);
			//renderer.material.color = Color.red;
		}
		if (!RightArmIn && !LeftArmIn)
		{
			DirectoryChoose.renderer.enabled = false;
			CancelInvoke("OpenDirectory");
			//renderer.material.color = Color.white;
		}

		if (LeftArmIn)
		{
			DirectoryChoose.renderer.enabled = true;
		}
		if (!LeftArmIn && !RightArmIn)
		{
			DirectoryChoose.renderer.enabled = false;
		}

		if (transform.localPosition.y<= 0.7f)
		{
			rigidbody.isKinematic = true;
		}
	
	}



     void OpenDirectory()
	   {
		Hierarchy.SendMessage("IntoDirectory", directoryName);
		Destroy(this.gameObject);
       }

	public void Rename(string _name)
	{
		directoryName = _name;
		DirectoryNameText.text = _name;
	}
}
