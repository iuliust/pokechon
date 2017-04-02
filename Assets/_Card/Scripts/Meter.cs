using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour {

	//les meter sont les compteurs sur la carte. A terme, on pourra ajouter des effets de style pour montrer quelle stat est boostee par un passif

	//le type de compteur determine la couleur globalement
	public int TypeDeCompteur;


	void Start () {
		ChooseColor ();

	}

	public void updateMeter (int valeur)
	{
		if (TypeDeCompteur != 2) {
			gameObject.GetComponent<Text> ().text = valeur.ToString ();
		} else {
			gameObject.GetComponent<Text> ().text = valeur.ToString ()+"/20";			// le /20 a remplacer par un /MaxHealth a un moment, si les cartes peuvent grimper en niveau
		}

	}


	void ChooseColor ()
	{
		if (TypeDeCompteur == 0) {    // 0 = compteur de defense
			gameObject.GetComponent<Text>().color = new Color32 (0,50,71,255);

		}
		if (TypeDeCompteur == 1) {    // 1 = compteur d'Attaque
			gameObject.GetComponent<Text>().color = new Color32 (130,0,0,255);

		}
		if (TypeDeCompteur == 2) {    // 2 = compteur de Vie
			gameObject.GetComponent<Text>().color = new Color32 (46, 184, 46, 255);

		}
	}
}

