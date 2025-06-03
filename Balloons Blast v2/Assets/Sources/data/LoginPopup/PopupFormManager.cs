using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro
using System.Collections; // Required for IEnumerator

public class PopupFormManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject popupPanel; // Drag your PopupFormPanel here
    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField repeatPasswordInput;
    public Button nextButton;
    public Button cancelButton;

    [Header("Animation")]
    public Animator popupAnimator; // Drag the PopupFormPanel (which has the Animator) here
    private readonly string showTrigger = "ShowPopup";
    private readonly string hideTrigger = "HidePopup";
    private float animationDuration = 0.5f; // Match your animation clip duration

    void Start()
    {
        // Ensure the panel is hidden initially
        popupPanel.SetActive(false);

        // Attach button listeners
        nextButton.onClick.AddListener(OnNextButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);

        // Optionally, add a button to show the popup for testing
        // You can link this to another UI button in your main menu, for example.
        // GameObject showPopupButton = GameObject.Find("YourShowPopupButtonName"); // Example
        // if (showPopupButton != null)
        // {
        //     showPopupButton.GetComponent<Button>().onClick.AddListener(ShowPopup);
        // }
    }

    public void ShowPopup()
    {
        Debug.Log("ShowPopup() called!");
        popupPanel.SetActive(true); // Make sure the panel is active to play animation
        popupAnimator.SetTrigger(showTrigger);
        // Optionally disable other UI interactions while popup is active
    }

    public void HidePopup()
    {
        popupAnimator.SetTrigger(hideTrigger);
        StartCoroutine(DeactivatePanelAfterAnimation(animationDuration)); // Wait for animation to finish
    }

    private IEnumerator DeactivatePanelAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        popupPanel.SetActive(false); // Deactivate panel after animation
        // Re-enable other UI interactions
    }

    void OnNextButtonClicked()
    {
        Debug.Log("Next button clicked!");

        // --- Input Validation ---
        string username = usernameInput.text;
        string email = emailInput.text;
        string password = passwordInput.text;
        string repeatPassword = repeatPasswordInput.text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(repeatPassword))
        {
            Debug.LogWarning("All fields must be filled!");
            // Display an error message to the user (e.g., using a TMP_Text component)
            return;
        }

        if (password != repeatPassword)
        {
            Debug.LogWarning("Passwords do not match!");
            // Display an error message to the user
            return;
        }

        // Basic email format check (can be more robust)
        if (!email.Contains("@") || !email.Contains("."))
        {
            Debug.LogWarning("Invalid email format!");
            return;
        }

        Debug.Log($"Username: {username}, Email: {email}, Password: {password}");
        // Here you would typically send this data to a server or save it locally.

        HidePopup(); // Hide the form after successful submission
    }

    void OnCancelButtonClicked()
    {
        Debug.Log("Cancel button clicked!");
        // Clear input fields when cancelling
        usernameInput.text = "";
        emailInput.text = "";
        passwordInput.text = "";
        repeatPasswordInput.text = "";
        HidePopup(); // Hide the form
    }
}