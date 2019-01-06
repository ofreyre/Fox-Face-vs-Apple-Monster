using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public enum ANIMATORSTATES
    {
        none,
        idle,
        hit,
        shoot,
        shootEnd,
        die,
        end,
        aim
    }

    public struct ANIMATORSTATEScomparer : IEqualityComparer<ANIMATORSTATES>
    {
        public bool Equals(ANIMATORSTATES x, ANIMATORSTATES y)
        {
            return x == y;
        }

        public int GetHashCode(ANIMATORSTATES obj)
        {
            // you need to do some thinking here,
            return (int)obj;
        }
    }

    public static ANIMATORSTATEScomparer AnimatorStatesComparer = new ANIMATORSTATEScomparer();

    public static AnimatorController instance;
    int triggerEnd, triggerIdle, triggerShoot, triggerMove, triggerHit, triggerAim, triggerAttack;
    int stateDie;

    void Awake()
    {
        instance = this;
        triggerEnd = Animator.StringToHash("end");
        triggerIdle = Animator.StringToHash("idle");
        triggerShoot = Animator.StringToHash("shoot");
        triggerMove = Animator.StringToHash("move");
        triggerHit = Animator.StringToHash("hit");
        stateDie = Animator.StringToHash("die");
        triggerAim = Animator.StringToHash("aim");
        triggerAttack = Animator.StringToHash("attack");
    }

    public void Die(Animator animator)
    {
        //animator.SetTrigger(triggerDie);
        animator.Play(stateDie);
    }

    public void End(Animator animator)
    {
        animator.SetTrigger(triggerEnd);
    }

    public void Idle(Animator animator)
    {
        animator.SetTrigger(triggerIdle);
    }

    public void Shoot(Animator animator)
    {
        animator.SetTrigger(triggerShoot);
    }

    public void Move(Animator animator)
    {
        animator.SetTrigger(triggerMove);
    }

    public void Hit(Animator animator)
    {
        animator.SetTrigger(triggerHit);
    }

    public void Aim(Animator animator)
    {
        animator.SetTrigger(triggerAim);
    }

    public void Attack(Animator animator)
    {
        animator.SetTrigger(triggerAttack);
    }
}
