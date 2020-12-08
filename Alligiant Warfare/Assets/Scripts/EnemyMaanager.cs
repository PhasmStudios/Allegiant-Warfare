using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMaanager : MonoBehaviour
{
    public GameObject[] enemy;
    public Toggle squareCheck, triangleCheck, circleBombCheck;

    private void Start()
    {
        StartCoroutine(SpawnSquares());
        StartCoroutine(SpawnTriangles());
        StartCoroutine(SpawnCircles());
    }
    private void Update()
    {
        
    }
    IEnumerator SpawnSquares()
    {
        while (true)
        {
            if (squareCheck.isOn)
            {
                GameObject enemyObject = Instantiate(enemy[0]);
                Destroy(enemyObject, 5);
            }
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    IEnumerator SpawnTriangles()
    {
        while (true)
        {
            if (triangleCheck.isOn)
            {
                GameObject enemyObject = Instantiate(enemy[1]);
                Destroy(enemyObject, 10);
            }
            yield return new WaitForSeconds(Random.Range(5f, 8f));
        }
    }

    IEnumerator SpawnCircles()
    {
        while (true)
        {
            if (circleBombCheck.isOn)
            {
                GameObject enemyObject = Instantiate(enemy[2]);
                Destroy(enemyObject, 10);
            }
            yield return new WaitForSeconds(Random.Range(1f, 1f));
        }
    }
}
