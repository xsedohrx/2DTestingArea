using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class FollowScreen : MonoBehaviour
{
    [SerializeField] float offset;
    Camera mainCam;
    public static event Action<Vector3> OnTerrainUpdate;
    [SerializeField] Transform targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(
            new Vector3(transform.position.x, transform.position.y, -10),
            new Vector3(targetPosition.position.x, targetPosition.position.y, -10),
            .1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TerrainSpawner"))
        {
            // Emit an event to notify other scripts that the camera has collided with the upper bounds
            OnTerrainUpdate?.Invoke(new Vector3(transform.position.x,transform.position.y + offset, transform.position.z) );
        }
    }
}