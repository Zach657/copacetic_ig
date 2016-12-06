using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]



    public class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target; // target to aim for

		private const float TARGETUPDATETIMER = 2000;

		private float timeSinceUpdate = 0;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;

			agent.SetDestination (target.position);
        }


        private void Update()
        {
			//Slight edits made to fix the AI while player is moving a lot
			if (timeSinceUpdate > TARGETUPDATETIMER) {
				//Debug.Log ("hit");
				timeSinceUpdate = 0;
				agent.SetDestination (target.position);
			}
			if (agent.remainingDistance > agent.stoppingDistance) {
				character.Move (agent.desiredVelocity, false, false);
			}
			else {
				character.Move (Vector3.zero, false, false);
			}
			timeSinceUpdate = timeSinceUpdate + (Time.deltaTime*1000);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
