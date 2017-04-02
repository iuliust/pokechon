using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherBoard : MonoBehaviour {


	//le motherboard est l'affichage en bas de l'ecran (en style pokemon en bas de la fenetre de combat)


	private Text instructions;	//ce qui est affiche sur le motherboard
	public GameManager Manager;

	void Start () {
		instructions = gameObject.GetComponent<Text>();
		instructions.text = "Le joueur 1 puis le 2 choisissent leurs cartes et les mettent a la place voulu (J1 en bas, J2 en haut).";
	}

	//message d'instruction de base parce que je suis le seul a connaitre les regles encore. 
	public void StateBoard () {

		if (Manager.SelectionEnded) {				//boucle de jeu principale, renvoit a playManager et les 2 handles

			if (Manager.isPlayer1) {
				if (!Manager.hasRotated) {
					instructions.text = "En utilisant gauche et droite,  joueur 1 fait tourner son jeu. La carte placee derriere est soignee et enclenche son passif. Espace pour valider.";

				} else if (Manager.hasRotated) {
					instructions.text = "Le joueur 1 clique sur un de ses cartes puis sur une cible possible en face pour l'attaquer. Une attaque par carte, espace pour passer la main au joueur 2";

					}
			} else if (!Manager.isPlayer1) {
				if (!Manager.hasRotated) {
					instructions.text = "En utilisant gauche et droite,  joueur 1 fait tourner son jeu. La carte placee derriere est soignee et enclenche son passif. Espace pour valider.";

				} else if (Manager.hasRotated) {
					instructions.text = "Le joueur 2 clique sur un de ses cartes puis sur une cible possible en face pour l'attaquer. Espace pour passer la main au joueur 1";

					}
			}
		}

	}


	//cette serie d'IEnumerator permet de faire passer un message sur le motherboard pendant un moment choisi dans le WaitForSeconds()
	//ils sont appelles par les main1 et 2
	public IEnumerator EventPassif (string name, int Amount)
	{
		
		instructions.color = new Color32 (0,153,51, 255);
		instructions.text = name + " est maintenant passif et a ete soigne de " + Amount + " PV.";
		yield return new WaitForSeconds(5f);
		instructions.color = Color.black;
		StateBoard();

	}

	public IEnumerator EventHit (string name, int Degats)
	{
		if (Degats != 11) {
			instructions.color = new Color32 (179,0,0,255);
			instructions.text = name + " a pris " + Degats + " de degats.";
			yield return new WaitForSeconds(3f);
			instructions.color = Color.black;
			StateBoard();
		} else if (Degats == 11) {

			instructions.color = new Color32 (179,0,0,255);
			instructions.text = name + " est mort !";
			yield return new WaitForSeconds(3f);
			instructions.color = Color.black;
			StateBoard();
		}

	}

	public void PassifText (string passiftext) {
		instructions.text = passiftext;
	}

}
