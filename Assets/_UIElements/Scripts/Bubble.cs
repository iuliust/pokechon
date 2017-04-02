using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour {

	//essentielle pour le systeme de Bark (aboiement)
	//c'est la classe qui gere la bulle de texte en elle-meme


	public float lifespan = 0;

	private float creationTime;
	public string contenu;

	void Start () {
		creationTime = Time.time;
		CreateText(contenu);
	}

	void CreateText(string contenu) {
		gameObject.GetComponentInChildren<Text>().text = contenu;
	}


	void Update () {
		if (Time.time - creationTime > lifespan) {
			Destroy(gameObject);
		}
	}
}
