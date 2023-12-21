using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.gameObject.GetComponent<PlayerMovement>().GetDamage();
            collision.gameObject.GetComponent<PlayFX>().PlayDamageEffect(collision.gameObject.transform.position);

        }
    }
}
