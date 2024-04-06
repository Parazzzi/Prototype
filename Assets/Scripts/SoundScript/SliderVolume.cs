using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    private SoundManager soundManager;
    [SerializeField] private Slider slider;

    [SerializeField] private Image SoundOn;
    [SerializeField] private Image SoundOff;

    private void OnEnable()
    {
        soundManager = SoundManager.instance;
        if (soundManager != null)
        {
            UpdateSliderValue(soundManager.volumeData.volume);
        }
    }

    private void Start()
    {
        if (soundManager != null)
        {
            slider.onValueChanged.AddListener(soundManager.SetVolume);
        }
    }

    private void Update()
    {
        UpdateSoundImage();
    }

    public void UpdateSliderValue(float value)
    {
        slider.value = value;
    }

    private void UpdateSoundImage()
    {
        if(slider.value == 0f)
        {
            SoundOn.gameObject.SetActive(false);
            SoundOff.gameObject.SetActive(true);
        }
        else
        {
            SoundOn.gameObject.SetActive(true);
            SoundOff.gameObject.SetActive(false);
        }
    }

}
