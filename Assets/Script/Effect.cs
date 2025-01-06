using System;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField]
    Animator effectAnimator;

    //0 : enemyDead 1: PlayerDead, 2: hit
    [SerializeField]
    AudioClip[] audioClips;

    AudioSource audioSource;


    
    public enum AnimType 
    { 
        PlayerDead,
        EnemyDead,
        BulletHit
    }
    public void Initalize(Vector3 position,AnimType animType)
    {
        audioSource = GetComponent<AudioSource>();

        gameObject.SetActive(true);
        transform.position = position;

        switch (animType)
        {
            case AnimType.PlayerDead:
                audioSource.clip = audioClips[1];
                effectAnimator.Play("Effect_Explosion1");
                audioSource.Play();
                break;
            case AnimType.EnemyDead:
                audioSource.clip = audioClips[0];
                effectAnimator.Play("Effect_Explosion2");
                audioSource.Play();

                break;
            case AnimType.BulletHit:
                audioSource.clip = audioClips[2];
                effectAnimator.Play("Effect_HitBullet");
                audioSource.Play();

                break;
        }
        
    }

    public void Deactive()
    {
        //Debug.Log("¿Ã∆Â∆Æ ¡æ∑·");
        gameObject.SetActive(false);
        EffectPoolManager.Instance.ReturnEffect(this);
    }

  
}
