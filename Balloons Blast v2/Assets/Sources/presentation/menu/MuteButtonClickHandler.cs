using UnityEngine;
using UnityEngine.UI;

class MuteButtonClickHandler : MonoBehaviour
{
    
    public AudioSource backgroundAudio;
    
    void Start()
    {
        HandleAudioState();
    }
    
    public void HandleAudioState()
    {
        bool isAudioEnabled = ApplicationAudioController.IsAudioEnabled();
        
        ApplicationAudioController.ChangeAudioState(!isAudioEnabled);
        
        Sprite audioToggleSprite;
        if (isAudioEnabled)
        {
            audioToggleSprite = Resources.Load<Sprite>("sound_muted");
            backgroundAudio.Stop();
        }
        else
        {
            audioToggleSprite = Resources.Load<Sprite>("sound_unmuted");
            backgroundAudio.Play();
        }
        
        gameObject.GetComponent<Image>()
            .sprite = audioToggleSprite;
    }
}