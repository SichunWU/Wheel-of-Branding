using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class loadScene : MonoBehaviour
{
    public float waitTime = 4f;
    public GameObject start, notice;
    public Button startButton;
    public VideoPlayer videoPlayer;

    private AudioSource startSound;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(PlayVideoAfterDelay());
        StartCoroutine(waitforIntro());
        startButton.onClick.AddListener(OnClick);
        startSound = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundDelayed());
    }

    /*
    private IEnumerator PlayVideoAfterDelay()
    {
        yield return new WaitForSeconds(10f);
        videoPlayer.Play();
    }*/

    IEnumerator waitforIntro()
    {
        yield return new WaitForSeconds(waitTime);
        start.SetActive(true);
        notice.SetActive(true);
    }

    IEnumerator PlaySoundDelayed()
    {
        yield return new WaitForSeconds(2f);
        startSound.Play();
    }

    private void OnClick()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
