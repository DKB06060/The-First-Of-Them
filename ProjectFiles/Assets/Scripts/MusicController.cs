using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [Header("Music File References")]
    [SerializeField] List<AudioClip> musicTracks;

    private void Start()
    {
        AudioSource musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicTracks[0];
        musicSource.Play();
    }
}