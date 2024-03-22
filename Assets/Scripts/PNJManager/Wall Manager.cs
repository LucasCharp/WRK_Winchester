using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WallManager : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PNJ") && animator.GetBool("willDance") == false)
        {
            GoBackInPub(other);
        }
    }

    public void GoBackInPub(Collider other)
    {
        animator = other.GetComponent<Animator>();
        int actions = animator.GetInteger("actions");
        if (-1 < actions && actions < 3)
        {
            animator.SetBool("shouldGoIn", true);
            animator.SetBool("isOutOfWall", true);
        }
    }
}
