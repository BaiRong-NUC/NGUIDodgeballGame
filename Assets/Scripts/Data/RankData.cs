using System.Collections.Generic;
using System.Xml.Serialization;

// 单条数据
public class RankData
{
    [XmlAttribute]
    public string userName;
    [XmlAttribute]
    public int time;
}

public class RankDatas
{
    public List<RankData> rankDataList = new List<RankData>();
}