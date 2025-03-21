using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections.Generic;


namespace NodeCanvas.Tasks.Conditions {

	public class OnPelletCT : ConditionTask {

		public BBParameter<List<Transform>> pellets;
		public BBParameter<Transform> closestPellet;
		public float offset;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			foreach (Transform pellet in pellets.value)
			{
				if (!pellet.gameObject.activeSelf)
					continue;
				if ((agent.transform.position - pellet.position).magnitude < offset)
				{
					closestPellet.SetValue(pellet); 
					return true;
				}
			}
			return (false);
		}
	}
}