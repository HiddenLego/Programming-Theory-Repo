using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected float speed = 10;
    protected bool isHittable = false;
    protected string destructionInput = "return";

    protected void Start()
    {
        // set color & input based on position?
    }

    protected void Update()
    {
        if (Input.GetKeyDown(destructionInput) && isHittable)
        {
            // give points, spawn object?
            Destroy(gameObject);
        }
        Move();
    }

    virtual protected void Move()
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
        // consequences
        // Destroy(gameObject); ?
    }
}
