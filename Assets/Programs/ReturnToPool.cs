using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool : MonoBehaviour
{
    public GameObject Particle;
    public ObjectPool<GameObject> Pool;

    void Start()
    {
        //Particle = GetComponent<GameObject>();
        //var main = Particle.main;
        // Callbackを指定すると、パーティクルが終了するときに
        // コールバックメソッドとしてOnParticleSystemStopped
        // が呼び出される
        //main.stopAction = ParticleSystemStopAction.Callback;
    }

    /*
    void OnParticleSystemStopped()
    {
        // パーティクルシステムが停止したときにここが呼び出される

        // プールから借りていたパーティクルを解放(返却)する
        //Pool.Release(Particle);
        //Pool.Release(this.gameObject);
    }
    */
    void Update()
    {
        if (this.transform.position.y > 5) returnToPool();
    }

    public void returnToPool()
    {
        Pool.Release(this.gameObject);
    }
}
