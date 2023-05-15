using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Hraði kúlu
    public Rigidbody2D rb;

    private void Start()
    {
        rb.velocity = transform.right * speed; // Hreyfing kúlunar.
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy") Destroy(collision.gameObject); // Ef kúla snertir óvin þá eyðist hann.
        Destroy(gameObject); // Eftir snertingu þá eyðist kúla
    }
}
