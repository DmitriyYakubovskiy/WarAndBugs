using UnityEngine;

public class Sound : MonoBehaviour
{
    public SoundArray[] sounds;
    private AudioSource audioSrc=>GetComponent<AudioSource>();

    protected void PlaySound(int i, float volume = 1f, float p1 = 0.85f, float p2 = 1.2f)
    {
        AudioClip clip = sounds[i].soundArray[Random.Range(0, sounds[i].soundArray.Length)];
        audioSrc.pitch = Random.Range(p1, p2);
        audioSrc.PlayOneShot(clip, volume); 
    }

    [System.Serializable]
    public class SoundArray
    {
        public AudioClip[] soundArray;
    }
}
