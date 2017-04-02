using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class essai : MonoBehaviour {

	private bool up = true;
	private float startTime;

	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time - startTime >= 1f) {
			up = !up;
			startTime=Time.time;
		}
		if (up) {
			transform.position += new Vector3 (0, 0.1f, 0);
		} else {
			transform.position += new Vector3 (0, -0.1f, 0);
		}
	}
}
