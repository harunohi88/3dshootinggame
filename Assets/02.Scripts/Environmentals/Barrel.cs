using System.Collections;
using System.Collections.Specialized;
using UnityEngine;

public class Barrel : MonoBehaviour, IDamageable
{
    public int MaxHealth;
    public float VanishTime;
    public float ExplosionForce;
    public float ExplosionRadius;
    public int ExplosionDamage;
    public float ExplosionKnockBackDistance;

    private int CurrentHealth;
    private Rigidbody _rigidbody;
    private bool _isExploded = false;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(Damage damage)
    {
        CurrentHealth -= damage.Value;
        if (CurrentHealth <= 0 && _isExploded == false)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Vector3 direction = UnityEngine.Random.onUnitSphere;
        direction.y = Mathf.Abs(direction.y);
        direction.Normalize();

        _isExploded = true;

        GameObject bombEffect = ObjectPool.Instance.GetObject(EPoolType.BombEffect);
        bombEffect.transform.position = transform.position;

        _rigidbody.AddForce(direction * ExplosionForce, ForceMode.Impulse);
        _rigidbody.AddTorque(UnityEngine.Random.onUnitSphere * ExplosionForce);
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(new Damage()
                {
                    Value = ExplosionDamage,
                    KnockBackDistance = ExplosionKnockBackDistance,
                    DamageFrom = gameObject,
                });
            }
        }

        StartCoroutine(Explode_Coroutine());
    }

    private IEnumerator Explode_Coroutine()
    {
        yield return new WaitForSeconds(VanishTime);
        Destroy(gameObject);
    }

}
