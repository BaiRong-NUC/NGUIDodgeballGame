public class DataManage
{
    private static DataManage _instance = new DataManage();

    public static DataManage instance => _instance;

    // 音乐数据
    public MusicData musicData;
    private DataManage()
    {
        this.musicData = XmlDataManage.instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
    }

    public void SaveMusicData()
    {
        XmlDataManage.instance.SaveData(this.musicData, "MusicData");
    }

    public void SetMusicVolume(float volume)
    {
        this.musicData.musicVolume = volume;
        // 修改音乐音量
        BkMusic.instance.SetMusicVolume(volume);
    }

    public void SetSoundVolume(float volume)
    {
        this.musicData.soundVolume = volume;
        // 修改音效音量
    }

    public void SetMusicOnOff(bool isOn)
    {
        this.musicData.isMusicOn = isOn;
        // 修改音乐开关
        BkMusic.instance.SetMusicOnOff(isOn);
    }

    public void SetSoundOnOff(bool isOn)
    {
        this.musicData.isSoundOn = isOn;
        // 修改音效开关
    }
}