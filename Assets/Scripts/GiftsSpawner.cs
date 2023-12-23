using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftsSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> gifts = new();
    [SerializeField] private int _maxGiftsCount = 100;

    public static GiftsSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }
    public int CurrentGiftsCount { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        CurrentGiftsCount = 99; 

        UI.instance.SetGiftsCount(CurrentGiftsCount);
    }

    public void AddToGiftsCount()
    {
        CurrentGiftsCount++;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CurrentGiftsCount);
        UI.instance.SetGiftsCount(CurrentGiftsCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() && CurrentGiftsCount != 0)
        {
            Instantiate(gifts[Random.Range(0, gifts.Count)], transform.position, Quaternion.identity);
            CurrentGiftsCount--;
        }
    }
}
