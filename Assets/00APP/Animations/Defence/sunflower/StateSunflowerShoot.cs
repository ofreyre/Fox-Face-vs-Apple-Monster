using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSunflowerShoot : StateMachineBehaviour
{
    public Vector2 relativeCenter;
    public float minAngle = 0;
    public float maxAngle = 160;
    public float dAngle = 10;
    public float radius = 0.8f;
    UNITTYPE type;
    float angle;
    bool initialized;
    bool active;
    Transform m_transform;
    Vector2 center;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!initialized)
        {
            initialized = true;
            type = animator.GetComponent<UnitType>().type;
            angle = 0;
            center = new Vector2(animator.transform.position.x, animator.transform.position.y) + relativeCenter;
        }

        int babies = UnitsSpawner.instance.GetMissPacmanBankItem(type).babies;
        for (int i = 0; i < babies; i++)
        {
            UnitsSpawner.instance.SpawnBullet(type, new Vector3(center.x + Mathf.Sin((minAngle + angle) * Mathf.Deg2Rad) * radius, center.y + Mathf.Cos((minAngle + angle) * Mathf.Deg2Rad) * radius, 0));
            angle = (angle + dAngle) % (maxAngle - minAngle);
        }
        animator.speed = animator.GetComponent<ShootData>().animatorSpeed;
        //m_animator.SetTrigger("shootEnd");
    }
}
