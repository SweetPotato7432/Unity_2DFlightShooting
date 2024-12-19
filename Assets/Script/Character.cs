using UnityEngine;

abstract public class Character : MonoBehaviour
{

    public abstract int hp { get; set; }
    public abstract float moveSpeed { get; set; }
    public abstract int atk { get; set; }
    public abstract int atkSpeed { get; set; }

    abstract public void Attack();

    abstract public void Move();
    abstract public void Deactive();

    
}
