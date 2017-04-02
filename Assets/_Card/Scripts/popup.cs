using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour {


	//ce sont les petits pop-up qui s'affiche lors de degats recu, de bonus,... les moments ou une stat change
	
	public int TypeDeCompteur;
	public string Valeur;


	private float duration = 4f;
	private float spawnTime;
	private bool _isMoving = true;
	private float timeTakenDuringLerp = 0.2f;
	private Vector3 startPosition;
	private Vector3 endPosition;

	void Start () {
		ChooseType ();
		gameObject.GetComponentInChildren<Text>().text = Valeur.ToString();
		spawnTime = Time.time;
		endPosition = startPosition + new Vector3(0f,0.3f,0f);
	}


	//selon le type de stat affectee, on va decale le popup, ca rend le tout plus lisible. On affecte la bonne couleur aussi
	void ChooseType ()
	{

		if (TypeDeCompteur == 0) {    // 0 = compteur de def
			gameObject.GetComponentInChildren<Text>().color = new Color32 (0,50,71, 255);
			startPosition = transform.position + new Vector3(+0.5f,+0.60f,-0.2f);

		}
		if (TypeDeCompteur == 1) {    // 1 = compteur d'attaque
			gameObject.GetComponentInChildren<Text>().color = new Color32 (130,0,0,255);
			startPosition = transform.position + new Vector3(-0.25f,+0.60f,-0.2f);

		}
		if (TypeDeCompteur == 2) {    // 2 = compteur de vie
			gameObject.GetComponentInChildren<Text>().color = new Color32 (46, 184, 46,255);

			startPosition = transform.position + new Vector3(+0.5f,+1f,-0.2f);
		}
		if (TypeDeCompteur == 4) {    // 4 = compteur de degat
			gameObject.GetComponentInChildren<Text>().color = new Color32 (0, 0, 0,255);

			startPosition = transform.position + new Vector3(+0.35f,+0.5f,-0.2f);
		}

	}

	//ici, on fait le mouvement du pop up vers le haut, son arret puis sa destruction
    void FixedUpdate ()
	{
		if (_isMoving) {
			float timeSinceStarted = Time.time - spawnTime;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
			transform.position = Vector3.Lerp (startPosition, endPosition, percentageComplete);
			if (percentageComplete >= 1.0f) {
				_isMoving = false;
			}
		} else {
			if (Time.time - spawnTime > duration) {
				Destroy(gameObject);
			}
		}

    }

	//public IEnumerator FadeAway (){}
}
