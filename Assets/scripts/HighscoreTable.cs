using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class HighscoreTable : MonoBehaviour
{
    private Transform Table;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList = new List<HighScoreEntry>();
    private List<Transform> highScoreEntryTransformList;
    private string jsonFilePath = "https://o4o.webtima.online/wp-content/uploads/2024/10/highscores.json";
    private string phpFilePath = "https://o4o.webtima.online/save_highscores.php";

    private void Awake()
    {
        Table = transform.Find("highScoreTable");
        entryContainer = Table.Find("highScoreEntryContainer");
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        Debug.Log("JSON file path: " + jsonFilePath);

        StartCoroutine(LoadHighscoresFromFile());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator LoadHighscoresFromFile()
    {
        string cacheBuster = System.DateTime.Now.Ticks.ToString();
        string urlWithCacheBuster = jsonFilePath + "?cb=" + cacheBuster;

        UnityWebRequest request = UnityWebRequest.Get(urlWithCacheBuster);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error loading JSON: " + request.error);
        }
        else
        {
            string jsonString = request.downloadHandler.text;
            Debug.Log("JSON content: " + jsonString);
            Highscores highScores = JsonUtility.FromJson<Highscores>(jsonString);

            if (highScores == null || highScores.highscoreEntryList == null)
            {
                highScoreEntryList = new List<HighScoreEntry>();
            }
            else
            {
                highScoreEntryList = highScores.highscoreEntryList;
            }

            RefreshHighscoreTable();
        }
    }

    public void AddHighscoreEntry(int score, string name)
    {
        // Check if an entry with the same name already exists
        HighScoreEntry existingEntry = highScoreEntryList.FirstOrDefault(entry => entry.name == name);
        if (existingEntry != null)
        {
            if (score > existingEntry.score)
            {
              existingEntry.score = score;   
            }
        }
        else
        {
            // Add a new entry
            HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };
            highScoreEntryList.Add(highScoreEntry);
        }

        // Sort the list by score in descending order
        highScoreEntryList = highScoreEntryList.OrderByDescending(entry => entry.score).ToList();

        // Save the updated list to the server
        StartCoroutine(SaveHighscoresToServer());

        // Refresh the highscore table
        RefreshHighscoreTable();
    }

    private void RefreshHighscoreTable()
    {
        foreach (Transform child in entryContainer)
        {
            if (child != entryTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        highScoreEntryTransformList = new List<Transform>();
        int maxEntries = 10;
        for (int i = 0; i < highScoreEntryList.Count && i < maxEntries; i++)
        {
            CreateHighscoreEntryTransform(highScoreEntryList[i], entryContainer, highScoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;
        entryTransform.Find("scoreText").GetComponent<Text>().text = highScoreEntry.score.ToString();
        entryTransform.Find("nameText").GetComponent<Text>().text = highScoreEntry.name;

        transformList.Add(entryTransform);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SaveHighscoresToServer()
    {
        Highscores highScores = new Highscores { highscoreEntryList = highScoreEntryList };
        string json = JsonUtility.ToJson(highScores, true);

        UnityWebRequest request = new UnityWebRequest(phpFilePath, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("JSON to save: " + json);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error saving JSON: " + request.error);
        }
        else
        {
            Debug.Log("High scores saved successfully.");
        }
    }

    [System.Serializable]
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