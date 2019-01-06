using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackerHit : StateMachineBehaviour {


    bool initialized;
    AttackerAttack attack;
    UnitState unitState;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!initialized)
        {
            initialized = true;
            attack = animator.GetComponent<AttackerAttack>();
        }
        unitState = Map.instance.m_defenders[attack.targetI, attack.targetJ].transform.GetComponent<UnitState>();
        
        if (!unitState.Hit(attack.m_hit))
        {
            Map.instance.KillUnit(attack.targetI, attack.targetJ);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

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
