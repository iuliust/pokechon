using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main2 : MonoBehaviour {


	//voir la description de Main1, car Main2 est l'exact equivalent pour le joueur 2



	public P2Slot place1;
	public P2Slot place2;
	public P2Slot place3;
	public P2Slot place4;
	public MotherBoard Affichage;
	public PassifManager passifManager;

	public int nbCartes = 4;

	private P2Slot[] Slots;

	void Start () {
		Slots = GameObject.FindObjectsOfType<P2Slot>();

	}

	public void RotationGauche ()
	{
		if (nbCartes == 4) {
			RotG4();
		}
		if (nbCartes == 3) {
			RotG3();

		}
		if (nbCartes == 2) {
			RotG2();
		}

	}

	void RotG4 ()
	{
		place1.GetComponentInChildren<Card>().emplacement = 4;
		place4.GetComponentInChildren<Card>().emplacement = 3;
		place3.GetComponentInChildren<Card>().emplacement = 2;
		place2.GetComponentInChildren<Card>().emplacement = 1;


		place1.GetComponentInChildren<Card>().ChangePlace();
		place2.GetComponentInChildren<Card>().ChangePlace();
		place3.GetComponentInChildren<Card>().ChangePlace();
		place4.GetComponentInChildren<Card>().ChangePlace();

	}

	void RotG3 ()
	{
		place4.GetComponentInChildren<Card>().emplacement = 3;
		place3.GetComponentInChildren<Card>().emplacement = 2;
		place2.GetComponentInChildren<Card>().emplacement = 4;

	
		place2.GetComponentInChildren<Card>().ChangePlace();
		place3.GetComponentInChildren<Card>().ChangePlace();
		place4.GetComponentInChildren<Card>().ChangePlace();

	}

	void RotG2 ()
	{
		place4.GetComponentInChildren<Card>().emplacement = 2;
		place2.GetComponentInChildren<Card>().emplacement = 4;


		place2.GetComponentInChildren<Card>().ChangePlace();
		place4.GetComponentInChildren<Card>().ChangePlace();

	}


	public void RotationDroite ()
	{
		if (nbCartes == 4) {
			RotD4();
		}
		if (nbCartes == 3) {
			RotD3();
		}
		if (nbCartes == 2) {
			RotD2();
		}

	}

	void RotD4 ()
	{
		place1.GetComponentInChildren<Card>().emplacement = 2;
		place2.GetComponentInChildren<Card>().emplacement = 3;
		place3.GetComponentInChildren<Card>().emplacement = 4;
		place4.GetComponentInChildren<Card>().emplacement = 1;

		place1.GetComponentInChildren<Card>().ChangePlace();
		place2.GetComponentInChildren<Card>().ChangePlace();
		place3.GetComponentInChildren<Card>().ChangePlace();
		place4.GetComponentInChildren<Card>().ChangePlace();

	}

	void RotD3 ()
	{

		place2.GetComponentInChildren<Card>().emplacement = 3;
		place3.GetComponentInChildren<Card>().emplacement = 4;
		place4.GetComponentInChildren<Card>().emplacement = 2;


		place2.GetComponentInChildren<Card>().ChangePlace();
		place3.GetComponentInChildren<Card>().ChangePlace();
		place4.GetComponentInChildren<Card>().ChangePlace();

	}

	void RotD2 ()
	{
		place2.GetComponentInChildren<Card>().emplacement = 4;
		place4.GetComponentInChildren<Card>().emplacement = 2;


		place2.GetComponentInChildren<Card>().ChangePlace();
		place4.GetComponentInChildren<Card>().ChangePlace();

	}

	public void DeclarePassive ()
	{
		if (nbCartes == 4) {
			DeclarePassive4 ();
		}
		if (nbCartes == 3) {
			DeclarePassive3 ();
		}
		if (nbCartes == 2) {
			DeclarePassive2 ();
		}

		Slots = GameObject.FindObjectsOfType<P2Slot> ();

		if (Slots.Length > 2) {
			foreach (P2Slot thisSlot in Slots) {
				if (!thisSlot.GetComponentInChildren<Card> ().IsActif) {
					thisSlot.GetComponentInChildren<Card> ().HealOnPassif (2);
					StartCoroutine (Affichage.EventPassif(thisSlot.GetComponentInChildren<Card> ().Name, 2));
					passifManager.Redirection (thisSlot.GetComponentInChildren<Card> ());
					break;
				} 
			}
		} else if (Slots.Length < 3) {
			foreach (P2Slot thisSlot in Slots) {
				thisSlot.GetComponentInChildren<Card> ().HealOnPassif (5 - nbCartes);
				StartCoroutine (Affichage.EventPassif(thisSlot.GetComponentInChildren<Card> ().Name, 2));
				passifManager.Redirection (thisSlot.GetComponentInChildren<Card> ());
			}
		}

	}

	public void DeclarePassive4 ()
	{
		place1.GetComponentInChildren<Card>().IsActif = true;
		place4.GetComponentInChildren<Card>().IsActif = true;
		place3.GetComponentInChildren<Card>().IsActif = false;
		place2.GetComponentInChildren<Card>().IsActif = true;


	}

	public void DeclarePassive3 ()
	{
		place4.GetComponentInChildren<Card>().IsActif = true;
		place3.GetComponentInChildren<Card>().IsActif = false;
		place2.GetComponentInChildren<Card>().IsActif = true;


	}

	public void DeclarePassive2 ()
	{
		place4.GetComponentInChildren<Card>().IsActif = true;
		place2.GetComponentInChildren<Card>().IsActif = true;


	}

	public void Dying (int emplacement)
	{
		if (nbCartes == 4) {
			
			Dying4 (emplacement);

		}
		if (nbCartes == 3) {
			Dying3 (emplacement);
		}
		if (nbCartes == 2) {
			Dying2 (emplacement);
		}
		if (nbCartes == 1) {
			print("Le joueur 1 a gagne !");
		}

		nbCartes -= 1;


	}

	public void Dying4 (int emplacement)
	{
		if (emplacement == 1) {
			place1.GetComponent<P2Slot>().Remove();
		}
		if (emplacement == 2) {

			Destroy(GameObject.FindGameObjectWithTag("mourant"));
			place1.GetComponentInChildren<Card>().emplacement = 2;
			place1.GetComponentInChildren<Card>().ChangePlace();
			place1.GetComponent<P2Slot>().Remove();
		}
		if (emplacement == 4) {

			Destroy(GameObject.FindGameObjectWithTag("mourant"));
			place1.GetComponentInChildren<Card>().emplacement = 4;
			place1.GetComponentInChildren<Card>().ChangePlace();
			place1.GetComponent<P2Slot>().Remove();
		}

	}
	public void Dying3 (int emplacement)
	{
		if (emplacement == 2) {

			Destroy(GameObject.FindGameObjectWithTag("mourant"));
			place3.GetComponentInChildren<Card>().emplacement = 2;
			place3.GetComponentInChildren<Card>().ChangePlace();
			place3.GetComponent<P2Slot>().Remove();
		}
		if (emplacement == 4) {

			Destroy(GameObject.FindGameObjectWithTag("mourant"));
			place3.GetComponentInChildren<Card>().emplacement = 4;
			place3.GetComponentInChildren<Card>().ChangePlace();
			place3.GetComponent<P2Slot>().Remove();
		}

	}
	public void Dying2 (int emplacement)
	{
		if (emplacement == 2) {
			Destroy(GameObject.FindGameObjectWithTag("mourant"));
			place4.GetComponentInChildren<Card>().emplacement = 2;
			place4.GetComponentInChildren<Card>().ChangePlace();
			place4.GetComponent<P2Slot>().Remove();
		}
		if (emplacement == 4) {
			place4.GetComponent<P2Slot>().Remove();
		}


	}

	//remettre tout le monde en !hasbeeenUsed pour pouvoir les rejour le tour d'apres
	public void reset ()
	{
		Slots = GameObject.FindObjectsOfType<P2Slot>();
		foreach (P2Slot thisSlot in Slots) {
			thisSlot.GetComponentInChildren<Card>().hasBeenUsed = false;
		}

	}


	public void targets (int emplacement)           //TODO link that part the an animation state in those cards
	{
		if (nbCartes == 4) {
			if (emplacement == 2) {
				place4.GetComponentInChildren<Card> ().Targetable(false);
				place1.GetComponentInChildren<Card> ().Targetable(true);
				place2.GetComponentInChildren<Card> ().Targetable(true);
				place3.GetComponentInChildren<Card> ().Targetable(false);
			} else if (emplacement == 4) {
				place2.GetComponentInChildren<Card> ().Targetable(false);
				place1.GetComponentInChildren<Card> ().Targetable(true);
				place4.GetComponentInChildren<Card> ().Targetable(true);
				place3.GetComponentInChildren<Card> ().Targetable(false);
			} else if (emplacement == 1) {
				place2.GetComponentInChildren<Card> ().Targetable(true);
				place1.GetComponentInChildren<Card> ().Targetable(true);
				place4.GetComponentInChildren<Card> ().Targetable(true);
				place3.GetComponentInChildren<Card> ().Targetable(false);
			}
			
		} else if (nbCartes == 3) {
			if (GameObject.FindObjectOfType<Main2>().nbCartes <= 3) {
				place4.GetComponentInChildren<Card> ().Targetable(true);
				place2.GetComponentInChildren<Card> ().Targetable(true);
				place3.GetComponentInChildren<Card> ().Targetable(false);
			} else {
					if (emplacement == 2) {
					place4.GetComponentInChildren<Card> ().Targetable(false);
					place2.GetComponentInChildren<Card> ().Targetable(true);
					place3.GetComponentInChildren<Card> ().Targetable(false);
				} else if (emplacement == 4) {
					place2.GetComponentInChildren<Card> ().Targetable(false);
					place4.GetComponentInChildren<Card> ().Targetable(true);
					place3.GetComponentInChildren<Card> ().Targetable(false);
				} else if (emplacement == 1) {
					place2.GetComponentInChildren<Card> ().Targetable(true);
					place4.GetComponentInChildren<Card> ().Targetable(true);
					place3.GetComponentInChildren<Card> ().Targetable(false);
				}
			}
		} else if (nbCartes == 2) {
			place2.GetComponentInChildren<Card> ().Targetable(true);
			place4.GetComponentInChildren<Card> ().Targetable(true);
		} else if (nbCartes == 1) {
			place2.GetComponentInChildren<Card> ().Targetable(true);
		}

	}

	public void ApplyPassif (int[] Effets)
	{

		if (Effets [0] == 1) {														// si c'est pour les enemis, on change l'attribut "cible" et on l'envoie a la main adverse
			Effets [0] = 0;
			GameObject.FindObjectOfType<Main1> ().ApplyPassif (Effets);
		} else if (Effets [0] == 2) {												//si c'est pour tous, on change cible pour l'envoyer puis on se le renvoie a nous meme
			Effets [0] = 0;
			GameObject.FindObjectOfType<Main1> ().ApplyPassif (Effets);				// 0 veut dire "a faire" en fait 
			ApplyPassif (Effets);
		} else if (Effets [0] == 0) {
			CheckType(Effets);
		}


	}

	void CheckType (int[] Effets)												//je redirige vers les cartes qui sont concernees
	{
		Slots = GameObject.FindObjectsOfType<P2Slot>();
		if (Effets [1] == 4) {

			foreach (P2Slot thisSlot in Slots) {							// 4 : on l'envoie a toutes
				thisSlot.GetComponentInChildren<Card> ().ApplyBonus (Effets);
			}
		} else if (Effets [1] != 4) {

			foreach (P2Slot thisSlot in Slots) {
				if (thisSlot.GetComponentInChildren<Card> ().Type == Effets [1]) {
					thisSlot.GetComponentInChildren<Card> ().ApplyBonus (Effets);
				}
			}
		}
	}

	public void BoulePuante2() {
		
	}


}
