using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavePeopleManager : MonoBehaviour {

		public GameObject rain;
		public GameObject stick;
		public Transform stickSpawn;
		public float stickRespawnTime = 10f;
		public float rainRespawnTime = 15f;
		public static bool isRaining;


		void Start(){

			StartCoroutine(manageScene());

		}


		void Update(){

		}




		IEnumerator manageScene(){

			
			if(stick.activeSelf ==false){
				yield return WaitForSeconds(stickRespawnTime);
				stick.SetActive(true);
			}
			
			if(rain.activeSelf ==false){
				yield return WaitForSeconds(rainRespawnTime);
				cavePeopleAgent.isRaining=true;
				rain.SetActive(true);
				yield return WaitForSeconds(rainRespawnTime);
				cavePeopleAgent.isRaining=false;
				rain.SetActive(false);
			}

		}
