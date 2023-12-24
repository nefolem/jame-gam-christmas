using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPickUp : MonoBehaviour
{
    [SerializeField] private GameObject _giftPickUpEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            Instantiate(_giftPickUpEffect, transform.position, Quaternion.identity);
            GiftsSpawner.Instance.CurrentGiftsCount++;    
            Destroy(gameObject);
        }
    }
}