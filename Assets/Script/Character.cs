using UnityEngine;

abstract public class Character : MonoBehaviour
{

    public abstract int maxHP { get; set; }
    public abstract int curHP {  get; set; }
    public abstract float moveSpeed { get; set; }
    public abstract int atk { get; set; }
    public abstract float atkSpeed { get; set; }

    

    void Start()
    {
        
    }

    protected virtual void Update()
    {
        if(curHP <= 0)
        {
            Deactive();
        }
    }

    abstract public void Attack();
    abstract public void Move();
    abstract public void Deactive();
    virtual public void TakeDamage(int atk)
    {
        curHP -= atk;
    }

}
