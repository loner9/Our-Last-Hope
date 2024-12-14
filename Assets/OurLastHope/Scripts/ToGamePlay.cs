using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ToGamePlay : MonoBehaviour
{
    VideoPlayer videoPlayer;
    [SerializeField] string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(checkFinished());
    }

    IEnumerator checkFinished(){
        yield return new WaitForSeconds(0.5f);
        if(!videoPlayer.isPlaying){
            SceneManager.LoadScene(sceneName);
        }
    }
}
