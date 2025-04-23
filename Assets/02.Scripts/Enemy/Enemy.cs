using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Trace,
        Return,
        Attack,
        Damaged,
        Die
    }

    public float FindDistance = 7f;
    public float ReturnDistance = 10f;
    public float MoveSpeed = 3f;
    public float AttackDistance = 2f;
    public float MinMoveDistance = 0.1f;
    public float AttackCooltime = 2f;
    public float MaxHealth = 100f;
    public float DamagedTime = 0.5f;
    public float DieTime = 2f;

    private GameObject _player;
    private CharacterController _characterController;
    private Vector3 _startPosition;
    private float _attackTimer = 0f;
    private float _health;

    public EnemyState CurrentState = EnemyState.Idle;

    private void Start()
    {
        _health = MaxHealth;
        _player = GameObject.FindGameObjectWithTag("Player");
        _characterController = GetComponent<CharacterController>();
        _startPosition = transform.position;
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
        if (Vector3.Distance(transform.position, _player.transform.position) < FindDistance)
        {
            CurrentState = EnemyState.Trace;
            Debug.Log("Transition: Idle -> Trace");
        }
    }

    private void Trace()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > ReturnDistance)
        {
            CurrentState = EnemyState.Return;
            Debug.Log("Transition: Trace -> Return");
            return;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) < AttackDistance)
        {
            CurrentState = EnemyState.Attack;
            Debug.Log("Transition: Trace -> Attack");
            return;
        }

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _characterController.Move(direction * MoveSpeed * Time.deltaTime);
    }

    private void Return()
    {
        if (Vector3.Distance(transform.position, _startPosition) < MinMoveDistance)
        {
            transform.position = _startPosition;
            CurrentState = EnemyState.Idle;
            Debug.Log("Transition: Return -> Idle");
            return;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) < FindDistance)
        {
            CurrentState = EnemyState.Trace;
            Debug.Log("Transition: Return -> Trace");
            return;
        }

        Vector3 direction = (_startPosition - transform.position).normalized;

        _characterController.Move(direction * MoveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > AttackDistance)
        {
            CurrentState = EnemyState.Trace;
            _attackTimer = 0f;
            Debug.Log("Transition: Attack -> Trace");
            return;
        }

        if (_attackTimer < AttackCooltime)
        {
            _attackTimer += Time.deltaTime;
            return;
        }

        _attackTimer = 0f;
        Debug.Log("Attack");
    }

    private IEnumerator Damaged_Coroutine()
    {
        yield return new WaitForSeconds(DamagedTime);
        Debug.Log("Transition: Damaged -> Trace");
        CurrentState = EnemyState.Trace;
    }

    private IEnumerator Die_Coroutine()
    {
        yield return new WaitForSeconds(DieTime);
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
            StartCoroutine(Die_Coroutine());
            return;
        }

        CurrentState = EnemyState.Damaged;
        StartCoroutine(Damaged_Coroutine());

    }
}
