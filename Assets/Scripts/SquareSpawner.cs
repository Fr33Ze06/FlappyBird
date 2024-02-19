using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    public GameObject squarePrefab;
    public float spawnInterval = 1.8f;
    public float scrollSpeed = 2.0f;
    public float holeSize = 2.0f;

    private float elapsedTime;

    private List<GameObject> squareList = new List<GameObject>();

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= spawnInterval)
        {
            if (squareList.Count < ((scrollSpeed * 2)/spawnInterval))
            {
                SpawnSquare();
            }
            else
            {
                ResetPosition(squareList[0]);
                GameObject temp = squareList[0];
                squareList.RemoveAt(0);
                squareList.Add(temp);
            }
            elapsedTime = 0f;
        }
        MoveSquares();
    }

    private void SpawnSquare()
    {
        float randomY = Random.Range(-holeSize, holeSize);

        GameObject newSquare = Instantiate(
            squarePrefab,
            new Vector3(transform.position.x, randomY, transform.position.z),
            Quaternion.identity
        );
        newSquare.transform.parent = transform;
        squareList.Add(newSquare);
        // Destroy(newSquare, 5.0f);
    }

    private void MoveSquares()
    {
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
    }

    private void ResetPosition(GameObject square)
    {
        Vector3 newPosition = square.transform.position;
        newPosition.x = transform.position.x;
        newPosition.y = Random.Range(-holeSize, holeSize);
        square.transform.position = newPosition;
    }
}
