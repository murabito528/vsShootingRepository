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
        // Callback���w�肷��ƁA�p�[�e�B�N�����I������Ƃ���
        // �R�[���o�b�N���\�b�h�Ƃ���OnParticleSystemStopped
        // ���Ăяo�����
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    
    void OnParticleSystemStopped()
    {
        // �p�[�e�B�N���V�X�e������~�����Ƃ��ɂ������Ăяo�����

        // �v�[������؂�Ă����p�[�e�B�N�������(�ԋp)����
        Pool.Release(this.gameObject);
    }
}
