using System;
using Unity.VisualScripting;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.Play("mining");
            animator.SetTrigger("StartMining");
        }
    }
}
