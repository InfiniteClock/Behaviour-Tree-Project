using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class LeapAT : ActionTask {

		public BBParameter<Transform> target;
		public BBParameter<Transform> swordHilt;
		public BBParameter<int> powerLevel;
        public float dashTime;

        private Vector3 destination;
		private Vector3 start;
		private float timer;
		private LineRenderer lr;
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
			
			// Set the destination and start positions so there is no tracking mid-leap
			destination = target.value.position;
			start = agent.transform.position;
            destination.y = start.y;
            timer = 0f;

            // Set the sword behind when launching the leap
            swordHilt.value.forward = -agent.transform.forward;

			// Enable the line renderer and make it red
			lr.enabled = true;
			lr.material.color = Color.red;
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			// Move to the target destination over dashTime
			timer += Time.deltaTime;
			float t = timer / dashTime;
			agent.transform.position = Vector3.Lerp(start, destination, t);

			// Draw a line from character to destination
			lr.SetPosition(0, agent.transform.position);
			lr.SetPosition(1, destination);
			
			// Wildly spin the sword based on power level
			switch (powerLevel.value)
			{
				case 0:
                    swordHilt.value.Rotate(0f, 0f, 0f);
                    break;
                case 1:
                    swordHilt.value.Rotate(0f, 360f / dashTime * Time.deltaTime, 0f);
					break;
                case 2:
					swordHilt.value.Rotate(0f, 720f / dashTime * Time.deltaTime, 0f);
					break;
			}
			if (timer >= dashTime)
			{
				EndAction(true);
			}
		}
        
        //Called when the task is disabled.
        protected override void OnStop() {
			// Disable the line renderer when finished
			lr.enabled = false;
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}