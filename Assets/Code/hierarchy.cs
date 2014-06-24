using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hierarchy : MonoBehaviour {

	public GameObject DirectoryVis;
	public GameObject ImageVis;
	public GameObject DirectoryBack;
	public TextMesh NowPos;
	string NewNameDir = "New_folder_";
	class Directory
	{
		public List<string> insertDirectoris = new List<string>();
		public List<string> insertImages = new List<string>();
		public List<string> insertPresentations = new List<string>();
		public string Name;
		public string BasedInDir;

		public void AddChildDirectory(string _newDirectoryName)
		{
			insertDirectoris.Add(_newDirectoryName);
		}
		public void AddChildImage(string _newImageName)
		{
			insertImages.Add(_newImageName);
		}
		public void AddChildPresentation(string _newPresentationName)
		{
			insertPresentations.Add(_newPresentationName);
		}

		public Directory(string _BID, string _name){
			BasedInDir = _BID;
			Name = _name;
		}
	}

	class Image
	{
		public string Name;
		public Texture TextureImage;
		public Image(string _name, Texture _texture)
		{
			Name = _name;
			TextureImage = _texture;
		}

	}

	class Presentation
	{
		public string Name;
		public Presentation(string _name)
		{
			Name = _name;
		}

	}




	List<Image> ListImg = new List<Image>();
	List<Directory> ListDir = new List<Directory>();
	List<Presentation> ListPr = new List<Presentation>();
	string now = "/Base";
	// Use this for initialization
	void Start () {
		Texture textur = Resources.Load("Eiffel0") as Texture;
		Image image = new Image("New Album", textur);
		ListImg.Add(image);

		Presentation present = new Presentation("New presentation");

		ListPr.Add(present);

		Directory dir = new Directory(null,"Base");
		dir.AddChildDirectory("New Directory(1)");
		ListDir.Add(dir);
		dir = new Directory(now,"New Directory(1)");
		dir.AddChildDirectory("Presentation");

		dir.AddChildDirectory("Another Presentation");
		ListDir.Add(dir);
		dir = new Directory("New Directory(1)","Presentation");
		dir.AddChildPresentation("New presentation");
		ListDir.Add(dir);
		dir = new Directory("New Directory(1)","Another Presentation");
		dir.AddChildImage("New Album");
		ListDir.Add(dir);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DeleteFolder(string nameOfDel)
	{


		int toSlash = now.Length;
		Debug.Log(nameOfDel);
		string inFolder;
		do 
		{
			toSlash--;
		}while(now[toSlash] != '/');
		inFolder = now.Substring(toSlash+1);
		Directory delDir;
		foreach(Directory dir in ListDir)
		{
			if (inFolder == dir.Name)
			{
				dir.insertDirectoris.Remove(nameOfDel);
			}
		}

		now = now.Substring(0, toSlash);
		IntoDirectory(inFolder);
	}


	public void CreateNewFolder()
	{
		NewNameDir+="1";
		int toSlash = now.Length;
		string inFolder;
		do 
		{
			toSlash--;
			Debug.Log("toslash " + toSlash);
		}while(now[toSlash] != '/');
		inFolder = now.Substring(toSlash+1);
		//Debug.Log(inFolder);
		Directory dir = new Directory(inFolder, NewNameDir);
		ListDir.Add(dir);
		foreach(Directory direct in ListDir)
		{
			if (direct.Name == inFolder)
			{
				direct.AddChildDirectory(NewNameDir);
			}
		}
		now = now.Substring(0,toSlash);
		IntoDirectory(inFolder);

	}


	public void BackDirectory()
	{
		//char letter;
		string str = null;
		string rightStr = null;
		int i = now.Length-1;
		int last = 0;
		int fst = 0;
		int j = 0;
		do
		{
			i--;
			last = i;
		}while(now[i] != '/');
		do
		{
			i--;
			fst = i;
		}while(now[i] != '/');
		j = last-fst-1;
		rightStr = now.Substring(fst+1,j);
		now = now.Substring(0, fst);
		Debug.Log(rightStr);
		Debug.Log(now);


		IntoDirectory(rightStr);
	}

	void DstrAll()
	{
		int childs = DirectoryBack.transform.childCount;
		
		for (int i1 = childs-1; i1>0; i1--)
		{
			Debug.Log(i1);
			if (DirectoryBack.transform.GetChild(i1).gameObject.tag != "Back" && DirectoryBack.transform.GetChild(i1).gameObject.tag != "Stop") 
				GameObject.Destroy(DirectoryBack.transform.GetChild(i1).gameObject);
			
		}
	}




	public void IntoDirectory(string _name)
	{
		now += "/"+_name;
		NowPos.text = now;
		DstrAll();
		Directory neededDir = null;


		foreach(Directory dir in ListDir)
		{
			if (dir.Name == _name)
			{
				neededDir = dir;
				break;
			}
		}
		float x = -0.3f;
		//float y = 3.1f;
		float z = 0.2f;
		foreach(string foundDir in neededDir.insertDirectoris)
		{
			foreach(Directory nDir in ListDir)
			{
				if (foundDir == nDir.Name)
				{
					GameObject newDirectory = Instantiate(DirectoryVis) as GameObject;
					//Debug.Log(x);
					newDirectory.transform.parent = DirectoryBack.transform;
					newDirectory.transform.localPosition = new Vector3(x,2.1f,z);
					newDirectory.rigidbody.AddForce(new Vector3(0, -950.0f,0));
					newDirectory.SendMessage("Rename", nDir.Name);
					x += 0.35f;
				}
			}
		}

		foreach(string foundImg in neededDir.insertImages)
		{
			foreach(Image nImage in ListImg)
			{
				if (foundImg == nImage.Name)
				{
					GameObject newImage = Instantiate(ImageVis) as GameObject;
					newImage.transform.parent = DirectoryBack.transform;
					newImage.transform.localPosition = new Vector3(x,2.1f,z);
					newImage.rigidbody.AddForce(new Vector3(0, -950.0f,0));
					newImage.SendMessage("Rename", foundImg);
					newImage.SendMessage("GetTxt", nImage.TextureImage);
					x += 0.35f;
				}
			}
		}
	}
}
