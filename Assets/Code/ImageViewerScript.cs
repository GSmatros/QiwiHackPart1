using UnityEngine;
using System.Collections;

public class ImageViewerScript : MonoBehaviour {

	public GameObject Des;
	bool OnLine = false;

	private ImageIcon imageIcon; 
	// Use this for initialization
	void Start () {
		imageIcon = GameObject.Find("Image Icon(Clone)").GetComponent<ImageIcon>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= 1.4f && !OnLine)
		{
			rigidbody.isKinematic = true;
			OnLine = true;
		}


	}
	public void ChangeTexture(Texture _texture)
	{
		renderer.material.mainTexture = _texture;
	}

	public void Stopped()
	{

		Invoke("Stop", 0.1f);
	}

	void Stop()
	{
		//rigidbody.isKinematic = true;
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
	}




}
