using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour {


	//le systeme d'aboiement (!) Loin d'etre au point
	//Il s'agit deja d'afficher des bulles partant des cartes et ne cachant pas le reste du jeu (setOffset)
	//Normalement, les aboiements contextualise l'action
	//PassifText est un ;oyen de dire ce que fait le passif de la carte en passif
	//avec un peu d'ecriture, on peut faire passer un peu de personalite dans ces messages

	public Bubble bubble;
	public popup PopUp;

	private float doubleClik;
	private Bubble bulle;
	private Vector3 offset;
	private popup stats;


	void Start () {
		
	}

	void SetOffset (int emplacement, bool BelongsToP1)
	{

		if (BelongsToP1) {
			if (emplacement != 4) {
				offset = new Vector3 (2.5f, -0.4f, -0.5f);
			} else {
				offset = new Vector3(-2f, 0f, -0.5f);
			}

		} else {

			if (emplacement != 4) {
				offset = new Vector3 (2.5f, +0.4f, -0.5f);
			} else {
				offset = new Vector3(-2f, 0f, -0.5f);
			}
		}

	}

	void DestroyPrevious () {

			//il faut un truc pour detruire la bulle d'avant du personnage

	}


	public IEnumerator PassifText (string description, int emplacement, bool BelongsToP1)
	{
		SetOffset(emplacement, BelongsToP1);
		//DestoyPrevious();       TODO pour gerer les conflits
		bulle = Instantiate(bubble, gameObject.transform.position+offset, Quaternion.identity);
		bulle.lifespan = 3.5f;
		bulle.contenu = description; 
		yield return null;
	}



	//founction principalement test pour voir comment foutre des petites bulles contextuelles
	public IEnumerator flinch (int emplacement, bool BelongsToP1, int degats)
	{
		SetOffset (emplacement, BelongsToP1);
		//DestoyPrevious();       TODO pour gerer les conflits
		bulle = Instantiate (bubble, gameObject.transform.position + offset, Quaternion.identity);
		bulle.lifespan = 1f;
		if (degats == 0) {
			bulle.contenu = "Haha !";
		} else if (degats == 1) {
			bulle.contenu = "Pfff";
		} else if (degats == 2) {
			bulle.contenu = "Aie!"; 
		} else if (degats > 2) {
			bulle.contenu = "OUCH !"; 
		}

		yield return null;
	}

	public void dammagesTaken (int typeStat, int amount)
	{
		stats = Instantiate (PopUp, gameObject.transform.position, Quaternion.identity);
		stats.TypeDeCompteur = typeStat;
		string valeur = "";
		if (typeStat == 4) {
			
			valeur = "- " + amount.ToString () + " PV";

		} else {
			if (typeStat == 2) {
				print("blablabla");
				valeur = "+ " + amount.ToString () + " PV";
			}
			if (typeStat == 1) {
				valeur = "+ " + amount.ToString () + " att";
			}
			if (typeStat == 0) {
				valeur = "+ " + amount.ToString () + " def";
			}
		}

		stats.Valeur = valeur;
	}
}
