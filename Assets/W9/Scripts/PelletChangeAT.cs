using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class PelletChangeAT : ActionTask {

        public BBParameter<List<Transform>> pellets;
		public BBParameter<Transform> target;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			foreach (Transform pellet in pellets.value)
			{
                pellet.gameObject.SetActive(true);
                bool active = Random.Range(0, 2) == 1;
                if (!active)
                {
                    pellet.gameObject.SetActive(false);
                }
                if (pellet == target.value)
                {
                    pellet.gameObject.SetActive(false);
                }
            }
			EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
            target.SetValue(null);
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}