using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public int turretType;
    private float fireRate;
    public Sprite[] turretSprites;
    public GameObject barrelOne, barrelTwoRight, barrelTwoLeft, barrelThreeRight, barrelThreeLeft, barrelThreeMiddle, turretBullets;
    void Start()
    {
        turretType = Random.Range(1, 6);
        this.GetComponent<SpriteRenderer>().sprite = turretSprites[turretType - 1];
        StartCoroutine(TurretShoot());
    }

    void Update()
    {
        
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
        }
    }
}
