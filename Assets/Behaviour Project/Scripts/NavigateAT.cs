using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class NavigateAT : ActionTask {

		public BBParameter<Movement> movement;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            movement.value.velocity += movement.value.acceleration;
			float groundSpeed = Mathf.Sqrt(movement.value.velocity.x * movement.value.velocity.x + movement.value.velocity.z * movement.value.velocity.z);
			if (movement.value.maxSpeed < groundSpeed)
			{
				float cappedX = movement.value.velocity.x / groundSpeed * movement.value.maxSpeed;
				float cappedZ = movement.value.velocity.z / groundSpeed * movement.value.maxSpeed;
                movement.value.velocity = new Vector3(cappedX, movement.value.velocity.y, cappedZ);
			}
			agent.transform.position += movement.value.velocity * Time.deltaTime;

            movement.value.acceleration = Vector3.zero;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}