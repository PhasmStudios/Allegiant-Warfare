using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankControls : MonoBehaviour
{
    private Transform tankPos;
    public GameObject bullet, barrel;
    private Rigidbody2D rb;
    private Vector3 move;
    public Joystick joystick;
    public Dropdown drop;
    private int selection;
    private bool fireButtonDown;
    private float direction, speed, bulletSpeed, firerate = 0.5f, nextfire;
    void Start()
    {
        tankPos = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
        bulletSpeed = 10f;
        fireButtonDown = false;
    }

    void Update()
    {
        GetInput();
        if (fireButtonDown && Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
        selection = drop.value;
        direction = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;
    }

    private void Move()
    {
        if (move.x != 0 || move.y != 0)
        {
            tankPos.eulerAngles = new Vector3(0, 0, -direction);
        }
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);
    }

    public void FireButton(bool buttonPushed)
    {
        if (buttonPushed == true)
        {
            fireButtonDown = true;
        } else
        {
            fireButtonDown = false;
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(barrel.transform.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(projectile, 3);
    }
}
