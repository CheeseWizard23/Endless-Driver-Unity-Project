using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 20;
    public int numberOfTiles = 5;
    private int tileNumber;
    private List<GameObject> activeTiles = new();

    public Transform playerTransform;
    
    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i==0)
            {
                SpawnTile(0);
            } else
            {
                tileNumber = Random.Range(0, tilePrefabs.Length);
                SpawnTile(tileNumber);
            }
        }  
    }

    void Update()
    {
        if (PlayerManager.gameOver) return; 
        
        if (playerTransform.position.z - 130 > zSpawn - (numberOfTiles * tileLength))
        {
            tileNumber = Random.Range(0, tilePrefabs.Length);
            SpawnTile(tileNumber);
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
