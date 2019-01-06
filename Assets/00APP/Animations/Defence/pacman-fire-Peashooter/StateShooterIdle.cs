using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateShooterIdle : StateMachineBehaviour {

    public float reloadDuration;
    float t;
    bool enabled;
    bool initialized;
    Attacker attacker;
    //ColliderLinear collider;
    float bulletX;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!initialized)
        {
            initialized = true;
            animator.speed = animator.GetComponent<ShootData>().animatorSpeed;
            reloadDuration = animator.GetComponent<ShootData>().reloadDuration;
            //collider = animator.GetComponent<ColliderLinear>();
            bulletX = animator.GetComponent<ColliderLinear>().limitX;
        }
        t = Time.time + reloadDuration;
        enabled = true;
    }
    	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (enabled && t < Time.time)
        {
            //if (CollisionManager.instance.ExistRightAttacker(animator.transform.position, animator.GetComponent<ColliderLinear>().limitX))
            //Debug.Log("Exist attacker "+CollisionManager.instance.ExistAttackerAtRightInBulletRange(animator.transform.position, animator.transform.position.x + bulletX));
            if (CollisionManager.instance.ExistAttackerAtRightInBulletRange(animator.transform.position, animator.transform.position.x + bulletX))
            {
                animator.SetTrigger("shoot");
                enabled = false;
            }
            else
            {
                t = Time.time + GameSettings.instance.m_gameConstants.checkLapseDurationCollisions;
            }
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
