using System;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField]
    Animator effectAnimator;

    public enum AnimType 
    { 
        PlayerDead,
        EnemyDead,
        BulletHit
    }
    public void Initalize(Vector3 position,AnimType animType)
    {
        gameObject.SetActive(true);
        transform.position = position;

        switch (animType)
        {
            case AnimType.PlayerDead:
                effectAnimator.Play("Effect_Explosion1");
                break;
            case AnimType.EnemyDead:
                effectAnimator.Play("Effect_Explosion2");
                break;
            case AnimType.BulletHit:
                effectAnimator.Play("Effect_HitBullet");
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
