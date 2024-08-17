using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider MasterSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("GameMasterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume()
    {
        float Volume = MasterSlider.value;
        mixer.SetFloat("MasterVolume",Mathf.Log10(Volume)*20);

        PlayerPrefs.SetFloat("GameMasterVolume", Volume);
    }

    private void LoadVolume()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("GameMasterVolume");

        SetMasterVolume();
    }
}
