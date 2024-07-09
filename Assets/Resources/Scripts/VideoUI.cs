using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoUI : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Button btnSkip;
    // public VideoClip videoClip;

    private bool isPlaying;
    private bool isPrepared;
    private bool isTriggered;

    // private UpdateComp updateComp;

    public void Start()
    {
        // base.Init();
        btnSkip.onClick.AddListener(OnSkip);
        videoPlayer.loopPointReached += OnPlayFinish;
    }

    public void SetVideo()
    {
        // videoPlayer.clip = videoClip;
        // Debug.Log($"VideoUI: set clip {videoPlayer.clip.name}, length {videoPlayer.clip.length}");
        videoPlayer.url = Application.streamingAssetsPath + "/开场动画.mp4";
        videoPlayer.prepareCompleted += (p) =>
        {
            Debug.Log($"VideoUI: player prepare complete, isTriggered: {isTriggered}");
            isPrepared = true;
            if (isTriggered)
            {
                DoPlay();
            }
        };
        videoPlayer.Prepare();
    }
    
    public void PlayVideo()
    {
        isTriggered = true;
        if (!isPrepared)
        {
            return;
        }
        DoPlay();
    }

    private void DoPlay()
    {
        Debug.Log("videoUI: start play video");
        isPlaying = true;
        // updateComp = new UpdateComp();
        // updateComp.ScheduleAction(LogTime, 1f, -1);
        videoPlayer.Play();
        // timer = 8f;
    }

    private void OnSkip()
    {
        if (isPlaying)
        {
            Debug.Log($"VideoUI: skip");
            // videoPlayer.time = videoPlayer.clip.length;
            videoPlayer.time = 7.5f;
            // timer = 0;
        }
    }

    // private float timer;
    // private void Update()
    // {
    //     if (!isPlaying)
    //     {
    //         return;
    //     }
    //     timer -= Time.deltaTime;
    //     // Debug.Log(timer);
    //     if (timer < 0)
    //     {
    //         IntEventSystem.Send(GameEventEnum.FinishPlayVideo, null);
    //     }
    // }

    private void LogTime()
    {
        Debug.Log($"VideoUI: time {videoPlayer.time} / {videoPlayer.length}");
    }

    private void OnPlayFinish(VideoPlayer player)
    {
        // updateComp.StopSchedule(LogTime);
        isPlaying = false;
        Debug.Log($"VideoUI: play finish");
        IntEventSystem.Send(GameEventEnum.FinishPlayVideo, null);
    }

    // private void OnDestroy()
    // {
    //     if (updateComp != null)
    //     {
    //         updateComp.Dispose();
    //         updateComp = null;
    //     }
    // }
}