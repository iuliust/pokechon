using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// la classe principale, qui gere pas mal de ce que fait une carte
//pour l'instant, y a des declarations interminables avec plein de truc expose publiquement
//c'est parce que je ne sais pas faire autrement

//j'aimerais que quelqu'un soit capable de faire en sorte que la carte n'est qu'un identifiant public (ID)
//A partir de cet ID, au demarrage, la carte va chercher dans une base de donnee les stats quelle doit avoir
//mais aussi la bonne image pour l'illustrer

//J'ai fais de cette classe un truc un peu fourre tout. Je pense qu'il y a plusieurs fonctions melangee
//pour l'instant, la logique est la suivante :

//1) au demarrage, la carte fait apparaitre un Card_UIElement, c'est a dire l'ensemble de tous les compteurs (class meter) pour afficher la vie et le reste
//
//2) La region OnMouseDown est ce qui gere la logique d'interactivite, pour faire attaquer les cartes, les selectionner,... en fonction de la phase de jeu, a qui le tour, etc...
//
//3) La region Mouvement de la carte qui fait bouger la carte d'un endroit a l'autre quand le joueur fait une rotation, ou lors des selections
//
//4) Les regions "Recevoir des attaques" et "Modif de stats" qui permettent de prendre des coup et d'en donner, de faire le calcul des degats en fonction de la defense, du type,...
//	qui marche bien :)



public class Card : MonoBehaviour {

	public Card_UIelement UIelements;
	public int ID;

	//etat de la carte, specifiquement pour un eventuel animateur (tremblements, reaction et surbrillance) 
	public bool IsTargetable = false; 			
	public bool IsClickable = true;      
	public bool hasBeenUsed = false;

	//emplacement refere a la place qu'occupe la carte sur le terrain, BelongsToP1 permet de faire le tri entre les cartes du joueur 1 et 2
	public int emplacement;
	public bool BelongsToP1;
	public bool IsActif;


	//---------------------------------------------------
	//les stats de la carte. C'est ca qui devrait etre tire d'une base de donnee, typiquement TODO 
	public string Name;							
	public int Vie;
	public int Attaque;
	public int Defense;
	public int BonusAtt;
	public int BonusVie;
	public int BonusDef;
	public int Type;

	//Tout ca, ca devrait venir d'une base de donnee, et d'une classe serializable
	//------------------------------------------------
	//------------------------------------------------




	private Color BaseColor = new Color32(210,210,210,255);


	private Card_UIelement goUI;	//l'UI de ce gameobject
	private GameManager GameManager;

	private P1Slot[] p1Slots;
	private P2Slot[] p2Slots;

	private int CurrentVie;
	private int CurrentDef;
	private int CurrentAtt;


	// Toutes ces variables servent a l'animation basique du mouvement des cartes. Elles servent dans Lerp (Linear interpol)
	private bool _isLerping = false;
	private Vector3 endPosition;
	private Vector3 startPosition;
	private float timeStartedLerping;
	private float timeTakenDuringLerp = 0.1f;

	void Start () {
		
		endPosition = gameObject.transform.position;

		gameObject.GetComponent<SpriteRenderer>().color = BaseColor;
		CurrentAtt = Attaque;
		CurrentDef = Defense;
		CurrentVie = Vie;

		Vector3 position = this.transform.position;

		goUI = Instantiate(UIelements, position, Quaternion.identity);
		goUI.transform.SetParent(transform, false);
		goUI.transform.position = transform.position + new Vector3(0,+0.36f,0);

		goUI.SetValues(CurrentVie, CurrentAtt, CurrentDef);
		goUI.SetName(Name);

		GameManager = GameObject.FindObjectOfType<GameManager>();
				
	}

	// Cette partie sert a gerer les comportement lors du clic de la sourie sur une carte
	#region name OnMouseDown

	void OnMouseDown ()
	{
		if (!GameManager.SelectionEnded) {
			
			SelectionClickEvent ();
		}

		if (GameManager.hasRotated) {				//Si on a fini de faire tourner ses cartes et qu'on joue
			
			if (GameManager.isPlayer1) { 
			            //C'est au tout de P1
				P1TurnClickEvents ();

			} else if (!GameManager.isPlayer1) {

				P2TurnClickEvents();
			}
		}
	}

	void SelectionClickEvent ()
	{
		if (gameObject.tag != "Selected") {
			if (GameManager.isPlayer1 == BelongsToP1) {
				if (GameObject.FindGameObjectWithTag ("CarteSelect") != null) {
					GameObject.FindGameObjectWithTag ("CarteSelect").GetComponent<SpriteRenderer> ().color = BaseColor;
					GameObject.FindGameObjectWithTag ("CarteSelect").GetComponent<Card> ().tag = "NonSelectionnee";
					gameObject.tag = "CarteSelect";																		//on peut associer a Carte select l'envoie d'un texte au mother board.
					gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (254,254,254, 255);						
				} else {
					gameObject.tag = "CarteSelect";
					gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (254,254,254, 255);
				}
			} else {
				gameObject.tag = "NonSelectionnee";						//un peu inutile mais bon, on confirme qu'elle n'est pas select. on peut peut etre quand meme envoyer un message a mother board pour afficher les carac ?
			}		

		}
	}

	void P1TurnClickEvents ()
	{
		if (BelongsToP1) {                   //S'il clique sur une de ses cartes
				
			if (IsActif && !hasBeenUsed) {
				if (GameObject.FindWithTag ("AttPotentiel") != null) {
					GameObject.FindWithTag ("AttPotentiel").tag = "actif";
					gameObject.tag = "AttPotentiel";
					GameManager.CiblesPossibles (BelongsToP1, emplacement);
				} else {
					gameObject.tag = "AttPotentiel";
					print (gameObject.GetComponent<Card> ().Name + " est attaquant potentiel");
					GameManager.CiblesPossibles (BelongsToP1, emplacement);
				}
			} else {
				print ("Je suis passif !");            //TODO A rafiner
			}

		} else {                         											//mais si une carte joueur 2
			if (IsClickable) {
				if (GameObject.FindWithTag ("AttPotentiel") != null) {				//et qu'il y a une carte attaquante et qu'on est une cible valide
					DealWithAttack ();
					Card[] Cards = GameObject.FindObjectsOfType<Card> ();
						foreach (Card thisCard in Cards) {
							thisCard.ReturnToBaseColor();						
						}
					}
				}
			}
			//GameObject.FindObjectOfType<Main1>().CheckFinTour();
	}

	void P2TurnClickEvents ()
	{
		if (!BelongsToP1) {                   //S'il clique sur une de ses cartes
				if (IsActif && !hasBeenUsed) {
					if (GameObject.FindWithTag ("AttPotentiel") != null) {
						GameObject.FindWithTag ("AttPotentiel").tag = "actif";
						gameObject.tag = "AttPotentiel";
					GameManager.CiblesPossibles (BelongsToP1, emplacement);
					} else {
						gameObject.tag = "AttPotentiel";
					GameManager.CiblesPossibles (BelongsToP1, emplacement);
					}
				} else {
					print ("Je suis passif !");            //TODO A rafiner
				}

			} 

			else {                         											//mais si  c'est une carte joueur 1
				if (IsClickable) {
					if (GameObject.FindWithTag ("AttPotentiel") != null) {				//et qu'il y a une carte attaquante
						DealWithAttack();
						Card[] Cards = GameObject.FindObjectsOfType<Card> ();
							foreach (Card thisCard in Cards) {
								thisCard.ReturnToBaseColor();
							}

					}
				}
			}

			//GameObject.FindObjectOfType<Main1>().CheckFinTour();
	}

	public void Targetable (bool Targetable)
	{				//ugly thing to replace by an animator

		if (Targetable) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255,255,255, 255);

		} else {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (150,150,150, 255);
		}

		IsTargetable = Targetable;
		IsClickable = Targetable;
	}

	public void ReturnToBaseColor ()
	{
		
		gameObject.GetComponent<SpriteRenderer>().color = BaseColor;
	}

	#endregion


	#region name Mouvements de la carte

	public void ChangePlace ()
	{
		if (BelongsToP1) {

			p1Slots = GameObject.FindObjectsOfType<P1Slot> ();

			foreach (P1Slot thisSlot in p1Slots) {
				if (thisSlot.NumeroEmplacement == emplacement) {
					
					transform.parent = thisSlot.transform;
					endPosition = thisSlot.transform.position + new Vector3(0f,0f,-0.1f);
					StartLerping();
					break;
				}
			}
		} else {

			p2Slots = GameObject.FindObjectsOfType<P2Slot> ();

			foreach (P2Slot thisSlot in p2Slots) {
				if (thisSlot.NumeroEmplacement == emplacement) {

					transform.parent = thisSlot.transform;
					endPosition = thisSlot.transform.position + new Vector3(0f,0f,-0.1f);
					StartLerping();
					break;
				}
			}
		}
	}


	void StartLerping() {
		
		timeStartedLerping = Time.time;
		startPosition = transform.position;
		_isLerping = true;

	}


    void FixedUpdate () {
    	if (_isLerping)
    		{
    			float timeSinceStarted = Time.time - timeStartedLerping;
    			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
    			transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
    			if (percentageComplete >=1.0f) {
    				_isLerping = false;
    			}
    		}

    }

	#endregion



	#region name Effets Speciaux et modif des stats


	//la carte passive est soignee, appel depuis main1 ou main2
	public void HealOnPassif (int ToHeal)
	{
		CurrentVie += ToHeal;
		if (CurrentVie > 20) {
			CurrentVie = 20;
		}
		goUI.SetValues(CurrentVie, CurrentAtt, CurrentDef);
		GetComponent<Bark>().dammagesTaken(2, ToHeal);
	}

	//appelle de main1 ou 2 pour appliquer les effets du passif d'une carte.
	public void ApplyBonus (int[] Effets)
	{
		

		if (Effets[3] == 0) {

			BonusDef = Effets[2];						//ici il va y avoir collision avec les bonus adverses TODO Faire en sorte qu'un bonus ne remplace pas l'autre
			CurrentDef = Defense + BonusDef;

			goUI.SetValues(CurrentVie, CurrentAtt, CurrentDef);

			GetComponent<Bark>().dammagesTaken(0, Effets[2]);
		}
		if (Effets[3] == 1) {
			BonusAtt = Effets[2];
			CurrentAtt = Attaque + BonusAtt;

			goUI.SetValues(CurrentVie, CurrentAtt, CurrentDef);

			GetComponent<Bark>().dammagesTaken(1, Effets[2]);
		}
		if (Effets[3] == 2) {
			BonusDef = Effets[2];
			BonusAtt = Effets[2];
			CurrentAtt = Attaque + BonusAtt;
			CurrentDef = Defense + BonusDef;

			goUI.SetValues(CurrentVie, CurrentAtt, CurrentDef);

			GetComponent<Bark>().dammagesTaken(1, Effets[2]);
			GetComponent<Bark>().dammagesTaken(0, Effets[2]);
		}
	}
	#endregion



	//ca on s'en fout, ca marche plutot bien :)
	#region name Recevoir les attaques
	void DealWithAttack () {
		int forceAtt = GameObject.FindWithTag("AttPotentiel").GetComponent<Card>().Attaque + GameObject.FindWithTag("AttPotentiel").GetComponent<Card>().BonusAtt;
		int TypeAtt = GameObject.FindWithTag("AttPotentiel").GetComponent<Card>().Type;
		DealDammage(forceAtt, TypeAtt);
		GameObject.FindWithTag("AttPotentiel").GetComponent<Card>().hasBeenUsed = true;
		GameObject.FindWithTag("AttPotentiel").tag = "actif";
	}

	public int EffectivenessCheck (int typeDef, int typeAtt)
	{
		int final = 0;
		if (typeDef == 1) {
			if (typeAtt == 2) { final = -1;}
			if (typeAtt == 3) {final = +1;
			}
		} else if (typeDef == 2) {
			if (typeAtt == 1) {final = +1;
			}
			if (typeAtt == 3) {final = -1;
			}
		} else if (typeDef == 3) {
			if (typeAtt == 1) {final = -1;
			}
			if (typeAtt == 2) {final = +1;
			}
		}
		return final;
		// if gameobject.find9('passif').Changementdefficacite() != null alors lancer la methode ???
	}

	public void DealDammage (int forceAtt, int TypeAtt)
	{

		//calcul des degats
		int typeRes = EffectivenessCheck (Type, TypeAtt);
		int degats = forceAtt - Defense - BonusDef + typeRes;


		// on envoie le cameraShake
		if (typeRes == -1) {
			GameObject.FindObjectOfType<CameraShake>().PlayShake(1);
		} else if (typeRes == 0) {
			GameObject.FindObjectOfType<CameraShake>().PlayShake(2);
		} else if (typeRes == +1) {
			GameObject.FindObjectOfType<CameraShake>().PlayShake(3);
		}

		//clamping de degats a 0 (ou a 1 ?) au minimum 
		if (degats < 0) {
			degats = 0;
		}

		CurrentVie -= degats;
		StartCoroutine(GetComponent<Bark>().flinch(emplacement, BelongsToP1, degats));
		goUI.SetValues(CurrentVie, CurrentAtt, CurrentDef);
		GetComponent<Bark>().dammagesTaken(4, degats);

		if (CurrentVie <= 0) {
			gameObject.tag = ("mourant");
			GameManager.DammageMessage(Name, 11);					//11 tue d'un coup, c'est donc le mdp pour dire que la carte est morte.
			DyingCard ();
		} else {
			GameManager.DammageMessage(Name, degats);
		}

	}

	void DyingCard ()
	{

		if (BelongsToP1) {
			GameObject.FindObjectOfType<Main1>().Dying(emplacement);
		} else {
			GameObject.FindObjectOfType<Main2>().Dying(emplacement);
		}
				
		

	}

	public void Suppr(){
		Destroy(gameObject);
	}
	#endregion



}
