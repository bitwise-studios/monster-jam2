using UnityEngine;
using System.Collections;

public class RestarterSound : MonoBehaviour {

    [SerializeField] private AudioClip deathClip;
    private float clipLength;

    public static void StopAllAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    void Start()
    {
        clipLength = deathClip.length;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StopAllAudio();
            AudioSource.PlayClipAtPoint(deathClip, Vector3.zero);
            System.Threading.Thread.Sleep((int) (clipLength * 1000));
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}
