using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreeShelter : MonoBehaviour
{
    private List<GameObject> enemyList = new();

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<Enemy>() || other.CompareTag("Gift"))
    //    {
    //        enemyList.Add(other.gameObject);
    //        other.gameObject.SetActive(false);
    //        Debug.Log(enemyList.Count);
    //        GetComponent<Collider>().isTrigger = false;


    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<NavMeshObstacle>().enabled = true;
        if (collision.gameObject.GetComponent<Enemy>() || collision.gameObject.CompareTag("Gift"))
        {
            enemyList.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
            Debug.Log(collision.gameObject.name);
        }
        else if (collision.gameObject.GetComponent<Snowball>())
        {
            foreach (GameObject go in enemyList)
            {
                Debug.Log(go.name);
                go.SetActive(true);
                go.transform.position = transform.position + new Vector3(10f, 10f, 10f);
            }
        }
    }
}
