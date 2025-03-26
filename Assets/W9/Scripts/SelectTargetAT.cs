using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class SelectTargetAT : ActionTask {

        public GameObject pelletParent;

        public BBParameter<List<Transform>> pellets;
		public BBParameter<Transform> target;

		private NavMeshAgent navAgent;
		private List<Transform> activeTargets;
        private List<Transform> pelletChildren;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			navAgent = agent.GetComponent<NavMeshAgent>();

            pelletChildren = new List<Transform>();
            for (int i = 0; i < pelletParent.transform.childCount; i++)
            {
                pelletChildren.Add(pelletParent.transform.GetChild(i));
            }
            pellets.SetValue(pelletChildren);
            foreach (Transform child in pelletChildren)
            {
                bool active = Random.Range(0, 2) == 1;
                if (!active)
                {
                    child.gameObject.SetActive(false);
                }
            }
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			activeTargets = new List<Transform>();
			foreach (Transform pellet in pellets.value)
			{
				if (pellet.gameObject.activeSelf)
				{
					activeTargets.Add(pellet);
				}
			}
			int i = Random.Range(0, activeTargets.Count);
			target.SetValue(activeTargets[i]);

            NavMeshHit hit;
            NavMesh.SamplePosition(target.value.position, out hit, 10, NavMesh.AllAreas);
            navAgent.SetDestination(hit.position);

			target.value.localScale = Vector3.one;
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