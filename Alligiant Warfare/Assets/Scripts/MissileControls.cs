using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControls : MonoBehaviour
{
    private int health;

    public GameObject tank;
    private Rigidbody2D rb;
    private bool lockedOn;
    void Start()
    {
        lockedOn = true;
        rb = GetComponent<Rigidbody2D>();
        tank = GameObject.Find("Tank");
        StartCoroutine(StopLocking());
    }
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)tank.transform.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.velocity = transform.up * 8;
        if (lockedOn)
        {
            rb.angularVelocity = -rotateAmount * 150f;
        }
        else
        {
            rb.angularVelocity = -rotateAmount * 0;
        }
    }

    IEnumerator StopLocking()
    {
        yield return new WaitForSeconds(4f);
        lockedOn = false;
    }
}
