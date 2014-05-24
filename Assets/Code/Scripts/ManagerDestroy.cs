using UnityEngine;
using System.Collections;

public class ManagerDestroy : MonoBehaviour {

	private static ManagerDestroy instanceRef;
	void Awake(){
		if (instanceRef == null) {
			instanceRef = this;
			DontDestroyOnLoad(gameObject);
		}
		else{
			DestroyImmediate(gameObject);
		}
	}
}
