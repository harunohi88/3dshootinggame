using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Trace,
        Return,
        Attack,
        Damaged,
        Die,
    }

    public EnemyData EnemyData;

    private GameObject _player;
    private CharacterController _characterController;
    private Vector3 _startPosition;
    private float _attackTimer = 0f;
    private float _health;
    private Coroutine _waitPatrolCoroutine;
    private int _patrolIndex;
    private Vector3 _patrolDirection;
    private Vector3[] _patrolPoints = new Vector3[3];

    public EnemyState CurrentState = EnemyState.Idle;

    private void Start()
    {
        _health = EnemyData.MaxHealth;
        _startPosition = transform.position;
        _player = GameObject.FindGameObjectWithTag("Player");
        _characterController = GetComponent<CharacterController>();
        _patrolPoints[0] = transform.position;
        _patrolPoints[1] = transform.position + new Vector3(0f, 0f, EnemyData.PatrolDistance);
        _patrolPoints[2] = transform.position + new Vector3(EnemyData.PatrolDistance, 0f, 0f);
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case EnemyState.Idle:
            {
                Idle();
                break;
            }
            case EnemyState.Patrol:
            {
                Patrol();
                break;
            }
            case EnemyState.Trace:
            {
                Trace();
                break;
            }
            case EnemyState.Return:
            {
                Return();
                break;
            }
            case EnemyState.Attack:
            {
                Attack();
                break;
            }
        }
    }

    private void Idle()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < EnemyData.FindDistance)
        {
            StopCoroutine(_waitPatrolCoroutine);
            _waitPatrolCoroutine = null;
            CurrentState = EnemyState.Trace;
            Debug.Log("Transition: Idle -> Trace");
            return;
        }

        if (_waitPatrolCoroutine == null)
        {
            _waitPatrolCoroutine = StartCoroutine(WaitPatrol_Coroutine());
        }
    }

    private IEnumerator WaitPatrol_Coroutine()
    {
        yield return new WaitForSeconds(EnemyData.PatrolConversionTime);
        if (CurrentState == EnemyState.Idle)
        {
            CurrentState = EnemyState.Patrol;
            _patrolIndex = 1;
            _patrolDirection = _patrolPoints[_patrolIndex];
            _waitPatrolCoroutine = null;
            Debug.Log("Transition: Idle -> Patrol");
        }
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < EnemyData.FindDistance)
        {
            CurrentState = EnemyState.Trace;
            Debug.Log("Transition: Patrol -> Trace");
            return;
        }

        if (Vector3.Distance(transform.position, _patrolDirection) < EnemyData.MinMoveDistance)
        {
            transform.position = _patrolDirection;
            _patrolIndex = (_patrolIndex + 1) % _patrolPoints.Length;
            _patrolDirection = _patrolPoints[_patrolIndex];
        }

        Vector3 direction = (_patrolDirection - transform.position).normalized;
        _characterController.Move(direction * EnemyData.MoveSpeed * Time.deltaTime);
    }

    private void Trace()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > EnemyData.ReturnDistance)
        {
            CurrentState = EnemyState.Return;
            Debug.Log("Transition: Trace -> Return");
            return;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) < EnemyData.AttackDistance)
        {
            CurrentState = EnemyState.Attack;
            Debug.Log("Transition: Trace -> Attack");
            return;
        }

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _characterController.Move(direction * EnemyData.MoveSpeed * Time.deltaTime);
    }

    private void Return()
    {
        if (Vector3.Distance(transform.position, _startPosition) < EnemyData.MinMoveDistance)
        {
            transform.position = _startPosition;
            CurrentState = EnemyState.Idle;
            Debug.Log("Transition: Return -> Idle");
            return;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) < EnemyData.FindDistance)
        {
            CurrentState = EnemyState.Trace;
            Debug.Log("Transition: Return -> Trace");
            return;
        }

        Vector3 direction = (_startPosition - transform.position).normalized;

        _characterController.Move(direction * EnemyData.MoveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > EnemyData.AttackDistance)
        {
            CurrentState = EnemyState.Trace;
            _attackTimer = 0f;
            Debug.Log("Transition: Attack -> Trace");
            return;
        }

        if (_attackTimer < EnemyData.AttackCooltime)
        {
            _attackTimer += Time.deltaTime;
            return;
        }

        _attackTimer = 0f;
        Debug.Log("Attack");
    }

    private IEnumerator Damaged_Coroutine()
    {
        yield return new WaitForSeconds(EnemyData.DamagedTime);
        Debug.Log("Transition: Damaged -> Trace");
        CurrentState = EnemyState.Trace;
    }

    private IEnumerator Die_Coroutine()
    {
        yield return new WaitForSeconds(EnemyData.DieTime);
        gameObject.SetActive(false);
    }

    public void TakeDamage(Damage damage)
    {
        if (CurrentState == EnemyState.Damaged || CurrentState == EnemyState.Die)
        {
            return;
        }

        _health -= damage.Value;

        if (_health <= 0f)
        {
            Debug.Log($"Transition: {CurrentState} -> Die");
            StopAllCoroutines();
            StartCoroutine(Die_Coroutine());
            return;
        }

        CurrentState = EnemyState.Damaged;

        Vector3 direction = (transform.position - damage.DamageFrom.transform.position).normalized;
        _characterController.Move(direction * damage.KnockBackDistance);

        StartCoroutine(Damaged_Coroutine());

    }
}
