using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

public class FirebaseAuthManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    [Header("Login")]
    public string email;
    public string password;
    public string username;

    void Start()
    {
        // Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
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
        Debug.Log("Setting up Firebase Auth");
        // Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }

    public void ClearLoginFields()
    {
        email = "";
        password = "";
    }

    public void ClearRegisterFields()
    {
        username = "";
        email = "";
        password = "";
    }

    // Function for the login button
    public void LoginButton()
    {
        // Call the login coroutine passing the email and password
        StartCoroutine(Login(email, password));
    }

    // Function for the register button
    public void RegisterButton()
    {
        // Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(email, password, username));
    }

    // Function for the sign out button
    public void SignOutButton()
    {
        auth.SignOut();
        Debug.Log("Signed Out");
    }

    private IEnumerator Login(string _email, string _password)
    {
        // Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        // Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            // If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            Debug.Log(message);
        }
        else
        {
            // User is now logged in
            // Now get the result
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            // If the username field is blank show a warning
            Debug.LogWarning("Missing Username");
        }
        else 
        {
            // Call the Firebase auth create user function
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            // Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                // If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                Debug.Log(message);
            }
            else
            {
                // User has now been created
                // Now get the result
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    // Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    // Call the Firebase auth update user profile function
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    // Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                    }
                    else
                    {
                        // Auth has now been updated
                        Debug.Log($"Firebase user created successfully: {User.DisplayName} ({User.Email})");
                    }
                }
            }
        }
    }
}