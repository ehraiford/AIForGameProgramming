using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombieStats : CharacterStats
{
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        InitVaribles();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void InitVaribles()
    {
        maxHealth = 25f;
        setCurrHealth(maxHealth);
        damage = 10f;
        attackSpeed = 1.5f;

    }
    public void DealDamage(CharacterStats doDmg)
    {
        doDmg.ApplyDamage(damage);
    }
    public float AttackSpeed()
    {
        return attackSpeed;
    }
    protected override void KillCharacter()
    {
        throw new System.NotImplementedException();
    }
}
