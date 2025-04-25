using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<PoolConfig> PoolConfigs;
    public Dictionary<EPoolType, List<GameObject>> PoolDictionary = new Dictionary<EPoolType, List<GameObject>>();

    public void Start()
    {
        MakePoolDictionary();
    }

    public void MakePoolDictionary()
    {
        foreach (PoolConfig config in PoolConfigs)
        {
            List<GameObject> pool = new List<GameObject>();
            for (int i = 0; i < config.PoolSize; ++i)
            {
                GameObject obj = Instantiate(config.Prefab);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                pool.Add(obj);
            }
            PoolDictionary.Add(config.PoolType, pool);
        }
    }

    public GameObject GetObject(EPoolType poolType)
    {
        if (PoolDictionary.ContainsKey(poolType))
        {
            foreach (GameObject item in PoolDictionary[poolType])
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    return item;
                }
            }
        }
        else
        {
            Debug.LogError("Pool Type not found: " + poolType);
        }
        return null;
    }

    public void ReturnObject(EPoolType poolType, GameObject item)
    {
        if (PoolDictionary.ContainsKey(poolType))
        {
            item.SetActive(false);
        }
        else
        {
            Debug.LogError("Pool Type not found: " + poolType);
        }
    }

    /*
    public GameObject BombPrefab;
    public int BombPoolSize;

    public GameObject BombEffectPrefab;
    public int BombEffectPoolSize;

    public GameObject BulletTracePrefab;
    public int BulletTracePoolSize;

    public GameObject HitEffectdPrefab;
    public int HitEffectPoolSize;

    private List<GameObject> _bombPool = new List<GameObject>();
    private List<GameObject> _bombEffectPool = new List<GameObject>();
    private List<GameObject> _bulletTracePool = new List<GameObject>();
    private List<GameObject> _hitEffectPool = new List<GameObject>();

    private void Start()
    {
        Debug.Log("Pool Start");
        MakePool(BombPoolSize, BombPrefab, _bombPool);
        MakePool(BulletTracePoolSize, BulletTracePrefab, _bulletTracePool);
        MakePool(HitEffectPoolSize, HitEffectdPrefab, _hitEffectPool);
        MakePool(BombEffectPoolSize, BombEffectPrefab, _bombEffectPool);
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

    public GameObject GetBombEffect()
    {
        foreach (GameObject item in _bombEffectPool)
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
    */
}
