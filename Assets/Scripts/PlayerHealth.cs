using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invinsibilityTimeAfterHit = 3f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvinsible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvinsible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvinsible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvinsibilityDelay());
        }
    }

    public void Die()
    {
        PlayerMovement.instance.enabled = false;
        graphics.color = new Color(1f, 1f, 1f, 0f);
    }
    //Coroutine
    public IEnumerator InvincibilityFlash()
    {
        while (isInvinsible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvinsibilityDelay()
    {
        yield return new WaitForSeconds(invinsibilityTimeAfterHit);
        isInvinsible = false;
    }
}
