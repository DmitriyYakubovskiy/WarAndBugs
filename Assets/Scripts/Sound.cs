using UnityEngine;

public class Sound : MonoBehaviour
{
    public SoundArray[] sounds;
    private AudioSource audioSrc=>GetComponent<AudioSource>();

    public void PlaySound()
    {
        AudioClip clip = sounds[0].soundArray[Random.Range(0, sounds[0].soundArray.Length)];
        audioSrc.pitch = Random.Range(0.85f, 1.2f);
        audioSrc.PlayOneShot(clip,1f);
    }

    public void PlaySound(int i, float volume = 1f, float p1 = 0.85f, float p2 = 1.2f, bool isDestroyed=false)
    {
        AudioClip clip = sounds[i].soundArray[Random.Range(0, sounds[i].soundArray.Length)];
        audioSrc.pitch = Random.Range(p1, p2);
        if (isDestroyed) AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        else audioSrc.PlayOneShot(clip, volume); 
    }

    public void AudioStop()
    {
        if (audioSrc.isPlaying) audioSrc.Stop();
    }

    public void AudioStart(int index = 0)
    {
        if (!audioSrc.isPlaying) PlaySound(index);
    }

    [System.Serializable]
    public class SoundArray
    {
        public AudioClip[] soundArray;
    }
}
