using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateShootOnAttacker : StateMachineBehaviour {

    Vector2 m_colRange;
    Vector2Int m_rowRange;
    UNITTYPE m_type;
    float m_t;
    bool enabled;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_type == UNITTYPE.none)
        {
            m_type = animator.GetComponent<UnitType>().type;
        }
        m_colRange = CollisionManager.instance.GetColRangeWorld(animator.transform.position, m_type);
        m_rowRange = CollisionManager.instance.GetRowRange(animator.transform.position, m_type);
        m_t = Time.time + GameSettings.instance.m_gameConstants.checkLapseDurationCollisions;
        enabled = true;
    }
    
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (enabled && m_t < Time.time)
        {
            if (CollisionManager.instance.IsAttackerInRange(m_rowRange.x, m_rowRange.y, m_colRange.x, m_colRange.y))
            {
                enabled = false;
                AnimatorController.instance.Shoot(animator);
            }
            else
            {
                m_t = Time.time + GameSettings.instance.m_gameConstants.checkLapseDurationCollisions;
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
