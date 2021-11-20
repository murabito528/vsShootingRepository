using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool : MonoBehaviour
{
    public GameObject Particle;
    public ObjectPool<GameObject> Pool;
    Transform tf;
    SpriteRenderer spriterenderer;
    Color32 color32;

    void Start()
    {
        spriterenderer = this.GetComponent<SpriteRenderer>();
        tf = transform;
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
        if (tf.position.y < -5 || Mathf.Abs(tf.position.x) > 3) returnToPool();
    }

    public void returnToPool()
    {
        color32 = spriterenderer.color;
        color32.g = 255;
        spriterenderer.material.color = color32;
        Pool.Release(this.gameObject);
    }
}
