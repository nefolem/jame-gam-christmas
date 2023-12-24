using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameIntroManager : MonoBehaviour
{
    [SerializeField] private float segmentEndTime = 14.82f;
    [SerializeField] private float skipTime = 70.6f;
    [SerializeField] private CinemachineVirtualCamera _introVC;
    [SerializeField] private CinemachineVirtualCamera _gameVC;
    [SerializeField] private List<Transform> _introVCPoints = new List<Transform>();
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _introPlayerObject;
    [SerializeField] private GameObject _sceneEnemiesObject;
    [SerializeField] private GameObject _introEnemiesObject;
    [SerializeField] private GameObject _giftsObject;
    [SerializeField] private GameObject _watch;


    private int _currentIndex = 0;
    public PlayableDirector timelineDirector;

    public static GameIntroManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timelineDirector.Stop();
        BeforeStartTimeline();
    }

    public void BeforeStartTimeline()
    {
        timelineDirector.Play();

        InvokeRepeating("RepeatTimelineSegment", 0f, 14.82f);
    }

    public void StartTimeline()
    {
        CancelInvoke("RepeatTimelineSegment");
        timelineDirector.time = segmentEndTime;
        
    }

    public void SetIntroCameraPosition()
    {
        if (UI.instance._isStarted)
        {
            _currentIndex++;
        }            
        _introVC.transform.position = _introVCPoints[_currentIndex].position;
        _introVC.transform.rotation = _introVCPoints[_currentIndex].rotation;


        if (_currentIndex == 1)
        {
            _introVC.LookAt = _introPlayerObject.transform;
        }
    }

    public void SkipIntro()
    {
        timelineDirector.time = skipTime;
        timelineDirector.Play();
    }

    public void AddToGiftsCount()
    {
        GiftsSpawner.Instance.AddToGiftsCount();
    }

    public void SetLookAtNull()
    {
        _introVC.LookAt = null;
    }

    public void SwitchToGameMode()
    {
        _introEnemiesObject.SetActive(false);
        _sceneEnemiesObject.SetActive(true);
        _giftsObject.GetComponent<Collider>().enabled = true;
        _playerObject.SetActive(true);
        _introPlayerObject.SetActive(false);
        _introVC.gameObject.SetActive(false);
        _gameVC.gameObject.SetActive(true);
        //timelineDirector.gameObject.SetActive(false);
        _watch.SetActive(true);
    }

    void RepeatTimelineSegment()
    {

        float segmentStartTime = 0f;
        float segmentEndTime = 14.82f;

        float currentTime = (float)timelineDirector.time;

        if (currentTime >= segmentEndTime)
        {
            timelineDirector.time = segmentStartTime;
            timelineDirector.Play();
        }
    }
}
