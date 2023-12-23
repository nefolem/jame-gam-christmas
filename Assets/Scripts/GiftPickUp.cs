using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPickUp : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            GiftsSpawner.Instance.CurrentGiftsCount++;    
            Destroy(gameObject);
        }
    }
}