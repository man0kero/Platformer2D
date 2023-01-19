using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chasingSpeed = 3f;
    [SerializeField] private float timeToWait = 5f;
    [SerializeField] private float timeToChase = 3f;
    [SerializeField] private float minDistanceToPlayer = 1.5f;
    [SerializeField] private Transform enemyModelTransform;
    

    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private Vector2 _leftBoundaryPosition;
    private Vector2 _rightBoundaryPosition;
    private Vector2 nextPoint;

    private float _waitTime;
    private float _chaseTime;
    private float _walkSpeed;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _isChasingPlayer;

    public bool IsFacingRight {
        get => _isFacingRight;
    }

    public void StartChasingPlayer() {
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeed = chasingSpeed;
    }

    private void Start() {

        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        _rb = GetComponent<Rigidbody2D>();
        _leftBoundaryPosition = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosition + Vector2.right * walkDistance;
        _waitTime = timeToWait;

        _chaseTime = timeToChase;

        _walkSpeed = patrolSpeed;
    }

    private void Update() {

        if(_isChasingPlayer) {
            StartChasingTimer();
        }

        if(_isWait && !_isChasingPlayer) {
            StartWaitTimer();
        }

         if(ShouldWait()) {
             _isWait = true;
         }
    }

    private void FixedUpdate () {
        nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if(_isChasingPlayer && Mathf.Abs(DistanceToPlayer()) < minDistanceToPlayer) {
            return;
        }

        if(_isChasingPlayer) {
            ChasePlayer();
        }

        if(!_isWait && !_isChasingPlayer) {
            Patrol();
        }
    }

    private void Patrol() {
        if(!_isFacingRight) {
            nextPoint.x *= -1;
        }
         _rb.MovePosition((Vector2)transform.position + nextPoint);
    }

    private void ChasePlayer() {
        float distance = DistanceToPlayer();
        if(distance < 0.2f) {
             nextPoint.x *= -1;
        }

        if(distance > 0.2f && !_isFacingRight) {
            Flip();
        } else if(distance < 0 && IsFacingRight) {
            Flip();
        }
       
        _rb.MovePosition((Vector2)transform.position + nextPoint);
    }

    private float DistanceToPlayer() {
        return _playerTransform.position.x - transform.position.x;
    }
    private void StartWaitTimer() {

         _waitTime -= Time.deltaTime;

        if(_waitTime < 0f) {
                _waitTime = timeToWait;
                _isWait = false;
                Flip();
            }
    }

    private void StartChasingTimer() {
        _chaseTime -= Time.deltaTime;

        if(_chaseTime < 0f) {
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
            _walkSpeed = patrolSpeed;
        }
    }

    private bool ShouldWait() {

        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _rightBoundaryPosition.x;
        bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= _leftBoundaryPosition.x;

        return isOutOfLeftBoundary || isOutOfRightBoundary;
    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundaryPosition, _rightBoundaryPosition);
    }

    void Flip() {

        _isFacingRight = !_isFacingRight;
            Vector3 playerScale = enemyModelTransform.localScale; 
            playerScale.x *= -1;
            enemyModelTransform.localScale = playerScale;
    }

    void OnCollisionEnter2D (Collision2D other) {
        if(other.gameObject.CompareTag("Respawn")) {
            Debug.Log("Respawn");
            Patrol();
        }
    }

}
