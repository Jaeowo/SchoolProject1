using System;
using System.Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundDatas
{
    public float masterSoundValue;
    public float BGMSoundValue;
    public float SFXSoundValue;
}


public class SoundManager : MonoBehaviour
{
    private SoundDatas soundDatas;
    public SoundDatas GetSoundDatas() { return soundDatas; }

    //public AudioMixer audioMixer;

    //public Slider MasterSlider;
    //public Slider BGMSlider;
    //public Slider SFXSlider;

    //[SerializeField]
    //public GameObject slider;

    //GameObject tabObj;

    // ===========================================
    // Sound Resources

    [SerializeField]
    private GameObject SFXAudioresource;
    [SerializeField]
    private GameObject BGMAudioresource;

    [SerializeField]
    public SFXResource[] SFX;
    [SerializeField]
    public BGMResource[] BGM;

    private AudioSource SFXFromChild;
    private AudioSource BGMFromChild;

    void GetSFXresource()
    {
        SFXFromChild = SFXAudioresource.GetComponent<AudioSource>();
    }

    void GetBGMResource()
    {
        BGMFromChild = BGMAudioresource.GetComponent<AudioSource>();
    }

    [Serializable]
    public struct SFXResource
    {
        [SerializeField]
        public string SoundName;
        [SerializeField]
        public AudioClip audioClip;
    }
    [Serializable]
    public struct BGMResource
    {
        [SerializeField]
        public string SoundName;
        [SerializeField]
        public AudioClip audioClip;
    }

    // BGM Play
    public void PlayBGMSound(string _soundName)
    {
        foreach (var bgm in BGM)
        {
            if (bgm.SoundName == _soundName)
            {
                BGMFromChild.clip = bgm.audioClip;
                BGMFromChild.Play();
                return;
            }
        }
        Debug.Log(_soundName + "사운드를 찾지 못했습니다.");
    }

    public void PauseBGMSound()
    {
        if (BGMFromChild.isPlaying)
        {
            BGMFromChild.Pause();
        }
    }

    public void StopBGMSound()
    {
        if (BGMFromChild.isPlaying)
        {
            BGMFromChild.Stop();
        }
    }

    public void PlaySFXSound(string _soundName)
    {
        foreach (var sfx in SFX)
        {
            if (sfx.SoundName == _soundName)
            {
                SFXFromChild.clip = sfx.audioClip;
                SFXFromChild.Play();
                return;
            }
        }
        Debug.Log(_soundName + "sound X");
    }

    public void PauseSFXSound()
    {
        if (SFXFromChild.isPlaying)
        {
            BGMFromChild.Pause();
        }
    }

    public void StopSFXSound()
    {
        if (SFXFromChild.isPlaying)
        {
            BGMFromChild.Stop();
        }
    }
    //=================================================

    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GetSFXresource();
        GetBGMResource();
    }

    //void Update()
    //{
    //    SetSound(MasterSlider, "Master");
    //    SetSound(BGMSlider, "BGM");
    //    SetSound(SFXSlider, "SFX");

    //}

    //private void InitializeDefaultData()
    //{
    //    soundDatas.masterSoundValue = MasterSlider.value;
    //    soundDatas.SFXSoundValue = SFXSlider.value;
    //    soundDatas.BGMSoundValue = BGMSlider.value;
    //}

    //public void SetSound(Slider slider, string mixerGroup)
    //{
    //    float sound = slider.value;

    //    if (sound == -40.0f)
    //    {
    //        audioMixer.SetFloat(mixerGroup, -80);
    //    }
    //    else
    //    {
    //        audioMixer.SetFloat(mixerGroup, sound);
    //    }
    //}

    //public void SoundDataSave()
    //{
    //    soundDatas.masterSoundValue = MasterSlider.value;
    //    soundDatas.SFXSoundValue = SFXSlider.value;
    //    soundDatas.BGMSoundValue = BGMSlider.value;
    //}
}