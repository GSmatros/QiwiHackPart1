using UnityEngine;
using System.Collections;

public class TakenParametrs : MonoBehaviour {

	public bool fstfinger = false;
	public bool scfinger = false;
	public Vector3 fstfingerPos;
	public Vector3 scfingerPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (fstfinger && scfinger) {
			//Vector3 newPos = new Vector3((fstfingerPos.x + scfingerPos.x)*0.5f, (fstfingerPos.y + scfingerPos.y)*0.5f, (fstfingerPos.z + scfingerPos.z)*0.5f);
			Vector3 newPos = new Vector3(fstfingerPos.x, fstfingerPos.y, fstfingerPos.z);
			Debug.Log(newPos);
			transform.position = newPos;
			Debug.Log("PUSHED");
			renderer.material.color = Color.red;
		}
		if (!fstfinger || !scfinger) {
			renderer.material.color = Color.white;

				}


	}

}
