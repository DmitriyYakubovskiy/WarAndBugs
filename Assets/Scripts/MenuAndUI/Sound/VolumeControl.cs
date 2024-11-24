using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public string volueParameter = "MasterVolue";
    public bool isMusic=false;
    public AudioMixer mixer;
    public Slider slider;

    private float volumeValue;
    private const float kValueVolume = 20f;

    private void Start()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);

        if (isMusic) volumeValue = SaveSystem.Data.musicVolume;
        else volumeValue = SaveSystem.Data.soundVolume;
        slider.value = Mathf.Pow(10f, volumeValue / kValueVolume);
    }

    private void HandleSliderValueChanged(float value)
    {
        volumeValue = Mathf.Log10(value)*kValueVolume;
        mixer.SetFloat(volueParameter, volumeValue);
    }

    private void OnDisable()
    {
        if(isMusic) SaveSystem.Data.musicVolume = volumeValue;
        else SaveSystem.Data.soundVolume = volumeValue;
    }
}
