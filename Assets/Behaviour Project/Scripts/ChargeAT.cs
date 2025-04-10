using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Threading;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;

namespace NodeCanvas.Tasks.Actions {

	public class ChargeAT : ActionTask {

		public BBParameter<Transform> target;
		public BBParameter<int> powerLevel;
		public BBParameter<Color> currentColor;
        public float chargeTime;

        public Color chargeMax;

		private LineRenderer lr;
		private float timer;
		private float t;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			lr = agent.GetComponent<LineRenderer>();
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			timer = 0;
			// Enable the line renderer and make it green
			lr.enabled = true;
			lr.material.color = Color.green;
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			// Adjust the color of the character over time while charging
			timer += Time.deltaTime;
            t = timer / chargeTime;
            agent.GetComponent<MeshRenderer>().materials[0].color = Color.Lerp(currentColor.value, chargeMax, t);
			
			// Set the line renderer to draw a green line from character that extends to target over time
			Vector3 linePos = Vector3.Lerp(agent.transform.position, target.value.position, t);
            lr.SetPosition(0, agent.transform.position);
            lr.SetPosition(1, linePos);

            if (timer >= chargeTime)
			{
				// Reset the color before launching into the dash
				agent.GetComponent<MeshRenderer>().materials[0].color = currentColor.value;
                EndAction(true);
			}
		}
		
		//Called when the task is disabled.
		protected override void OnStop() {
			// Disable the line renderer. The Dash will re-enable it, but in case there is an interuption this prevents it from lingering
			lr.enabled = false;
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}