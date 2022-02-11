using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclesPrefabs;
    private Vector3 spawnPos;
    private float startDelay = 2;
    private float repeartRate = 2;

    private PlayerController _playerController;


    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.transform.position; //(30, 0, 0)
        InvokeRepeating("SpawnObstacle", startDelay, repeartRate);
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    private void SpawnObstacle()
    {
        if (!_playerController.GameOver)
        {
            GameObject obstaclesPrefab = obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)];
            Instantiate(obstaclesPrefab,
                        spawnPos,
                        obstaclesPrefab.transform.rotation);
        }
    }
}
