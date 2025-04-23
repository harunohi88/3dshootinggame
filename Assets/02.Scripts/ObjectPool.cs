using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool>
{
    public GameObject BombPrefab;
    public int BombPoolSize;

    public GameObject BulletTracePrefab;
    public int BulletTracePoolSize;

    public GameObject HitEffectdPrefab;
    public int HitEffectPoolSize;

    private List<GameObject> _bombPool = new List<GameObject>();
    private List<GameObject> _bulletTracePool = new List<GameObject>();
    private List<GameObject> _hitEffectPool = new List<GameObject>();

    private void Start()
    {
        Debug.Log("Pool Start");
        MakePool(BombPoolSize, BombPrefab, _bombPool);
        MakePool(BulletTracePoolSize, BulletTracePrefab, _bulletTracePool);
        MakePool(HitEffectPoolSize, HitEffectdPrefab, _hitEffectPool);
    }

    public void MakePool(int poolSize, GameObject prefab, List<GameObject> pool)
    {
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetBomb()
    {
        foreach (GameObject item in _bombPool)
        {
            if (item.activeInHierarchy == false)
            {
                item.SetActive(true);
                return item;
            }
        }
        return null;
    }

    public GameObject GetBulletTrace()
    {
        foreach (GameObject item in _bulletTracePool)
        {
            if (item.activeInHierarchy == false)
            {
                item.SetActive(true);
                return item;
            }
        }
        return null;
    }

    public GameObject GetHitEffect()
    {
        foreach (GameObject item in _hitEffectPool)
        {
            if (item.activeInHierarchy == false)
            {
                item.SetActive(true);
                return item;
            }
        }
        return null;
    }
}
