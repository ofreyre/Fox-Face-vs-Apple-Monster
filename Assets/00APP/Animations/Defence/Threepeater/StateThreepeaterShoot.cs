using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateThreepeaterShoot : StateMachineBehaviour {

    public Vector3 m_relativePosition;
    bool initialized;
    BULLETTYPE type;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!initialized)
        {
            initialized = true;
            type = UnitsSpawner.instance.m_unitBullet[animator.GetComponent<UnitType>().type];
            m_relativePosition = animator.GetComponent<ShootData>().relativePosition;
        }
        ThreepeaterSpawner.instance.Shoot(type, animator.transform.position + m_relativePosition);
    }
}
