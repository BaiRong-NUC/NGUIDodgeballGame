public class DataManage
{
    private static DataManage _instance = new DataManage();

    public static DataManage instance => _instance;

    // 音乐数据
    public MusicData musicData;

    // 排行榜数据
    public RankDatas rankDatas = new RankDatas();

    // 飞机数据
    public AirPlaneDatas airplaneDatas = new AirPlaneDatas();

    // 当前选择的飞机索引
    public int currentAirplaneIndex = 0;

    private DataManage()
    {
        this.musicData = XmlDataManage.instance.LoadData(typeof(MusicData), "MusicData.xml") as MusicData;
        this.rankDatas = XmlDataManage.instance.LoadData(typeof(RankDatas), "RankDatas.xml") as RankDatas;
        this.airplaneDatas = XmlDataManage.instance.LoadData(typeof(AirPlaneDatas), "AirplaneDatas.xml") as AirPlaneDatas;
    }

    public void SaveMusicData()
    {
        XmlDataManage.instance.SaveData(this.musicData, "MusicData.xml");
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

    public void SaveRankDatas()
    {
        XmlDataManage.instance.SaveData(this.rankDatas, "RankDatas.xml");
    }

    // 添加排行榜数据
    public void AddRankData(string userName, int time)
    {
        RankData newRankData = new RankData();
        newRankData.userName = userName;
        newRankData.time = time;
        // 按照时间从大到小排列
        this.rankDatas.rankDataList.Add(newRankData);
        this.rankDatas.rankDataList.Sort((a, b) => b.time.CompareTo(a.time));
        // 只保留前20名
        if (this.rankDatas.rankDataList.Count > 20)
        {
            this.rankDatas.rankDataList.RemoveRange(20, this.rankDatas.rankDataList.Count - 20);
        }
        SaveRankDatas();
    }

    // 获取当前选择的飞机数据
    public AirplaneData GetCurrentAirplaneData()
    {
        return this.airplaneDatas.airplaneDatas[this.currentAirplaneIndex];
    }

    // 时间格式化
    public string FormatTime(int time, bool keepZero = true)
    {
        // 时间转换为xxx h xxx min xxx s
        int hours = (int)(time / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);
        if (!keepZero)
        {
            if (hours == 0)
            {
                if (minutes == 0)
                {
                    return $"{seconds}s";
                }
                else
                {
                    return $"{minutes}m{seconds}s";
                }
            }
        }
        return $"{hours}h{minutes}m{seconds}s";
    }
}