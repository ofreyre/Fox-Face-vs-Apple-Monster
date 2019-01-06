using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMousetrapFall : StateMachineBehaviour {

    Vector3 m_arrivePos;
    Vector3 m_velocity;
    bool m_initialized;
    Transform m_transform;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!m_initialized)
        {
            m_transform = animator.transform;
            m_initialized = true;
        }
        m_arrivePos = EarnMousetrap.instance.m_arrivePos;
        m_velocity = new Vector3(0, EarnMousetrap.instance.m_fallSpeed, 0);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        m_transform.position += m_velocity * Time.deltaTime;
        if (m_transform.position.y < m_arrivePos.y)
        {
            m_transform.position = m_arrivePos;
            AnimatorController.instance.Idle(animator);
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
