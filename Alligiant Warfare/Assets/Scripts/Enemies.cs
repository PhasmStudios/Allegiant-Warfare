using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public string type;
    private float health, speed, spawnRange, direction, directionlimit, timeManager;
    public int damage;
    private GameObject tank;
    public GameObject circleBullets;
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

        }
        transform.position = new Vector3(Random.Range(-spawnRange, spawnRange), 7);
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
