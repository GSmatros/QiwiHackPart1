using UnityEngine;
using System.Collections;

public class Image : MonoBehaviour {
	public bool RightArmIn = false;
	public GameObject ImageChoose;
	public TextMesh ImageNameText;
	public GameObject ImageViewer;
	Texture GetTexture;
	GameObject vwr ;
	GameObject Hierarchy;
	bool viewerOpen = false;
	public string imageName = "Image_1";
	// Use this for initialization
	void Start () {
		ImageNameText.text = imageName;
		//transform.localPosition = new Vector3(-0.3f,0.1f,0.2f);
		Hierarchy = GameObject.Find("Hierarchy");
		//ImageChoose.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {


		if (this.transform.position.y <= 8.7f)
		{
			//rigidbody.isKinematic = true;
			
		}
		if (RightArmIn)
		{
			ImageChoose.renderer.enabled = true;
			Rename("true");
			Invoke("OpenImage", 1.0f);
			//renderer.material.color = Color.red;
		}
		if (!RightArmIn)
		{
			ImageChoose.renderer.enabled = false;
			viewerOpen = false;
			Rename("false");
			CancelInvoke("OpenImage");
			//renderer.material.color = Color.white;
		}

	}

	void OpenImage()
	{
		 if (!viewerOpen)
		{
		vwr = Instantiate(ImageViewer) as GameObject;
			vwr.transform.parent = this.transform;
			vwr.transform.position = new Vector3(-0.7073197f,10.0f,-10.33551f);
			//vwr.gameObject.SendMessage("ChangeTexture", GetTexture);
			vwr.rigidbody.AddForce(new Vector3(0, -950.0f,0));
		}

		viewerOpen = true;
	}





	public void Rename(string _name)
	{
		imageName = _name;
		ImageNameText.text = _name;
	}

	public void GetTxt(Texture _texture)
	{
		GetTexture = _texture;
	}


}
