using UnityEngine;
using UnityEngine.Playables;

public class GameIntroManager : MonoBehaviour
{
    [SerializeField] private float segmentEndTime = 14.82f;
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
