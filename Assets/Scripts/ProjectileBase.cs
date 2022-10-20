using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected float speed = 30;
    protected int pointValue;
    protected bool isHittable = false;

    protected string destructionInput = "return";
    protected Material projectileColor;

    protected void Start()
    {
        // sets color and input based on position
        if (transform.position.y < -10)
        {
            SetUpConditions(0, "down");
        }
        else if (transform.position.x < -10)
        {
            SetUpConditions(1, "left");
        }
        else if (transform.position.y > 10)
        {
            SetUpConditions(2, "up");
        }
        else if (transform.position.x > 10)
        {
            SetUpConditions(3, "right");
        }
        
        GetComponent<Renderer>().material = projectileColor;
    }

    protected virtual void SetUpConditions(int multiplier, string input) // Abstraction
    {
        projectileColor = PlayerManager.Instance.materials[multiplier];
        destructionInput = input;
        transform.Rotate(Vector3.forward * -90 * multiplier);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(destructionInput) && isHittable)
        {
            HitProjectile();
        }
        if (!PlayerManager.Instance.gameOver)
        {
            Move();
        }
    }

    virtual protected void Move() // Polymorphism
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    protected void OnTriggerEnter(Collider other)
    {
        isHittable = true;
    }
    protected void OnTriggerExit(Collider other)
    {
        isHittable = false;
    }

    protected void HitProjectile() // Abstraction
    {
        PlayerManager.Instance.DelayedSpawn();
        PlayerManager.Instance.score += pointValue;
        Destroy(gameObject);
    }
}
