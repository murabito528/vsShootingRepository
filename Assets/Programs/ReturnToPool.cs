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
        // Callback���w�肷��ƁA�p�[�e�B�N�����I������Ƃ���
        // �R�[���o�b�N���\�b�h�Ƃ���OnParticleSystemStopped
        // ���Ăяo�����
        //main.stopAction = ParticleSystemStopAction.Callback;
    }

    /*
    void OnParticleSystemStopped()
    {
        // �p�[�e�B�N���V�X�e������~�����Ƃ��ɂ������Ăяo�����

        // �v�[������؂�Ă����p�[�e�B�N�������(�ԋp)����
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
