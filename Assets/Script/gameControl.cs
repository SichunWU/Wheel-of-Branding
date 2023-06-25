using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
//using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using UnityEngine.WSA;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem;
using static UnityEngine.UI.CanvasScaler;
using Random = UnityEngine.Random;

public class gameControl : MonoBehaviour
{
    public static GameObject card;

    private Dictionary<int, int> cardNumDict = new Dictionary<int, int>
    {
        { 2, 11},
        { 3, 10},
        { 0, 16},
        { 1, 10}
    };

    private Dictionary<int, int> cardNumDictTotal = new Dictionary<int, int>
    {
        { 2, 11},
        { 3, 10},
        { 0, 16},
        { 1, 10}
    };


    /* THIS DOES NOT USE ANYMORE:
    private Dictionary<int, string> typeDict = new Dictionary<int, string>
    {
        { 2, "Stijl en marketing"},
        { 3, "Doelgroep"},
        { 0, "Merk"},
        { 1, "Actie"}
    };

    private string[] merkQues = new string[] { 
        "Waarom willen jullie met je merk aan de slag?",
        "Waarom zijn jullie met dit bedrijf begonnen?",
        "Wat bieden jullie klanten aan?",
        "Wat is jullie doel met het bedrijf?",
        "Welke reputatie wil je opbouwen met je merk?",
        "Hoe ziet jouw droomdag eruit over 2 jaar?",
        "Heb je een speciaal moment waar je aan terug denkt bij je bedrijf?",
        "Wat maakt jullie uniek?",
        "Wie zien jullie als concurrenten?",
        "Wat maakt jullie anders dan de concurrent?",
        "Waar zijn jullie heel goed in?",
        "Waar zijn jullie niet zo goed in?",
        "Wat vinden jullie belangrijk om over te brengen op de ideale klant?",
        "Als je alles vergeet wat je zojuist hebt gezegd, wat is het enige waarvan je denkt:",
        "Wat is jullie werkwijze?",
        "Wat zien jullie als obstakels om te groeien?"
    };
    private string[] merkSubQues = new string[] { 
        "Wat hoop je te bereiken?",
        "Waar zagen jullie kansen?",
        "Waarom willen klanten bij jullie komen?",
        "Waar willen jullie naartoe groeien? Waarom?",
        "Waarom willen jullie dat?",
        "Wat zijn je uitdagingen daarin?",
        "Kan je een waardevol moment of juist dieptepunt benoemen?",
        "Waarom is dat uniek aan jullie?",
        " ",
        " ",
        "Waarom zijn jullie specifiek hier goed in?",
        "Waarom zijn jullie hier specifiek minder goed in?",
        "Waarom vinden jullie dat belangrijk?",
        "ik wil dat ze DIT van mij weten?",
        " ",
        "Waarom is dit een obstakel voor jullie?"
    };

    private string[] doelgroepQues = new string[] {
        "Wie is jullie ideale klant?",
        "Wat typeert jullie ideale klant?",
        "Wat komt er bij jullie ideale klanten allemaal overeen?",
        /*"Met welk probleem komt de klant bij jullie?",
        "Welke oplossing heb je voor het probleem van je doelgroep en welke impact heeft dat op zijn/haar leven?",
        "Waar zit de frustratie van je ideale klant?\nWat is het interne probleem?",
        "Hoe komen jullie nu aan deze ideale klant?",
        "Wat zijn de intenties van de ideale klant bij jullie?",
        "Wie is jullie minst ideale klant?",
        "Wat typeert jullie minst ideale klant?"
    };
    private string[] doelgroepSubQues = new string[] {
        "Waarom?",
        " ",
        " ",
        "Waarom heeft jouw klant dit probleem?",
        "Waarom heeft het impact?",
        " ",
        " ",
        " ",
        "Waarom?",
        "Wat komt er bij jullie minst ideale klanten overeen?"
    };

    private string[] stijlQues = new string[] {
        "Wat willen jullie uitstralen als merk?",
        "Welk gevoel moet je krijgen bij het merk?",
        "Wat voor soort kleuren passen bij deze uitstraling?",
        "Waarom hebben jullie gekozen voor de huidige kleuren?",
        "Hoe communiceren jullie met klanten?",
        "Hoe persoonlijk communiceren jullie met klanten?",
        "Is er een verschil in communicatie intern en extern?",
        "Welke marketingkanalen gebruiken jullie nu?",
        "Zijn er marketingkanalen die jullie graag willen gaan gebruiken?",
        "Op welke basis gebruiken jullie deze?",
        "Heb je voorbeelden van websites/marketingkanalen die jullie mooi vinden en welke niet?"
    };
    private string[] stijlSubQues = new string[] {
        " ",
        " ",
        " ",
        "Willen jullie deze aanhouden?",
        " ",
        " ",
        " ",
        " ",
        " ",
        " ",
        " "
    };

    private string[] actieQues = new string[] {
        "Teken uit in 30s: Het mooiste moment met jullie bedrijf tot nu toe",
        "Kies uit: 5 iconen (van Comfy) die jullie merk het beste beschrijven (&waarom juist deze?)",
        "Beeld uit: Een vaardigheid die jullie graag nog willen leren"
    };
    private string[] actieSubQues = new string[] {
        " ",
        " ",
        " ",
    };
   */

    private int merkQues = 16;
    private int doelgroepQues = 10;
    private int stijlQues = 11;
    private int actieQues = 10;

    public static List<int> availableIndexMerk;
    public static List<int> availableIndexDoelgroep;
    public static List<int> availableIndexStijl;
    public static List<int> availableIndexActie;


    // Start is called before the first frame update
    void Start()
    {
        ResetIndex(ref availableIndexMerk, merkQues);
        ResetIndex(ref availableIndexDoelgroep, doelgroepQues);
        ResetIndex(ref availableIndexStijl, stijlQues);
        ResetIndex(ref availableIndexActie, actieQues);
        //Debug.Log(doelgroepQues.Length);
        //Debug.Log(doelgroepSubQues.Length);
        //Debug.Log(availableIndexStijl.Count);
    }

    /* THIS DOES NOT USE ANYMORE:
    private void ResetIndex(ref List<int> availableIndex, string[] Ques)
    {
        availableIndex = new List<int>(Ques.Length);
        for (int i = 0; i < Ques.Length; i++)
        {
            availableIndex.Add(i);
        }
    }*/

    private void ResetIndex(ref List<int> availableIndex, int Ques)
    {
        availableIndex = new List<int>(Ques);
        for (int i = 0; i < Ques; i++)
        {
            availableIndex.Add(i);
        }
    }

    /*THIS DOES NOT USE ANYMORE:
    private (string, string) selectRandomQues(List<int> availableIndex, string[] Ques, string[] subQues)
    {
        //Debug.Log(availableIndex.Count);
        if (availableIndex.Count == 0)
        {
            //Debug.Log("None");
            return (null,null);
        }

        int randomIndex = Random.Range(0, availableIndex.Count);
        int selectedIndex = availableIndex[randomIndex];
        availableIndex.RemoveAt(randomIndex);

        string selectedQues = Ques[selectedIndex];
        //merkQues[selectedIndex] = null;
        string selectedSubQues = subQues[selectedIndex];
        return (selectedQues, selectedSubQues);

    } */

    private int selectRandomQues(List<int> availableIndex)
    {
        if (availableIndex.Count == 0)
        {
            return -1;
        }

        int randomIndex = Random.Range(0, availableIndex.Count);
        int selectedIndex = availableIndex[randomIndex];
        availableIndex.RemoveAt(randomIndex);

        return selectedIndex+1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        if (spin.rotationComplete)
        {
            //Debug.Log("finish: " + spin.indexArea);
            int indexArea = spin.indexArea;
            //(string ques, string subQues) result;
            int result;

            switch (indexArea)
            {
                case 2:
                    result = selectRandomQues(availableIndexStijl);
                    updateQuestion(indexArea,result);
                    break;
                case 3:
                    result = selectRandomQues(availableIndexDoelgroep);
                    updateQuestion(indexArea, result);
                    break;
                case 0:
                    result = selectRandomQues(availableIndexMerk);
                    updateQuestion(indexArea, result);
                    break;
                case 1:
                    //result = selectRandomQues(availableIndexActie, actieQues, actieSubQues);
                    //updateQuestion(indexArea, kaarten_03_back, result);
                    result = selectRandomQues(availableIndexActie);
                    updateQuestion(indexArea, result);
                    break;
            }

            spin.rotationComplete = false;
            clickSpin.canRoatate = true;
        }
    }

    /*THIS DOES NOT USE ANYMORE:
    void updateQuestion(int quesType, GameObject card, (string ques, string subQues) result)
    {

      if (result.ques == null)
      {

          card.GetComponent<SpriteRenderer>().sortingOrder = 2;
          Transform canva = card.transform.Find("Canvas");
          canva.GetComponent<Canvas>().enabled = true;

          string note = "Gefeliciteerd!\nJe hebt alle vragen over \"" + typeDict[quesType] + "\" voltooid.";
          Text mainQuesBox = canva.transform.Find("mainQues").GetComponentInChildren<Text>();
          Text subQuesBox = canva.transform.Find("subQues").GetComponentInChildren<Text>();;
          mainQuesBox.color = new Color (0, 0, 128);
          mainQuesBox.text = note;
          subQuesBox.text = " ";

          spin.areaSet.Remove(quesType);
          
          //Debug.Log("Remove" + quesType);
          //foreach (int value in spin.areaSet)
          //{
          //    Debug.Log("Rest" + value);
          //}

        }
        else
        {
            card.GetComponent<SpriteRenderer>().sortingOrder = 2;
            Transform canva = card.transform.Find("Canvas");
            canva.GetComponent<Canvas>().enabled = true;
            Text mainQuesBox = canva.transform.Find("mainQues").GetComponentInChildren<Text>();
            Text subQuesBox = canva.transform.Find("subQues").GetComponentInChildren<Text>();
            mainQuesBox.text = result.ques;
            subQuesBox.text = result.subQues;
        }
    }*/

    void updateQuestion(int quesType, int result)
    {
        /*
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
            numCardText.GetComponent<Text>().text = (cardNumDictTotal[quesType] - cardNumDict[quesType]).ToString();
            numCardText.GetComponent<Text>().enabled = false;
            numCardText.transform.Find("Text").GetComponentInChildren<Text>().enabled = false;

            spin.areaSet.Remove(quesType);

            Debug.Log("Remove" + quesType);
            foreach (int value in spin.areaSet)
            {
                Debug.Log("Rest" + value);
            }
        }
        else*/
        if (result != -1)
        {
            string numCard = quesType.ToString() + "_200";
            GameObject numCardText = GameObject.Find(numCard);
            numCardText.GetComponent<Text>().enabled = false;
            numCardText.transform.Find("Text").GetComponentInChildren<Text>().enabled = false;

            string obj = quesType.ToString() + "_" + result.ToString();
            //Debug.Log(obj);
            card = GameObject.Find(obj);

            card.GetComponent<SpriteRenderer>().enabled = true;
            card.GetComponent<SpriteRenderer>().sortingOrder = 34;

            string hodler = quesType.ToString() + "_" + cardNumDict[quesType].ToString() + "_1";
            //Debug.Log(hodler);
            GameObject placeHolder = GameObject.Find(hodler);
            Destroy(placeHolder);
            cardNumDict[quesType] -= 1;

            numCardText.GetComponent<Text>().text = (cardNumDictTotal[quesType] - cardNumDict[quesType]).ToString();
        }
    }
}
