using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public float waitTime = 4f;
    public GameObject start, end;
    public Button startButton, endButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitforIntro());
        startButton.onClick.AddListener(onStartClick);
        endButton.onClick.AddListener(OnEndClick);
    }

    IEnumerator waitforIntro()
    {
        yield return new WaitForSeconds(waitTime);
        start.SetActive(true);
        end.SetActive(true);
    }

    private void onStartClick()
    {
        SceneManager.LoadScene(1);
    }

    private void OnEndClick()
    {
        Application.Quit();
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
