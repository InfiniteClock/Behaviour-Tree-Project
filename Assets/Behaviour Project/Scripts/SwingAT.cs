using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class SwingAT : ActionTask {

		public BBParameter<Transform> target;
		public BBParameter<Transform> hilt;
		public float swingTime;
		public float swingAngle;
		
		private float timer;
		private Vector3 startAngle;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			timer = 0f;
			startAngle = hilt.value.forward;


            // Determine if the target is closer to swing at clockwise or counterclockwise
            //if (Vector3.Dot(startAngle, (agent.transform.position - target.value.position).normalized) > 0)
            if (Vector3.SignedAngle(startAngle, (agent.transform.position - target.value.position).normalized, Vector3.up) > 0)
                swingAngle = Mathf.Abs(swingAngle) * -1;
			else
				swingAngle = Mathf.Abs(swingAngle);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			// Swing the sword from start to end over the duration of swing
			timer += Time.deltaTime;
			Vector3 rotation = new Vector3(0f, (swingAngle/swingTime) * Time.deltaTime, 0f);
			hilt.value.Rotate(rotation);
			if (timer >= swingTime)
			{
				EndAction(true);
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			hilt.value.forward = agent.transform.forward;
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}