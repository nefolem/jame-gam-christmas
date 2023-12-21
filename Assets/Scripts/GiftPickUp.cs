using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPickUp : MonoBehaviour
{
    private GiftsSpawner _spawner;

    private void Start()
    {
        _spawner = FindObjectOfType<GiftsSpawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            _spawner.CurrentGiftsCount++;    
            Destroy(gameObject);
        }
    }
}