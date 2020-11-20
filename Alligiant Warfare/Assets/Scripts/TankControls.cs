using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankControls : MonoBehaviour
{
    //references
    private Transform tankPos;
    public GameObject bullet, barrel, barrel2, joystickRight, fireButton, tank1, tank2, tankTread;
    private Rigidbody2D rb, treadRigid;
    private Vector3 move, moveBod;
    public Joystick joystick1, joystick2;
    public Dropdown drop;
    //variables
    private int selection;
    private bool fireButtonDown;
    private float direction, direction2, speed, bulletSpeed, firerate = 0.5f, nextfire;
    //Optimization
    public Slider moveSpeedSli, fireRateSli;
    public Text moveSpeedText, fireRateText;
    void Start()
    {
        tankPos = this.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        treadRigid = tankTread.GetComponent<Rigidbody2D>();
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
        moveSpeedText.text = $"Move Speed: {moveSpeedSli.ToString()}";
        firerate = fireRateSli.value;
        moveSpeedText.text = $"Fire Rate: {fireRateSli.value.ToString()}";
    }
    void GetInput()
    {
        //tank1
        move.x = joystick1.Horizontal;
        move.y = joystick1.Vertical;
        direction = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;
        //tank2
        moveBod.x = joystick2.Horizontal;
        moveBod.y = joystick2.Vertical;
        direction2 = Mathf.Atan2(moveBod.x, moveBod.y) * Mathf.Rad2Deg;

        //other
        selection = drop.value;
        if (fireButtonDown && Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            Shoot();
        }
        //selection
        if (selection == 1)
        {
            joystickRight.SetActive(true);
            fireButton.SetActive(false);
            tank1.SetActive(false);
            tank2.SetActive(true);
            tankTread.SetActive(true);
        } else
        {
            joystickRight.SetActive(false);
            fireButton.SetActive(true);
            tank1.SetActive(true);
            tank2.SetActive(false);
            tankTread.SetActive(false);
        }
    }

    private void Move()
    {
        //tankOne
        if (gameObject.name == "Tank")
        {
            if (move.x != 0 || move.y != 0)
            {
                tankPos.eulerAngles = new Vector3(0, 0, -direction);
            }
            rb.MovePosition(transform.position + move * speed * Time.deltaTime);
        }
        //TankTwo
        if (gameObject.name == "Tank2")
        {
            //tread
            tank2.transform.position = tankTread.transform.position;
            if (move.x != 0 || move.y != 0)
            {
                tankTread.transform.eulerAngles = new Vector3(0, 0, -direction);
            }
            treadRigid.MovePosition(transform.position + move * speed * Time.deltaTime);
            //body
            if (moveBod.x != 0 || moveBod.y != 0)
            {
                tank2.transform.eulerAngles = new Vector3(0, 0, -direction2);
            }
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
        if (gameObject.name == "Tank")
        {
            GameObject projectile = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(barrel.transform.up * bulletSpeed, ForceMode2D.Impulse);
            Destroy(projectile, 3);
        }
        else
        {
            GameObject projectile = Instantiate(bullet, barrel2.transform.position, barrel2.transform.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(barrel2.transform.up * bulletSpeed, ForceMode2D.Impulse);
            Destroy(projectile, 3);
        }
    }
}
