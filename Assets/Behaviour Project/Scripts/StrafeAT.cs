using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class StrafeAT : ActionTask {

		public BBParameter<Movement> movement;
		public BBParameter<bool> clockwise;
		public BBParameter<Transform> target;
		public float steeringAcceleration;
		public float boundaryRange;
        public Vector3 mapCenter;
		public float rotationDirectionOffset;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
			// Get the direction to the center of arena and the world direction of "left"
			Vector3 dirToCenter = mapCenter - agent.transform.position;
			dirToCenter = new Vector3(dirToCenter.x, 0, dirToCenter.z);
			Vector3 left = Vector3.Cross(dirToCenter, Vector3.up);
			
			// Determine if we are strafing towards target or away, and change direction if not within offset
			Vector3 dirToTarget = target.value.position - agent.transform.position;
			if (Vector3.Dot(left.normalized, dirToTarget.normalized) > rotationDirectionOffset)
				clockwise.value = true;
            else if (Vector3.Dot(left.normalized, dirToTarget.normalized) < -rotationDirectionOffset)
				clockwise.value = false;
			dirToTarget.y = 0;
			// Set rotation towards the target
			agent.transform.forward = dirToTarget;

			// Add acceleration in the strafing direction
            if (clockwise.value)
			{
				movement.value.acceleration -= left * steeringAcceleration * Time.deltaTime;
			}
			else
			{
                movement.value.acceleration += left * steeringAcceleration * Time.deltaTime;
			}

			// Remain within the boundary circle
			if (dirToCenter.magnitude > boundaryRange)
			{
                movement.value.acceleration += dirToCenter * steeringAcceleration * Time.deltaTime;
			}
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