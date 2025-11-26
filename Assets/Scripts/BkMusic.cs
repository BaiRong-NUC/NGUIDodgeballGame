using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkMusic : MonoBehaviour
{
    private static BkMusic _instance;

    public static BkMusic instance => _instance;

    public AudioSource audioSource;
    private void Awake()
    {
        BkMusic._instance = this;
        this.audioSource = this.GetComponent<AudioSource>();

        // 初始化值
        MusicData musicData = DataManage.instance.musicData;
        this.SetMusicVolume(musicData.musicVolume);
        this.SetMusicOnOff(musicData.isMusicOn);
    }

    // 设置值
    public void SetMusicVolume(float volume)
    {
        this.audioSource.volume = volume;
    }
    public void SetMusicOnOff(bool isOn)
    {
        this.audioSource.mute = !isOn;
    }
}
