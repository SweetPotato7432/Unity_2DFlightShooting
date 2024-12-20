using UnityEngine;

abstract public class Character : MonoBehaviour
{

    public abstract int hp { get; set; }
    public abstract float moveSpeed { get; set; }
    public abstract int atk { get; set; }
    public abstract float atkSpeed { get; set; }

    protected virtual void Update()
    {
        if(hp <= 0)
        {
            Deactive();
        }
    }

    abstract public void Attack();
    abstract public void Move();
    abstract public void Deactive();
    public void TakeDamage(int atk)
    {
        hp -= atk;
    }
}
