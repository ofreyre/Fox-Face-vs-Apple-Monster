using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSunflowerIdle : StateMachineBehaviour {
    bool initialized;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!initialized)
        {
            initialized = true;
            animator.speed = animator.GetComponent<ShootData>().animatorSpeed / UnitsSpawner.instance.GetMissPacmanBankItem(animator.GetComponent<UnitType>().type).timeFirstDeliver;
        }
        else
        {
            animator.speed = animator.GetComponent<ShootData>().animatorSpeed / UnitsSpawner.instance.GetMissPacmanBankItem(animator.GetComponent<UnitType>().type).timeDeliver;
        }
    }
}
