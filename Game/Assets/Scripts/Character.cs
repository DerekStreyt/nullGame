using UnityEngine;

public class Character : UnitWithHealth
{
    public string dieAnimationTriggerName = "IsDie";
    protected Animator animator;
    protected CharacterController characterController;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    protected override void OnDie()
    {
        animator.SetTrigger(dieAnimationTriggerName);
        characterController.enabled = false;
    }
}
