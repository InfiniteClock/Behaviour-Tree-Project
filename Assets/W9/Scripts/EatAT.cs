using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class EatAT : ActionTask {

		public BBParameter<List<Transform>> pellets;
		public BBParameter<Transform> closestPellet;
		public BBParameter<Transform> target;
		public float eatTime;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			StartCoroutine(EatPellet(closestPellet.value, eatTime));
		}
		IEnumerator EatPellet(Transform pellet, float time)
		{
			float timer = 0f;
			Vector3 startSize = pellet.localScale;
			while (timer < time)
			{
				timer += Time.deltaTime;
				pellet.localScale = Vector3.Lerp(startSize, Vector3.zero, timer/time);
				yield return null;
			}
			pellet.localScale = Vector3.one * 0.5f;
			if (pellet != target.value)
				pellet.gameObject.SetActive(false);
			EndAction(true);
		}
		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}