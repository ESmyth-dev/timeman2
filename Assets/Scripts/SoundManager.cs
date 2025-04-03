using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
    public AudioClip[] playlist; // Assign your songs in the Inspector
    public AudioSource audioSource;
    private int currentTrack = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (playlist.Length > 0)
        {
            //Pick a random track to start with
            currentTrack = Random.Range(0, playlist.Length);
            PlayTrack(currentTrack);
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying) // When the song finishes, play the next
        {
            NextTrack();
        }
    }

    void PlayTrack(int trackIndex)
    {
        audioSource.clip = playlist[trackIndex];
        audioSource.Play();
    }

    void NextTrack()
    {
        currentTrack = (currentTrack + 1) % playlist.Length; // Loops back to first song
        PlayTrack(currentTrack);
    }
}
