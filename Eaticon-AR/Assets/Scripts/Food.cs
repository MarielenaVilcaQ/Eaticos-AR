using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] public int value;

    public void StartAnimationDestroy()
    {
        animator.SetBool("Destroy", true);
    }
}
