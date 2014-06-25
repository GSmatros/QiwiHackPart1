using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ImageIcon : MonoBehaviour {
	public bool RightArmIn = false;
	public GameObject ImageChoose;
	public TextMesh ImageNameText;
	public GameObject ImageViewer;
	Texture GetTexture;
	GameObject vwr ;
	GameObject Hierarchy;
	bool viewerOpen = false;
	public string imageName = "Image_1";

	private byte counter = 0; 
	public List<GameObject> imageList = new List<GameObject>();
	private bool isAlbumOpen = false;
	public List<Texture> textureList = new List<Texture>();
	public GameObject albumName;

	// Use this for initialization
	void Start () {
		ImageNameText.text = imageName;
		//transform.localPosition = new Vector3(-0.3f,0.1f,0.2f);
		Hierarchy = GameObject.Find("Hierarchy");
		//ImageChoose.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (transform.localPosition.y<= 0.2f)
		{
			rigidbody.isKinematic = true;
			
		}
		if (RightArmIn)
		{
			ImageChoose.renderer.enabled = true;
			Invoke("OpenImage", 1.0f);
			//renderer.material.color = Color.red;
		}
		if (!RightArmIn)
		{
			ImageChoose.renderer.enabled = false;
			viewerOpen = false;
			CancelInvoke("OpenImage");
			//renderer.material.color = Color.white;
		}

		if(isAlbumOpen){
			/*if(Input.GetKeyDown(KeyCode.RightArrow)){
				SwipeRight();
			}
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				SwipeLeft();
			}*/

			albumName.renderer.enabled = false;
		}
		else {
			albumName.renderer.enabled = true;
		}
		
	}
	
	void OpenImage()
	{
		if (!viewerOpen)
		{

			for (int i = 0; i < 4; i++){
				vwr = Instantiate(ImageViewer) as GameObject;
				imageList.Add(vwr);
				Texture textureImage = Resources.Load("Eiffel" + i) as Texture;
				textureList.Add (textureImage);
				vwr.transform.parent = this.transform;
				vwr.transform.position = new Vector3(i * 5.5f + 0.0f, 5.0f, -10.2f);
				//vwr.gameObject.SendMessage("ChangeTexture", GetTexture);
				vwr.renderer.material.mainTexture = textureList[i];
				vwr.rigidbody.AddForce(new Vector3(0, -950.0f,0));
				//Debug.Log("LengthLisT " + imageList.Count);
			}

		}
		
		viewerOpen = true;
		isAlbumOpen = true;
	}
	
	/*void OnGUI(){
		for (int i = 0; i < 4; i++){
			GUI.DrawTexture(new Rect(i * 100, 200, 100,100), textureList[i]);
		}

	}*/


	public void Rename(string _name)
	{
		imageName = _name;
		ImageNameText.text = _name;
	}
	
	public void GetTxt(Texture _texture)
	{
		GetTexture = _texture;
	}

	public void Swipe(float _force)
	{
		foreach (GameObject vvr in imageList){
		Debug.Log("MOVED");
			vvr.rigidbody.isKinematic = false;
			vvr.rigidbody.AddForce(_force*100.0f,0,0);
		}
	}


	public void SwipeLeft(){
		//Debug.Log("Movedleft");
		foreach (GameObject vvr in imageList){
		//	counter -= 1;
		//	Vector3 temp = new Vector3(vvr.transform.localPosition.x - 7.45f, -64.0f, vvr.transform.localPosition.z); 
		//	if(temp.x > -2.0f && temp.x < -1.9f){
		//		temp.y = 32.0f;
		//	} 
		//	vvr.transform.localPosition = temp;
		//	vvr.rigidbody.isKinematic = false;
		//	vvr.rigidbody.AddForce(-100.0f,0,0);
		//	Invoke("Stop",1.0f);
			
		}
	}

	public void SwipeRight(){
		//Debug.Log("Moved");
		foreach (GameObject vvr in imageList){
		//	counter += 1;
		//	Vector3 temp = new Vector3(vvr.transform.localPosition.x + 7.45f, -64.0f,vvr.transform.localPosition.z); 
		//	if(temp.x > -2.0f && temp.x < -1.9f){
		//		temp.y = 32.0f;
		//	} 
		//	vvr.transform.localPosition = temp;
			//vvr.rigidbody.isKinematic = false;
		//	vvr.rigidbody.AddForce(100.0f,0,0);
		//	Invoke("Stop",1.0f);

		}
	}

	public void Stop()
	{
		foreach(GameObject gm in imageList)
		{
			Debug.Log("STOPPED");
			gm.rigidbody.velocity = new Vector3(0, gm.rigidbody.velocity.y, gm.rigidbody.velocity.z);
		}
	}

	public void Close1(){
		foreach (GameObject vvr in imageList){
			Destroy(vvr);
		}
		isAlbumOpen = false;
	}


	
}