using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public string type;
    private float health, speed, spawnRange, direction, directionlimit, timeManager, turretDirection;
    public int damage;
    private GameObject tank;
    public GameObject circleBullets, turretBullets;
    public Transform turretBody, turretBarrel;
    private TankControls script;
    void Start()
    {
        tank = GameObject.Find("Tank");
        script = tank.GetComponent<TankControls>();
        switch (gameObject.name)
        {
            case "SquareEnemy(Clone)":
                type = "square";
                health = 5;
                speed = 2;
                spawnRange = 6;
                damage = 5;
                break;
            case "TriangleEnemy(Clone)":
                type = "triangle";
                health = 2;
                speed = 3;
                spawnRange = 4;
                damage = 3;
                directionlimit = Random.Range(50, 70);
                break;
            case "CircleBombEnemy(Clone)":
                type = "CircleB";
                health = 5;
                speed = 1f;
                spawnRange = 6;
                damage = 1;
                break;
            case "EnemyTurret":
                type = "turret";
                health = 5;
                speed = 2f;
                turretDirection = Random.Range(1, 3);
                break;
        }
        Destroy(gameObject, 10);
        //turrets
        if(type != "turret")
        {
            transform.position = new Vector3(Random.Range(-spawnRange, spawnRange), 7);
            
        }
        else
        {
            StartCoroutine(TurretShoot());
            if (turretDirection == 1)
            {
                transform.position = new Vector3(-11, 3.5f);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                turretBody.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (turretDirection == 2)
            {
                transform.position = new Vector3(11, 3.5f);
                transform.rotation = Quaternion.Euler(0, 0, -90);
                turretBody.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        //turrets end
        if (type == "triangle")
        {
            StartCoroutine(DirectionChange());
        }
    }

    void Update()
    {
        Move();
        GetInput();
        
    }

    void Move()
    {
        if (type == "square" || type == "CircleB")
        {
            transform.Translate(new Vector2(0, -speed) * Time.deltaTime);
        }
        else if (type == "triangle")
        {
            transform.Translate(new Vector2(0, -speed) * Time.deltaTime);
            this.transform.eulerAngles = new Vector3(0, 0, direction);
        }
        else if (type == "turret")
        {

            transform.Translate(new Vector2(0, -speed) * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Bullet(Clone)")
        {
            Destroy(collision.collider.gameObject);
            health -= script.damage;
        }
    }

    void GetInput()
    {
        if (health <= 0.01f)
        {
            if (type != "CircleB")
            {
                Destroy(gameObject);
            }
            else
            {
                Explode();
            }
        }
    }

    IEnumerator DirectionChange() {
        while (true)
        {
            while (direction <= directionlimit)
            {
                if (Time.time > timeManager)
                {
                    timeManager = Time.time + 0.01f;
                    direction += 1;
                }
                yield return null;
            }
            while (direction >= -directionlimit)
            {
                if (Time.time > timeManager)
                {
                    timeManager = Time.time + 0.01f;
                    direction -= 1;
                }
                yield return null;
            }
            yield return null;
        }
    }

    IEnumerator TurretShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject projectile = Instantiate(turretBullets, turretBarrel.transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(turretBarrel.transform.up * 10, ForceMode2D.Impulse);
            Destroy(projectile, 3);
        }
    }

    void Explode()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = Instantiate(circleBullets, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, i * 45));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.up * 7f, ForceMode2D.Impulse);
            Destroy(bullet, 7);
        }
        Destroy(gameObject);
    }
}
