using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreeShelter : MonoBehaviour
{
    private List<GameObject> enemyList = new();
    [SerializeField] private Transform _spawnPosition;

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
        GetComponent<NavMeshObstacle>().carving = true;
        if (collision.gameObject.GetComponent<Enemy>() || collision.gameObject.CompareTag("Gift"))
        {
            enemyList.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Snowball>())
        {
            if(enemyList.Count > 0)
            {
                //print("snowball");
                foreach (GameObject go in enemyList)
                {
                    go.SetActive(true);
                    
                    go.transform.position = _spawnPosition.position;
                    
                    //print(go.transform.position);  
                }
            }
            
        }
    }
}
