using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {


	
	private float duration = 0.23f;
	private float magnitude = 0.2f;
	private float damperProportion = 0.3f;
	private popup Pop;

	public bool Shaking = false;


	
	public void PlayShake (int shakeType)
	{
		
		StopAllCoroutines ();
		if (shakeType == 1) {
			duration = 0.07f;
			magnitude = 0.03f;
			damperProportion = 0.75f;
		}
		if (shakeType == 2) {
			duration = 0.13f;
			magnitude = 0.09f;
			damperProportion = 0.7f;
		}
		if (shakeType == 3) {
			duration = 0.20f;
			magnitude = 0.2f;
			damperProportion = 0.6f;
		}

		StartCoroutine(Shake());
	}


	IEnumerator Shake() {
		
		float elapsed = 0.0f;
		float DamperValue = 1/damperProportion;
		Vector3 originalCamPos = Camera.main.transform.position;

		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;			
			
			float percentComplete = elapsed / duration;			
			
			// We want to reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(DamperValue * percentComplete - 1.0f, 0.0f, 1.0f);
			
			Vector2 ShakePos = Random.insideUnitCircle * damper * magnitude;
			
			Camera.main.transform.position = new Vector3(ShakePos[0], ShakePos[1], originalCamPos.z);
				
			yield return null;
		}
		Camera.main.transform.position = originalCamPos;
	}


}
