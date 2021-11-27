using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool_effect : MonoBehaviour
{
    public ParticleSystem Particle;
    public ObjectPool<GameObject> Pool;

    void Start()
    {
        Particle = GetComponent<ParticleSystem>();
        var main = Particle.main;
        // Callbackを指定すると、パーティクルが終了するときに
        // コールバックメソッドとしてOnParticleSystemStopped
        // が呼び出される
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    
    void OnParticleSystemStopped()
    {
        // パーティクルシステムが停止したときにここが呼び出される

        // プールから借りていたパーティクルを解放(返却)する
        Pool.Release(this.gameObject);
    }
}
