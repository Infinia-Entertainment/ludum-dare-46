using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] internal Rigidbody2D rb;
    private bool isKnocked;
    protected bool noKnockbackCooldown;
    private float knockbackTime = 0.3f;
    protected Vector2 movementDirection;
    protected Vector2 movementVelocity;
    public Vector2 additionalVelocity;
    public int health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ReceiveKnockBackFromPosition(Vector2 relativePos, float force)
    {
        StartCoroutine(ApplyKnockbackFromPosition(relativePos, force));
    }

    IEnumerator ApplyKnockbackFromPosition(Vector2 relative, float force)
    {

        if (!isKnocked || noKnockbackCooldown)
        {
            Vector2 direction = ((Vector2)transform.position - relative).normalized;
            Vector2 appliedVelocity = direction * force * 10;

            additionalVelocity += appliedVelocity;

            isKnocked = true;
            yield return new WaitForSeconds(knockbackTime);
            additionalVelocity -= appliedVelocity;

            isKnocked = false;

            yield return new WaitForEndOfFrame();
        }


    }

    public void ReceiveKnockBackFromMovementDirection(Vector2 relativeDirection, float force)
    {
        StartCoroutine(ApplyKnockbackMovementDirection(relativeDirection, force));
    }

    IEnumerator ApplyKnockbackMovementDirection(Vector2 relativeDirection, float force)
    {

        if (!isKnocked || noKnockbackCooldown)
        {
            Vector2 appliedVelocity = relativeDirection * force * 10;

            additionalVelocity += appliedVelocity;

            isKnocked = true;
            yield return new WaitForSeconds(knockbackTime);
            additionalVelocity -= appliedVelocity;

            isKnocked = false;

            yield return new WaitForEndOfFrame();
        }


    }
}
