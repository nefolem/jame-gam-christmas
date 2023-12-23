using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameIntroManager : MonoBehaviour
{
    [SerializeField] private float segmentEndTime = 14.82f;
    [SerializeField] private CinemachineVirtualCamera _introVC;
    [SerializeField] private List<Transform> _introVCPoints = new List<Transform>();
    [SerializeField] private GameObject _player;
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
            _introVC.LookAt = _player.transform;
        }
    }

    public void AddToGiftsCount()
    {
        GiftsSpawner.Instance.AddToGiftsCount();
    }

    public void SetLookAtNull()
    {
        _introVC.LookAt = null;
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
