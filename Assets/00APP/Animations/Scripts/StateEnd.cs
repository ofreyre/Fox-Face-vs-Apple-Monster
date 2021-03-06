﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnd : StateMachineBehaviour {
    public float duration = 1;
    int initialized;
    float t;
    protected bool enabled;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t = Time.time + duration;
        enabled = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enabled && t < Time.time)
        {
            enabled = false;
            animator.gameObject.SetActive(false);
        }
    }
}
