using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    // Gildi leikmanns
    public int maxHealth = 5; // Hámark líf sem spilari getur verið með
    public int health { get { return currentHealth; } }
    int currentHealth; // Núverandi líf sem spilari á eftir.
    public float speed = 3.0f; // Hraði leikmanns
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float inVincibleTimer;

    // Almennar breytur
    Rigidbody2D rigidbody2d;
    public GameObject projectilePrefab;
    float horizontal;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(horizontal, 0);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            inVincibleTimer -= Time.deltaTime;
            if (inVincibleTimer < 0)
                isInvincible = false;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            inVincibleTimer = timeInvincible;
        }
        if (currentHealth <= 0)
        {
            Debug.Log("Spilari er dauður!");
            SceneManager.LoadScene(1);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}