using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveYToTargetCyclic : MonoBehaviour {
    public Vector3 m_targetPos;
    public Vector3 m_velocity;
    Vector3 m_startPos;

    public void Animate()
    {
        m_startPos = transform.localPosition;
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        transform.localPosition += m_velocity * Time.deltaTime;
        if (transform.localPosition.x > m_targetPos.x)
        {
            transform.localPosition = m_startPos;
        }
    }
}
