using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class spin : MonoBehaviour
{
    public int numArea = 4;
    public float timeRotate = 3;
    public float numberCircleRotate = 2;
    public AnimationCurve curve;
    public GameObject MerkNum, ActieNum, StijlNum, DoelgroepNum;
    public GameObject winText;

   
    public static bool rotationComplete = false;
    public static int indexArea;
    public static List<int> areaSet;


    private const float circle = 360.0f;
    private float angleOneArea;
    private float currentTime;
    private AudioSource spinSound;

    // Start is called before the first frame update
    void Start()
    {
        angleOneArea = circle / numArea;
        areaSet = new List<int> { 0, 1, 2, 3 };
        spinSound = GetComponent<AudioSource>();
    }

    /* THIS DOES NOT USE ANYMORE:
    void turnBackCard() 
    {

        if (kaarten_00_back.transform.Find("Canvas").GetComponent<Canvas>().enabled) 
        {
            kaarten_00_back.GetComponent<SpriteRenderer>().sortingOrder = 0;
            kaarten_00_back.transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
        }
        if (kaarten_01_back.transform.Find("Canvas").GetComponent<Canvas>().enabled)
        {
            kaarten_01_back.GetComponent<SpriteRenderer>().sortingOrder = 0;
            kaarten_01_back.transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
        }
        if (kaarten_02_back.transform.Find("Canvas").GetComponent<Canvas>().enabled)
        {
            kaarten_02_back.GetComponent<SpriteRenderer>().sortingOrder = 0;
            kaarten_02_back.transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
        }
        if (kaarten_03_back.transform.Find("Canvas").GetComponent<Canvas>().enabled)
        {
            kaarten_03_back.GetComponent<SpriteRenderer>().sortingOrder = 0;
            kaarten_03_back.transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
        }

    }*/

    void turnBackCard()
    {
        if (gameControl.card != null)
            gameControl.card.GetComponent<SpriteRenderer>().enabled = false;
    
    }

    void showNum()
    {
        if (!MerkNum.GetComponent<Text>().enabled && areaSet.Contains(0))
        {
            MerkNum.GetComponent<Text>().enabled = true;
            MerkNum.transform.Find("Text").GetComponentInChildren<Text>().enabled = true;
        }
        if (!ActieNum.GetComponent<Text>().enabled && areaSet.Contains(1))
        {
            ActieNum.GetComponent<Text>().enabled = true;
            ActieNum.transform.Find("Text").GetComponentInChildren<Text>().enabled = true;
        }
        if (!StijlNum.GetComponent<Text>().enabled && areaSet.Contains(2))
        {
            StijlNum.GetComponent<Text>().enabled = true;
            StijlNum.transform.Find("Text").GetComponentInChildren<Text>().enabled = true;
        };
        if (!DoelgroepNum.GetComponent<Text>().enabled && areaSet.Contains(3))
        {
            DoelgroepNum.GetComponent<Text>().enabled = true;
            DoelgroepNum.transform.Find("Text").GetComponentInChildren<Text>().enabled = true;
        }
    }

    IEnumerator Rotate()
    {
        float startAngle = transform.eulerAngles.z;
        currentTime = 0;
        //Debug.Log("areaSetNum" + areaSet.Count);

        if (areaSet.Count != 0)
        {
            turnBackCard();
            showNum();
            spinSound.Play();
            int randomIndex = Random.Range(0, areaSet.Count);
            indexArea = areaSet[randomIndex];

            float minAngle = angleOneArea * indexArea;
            float maxAngle = angleOneArea * (indexArea + 1);

            float randomAngle = Random.Range(minAngle, maxAngle);
            //float angleRotate = (numberCircleRotate * circle) + randomAngle - startAngle;
            float angleRotate = (numberCircleRotate * circle) + randomAngle + startAngle;

            while (currentTime < timeRotate)
            {
                yield return new WaitForEndOfFrame();
                currentTime += Time.deltaTime;
                float angleCurrent = angleRotate * curve.Evaluate(currentTime / timeRotate);
                //this.transform.eulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
                this.transform.eulerAngles = new Vector3(0, 0, startAngle - angleCurrent);
            }
            spinSound.Stop();
            rotationComplete = true;
        }
        else 
        {
            Debug.Log("GameOver");
            clickSpin.button.interactable = false;
            turnBackCard();
            winText.GetComponent<Text>().enabled = true;
            StartCoroutine(waitforEnd());
        }
    }

    IEnumerator waitforEnd()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }


    //[ContextMenu("Rotate")]
    public void RotateNow()
    {
        StartCoroutine(Rotate());
    }


    // Update is called once per frame
    void Update()
    {

    }
}
