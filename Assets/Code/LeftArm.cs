using UnityEngine;
using System.Collections;

public class LeftArm : MonoBehaviour {
	Directory direct;
	public GameObject DropList;
	bool insert = false;
	GameObject dropList;
	public GameObject DiretoryBack; 
	TakenParametrs TakeP;
	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.S))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-0.3f);
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+0.3f);
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			transform.position = new Vector3(transform.position.x-0.3f, transform.position.y, transform.position.z);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			transform.position = new Vector3(transform.position.x+0.3f, transform.position.y, transform.position.z);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Directory")
		{

			if (insert)
			{
				direct.LeftArmIn = false;
				Destroy(dropList);
				insert = false;
			}
			else if (!insert)
			{
			direct = other.GetComponent<Directory>();
			direct.LeftArmIn = true;
			dropList = Instantiate(DropList,new Vector3(other.transform.position.x , other.transform.position.y +0.6f, other.transform.position.z), Quaternion.identity) as GameObject;
				dropList.transform.parent = DiretoryBack.transform;
				dropList.transform.GetChild(1).gameObject.SendMessage("GetName", direct.directoryName);
			//dropList.rigidbody.isKinematic = false;
			//dropList.rigidbody.AddForce(new Vector3(0,2.0f,0));
				insert = true;
			}

		}

		if (other.tag == "Controlled")
		{
			TakeP = other.GetComponent<TakenParametrs>();
			TakeP.scfinger = true;
			renderer.material.color = Color.cyan;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Controlled")
		{
			TakeP.scfingerPos = transform.position;

		}
	}


	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Controlled")
		{
			TakeP.scfinger = false;
			renderer.material.color = Color.yellow;
		}
	}

	// Update is called once per frame

}
