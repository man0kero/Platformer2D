using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool _isAttack;
    public bool IsAttack {get => _isAttack;}
    [SerializeField] private AudioSource attackSound;


        public void FinishAttack() {
        _isAttack = false;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.K)) {
            Attack();
        }
    }

    public void Attack() {
        _isAttack = true;
            animator.SetTrigger("attack");
            attackSound.Play();
    }
}
