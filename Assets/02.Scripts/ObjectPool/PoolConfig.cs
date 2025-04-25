using UnityEngine;

[CreateAssetMenu(fileName = "PoolConfig", menuName = "Scriptable Objects/PoolConfig")]
public class PoolConfig : ScriptableObject
{
    public EPoolType PoolType;
    public int PoolSize;
    public GameObject Prefab;
}
