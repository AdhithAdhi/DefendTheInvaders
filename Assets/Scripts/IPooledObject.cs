using UnityEngine;

public interface IPooledObject 
{
    void OnObjectSpwan();
    void ReturnToPool();
}
