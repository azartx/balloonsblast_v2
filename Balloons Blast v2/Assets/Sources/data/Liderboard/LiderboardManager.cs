using UnityEngine;
using System.Collections.Generic; // For List
using System.Linq; // For OrderBy descending

public class LeaderboardManager : MonoBehaviour
{
    [System.Serializable] // Makes the struct visible in the Inspector
    public struct LeaderboardEntryData
    {
        public string userName;
        public int score;
    }

    public GameObject leaderboardEntryPrefab; // Assign your LeaderboardEntryPrefab here
    public Transform contentParent;          // Assign the Content GameObject of your ScrollView here

    public List<LeaderboardEntryData> leaderboardData = new List<LeaderboardEntryData>();

    void Start()
    {
        // Example: Add some dummy data
        AddEntry("Alice", 1200);
        AddEntry("Bob", 850);
        AddEntry("Charlie", 2000);
        AddEntry("David", 900);
        AddEntry("Eve", 1500);
        AddEntry("Davider", 1900);
        AddEntry("Everen", 1700);

        DisplayLeaderboard();
    }

    public void AddEntry(string userName, int score)
    {
        leaderboardData.Add(new LeaderboardEntryData { userName = userName, score = score });
    }

    public void DisplayLeaderboard()
    {
        // 1. Clear existing entries from the UI
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 2. Sort the data (highest score first)
        var sortedData = leaderboardData.OrderByDescending(entry => entry.score).ToList();

        // 3. Instantiate and populate new entries
        foreach (var entryData in sortedData)
        {
            GameObject entryGO = Instantiate(leaderboardEntryPrefab, contentParent);
            LeaderboardEntryUI entryUI = entryGO.GetComponent<LeaderboardEntryUI>();
            if (entryUI != null)
            {
                entryUI.SetEntry(entryData.userName, entryData.score);
            }
        }
    }
}