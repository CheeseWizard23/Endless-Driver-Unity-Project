using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] Roads;
    public GameObject Chunk;
    public GameObject Coin;
    public GameObject[] Obstacles;

    /* private int[] zCoords = { -15, -14, -13, -12, -11, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }; */
    private List<int> xCoords = new List<int>();
    public int roadCoord = -80;
    public int numberOfMaxObstacleSegments = 5;
    private int numberOfObstacleSegments;

    public int numberofMaxObstacles = 3;
    private int numberofObstacles;

    private int segmentCoords;
    private int startingCoords;

    private int obstacleCoords;
    private int lastObstacleCoords = 20;
    private int coinChooser;
    void Start()
    {
        for (int a = -5; a < 6; a = a + 3)
        {
            xCoords.Add(a);
        }

        GameObject startroad = Instantiate(Roads[Random.Range(0, Roads.Length)], new Vector3(0, 0, Chunk.transform.position.z + roadCoord), Quaternion.identity, Chunk.transform);
        roadCoord += 40;

        for (int i = 0; i < 4; i++) {
            GameObject road = Instantiate(Roads[Random.Range(0, Roads.Length)], new Vector3(0, 0, Chunk.transform.position.z + roadCoord), Quaternion.identity, Chunk.transform);
            roadCoord += 40;

            if (Random.Range(1, 4) >= 1) {
                numberOfObstacleSegments = Random.Range(3, numberOfMaxObstacleSegments);
                segmentCoords = 30 / numberOfObstacleSegments;
                startingCoords = -15;

                for (int w = 0; w < numberOfObstacleSegments; w++)
                {
                    numberofObstacles = Random.Range(1, 3);
                    
                    for (int x = 0; x < numberofObstacles; x++) {
                        obstacleCoords = Random.Range(0, xCoords.Count);
                        coinChooser = Random.Range(0, 6);

                        if (coinChooser > 3)
                        {
                            GameObject coin = Instantiate(Obstacles[0], new Vector3(road.transform.position.x + xCoords[obstacleCoords], 0.6f, road.transform.position.z + startingCoords), Quaternion.identity, road.transform);
                        } else
                        {
                            GameObject obstacle = Instantiate(Obstacles[Random.Range(1, Obstacles.Length)], new Vector3(road.transform.position.x + xCoords[obstacleCoords], 1, road.transform.position.z + startingCoords), Quaternion.identity, road.transform);
                        }
                        
                        lastObstacleCoords = obstacleCoords;
                    }

                    startingCoords += segmentCoords;
                }
            }
        }
    }
} 
