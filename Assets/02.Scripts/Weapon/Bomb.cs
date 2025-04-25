using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionVFXPrefab;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject bombEffect = ObjectPool.Instance.GetObject(EPoolType.BombEffect);
        bombEffect.transform.position = transform.position;

        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;

        gameObject.SetActive(false);
    }
}
