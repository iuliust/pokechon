using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// GameManager est le chef d'orchestre qui dit quelle est la prochaine action a faire
	// c'est le chef d'orchestre


	//les 3 bool qui permettent de changer les phases de jeu, entre la selection des cartes et le jeu lui-meme (SelectionEnded), a qui le tour (isPlayer1) et si on est en phase de rotation ou d'attaque
	//-----------------------------------
	public bool isPlayer1 = true;
	public bool SelectionEnded = false;
	public bool hasRotated = false;
	//-----------------------------------


	//il est connecte a la plupart des objets du jeu
	//au debut j'en faisais un hub central pour les appell de fonction, mais en fait je m'en passe quand je peux
	public int nbcartes = 0;
	public Main1 main1;
	public Main2 main2;
	public MotherBoard Affichage;

	//C'est ce qui remplace une vraie UI de selection des cartes en attendant
	//la c'est juste les deux paquets de cartes qui permettent de faire le choix
	//TEMPORAIRE
	//----------------------------------------------
	public GameObject Selection1;
	public GameObject Selection2;
	private bool P1CardDestroyed = false;
	//----------------------------------------------

	private Card[] Cards;

	void Start () {
		Cards = GameObject.FindObjectsOfType<Card>();
	}


	//Update sert exclusivement a lire l'etat du jeu en fonction des 3 bool
	//ca marche bien
	void Update ()
	{
		
		if (!SelectionEnded) {
			if (nbcartes < 4) {
				isPlayer1 = true;
			} else if (nbcartes >= 4 && nbcartes <= 7) {
				if (!P1CardDestroyed) {
					foreach (Card thisCard in Cards) {
						
						if (thisCard.BelongsToP1) {
							if (thisCard.tag == "NonSelectionnee") {
								thisCard.Suppr ();
							}
						}
					}

					P1CardDestroyed = true;
					Selection2.transform.position += new Vector3 (-5f, 0, 0);
				}
				isPlayer1 = false;

			}

			if (nbcartes >= 8) {
				
				SelectionEnded = true;
				Cards = GameObject.FindObjectsOfType<Card> ();						//on reassigne juste pour pas avoir un potentiel de missing reference
					foreach (Card thisCard in Cards) {
						if (thisCard.tag == "NonSelectionnee") {
							thisCard.Suppr ();
						}
					}
				isPlayer1 = true;
				Affichage.StateBoard();
			}
		}

		if (SelectionEnded) {				//boucle de jeu principale, renvoit a playManager et les 2 handles

			if (isPlayer1) {
				if (!hasRotated) {
					Rotation1 ();


				} else if (hasRotated) {
					Play1 ();


					}
			} else if (!isPlayer1) {
					if (!hasRotated) {
						
						Rotation2 ();

					} else if (hasRotated) {
						Play2 ();

					}
			}
		}

	}

	void Rotation1() {
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			main1.RotationGauche();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			main1.RotationDroite();
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			main1.DeclarePassive();
			hasRotated=true;

		}

		//tagger les actifs, les inactifs, et ceux qui ont deja joue, puis les retager normalement

	}

	void Rotation2() {
		
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			main2.RotationGauche();

		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			main2.RotationDroite();

		}
		if(Input.GetKeyDown(KeyCode.Space)){
			hasRotated=true;
			main2.DeclarePassive();

		}

	}

	void Play1 ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			main1.reset ();
			isPlayer1 = false;
			hasRotated = false;
			Affichage.GetComponent<MotherBoard>().StateBoard();
			Cards = GameObject.FindObjectsOfType<Card> ();
			foreach (Card thisCard in Cards) {
				thisCard.ReturnToBaseColor();
			}
		}
	
	}

	void Play2 ()
	{
		if(Input.GetKeyDown(KeyCode.Space)){
			main2.reset();
			isPlayer1 = true;
			hasRotated = false;
			Affichage.GetComponent<MotherBoard>().StateBoard();
			Cards = GameObject.FindObjectsOfType<Card> ();
			foreach (Card thisCard in Cards) {
				thisCard.ReturnToBaseColor();
			}

		}

	}

	public void CiblesPossibles (bool BelongsToP1, int emplacement)
	{
		if (BelongsToP1) {
			main2.targets (emplacement);
		} else {
			main1.targets(emplacement);
		}
	}

	public void DammageMessage (string Name, int Degats)
	{
		StartCoroutine(Affichage.EventHit(Name, Degats));
	}

}
