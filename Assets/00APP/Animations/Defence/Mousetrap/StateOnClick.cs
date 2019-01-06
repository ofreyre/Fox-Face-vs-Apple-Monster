using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOnClick : StateMachineBehaviour {

    public Vector2 m_clickSize;
    Vector4 m_clickRect;
    Camera camera;
    bool initialized;
    [HideInInspector]
    public bool enabled;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!initialized)
        {
            camera = Camera.main;
            initialized = true;
        }
        Vector3 v = animator.transform.position;
        Vector3 v0 = camera.WorldToScreenPoint(new Vector3(v.x - m_clickSize.x * 0.5f, v.y, 0));
        Vector3 v1 = camera.WorldToScreenPoint(new Vector3(v.x + m_clickSize.x * 0.5f, v.y + m_clickSize.y, 0));
        m_clickRect = new Vector4(v0.x, v0.y, v1.x, v1.y);
        enabled = true;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (enabled && Input.GetMouseButtonUp(0))
        {
            Vector3 v = Input.mousePosition;
            if (m_clickRect.x < v.x && m_clickRect.y < v.y && m_clickRect.z > v.x && m_clickRect.w > v.y)
            {
                enabled = false;
                EarnMousetrap.instance.OnMousetrapClick(this);
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
