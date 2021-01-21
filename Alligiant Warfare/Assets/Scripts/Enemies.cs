using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public string type;
    private float health, speed, spawnRange, direction, directionlimit, timeManager, turretDirection;
    public int missleTurretSide, diamondSide;
    public int damage;
    private GameObject tank;
    public GameObject circleBullets, diamondWarning;
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
            case "RocketTurret":
                type = "RocketTurret";
                health = 10;
                speed = 1.5f;
                missleTurretSide = Random.Range(1, 3);
                break;
            case "DiamondEnemy":
                type = "Diamond";
                health = 3;
                damage = 2;
                diamondSide = Random.Range(1, 4);
                speed = 10;
                break;
            case "HexagonEnemy":
                type = "Hexagon";
                health = 10;
                damage = 5;
                spawnRange = 5;
                speed = 1;
                break;
        }
        Destroy(gameObject, 60);
        //turrets
        if(type != "turret" && type != "RocketTurret" && type != "Diamond")
        {
            transform.position = new Vector3(Random.Range(-spawnRange, spawnRange), 7);
            
        }
        else if(type == "Diamond")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DiamondCreate());
        }
        else if(type == "turret")
        {
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
        else if (type == "RocketTurret")
        {
            if (missleTurretSide == 1)
            {
                transform.position = new Vector2(-7.5f, 8);
            }
            else if (missleTurretSide == 2)
            {
                transform.position = new Vector2(7.5f, 8);
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
        if (type == "square" || type == "CircleB" || type  == "Diamond" || type == "Hexagon")
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
        else if (type == "RocketTurret")
        {
            if (transform.position.y > 4)
            {
                transform.Translate(new Vector2(0, -speed) * Time.deltaTime);
            }

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

    IEnumerator DiamondCreate()
    {
        if (diamondSide == 1)
        {
            transform.position = new Vector2(Random.Range(-8f, 8f), 25);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject warning = Instantiate(diamondWarning, new Vector3(transform.position.x, transform.position.y - 21, 0), Quaternion.identity);
            SpriteRenderer renderer = warning.GetComponent<SpriteRenderer>();
            for (int i = 0; i < 5; i++)
            {
                renderer.enabled = true;
                yield return new WaitForSeconds(0.2f);
                renderer.enabled = false;
                yield return new WaitForSeconds(0.2f);
            }
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (diamondSide == 2)
        {
            transform.position = new Vector2(-29, Random.Range(-4f, 4f));
            transform.rotation = Quaternion.Euler(0, 0, 90);
            GameObject warning = Instantiate(diamondWarning, new Vector3(transform.position.x + 21, transform.position.y, 0), Quaternion.identity);
            SpriteRenderer renderer = warning.GetComponent<SpriteRenderer>();
            for (int i = 0; i < 5; i++)
            {
                renderer.enabled = true;
                yield return new WaitForSeconds(0.2f);
                renderer.enabled = false;
                yield return new WaitForSeconds(0.2f);
            }
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (diamondSide == 3)
        {
            transform.position = new Vector2(29, Random.Range(-4f, 4f));
            transform.rotation = Quaternion.Euler(0, 0, -90);
            GameObject warning = Instantiate(diamondWarning, new Vector3(transform.position.x - 21, transform.position.y, 0), Quaternion.identity);
            SpriteRenderer renderer = warning.GetComponent<SpriteRenderer>();
            for (int i = 0; i < 5; i++)
            {
                renderer.enabled = true;
                yield return new WaitForSeconds(0.2f);
                renderer.enabled = false;
                yield return new WaitForSeconds(0.2f);
            }
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
