using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform EntryContainer;
    private Transform EntryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        EntryContainer = transform.Find("asdfasdfsadfsa");
        EntryTemplate = EntryContainer.Find("HighScoreEntryTemplate");
       
        EntryTemplate.gameObject.SetActive(false);

        AddHighScoreEntry(10000, "CMK");


        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        Debug.Log(highscores);
        
        //Sort entry list by Score
        /*
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                { 
                    //swap
                    HighScoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        */
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highscoreEntry in highscores.highscoreEntryList)
        { 
            CreateHighscoreEntryTransform(highscoreEntry, EntryContainer, highscoreEntryTransformList);
        }
        
    }

    private void CreateHighscoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(EntryTemplate, container);

        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;

        string rankString;

        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;


            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;

        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        transformList.Add(entryTransform);

        // Set background visible odds and evens, is easier to read
        if (rank == 1)
        {
            entryTransform.Find("PosText").GetComponent<Text>().color = Color.green;

            entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;

            entryTransform.Find("NameText").GetComponent<Text>().color = Color.green;

        }

        // Set trophy
        /*
        switch (rank)
        {
            default:
                entryTransform.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetcolorFromString("");
                break;
            case 2:
                entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetcolorFromString("");
                break;
            case 3:
                entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetcolorFromString("");
                break;
        }*/

        //transformList.Add(entryTransform);

    }
    // Represents a single High score entry 
    private void AddHighScoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighScoreEntry highscoreEntry = new HighScoreEntry { score = score, name = name };

        // Load Saved Highscores
        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighScoreTable", json);
        PlayerPrefs.Save();
    }
    private class Highscores
    {
        public List<HighScoreEntry> highscoreEntryList; 
    }
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;

    }
}
