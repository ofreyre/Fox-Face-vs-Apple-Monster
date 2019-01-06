using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateThreepeaterIdle : StateMachineBehaviour {

    float reloadDuration;
    float t;
    bool enabled;
    bool initialized;
    int row;
    Vector3 m_podUp, m_posDown;
    float posX;
    float limitX;
    int m_cellsY;
    CollisionManager colisionManager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!initialized)
        {
            initialized = true;
            animator.speed = animator.GetComponent<ShootData>().animatorSpeed;
            reloadDuration = animator.GetComponent<ShootData>().reloadDuration;
            m_cellsY = Map.instance.m_cellsY - 1;
            colisionManager = CollisionManager.instance;
        }
        row = Map.instance.World2GridJ(animator.transform.position.y);
        posX = animator.transform.position.x;
        limitX = animator.GetComponent<ColliderLinear>().limitX;
        t = Time.time + reloadDuration;
        enabled = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enabled && t < Time.time)
        {
            if (colisionManager.ExistRightAttacker(animator.transform.position, limitX) ||
                (row > 0 && colisionManager.ExistRightAttacker(row - 1, posX, limitX)) ||
                (row < m_cellsY && colisionManager.ExistRightAttacker(row + 1, posX, limitX))
                )
            {
                animator.SetTrigger("shoot");
                enabled = false;
            }
            else
            {
                t = Time.time + reloadDuration;
            }
        }
    }
}
