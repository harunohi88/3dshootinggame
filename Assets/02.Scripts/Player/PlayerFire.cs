using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public PlayerWeaponData PlayerWeaponData;
    public GameObject FirePosition;
    public ParticleSystem BulletEffect;

    private int _bombCount;
    private float _bombChargeTime;
    private int _bulletCount;
    private float _bulletCoolTimer;
    private float _bulletReloadTimer;
    private bool _isReloading;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _bombCount = PlayerWeaponData.MaxBombCount;
        _bulletCount = PlayerWeaponData.MaxBulletCount;
        _bulletCoolTimer = 0f;
        _bulletReloadTimer = 0f;
        UI_Canvas.Instance.UpdateBombCounter(_bombCount, PlayerWeaponData.MaxBombCount);
        UI_Canvas.Instance.UpdateBulletCounter(_bulletCount, PlayerWeaponData.MaxBulletCount);
    }

    private void Update()
    {
        BulletReload();
        Bullet();
        BombCharge();
        Bomb();
    }

    private void BulletReload()
    {
        if (Input.GetButtonDown("Reload"))
        {
            if (_isReloading == false)
            {
                UI_Canvas.Instance.ReloadIndicator.gameObject.SetActive(true);
            }
            Debug.Log("Reload");
            _isReloading = true;
        }

        if (_isReloading == false) return;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            _isReloading = false;
            _bulletReloadTimer = 0f;
            UI_Canvas.Instance.ReloadIndicator.gameObject.SetActive(false);
            UI_Canvas.Instance.UpdateReloadIndicator(_bulletReloadTimer);
            return;
        }

        if (_bulletReloadTimer >= PlayerWeaponData.BulletReloadTime)
        {
            _isReloading = false;
            _bulletCount = PlayerWeaponData.MaxBulletCount;
            _bulletReloadTimer = 0f;
            UI_Canvas.Instance.ReloadIndicator.gameObject.SetActive(false);
            UI_Canvas.Instance.UpdateReloadIndicator(_bulletReloadTimer);
            UI_Canvas.Instance.UpdateBulletCounter(_bulletCount, PlayerWeaponData.MaxBulletCount);
            return;
        }
        _bulletReloadTimer += Time.deltaTime;
        UI_Canvas.Instance.UpdateReloadIndicator(_bulletReloadTimer / PlayerWeaponData.BulletReloadTime);
    }

    private void Bullet()
    {
        if (_bulletCoolTimer >= 0f)
        {
            _bulletCoolTimer -= Time.deltaTime;
            return;
        }

        if (Input.GetMouseButton(0) == false) return;
        if (_bulletCount <= 0) return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo = new RaycastHit();

        bool isHit = Physics.Raycast(ray, out hitInfo);

        if (isHit == true)
        {
            GameObject bulletTrace = ObjectPool.Instance.GetBulletTrace();
            bulletTrace.GetComponent<BulletTrace>().Effect(FirePosition.transform.position, hitInfo.point);

            GameObject hitEffect = ObjectPool.Instance.GetHitEffect();
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;

            hitEffect.GetComponent<ParticleSystem>().Play();

            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                enemy.TakeDamage(new Damage()
                {
                    Value = PlayerWeaponData.BulletDamage,
                });
            }
        }

        _bulletCoolTimer = PlayerWeaponData.BulletCoolTime;
        _bulletCount -= 1;
        UI_Canvas.Instance.UpdateBulletCounter(_bulletCount, PlayerWeaponData.MaxBulletCount);
    }

    private void BombCharge()
    {
        if (Input.GetMouseButton(1) == false) return;
        _bombChargeTime += Time.deltaTime;
    }

    private void Bomb()
    {
        if (Input.GetMouseButtonUp(1) == false) return;

        if (_bombCount <= 0) return;

        _bombChargeTime = Mathf.Clamp(_bombChargeTime, 0, PlayerWeaponData.MaxBombChargeTime);
        float bombSpeed = PlayerWeaponData.BombSpeed + PlayerWeaponData.BombSpeed * _bombChargeTime;

        GameObject bomb = ObjectPool.Instance.GetBomb();
        if (bomb == null)
        {
            _bombChargeTime = 0f;
            return;
        }
        bomb.transform.position = FirePosition.transform.position;
        bomb.transform.rotation = FirePosition.transform.rotation;
        Rigidbody bombRigidBody = bomb.GetComponent<Rigidbody>();
        bombRigidBody.AddForce(Camera.main.transform.forward * bombSpeed, ForceMode.Impulse);
        bombRigidBody.AddTorque(Camera.main.transform.right * bombSpeed, ForceMode.Impulse);
        _bombCount--;
        _bombChargeTime = 0f;
        UI_Canvas.Instance.UpdateBombCounter(_bombCount, PlayerWeaponData.MaxBombCount);
    }
}
