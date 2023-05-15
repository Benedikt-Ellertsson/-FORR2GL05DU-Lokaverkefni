using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject pointA; // Staðsetning A
    public GameObject pointB; // Staðseting B
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint; // Núverandi staðssetning óvinar
    public float speed; //Hraði óvinar

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
    }

    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform) // Ef staðssetning er pointB þá..
        {
            rb.velocity = new Vector2(speed, 0); // 
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 1.5f && currentPoint == pointB.transform) // Ef óvinur hefur náð pointB þá...
        {
            flip(); // Snúið sprite óvinar
            currentPoint = pointA.transform; // Óvinur fær nýja leið og fer að pointA
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 1.5f && currentPoint == pointA.transform) // Ef óvinur hefur náð pointA þá...
        {
            flip(); // Snúið sprite óvinar
            currentPoint = pointB.transform; // Óvinur fær nýja leið og fer að pointB
        }
    }

    private void flip() // Snýr sprite óvinar
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos() // Teiknar upp slóðina og staðina sem óvinur snýr við (Bara fyrir mig til að sjá við hönnun leiksins)
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Projectile") // Ef snerting við hlut með tagið "Projectile" þá...
        {
            Destroy(gameObject); // Óvin eytt úr leiknum.
        }
    }
}
