using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackerAttack : StateMachineBehaviour {

    float idleTime;
    bool initialized;
    bool enabled;
    float t;
    AttackerAttack attack;
    UnitState unitState;
    MoveLinear move;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!initialized)
        {
            initialized = true;

            attack = animator.GetComponent<AttackerAttack>();
            move = animator.GetComponent<MoveLinear>();

            idleTime = attack.idelTime;
            t = 0;
        }
        else
        {
            t = Time.time + idleTime;
        }
        move.moving = false;
        unitState = Map.instance.m_defenders[attack.targetI, attack.targetJ].transform.GetComponent<UnitState>();
        enabled = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enabled && Time.time > t)
        {
            enabled = false;

            if (unitState.stamina > 0)
            {
                AnimatorController.instance.Hit(animator);
                GameAudioPlayer.instance.PlayMinionAttack();
            }
            else
            {
                AnimatorController.instance.Move(animator);
            }
        }
    }
}
