using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PresentationIcon : MonoBehaviour {

	public bool RightArmIn = false;
	public bool LeftArmIn = false;
	public GameObject PresentChoose;
	public TextMesh PresentNameText;
	GameObject Hierarchy;
	public string PresentName = "New_presentation";
	public GameObject chart;
	private bool isPresentationOpened = false;
	private GameObject cube1;
	private GameObject cube2;
	private GameObject cube3;
	private GameObject axisX;
	private GameObject axisY;
	private List<GameObject> chartList = new List<GameObject>();
	public GameObject directoryName;
	//private bool isChartOpened = false;

	// Use this for initialization
	void Start () {
		//directoryName = GameObject.Find ("Directory Name") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	

		if (RightArmIn)
		{
			PresentChoose.renderer.enabled = true;
			Invoke("OpenPresentation", 1.0f);
		}
		if (!RightArmIn && !LeftArmIn)
		{
			PresentChoose.renderer.enabled = false;
			CancelInvoke("OpenPresentation");
		}

		if(isPresentationOpened){
			renderer.enabled = false;
			directoryName.renderer.enabled = false;
		}
		else {
			renderer.enabled = true;
			directoryName.renderer.enabled = true;
		}

	}

	public void Rename(string _name)
	{
		PresentName =  _name;
		PresentNameText.text = _name;
	}

	void OpenPresentation()
	{

		if(!isPresentationOpened){
			GameObject _chart =  Instantiate(chart) as GameObject;
			chart.transform.position = new Vector3 (-2.0f, 0.5f, -12.0f);
			cube1 = GameObject.Find("Cube1") as GameObject;
			cube2 = GameObject.Find("Cube2") as GameObject;
			cube3 = GameObject.Find("Cube3") as GameObject;
			axisX = GameObject.Find("AxisX") as GameObject;
			axisY = GameObject.Find("AxisY") as GameObject;

			chartList.Add(cube1);
			chartList.Add(cube2);
			chartList.Add(cube3);

			cube1.renderer.material.color = Color.red;
			cube2.renderer.material.color = Color.green;
			cube3.renderer.material.color = Color.blue;
			axisX.renderer.material.color = Color.black;
			axisY.renderer.material.color = Color.black;

			cube1.transform.localScale = new Vector3(1.0f, 1.0f, 3.0f);
			cube2.transform.localScale = new Vector3(1.0f, 1.0f, 3.0f);
			cube3.transform.localScale = new Vector3(1.0f, 1.0f, 4.0f);

			ChangeChartSize();

		}
		isPresentationOpened = true;
	}

	public void ChangeChartSize(){
		foreach (GameObject cube in chartList){
			if(cube.transform.localScale.z != 1.0f){
				Vector3 temp = new Vector3(cube.transform.localPosition.x, 
				                           cube.transform.localPosition.y, 
				                           (cube.transform.localScale.z - 1.0f) * 0.5f);
				cube.transform.localPosition = temp;
			}
		}
	}

	public void CloseChart(){
		Destroy(GameObject.Find("Chart(Clone)"));
		isPresentationOpened = false;
	}


	public void GetResize(float amount, int chartNumber){
		Debug.Log(amount + "______________------" + chartNumber);
		Vector3 newSize;
		switch(chartNumber){
		case 1:
			newSize = new Vector3(cube1.transform.localScale.x, 
			                      cube1.transform.localScale.y, 
			                      cube1.transform.localScale.z + amount);
			cube1.transform.localScale = newSize;

			newSize = new Vector3(cube2.transform.localScale.x, 
			                      cube2.transform.localScale.y, 
			                      cube2.transform.localScale.z - amount * 0.5f);
			cube2.transform.localScale = newSize;

			newSize = new Vector3(cube3.transform.localScale.x, 
			                      cube3.transform.localScale.y, 
			                      cube3.transform.localScale.z - amount * 0.5f);
			cube3.transform.localScale = newSize;

			break;
		case 2:
			newSize = new Vector3(cube2.transform.localScale.x, 
			                      cube2.transform.localScale.y, 
			                      cube2.transform.localScale.z + amount);
			cube2.transform.localScale = newSize;

			newSize = new Vector3(cube1.transform.localScale.x, 
			                      cube1.transform.localScale.y, 
			                      cube1.transform.localScale.z - amount * 0.5f);
			cube1.transform.localScale = newSize;
			
			newSize = new Vector3(cube3.transform.localScale.x, 
			                      cube3.transform.localScale.y, 
			                      cube3.transform.localScale.z - amount * 0.5f);
			cube3.transform.localScale = newSize;
			break;
		case 3:
			newSize = new Vector3(cube3.transform.localScale.x, 
			                      cube3.transform.localScale.y, 
			                      cube3.transform.localScale.z + amount);
			cube3.transform.localScale = newSize;

			newSize = new Vector3(cube1.transform.localScale.x, 
			                      cube1.transform.localScale.y, 
			                      cube1.transform.localScale.z - amount * 0.5f);
			cube1.transform.localScale = newSize;
			
			newSize = new Vector3(cube2.transform.localScale.x, 
			                      cube2.transform.localScale.y, 
			                      cube2.transform.localScale.z - amount * 0.5f);
			cube2.transform.localScale = newSize;
			break;
		}
		ChangeChartSize();
	}

}




/*Vector3 tempSize;
switch(chartNumber){
case 1: 
	tempSize = new Vector3 (cube1.transform.localScale.x, cube1.transform.localScale.y, newSize );
	cube1.transform.localScale = tempSize;
	break;
case 2:
	tempSize = new Vector3 (cube2.transform.localScale.x, cube2.transform.localScale.y, newSize );
	cube2.transform.localScale = tempSize;
	break;
case 3:
	tempSize = new Vector3 (cube3.transform.localScale.x, cube3.transform.localScale.y, newSize );
	cube3.transform.localScale = tempSize;
	break;
}*/