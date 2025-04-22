using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    public GameObject BombPrefab;
    public int BombPoolSize;

    private List<GameObject> _bombPool = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < BombPoolSize; ++i)
        {
            GameObject bomb = Instantiate(BombPrefab);
            bomb.transform.SetParent(transform);
            bomb.SetActive(false);
            _bombPool.Add(bomb);
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
}
