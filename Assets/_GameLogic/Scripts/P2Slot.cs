using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Slot : MonoBehaviour {


	//les P2Slots sont les emplacements de cartes dans la main du joueur 2
	
	public int NumeroEmplacement;
	public GameManager gameManager;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown ()
	{
		if (!GameObject.FindObjectOfType<GameManager> ().isPlayer1) {
			
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
		Destroy(gameObject);
	}
}
