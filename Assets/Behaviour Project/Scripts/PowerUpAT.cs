using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NodeCanvas.Tasks.Actions {

	public class PowerUpAT : ActionTask {

		public BBParameter<Movement> movement;
		public BBParameter<Health> health;
		public BBParameter<int> powerLevel;
		public BBParameter<int> maxPower;
		public BBParameter<Color> currentColor;

		public float invicibilityTime;
		public Color powerupFlash;
		public List<Color> powerLevels;

		private float timer;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
            currentColor.value = powerLevels[powerLevel.value];
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            
            // Heal the Character and power them up another stage
            if (powerLevel.value < maxPower.value)
			{
				health.value.Restore();
				timer = 0f;
				StartCoroutine(PowerUp());
			}
			// If Character is already at max power, then they die
			else
			{
				EndAction(false);
			}
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}
        private IEnumerator PowerUp()
        {
			// Play a flash animation as the character power-ups
            Material materialColor = agent.GetComponent<MeshRenderer>().materials[0];
            while (timer < invicibilityTime)
            {
                materialColor.color = powerupFlash;
                timer += 0.1f;
                yield return new WaitForSeconds(0.1f);

                materialColor.color = currentColor.value;
                timer += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
			powerLevel.value++;
            materialColor.color = powerLevels[powerLevel.value];
            currentColor.value = powerLevels[powerLevel.value];
            // Change the character's speed as they power-up
            movement.value.maxSpeed += movement.value.speedIncrease;		
			EndAction(true);
        }
        //Called when the task is disabled.
        protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}