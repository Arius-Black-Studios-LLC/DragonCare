using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimManager : MonoBehaviour
{

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSleepingTrue()
    {
        animator.SetBool("Sleeping", true);
    }

    public void SetSleepingFalse()
    {
        animator.SetBool("Sleeping", false);
    }

    public void SetAnimatorVelocity(Vector3 movement)
    {
        animator.SetFloat("Movement", movement.x);
        animator.SetFloat("Rotation", movement.z);
    }



}
