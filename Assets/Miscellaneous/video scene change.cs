using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videoscenechange : MonoBehaviour
{
    [SerializeField]
    VideoPlayer video;
    // Start is called before the first frame update
    void Start()
    {
        video.loopPointReached += vidFinish;
    }

    // Update is called once per frame
    void vidFinish(VideoPlayer vp)
    {
        Debug.Log("works");
        SceneManager.LoadScene("Title Screen");
    }
}
