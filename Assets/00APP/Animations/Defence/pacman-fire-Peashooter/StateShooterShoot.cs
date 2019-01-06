using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateShooterShoot : StateMachineBehaviour {

    public Vector3 m_relativePosition;
    bool initialized;
    UNITTYPE type;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!initialized)
        {
            initialized = true;
            type = animator.GetComponent<UnitType>().type;
            m_relativePosition = animator.GetComponent<ShootData>().relativePosition;
        }
        UnitsSpawner.instance.Shoot(type, animator.transform.position + m_relativePosition);
	}
}
