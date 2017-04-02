using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette : MonoBehaviour {


	//les vignettes sont la pour illustrer la carte, a partir des portraits en _Card/Sprites/Portraits
	//il faudrait que la carte, a partir de son ID, puisse aller tirer le bon portrait
	//depuis ce script, on pourra eventuellement appliquer un shader ou des effets particuliers


	public Sprite portrait;


	void Start () {
		gameObject.GetComponent<SpriteRenderer>().sprite = portrait;
	}
	

	void Update () {
		
	}
}
