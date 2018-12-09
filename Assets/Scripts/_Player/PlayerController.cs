using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Character
{
    public GameObject attackCollider;
    public GameObject smashCollider;

    private float horizontal;
    private float vertical;
    private int targetRotation = -90;

    int currentCombo = 0;
    int currentKnockback = 0;

    public IntReference maxHealth;
    public IntVariable health;
    public UnityEvent damageEvent;
    public UnityEvent deathEvent;
    public FloatReference moveSpeed;
    public FloatVariable playerHitTime;
    public TransformVariable playerTransform;

    string[] attacks = { "Combo1", "Combo2", "Combo3" };
    string[] knockbacks = { "Knockback1", "Knockback2", "Knockback3" };

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        playerTransform.Value.position = transform.position;
        playerHitTime.Value = -10;
        health.Value = maxHealth.Value;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // set the point for enemies to follow
        playerTransform.Value.position = transform.position;

        // tell animator the time for combos and knockback
        anim.SetFloat("StateTime", Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
        anim.ResetTrigger("Melee");

        //DepleteHealth();

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Move(horizontal, vertical);

        if (Input.GetButtonDown("Fire1"))
        {
            Melee();
        }

        if ((health.Value <= 0 || transform.position.y < -100) && isAlive)
        {
            isAlive = false;
            deathEvent.Invoke();
            Die("PlayerIsDead");
        }
    }

    protected override void Melee()
    { 
        anim.SetTrigger("Melee");
    }

    private void Move(float h, float v)
    {
        // If you can't move or player isn't pushing wasd, set velocity to 0
        if (!canMove || (h == 0 & v == 0))
        {
            body.velocity = new Vector3(0, body.velocity.y, 0);
            anim.SetFloat("MoveSpeed", 0);
        }

        // Otherwise, move 
        else
        {
            body.velocity = new Vector3(h * moveSpeed.Value, body.velocity.y, v * moveSpeed.Value);
            anim.SetFloat("MoveSpeed", 1);

            Vector3 movement = new Vector3(h, 0.0f, v);
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * moveSpeed.Value * Time.deltaTime, Space.World);
        }
    }

    // Player slowly dies over time
    void DepleteHealth()
    {
        if (Time.time % 10 == 0)
        {
            health.Value--;
        }
    }

    void TakeDamage()
    {
        if (!isInvincible)
        {
            // Reset combo counter
            currentCombo = 0;
            isInvincible = true;

            if ((Time.time - playerHitTime.Value < 5) && currentKnockback < 2)
            {
                currentKnockback++;
            }
            else
            {
                currentKnockback = 0; // Reset player hit counter
            }

            playerHitTime.Value = Time.time;

            // Trigger the knockback
            anim.SetTrigger(knockbacks[currentKnockback]);
            health.Value -= 1;
            damageEvent.Invoke();
        }
    }

    void TakeHeavyDamage()
    {
        if (!isInvincible)
        {
            health.Value -= 2;
            isInvincible = true;
            anim.SetTrigger("Knockback3");
            damageEvent.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "AttackCollider")
        {
            TakeDamage();
        }

        else if (other.name == "CoffinBody" || other.name == "Bullet(Clone)" || other.name == "SmashCollider")
        {
            TakeHeavyDamage();
        }
    }
}
