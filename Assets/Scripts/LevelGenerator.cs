using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

/// <summary>
/// This class is used to generate a procedural background for a side scrolling game that moves in the Y axis.
/// The generator will create the 1st bit of environment from a prefab.
/// When the camera collides with the 2dBox Collider from the Terrain Prefab it will generate the new piece of the prefab.
/// This will keep happening until the level has been completed
/// </summary>
public class LevelGenerator : MonoBehaviour
{

    private GameObject _terrainPrefab;
    private Vector3 _terrainPlacementPosition;
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private List<GameObject> terrainList = new List<GameObject>();
    
    private void OnEnable()
    {
        FollowScreen.OnTerrainUpdate += PlaceTerrainPieceAtLocation;
    }
    private void OnDisable()
    {
        FollowScreen.OnTerrainUpdate -= PlaceTerrainPieceAtLocation;
    }

    private void Awake()
    {
        _terrainPrefab = terrainList[0];
    }

    private void GenerateTerrainPiece(GameObject terrainPiece)
    {
        Instantiate(RandomTerrainPiece(), _terrainPlacementPosition, Quaternion.identity);
    }

    private void PlaceTerrainPieceAtLocation(Vector3 spawnLocation)
    {
        _terrainPlacementPosition = spawnLocation;
        GenerateTerrainAtUpperBounds(_terrainPlacementPosition);
    }
    
    private void GenerateTerrainAtUpperBounds(Vector3 position)
    {
        GameObject terrainFromPool = objectPoolManager.GetTerrainFromPool();
        float upperScreenY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        Vector3 spawnPosition = new Vector3(position.x, upperScreenY + 5, position.z);

        terrainFromPool.transform.position = spawnPosition;
        // Additional logic or setup for the spawned terrain if needed
    }

    private GameObject RandomTerrainPiece()
    {
        if (terrainList.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, terrainList.Count);
        return terrainList[randomIndex];
    }
}