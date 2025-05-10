using UnityEngine;
using TMPro;

public class LevelIntroController : MonoBehaviour
{
  
    [Header("Audio Settings")]
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioSource musicSource;

   
    

    void Start()
    {
        
        PlayLevelMusic();
    }

    void PlayLevelMusic()
    {
        if (levelMusic == null) return;

        musicSource.clip = levelMusic;
        musicSource.Play();    
        }
}