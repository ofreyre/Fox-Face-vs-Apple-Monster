using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorWrapper : MonoBehaviour {

    Animator m_animator;

    // Use this for initialization
    void Awake () {
        m_animator = gameObject.GetComponent<Animator>();
    }
	
	public float speed
    {
        get { return m_animator.speed; }
        set { m_animator.speed = value; }
    }
}
