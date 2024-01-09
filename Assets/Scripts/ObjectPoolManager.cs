using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject terrainPrefab;
    [SerializeField] int poolSize = 20;
    private Queue<GameObject> objectQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateObjectPool();
    }

    private void CreateObjectPool()
    {
        GameObject poolParent = new GameObject();
        for (int i = 0; i < poolSize; i++)
        {

            GameObject newTerrainPiece = Instantiate(terrainPrefab, poolParent.transform, true);
            newTerrainPiece.SetActive(false);
            objectQueue.Enqueue(newTerrainPiece);
        }
    }

    public GameObject GetTerrainFromPool()
    {
        if (objectQueue.Count > 0)
        {
            GameObject eldestPiece = objectQueue.Dequeue();
            eldestPiece.SetActive(true);
            objectQueue.Enqueue(eldestPiece);
            return eldestPiece;
        }
        Debug.LogWarning("Object pool exhausted. Reusing oldest object.");

        GameObject oldest = Instantiate(terrainPrefab);
        objectQueue.Enqueue(oldest);
        oldest.SetActive(true);
        return oldest;
    }

}
