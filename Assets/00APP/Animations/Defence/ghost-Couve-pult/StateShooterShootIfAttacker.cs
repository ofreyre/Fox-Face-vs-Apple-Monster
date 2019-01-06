using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateShooterShootIfAttacker : StateMachineBehaviour
{
    Vector3 m_relativePosition;
    bool initialized;
    bool enabled;
    float t;
    Attacker m_localAttacker;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!initialized)
        {
            initialized = true;
            m_relativePosition = animator.GetComponent<ShootData>().relativePosition;
        }
        t = Time.time + GameSettings.instance.m_gameConstants.checkLapseDurationCollisions;
        enabled = true;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enabled && t < Time.time)
        {
            m_localAttacker = CollisionManager.instance.GetFirstRightAttacker(animator.transform.position, (animator.transform.position + m_relativePosition).x);
            if (m_localAttacker != null)
            {
                animator.GetComponent<ElipticTarget>().attacker = m_localAttacker;
                AnimatorController.instance.Shoot(animator);
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
