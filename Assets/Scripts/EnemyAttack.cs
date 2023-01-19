using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float timeTodamage = 1f;

    private float _damagetime;
    private bool _isDamage = true;

    private void Start() {
        _damagetime = timeTodamage;
    }

    private void Update() {
        if(!_isDamage) {
            _damagetime -= Time.deltaTime;
            if(_damagetime <= 0f) {
                _isDamage = true;
                _damagetime = timeTodamage;
            }        
        }
    }
    private void OnCollisionStay2D (Collision2D other) {
      PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

      if(playerHealth != null && _isDamage) {
          playerHealth.ReduceHealth(damage);
          _isDamage = false;
      }
    }
}
