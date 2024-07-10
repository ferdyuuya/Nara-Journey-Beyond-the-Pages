using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleWalk : MonoBehaviour
{
    private Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Animator != null)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                Animator.SetBool("isWalking", true);
            }
            else
            {
                Animator.SetBool("isWalking", false);
            }
        }
        else
        {
            Animator.SetBool("isWalking", false);
        }
    }
}
