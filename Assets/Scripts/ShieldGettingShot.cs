using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGettingShot : Health
{

    [SerializeField] private Animation m_animation; 
    

    public override void TakeDamage(float _damage, bool isPlayer)
    {
        m_animation.Stop();
        m_animation.CrossFade("ShieldGettingShot",0);
    }

    public override void Heal(int _healAmount)
    {
        
    }

    public override void Die()
    {
        
    }
    
}
