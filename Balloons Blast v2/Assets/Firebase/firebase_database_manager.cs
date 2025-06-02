using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using System.Linq;


public class FirebaseDatabaseManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference databaseReference;

    void Start()
    {
        // Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp
                InitializeFirebase();
            }
            else
            {
                UnityEngine.Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Database");
        // Set the database reference object
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveData(string userID, string userName, int score)
    {
        // Create a user data object
        UserData user = new UserData(userName, score);
        
        // Save the data to the database
        string json = JsonUtility.ToJson(user);
        databaseReference.Child("users").Child(userID).SetRawJsonValueAsync(json);

        Debug.Log("Data saved successfully!");
    }

    public void LoadUserData(string userID)
    {
        StartCoroutine(LoadUserDataCoroutine(userID));
    }

    private IEnumerator LoadUserDataCoroutine(string userID)
    {
        // Get the data from the database
        var DBTask = databaseReference.Child("users").Child(userID).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to load data with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            Debug.Log("No data exists for this user");
        }
        else
        {
            // Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            
            // Parse the JSON data
            string jsonData = snapshot.GetRawJsonValue();
            UserData userData = JsonUtility.FromJson<UserData>(jsonData);
            
            Debug.Log($"Data loaded successfully: {userData.username}, Score: {userData.score}");
            
            // Use the loaded data as needed
            // For example, update UI elements
        }
    }

    public void LoadLeaderboard()
    {
        StartCoroutine(LoadLeaderboardCoroutine());
    }

    private IEnumerator LoadLeaderboardCoroutine()
    {
        // Query to get top 10 scores ordered by score
        var DBTask = databaseReference.Child("users").OrderByChild("score").LimitToLast(10).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to load leaderboard with {DBTask.Exception}");
        }
        else
        {
            // Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            List<UserData> leaderboard = new List<UserData>();

            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string jsonData = childSnapshot.GetRawJsonValue();
                UserData userData = JsonUtility.FromJson<UserData>(jsonData);
                leaderboard.Add(userData);
            }

            Debug.Log($"Leaderboard loaded with {leaderboard.Count} entries");
            
            // Display leaderboard data
            foreach (UserData user in leaderboard)
            {
                Debug.Log($"{user.username}: {user.score}");
            }
        }
    }

    public void UpdateScore(string userID, int newScore)
    {
        StartCoroutine(UpdateScoreCoroutine(userID, newScore));
    }

    private IEnumerator UpdateScoreCoroutine(string userID, int newScore)
    {
        // Update just the score field
        var DBTask = databaseReference.Child("users").Child(userID).Child("score").SetValueAsync(newScore);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to update score with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("Score updated successfully!");
        }
    }

    public void DeleteUserData(string userID)
    {
        StartCoroutine(DeleteUserDataCoroutine(userID));
    }

    private IEnumerator DeleteUserDataCoroutine(string userID)
    {
        var DBTask = databaseReference.Child("users").Child(userID).RemoveValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to delete data with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("User data deleted successfully!");
        }
    }
}

// Data class for user information
[System.Serializable]
public class UserData
{
    public string username;
    public int score;

    public UserData(string username, int score)
    {
        this.username = username;
        this.score = score;
    }
}