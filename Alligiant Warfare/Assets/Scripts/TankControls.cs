﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class TankControls : MonoBehaviour
{
    //references
    private Animator animator;
    public GameObject bullet, barrel, tank, tankTread;
    private GameObject optimization;
    private Rigidbody2D rb;
    private Vector3 move, moveBod;
    public Joystick joystick1, joystick2;
    //variables
    private bool fireButtonDown;
    public int tankNumber;
    private float direction, direction2, speed, bulletSpeed, firerate = 0.5f, nextfire;
    //Optimization
    public Slider moveSpeedSli, fireRateSli, bulletSpeedSli;
    public Text moveSpeedText, fireRateText, bulletSpeedText;
    public Toggle debugToggle;
    void Start()
    {
        animator = GetComponent<Animator>();
        moveSpeedSli.value = speed;
        fireRateSli.value = firerate;
        rb = GetComponent<Rigidbody2D>();
        optimization = GameObject.Find("Optimization");
        speed = 5f;
        bulletSpeed = 10f;
        fireButtonDown = false;
    }

    void LateUpdate()
    {
        GetInput();
        Debug();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Debug()
    {
        speed = moveSpeedSli.value;
        moveSpeedText.text = $"Move Speed: {speed.ToString("0.00")}";
        firerate = fireRateSli.value;
        fireRateText.text = $"Fire Rate: {firerate.ToString("0.00")}";
        bulletSpeed = bulletSpeedSli.value;
        bulletSpeedText.text = $"Bullet Speed: {bulletSpeed.ToString("0.00")}";
        if (debugToggle.isOn == true)
        {
            optimization.SetActive(true);
        }
        else
        {
            optimization.SetActive(false);
        }
    }
    void GetInput()
    {

        move.x = joystick1.Horizontal;
        move.y = joystick1.Vertical;
        moveBod.x = joystick2.Horizontal;
        moveBod.y = joystick2.Vertical;
        direction = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;
        direction2 = Mathf.Atan2(moveBod.x, moveBod.y) * Mathf.Rad2Deg;

        //other
        if (fireButtonDown && Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            Shoot();
        }
        //animation
        animator.SetInteger("Tank", tankNumber);
        if (move.x == 0 && move.y == 0)
        {
            animator.SetFloat("Speed", 0);
        } else
        {
            animator.SetFloat("Speed", 1);
        }
        
    }

    private void Move()
    {
        if (move.x != 0 || move.y != 0)
        {
            tankTread.transform.eulerAngles = new Vector3(0, 0, -direction);
        }
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);
        if (moveBod.x != 0 || moveBod.y != 0)
        {
            tank.transform.eulerAngles = new Vector3(0, 0, -direction2);
        }
    }

    public void FireButton(bool buttonPushed)
    {
        if (buttonPushed == true)
        {
            fireButtonDown = true;
        } 
        else
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
