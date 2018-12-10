using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Damageable : MonoBehaviour
{
    //[Serializable]
    //public class HealthEvent : UnityEvent<Damageable>
    //{ }

    //[Serializable]
    //public class DamageEvent : UnityEvent<DamageDealer, Damageable>
    //{ }

    //[Serializable]
    //public class HealEvent : UnityEvent<int, Damageable>
    //{ }

    public UnityEvent OnHealthSet;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;
    public UnityEvent OnGainHealth;
    public IntVariable maxHealth;
    public IntVariable publicHealth;
    public int currentHealth;
    public bool invincible = false;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth.Value;

    }

    // Update is called once per frame
    void Update()
    {
        publicHealth.Value = currentHealth;
    }

    //public void TakeDamage(DamageDealer damager, Damageable damageable)
    //{
    //    Debug.Log("TakeDamage"); 
    //    if (!invincible)
    //    {
    //        currentHealth -= damager.DamageAmount.Value;
    //        OnHealthSet.Invoke();
    //    }

    //    OnTakeDamage.Invoke(damager, this);

    //    if (currentHealth <= 0)
    //    {
    //        OnDie.Invoke(damager, this);
    //        invincible = true;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        other.enabled = false;
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
        if (damage != null)
        {
            currentHealth -= damage.DamageAmount.Value;
            OnTakeDamage.Invoke();
        }

        if (currentHealth <= 0.0f)
        {
            OnDie.Invoke();
        }
    }
}

