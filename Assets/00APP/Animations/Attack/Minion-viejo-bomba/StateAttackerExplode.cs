using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackerExplode : StateMachineBehaviour {

    float m_hit;
    float m_range;
    bool m_initialized;
    MoveLinear m_linear;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!m_initialized)
        {
            m_initialized = true;
            m_hit = animator.GetComponent<AttackerAttack>().m_hit;
            m_range = animator.GetComponent<AttckerExplodeRange>().m_range;
            m_linear = animator.GetComponent<MoveLinear>();
        }
        m_linear.ToNormal();
        Map.instance.DamageUnitsRange(animator.transform.position, m_range, m_hit);
        CollisionManager.instance.RemoveAttacker(animator.transform);
    }
}
