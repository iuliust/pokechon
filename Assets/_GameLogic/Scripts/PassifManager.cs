using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassifManager : MonoBehaviour {


	//dictionnaire des effets des passifs de chacun
	//probablement a remplacer avec une base de donnee aussi, mais bon, dans l'etat ca fonctionne bien
	//je detaillerais la structure de Effets plus tard TODO


	private int[] Effets = new int[4];



	private Card thisCard;

	void Start () {
		
	}



	public void Redirection (Card card)
	{
		thisCard = card;
		int ID = thisCard.ID;


		if (ID == 1) {
			Effets = Arnault ();
		}
		if (ID == 2) {
			Effets = Dassault ();
		}
		if (ID == 3) {
			Effets = Niel ();
		}
		if (ID == 5) {
			Effets = Pugadas ();
		}
		if (ID == 4) {
			Effets = Barbier ();
		}
		if (ID == 6) {
			Effets = Zemmour ();
		}
		if (ID == 8) {
			Effets = Chirac ();
		}
		if (ID == 9) {
			Effets = Zarkozy ();
		}
		if (ID == 7) {
			Effets = Hollande ();
		}

		if (thisCard.BelongsToP1) {
			GameObject.FindObjectOfType<Main1>().ApplyPassif(Effets);
		} else {
			GameObject.FindObjectOfType<Main2>().ApplyPassif(Effets);
		}

		Effets[0]=0;
		Effets[1]=0;
		Effets[2]=0;
		Effets[3]=0;
	}

	int[] Arnault()
	{
		
		Effets[0]=0;
		Effets[1]=1;
		Effets[2]=1;
		Effets[3]=1;
		StartCoroutine (thisCard.GetComponent<Bark>().PassifText("Les politiques allies ont +1 d'Att", thisCard.emplacement, thisCard.BelongsToP1));
		return Effets;

	}

	int[]  Dassault()
	{
		Effets[0]=0;
		Effets[1]=4;
		Effets[2]=1;
		Effets[3]=0;
		StartCoroutine (thisCard.GetComponent<Bark>().PassifText("Tous les allies ont +1 de Def", thisCard.emplacement, thisCard.BelongsToP1));
		return Effets;

	}

	int[]  Niel()
	{
		Effets[0]=2;
		Effets[1]=2;
		Effets[2]=-1;
		Effets[3]=1;
		StartCoroutine (thisCard.GetComponent<Bark>().PassifText("Tous les Capitalistes perdent 1 d'Att", thisCard.emplacement, thisCard.BelongsToP1));
		return Effets;
	}

	int[]  Pugadas()
	{
		Effets[0]=2;
		Effets[1]=1;
		Effets[2]=1;
		Effets[3]=0;
		StartCoroutine (thisCard.GetComponent<Bark>().PassifText("Tous les Politiques ont +1 de Def", thisCard.emplacement, thisCard.BelongsToP1));
		return Effets;
	}

	int[]  Barbier()
	{
		Effets[0]=0;
		Effets[1]=3;
		Effets[2]=1;
		Effets[3]=1;
		StartCoroutine (thisCard.GetComponent<Bark>().PassifText("Tous les Medias allies ont +1 d'Att", thisCard.emplacement, thisCard.BelongsToP1));
		return Effets;
	}

	int[]  Zemmour()
	{
		Effets[0]=2;
		Effets[1]=4;
		Effets[2]=1;
		Effets[3]=1;
		StartCoroutine (thisCard.GetComponent<Bark>().PassifText("Tous les hommes ont +1 d'Att", thisCard.emplacement, thisCard.BelongsToP1));
		return Effets;
	}

	int[]  Chirac()
	{
		Effets[0]=2;
		Effets[1]=4;
		Effets[2]=0;
		Effets[3]=2;
		StartCoroutine (thisCard.GetComponent<Bark>().PassifText("Annule les effets de la carte passive adverse", thisCard.emplacement, thisCard.BelongsToP1));
		return Effets;
	}

	int[]  Zarkozy()
	{
		Effets[0]=0;
		Effets[1]=2;
		Effets[2]=1;
		Effets[3]=2;
		StartCoroutine (thisCard.GetComponent<Bark>().PassifText("Les Capitalistes allies ont +1 Att et +1 Def", thisCard.emplacement, thisCard.BelongsToP1));
		return Effets;
	}

	int[]  Hollande()
	{
		Effets[0]=0;
		Effets[1]=4;
		Effets[2]=1;
		Effets[3]=0;
		StartCoroutine (thisCard.GetComponent<Bark>().PassifText("Tous les allies ont +1 Def", thisCard.emplacement, thisCard.BelongsToP1));
		return Effets;
	}
}
