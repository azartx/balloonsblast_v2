using UnityEngine;
using UnityEngine.UI;

public class FirebaseUIController : MonoBehaviour
{
    [Header("UI References")]
    public InputField emailInput;
    public InputField passwordInput;
    public InputField usernameInput;
    public Button loginButton;
    public Button registerButton;
    public Button signOutButton;
    public Button saveDataButton;
    public Button loadDataButton;
    public Text statusText;

    [Header("Firebase References")]
    public FirebaseAuthManager authManager;
    public FirebaseDatabaseManager databaseManager;

    void Start()
    {
        // Setup button listeners
        loginButton.onClick.AddListener(LoginUser);
        registerButton.onClick.AddListener(RegisterUser);
        signOutButton.onClick.AddListener(SignOutUser);
        saveDataButton.onClick.AddListener(SaveUserData);
        loadDataButton.onClick.AddListener(LoadUserData);
    }

    public void LoginUser()
    {
        if (emailInput.text != "" && passwordInput.text != "")
        {
            authManager.email = emailInput.text;
            authManager.password = passwordInput.text;
            authManager.LoginButton();
            statusText.text = "Logging in...";
        }
        else
        {
            statusText.text = "Please fill in all fields";
        }
    }

    public void RegisterUser()
    {
        if (emailInput.text != "" && passwordInput.text != "" && usernameInput.text != "")
        {
            authManager.email = emailInput.text;
            authManager.password = passwordInput.text;
            authManager.username = usernameInput.text;
            authManager.RegisterButton();
            statusText.text = "Registering...";
        }
        else
        {
            statusText.text = "Please fill in all fields";
        }
    }

    public void SignOutUser()
    {
        authManager.SignOutButton();
        statusText.text = "Signed out";
        ClearInputFields();
    }

    public void SaveUserData()
    {
        if (authManager.User != null)
        {
            // Save some example data
            string userID = authManager.User.UserId;
            string userName = authManager.User.DisplayName;
            int score = Random.Range(100, 1000); // Example random score

            databaseManager.SaveData(userID, userName, score);
            statusText.text = "Data saved!";
        }
        else
        {
            statusText.text = "Please login first";
        }
    }

    public void LoadUserData()
    {
        if (authManager.User != null)
        {
            string userID = authManager.User.UserId;
            databaseManager.LoadUserData(userID);
            statusText.text = "Loading data...";
        }
        else
        {
            statusText.text = "Please login first";
        }
    }

    public void LoadLeaderboard()
    {
        databaseManager.LoadLeaderboard();
        statusText.text = "Loading leaderboard...";
    }

    private void ClearInputFields()
    {
        emailInput.text = "";
        passwordInput.text = "";
        usernameInput.text = "";
    }
}