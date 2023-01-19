using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverArm : MonoBehaviour
{
    private Finish finish;
    private Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
    }
    public void ActiveLeverArm() {
        animator.SetTrigger("activate");
        finish.Activate();
    }
}
