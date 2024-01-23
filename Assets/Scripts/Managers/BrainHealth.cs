using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BrainHealth : Health
{
    [SerializeField] private GameObject m_brainDeathVFX;
    [SerializeField] private Transform m_brainExplosionLocationTF;
    [SerializeField] private AudioSource audioSourceTakeDamage;
    [SerializeField] private Slider bossSlider;
    public UnityEvent unityEvent;

    public override void Die()
    {
        base.Die();
        EventManager.I.InvokeOnFacilityShutdown();
        unityEvent.Invoke();
        Instantiate(m_brainDeathVFX, m_brainExplosionLocationTF.position, Quaternion.identity);
    }

    public override void TakeDamage(float _damage, bool isPlayer)
    {
        base.TakeDamage(_damage, false);
        bossSlider.value = m_health;
        audioSourceTakeDamage.PlayOneShot(audioSourceTakeDamage.clip);
    }
}
