using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DBCardManager : MonoBehaviour {

	

	string path;
	string jsonString;

	void Start () {
		path = Application.streamingAssetsPath + "/Card.json";
		jsonString = File.ReadAllText(path);
		Debug.Log (jsonString);
	}

}


