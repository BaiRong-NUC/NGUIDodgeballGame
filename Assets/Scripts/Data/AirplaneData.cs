using System.Collections.Generic;
using System.Xml.Serialization;

public class AirplaneData
{
    [XmlAttribute]
    public int hp;
    [XmlAttribute]
    public int speed;
    [XmlAttribute]
    public int volume;
    [XmlAttribute]
    public string resPath;

    // 模型展示时的大小
    [XmlAttribute]
    public float modelScale;
}

public class AirPlaneDatas
{
    public List<AirplaneData> airplaneDatas = new List<AirplaneData>();
}