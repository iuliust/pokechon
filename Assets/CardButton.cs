using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour {

	//C'est la ou j'ai essaye de connecter les cartes avec un DB, sans succes


	public Text Name, Vie, Att, Def;

	public void Display (CardButton Card)
	{
		Name.text = Card.Name.ToString();
		Vie.text = Card.Vie.ToString();
		Att.text = Card.Att.ToString();
		Def.text = Card.Def.ToString();
	}
}
