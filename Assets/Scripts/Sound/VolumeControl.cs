using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public string volueParameter = "MasterVolue";
    public AudioMixer mixer;
    public Slider slider;

    private float volumeValue;
    private const float kValueVolume = 20f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        volumeValue = Mathf.Log10(value)*kValueVolume;
        mixer.SetFloat(volueParameter, volumeValue);
    }

    private void Start()
    {
        volumeValue=PlayerPrefs.GetFloat(volueParameter, Mathf.Log10(slider.value) * kValueVolume);
        slider.value = Mathf.Pow(10f,volumeValue/kValueVolume);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volueParameter, volumeValue);
    }
}
