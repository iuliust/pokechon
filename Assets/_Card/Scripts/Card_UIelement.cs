using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_UIelement : MonoBehaviour {


	//le truc invoque car la classe Card, et qui permet d'afficher les differents compteurs
	//SetValues() est la fonction qui met a jour les compteurs, appeller en cas de coup recu ou de bonus de passif

	public Vignette image;
	public Meter VieMeter;
	public Meter AttMeter;
	public Meter DefMeter;
	public Slider HealthBar;
	public Text Nom;



	void Start () {
		
	}

	public void SetName(string Name){
		Nom.text = Name;
	}

	public void SetValues(int Vie, int Att, int Def){
		VieMeter.updateMeter(Vie);
		AttMeter.updateMeter(Att);
		DefMeter.updateMeter(Def);
		HealthBar.value = Vie;

	}

}
