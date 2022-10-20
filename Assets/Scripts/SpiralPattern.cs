using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralPattern : ProjectileBase // Inheritance
{
    private bool isTurning = false;
    private float turnSpeed = 90.0f;
    private float offSet = 30.0f;
    private float arcStartDistance = 9;
    private Vector3 turningPoint = Vector3.zero;

    private new void Start()
    {
        StartCoroutine(FlipTimer(transform.position.magnitude));
        base.Start();
        pointValue = 2;
    }

    protected override void SetUpConditions(int multiplier, string input)
    {
        Vector3[] directions = {Vector3.left, Vector3.up, Vector3.right, Vector3.down };
        transform.Translate(directions[multiplier] * offSet);
        transform.RotateAround(turningPoint, Vector3.forward, 180);
        turningPoint = directions[(multiplier + 2) % 4] * offSet / 2 + directions[(multiplier + 3) % 4] * arcStartDistance;
        base.SetUpConditions(multiplier, input);
    }

    protected override void Move()
    {
        if (!isTurning)
        {
            base.Move();
        }
        else
        {
            transform.RotateAround(turningPoint, Vector3.forward, turnSpeed * Time.deltaTime * -1);
        }
    }

    IEnumerator FlipTimer(float startValue)
    {
        yield return new WaitForSeconds((startValue + arcStartDistance) / speed);
        isTurning = true;
        StartCoroutine(FlipTimer2());
    }

    IEnumerator FlipTimer2()
    {
        yield return new WaitForSeconds(180 / turnSpeed);
        isTurning = false;
    }
}
