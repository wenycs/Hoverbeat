using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.J)) 
        {
            animator.SetBool("isHit", true);
            StartCoroutine(HitDelay());
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.K)) 
        {
            animator.SetBool("isBlock", true);
            StartCoroutine(BlockDelay());
        }
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("isHit", false);
    }

    IEnumerator BlockDelay()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("isBlock", false);
    }
}
