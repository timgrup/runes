using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("attackTrigger");
    }
}