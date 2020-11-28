using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMaanager : MonoBehaviour
{
    public GameObject[] enemy;
    public Toggle squareCheck, triangleCheck;

    private void Start()
    {
        StartCoroutine(SpawnSquares());
        StartCoroutine(SpawnTriangles());
    }
    private void Update()
    {
        
    }
    IEnumerator SpawnSquares()
    {
        while (true)
        {
            if (squareCheck)
            {
                Instantiate(enemy[0]);
            }
            yield return new WaitForSeconds(Random.Range(5f, 8f));
        }
    }

    IEnumerator SpawnTriangles()
    {
        while (true)
        {
            if (triangleCheck)
            {
                Instantiate(enemy[1]);
            }
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }
}
