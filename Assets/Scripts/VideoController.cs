using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public GameObject infoUI;
    bool playerIsHere;
    // Update is called once per frame
    void Update()
    {
        if (playerIsHere)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartVideo();
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartVideo();
            }

            if (videoPlayer.isPlaying && videoPlayer.time >= videoPlayer.length)
            {
                // Stop the video
                videoPlayer.Stop();
                // Do something else after the video ends

            }
            if (videoPlayer.isPlaying)
            {
                infoUI.SetActive(false);
            }
        }
    }

    private void Start()
    {
        // Start playing the video on start
        //StartVideo();
    }

    public void StartVideo()
    {
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }

    public void RestartVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerIsHere = true;
            infoUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerIsHere = false;
            infoUI.SetActive(false);
        }
    }
}
