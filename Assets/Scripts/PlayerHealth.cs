using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator animator;
    [SerializeField] private float totalHealth = 100f;

    private float _health;

    private void Start() {
        _health = totalHealth;
        InitHealth();
    }
    

    public void ReduceHealth(float damage) {
        hitSound.Play();
        _health -= damage;
        InitHealth();
        animator.SetTrigger("takeDamage");
        if(_health <= 0f) {
            Die();
        }
    }

    private void InitHealth() {
        healthSlider.value = _health / totalHealth;
    }

    private void Die() {
        gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);

    }
}
