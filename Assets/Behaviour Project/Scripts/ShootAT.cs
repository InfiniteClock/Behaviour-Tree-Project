using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ShootAT : ActionTask {

		
		public BBParameter<Transform> target;
        public GameObject projectile;
		[Tooltip("How far in degrees can a shot fire off target?")]
        public float shotConeWidth;
		public Transform projectileSpawn;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			// Create a projectile in the right location
            GameObject shot = GameObject.Instantiate(projectile);
			Vector3 pos = projectileSpawn.position;
            shot.transform.position = pos;

			// Set the direction of the projectile towards the target
            Vector3 directionToTarget = (target.value.position - shot.transform.position).normalized;
			directionToTarget.y = 0;
            shot.transform.forward = directionToTarget;
			// Add a random offset to the angle towards the target 
            float randomAngle = Random.Range(-shotConeWidth, shotConeWidth);
			// Apply the rotation
			shot.transform.Rotate(0, randomAngle, 0);
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