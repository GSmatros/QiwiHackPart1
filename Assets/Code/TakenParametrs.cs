using UnityEngine;
using System.Collections;

public class TakenParametrs : MonoBehaviour {

	public bool fstfinger = false;
	public bool scfinger = false;
	public Vector3 fstfingerPos;
	public Vector3 scfingerPos;
	public float X = 0.0f;
	public float Y = 0.0f;
	public float Z = 0.0f;
	float XPoint = 0.0f;
	float ZPoint = 0.0f;
	public GameObject Where;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (fstfinger && scfinger) {
			//Vector3 newPos = new Vector3((fstfingerPos.x + scfingerPos.x)*0.5f, (fstfingerPos.y + scfingerPos.y)*0.5f, (fstfingerPos.z + scfingerPos.z)*0.5f);
			Vector3 newPos = new Vector3(fstfingerPos.x, fstfingerPos.y, fstfingerPos.z);
			Debug.Log(newPos);
			transform.eulerAngles = new Vector3(transform.rotation.x,Y,transform.rotation.x); 
			transform.position = newPos;
			Debug.Log("PUSHED");
			Where.gameObject.renderer.enabled = true;
			RaycastHit hit;
			if (Physics.Raycast(transform.position, Vector3.down, out hit))
			{
				if(hit.collider.tag =="pov")
				{
					XPoint = hit.point.x;
					ZPoint = hit.point.z;
					Where.transform.position = new Vector3(hit.point.x, 0.3f, hit.point.z);
					
				}
			}


		}



		if (!fstfinger || !scfinger) {
			//renderer.material.color = Color.white;
			transform.position = new Vector3(XPoint, 1.4f, ZPoint);
			Where.gameObject.renderer.enabled = false;

				}


	}

}
