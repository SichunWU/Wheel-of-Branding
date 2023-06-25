using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class clickSpin : MonoBehaviour
{
    public static Button button;
    public static bool canRoatate = true;

    private GameObject wheel;
    private spin spinWheel;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        wheel = GameObject.Find("Wheel");
        spinWheel = wheel.GetComponent<spin>();

        if (!button.interactable)
            button.interactable = true;
    }

    private void OnClick()
    {
        if (canRoatate)
        { 
            spinWheel.RotateNow();
            canRoatate = false;

            if (gameControl.availableIndexStijl.Count == 0)
            {
                updateQuestion(2, -1);
            }
            if (gameControl.availableIndexDoelgroep.Count == 0)
            {
                updateQuestion(3, -1);
            }
            if (gameControl.availableIndexMerk.Count == 0)
            {
                updateQuestion(0, -1);
            }
            if (gameControl.availableIndexActie.Count == 0)
            {
                updateQuestion(1, -1);
            }
        }
    }

    void updateQuestion(int quesType, int result)
    {
        if (result == -1)
        {
            string cardback = quesType.ToString() + "_0";
            //Debug.Log(cardback);
            GameObject beginCard = GameObject.Find(cardback);
            Destroy(beginCard);

            string obj = quesType.ToString() + "_100";
            //Debug.Log(obj);
            GameObject finalCard = GameObject.Find(obj);
            finalCard.GetComponent<SpriteRenderer>().enabled = true;

            string numCard = quesType.ToString() + "_200";
            GameObject numCardText = GameObject.Find(numCard);

            numCardText.GetComponent<Text>().enabled = false;
            numCardText.transform.Find("Text").GetComponentInChildren<Text>().enabled = false;

            spin.areaSet.Remove(quesType);

            //Debug.Log("Remove" + quesType);
            //foreach (int value in spin.areaSet)
            //{
            //    Debug.Log("Rest" + value);
            //}
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
