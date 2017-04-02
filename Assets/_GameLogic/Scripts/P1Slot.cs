using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Slot : MonoBehaviour {


	//les P1Slots sont les emplacements de cartes dans la main du joueur 1
	//ils servent surtout lors de la selection pour dire au GameManager qu'une carte a ete selectionnee


	public int NumeroEmplacement;
	public GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown ()
	{
		if (GameObject.FindObjectOfType<GameManager> ().isPlayer1) {
			
			if (GameObject.FindGameObjectWithTag ("CarteSelect") != null) {
					
					if( gameObject.tag != "plein"){
					GameObject.FindGameObjectWithTag ("CarteSelect").GetComponent<Card>().emplacement = NumeroEmplacement;
					GameObject.FindGameObjectWithTag ("CarteSelect").GetComponent<Card>().ChangePlace();
					GameObject.FindGameObjectWithTag ("CarteSelect").tag = "Selected";
					gameObject.tag = "plein";
					gameManager.nbcartes +=1;
					}

			}

		}
	}

	public void Remove ()
	{
		// + eventuellement faire une animation de sortie ?
		Destroy(gameObject);
	}
}
