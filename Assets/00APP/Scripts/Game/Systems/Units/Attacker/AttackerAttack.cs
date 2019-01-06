using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAttack : MonoBehaviour {
    public float m_range = -0.469f;
    public float m_hit = 1;
    public int targetI, targetJ;
    public float idelTime;
    Animator m_animator;

    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
    }

    public void Begin(int targetI, int targetJ)
    {
        this.targetI = targetI;
        this.targetJ = targetJ;
        AnimatorController.instance.Attack(m_animator);
    }
}
