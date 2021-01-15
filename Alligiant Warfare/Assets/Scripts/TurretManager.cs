using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public int turretType;
    private float fireRate;
    public Sprite[] turretSprites;
    public GameObject barrelOne, barrelTwoRight, barrelTwoLeft, barrelThreeRight, barrelThreeLeft, barrelThreeMiddle, barrelRocket, turretBullets, homingMissile;
    public Transform tank;
    Vector2 tankPosition;
    void Start()
    {
        if (gameObject.name == "EnemyTurret(Clone)")
        {
            turretType = Random.Range(1, 6);
        }
        else
        {
            turretType = Random.Range(6, 8);
        }
        this.GetComponent<SpriteRenderer>().sprite = turretSprites[turretType - 1];
        StartCoroutine(TurretShoot());
    }

    void Update()
    {
        tankPosition = tank.position;
    }

    void FixedUpdate()
    {
        Vector2 lookDirection = tankPosition - GetComponent<Rigidbody2D>().position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
        GetComponent<Rigidbody2D>().rotation = angle;
    }
    IEnumerator TurretShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            if (turretType == 1 || turretType == 5)
            {
                if (turretType == 1)
                {
                    fireRate = 0.5f;
                }
                if (turretType == 5)
                {
                    fireRate = 0.2f;
                }
                GameObject projectile = Instantiate(turretBullets, barrelOne.transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(barrelOne.transform.up * 10, ForceMode2D.Impulse);
                Destroy(projectile, 4);
            }
            else if (turretType == 2)
            {
                fireRate = 0.5f;
                //one
                GameObject projectile = Instantiate(turretBullets, barrelTwoLeft.transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(barrelTwoLeft.transform.up * 10, ForceMode2D.Impulse);
                Destroy(projectile, 4);
                //two
                GameObject projectile2 = Instantiate(turretBullets, barrelTwoRight.transform.position, Quaternion.identity);
                Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
                rb2.AddForce(barrelTwoRight.transform.up * 10, ForceMode2D.Impulse);
                Destroy(projectile, 4);
            }
            else if (turretType == 3)
            {
                fireRate = 0.5f;
                //one
                GameObject projectile = Instantiate(turretBullets, barrelThreeLeft.transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(barrelThreeLeft.transform.up * 10, ForceMode2D.Impulse);
                Destroy(projectile, 4);
                //two
                GameObject projectile2 = Instantiate(turretBullets, barrelThreeMiddle.transform.position, Quaternion.identity);
                Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
                rb2.AddForce(barrelThreeMiddle.transform.up * 10, ForceMode2D.Impulse);
                Destroy(projectile, 4);
                //three
                GameObject projectile3 = Instantiate(turretBullets, barrelThreeRight.transform.position, Quaternion.identity);
                Rigidbody2D rb3 = projectile3.GetComponent<Rigidbody2D>();
                rb3.AddForce(barrelThreeRight.transform.up * 10, ForceMode2D.Impulse);
                Destroy(projectile, 4);
            }
            else if (turretType == 4)
            {
                fireRate = 0.8f;
                for (int i = 0; i < 3; i++)
                {
                    GameObject projectile = Instantiate(turretBullets, barrelOne.transform.position, Quaternion.identity);
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    rb.AddForce(barrelOne.transform.up * 10, ForceMode2D.Impulse);
                    Destroy(projectile, 4);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (turretType == 6)
            {
                fireRate = 2f;
                GameObject projectile = Instantiate(turretBullets, barrelRocket.transform.position, barrelRocket.transform.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(barrelRocket.transform.up * 15, ForceMode2D.Impulse);
                Destroy(projectile, 4);
            }
            else if (turretType == 7)
            {
                fireRate = 5f;
                Instantiate(homingMissile, barrelRocket.transform.position, barrelRocket.transform.rotation);
            }
        }
    }
}
