// 开火点数据
using System.Collections.Generic;
using System.Xml.Serialization;

public enum FortType
{
    Sequence = 0,   // 顺序开火
    Scatter = 1     // 散射开火
}

public class FortData
{
    [XmlAttribute]
    public int id;             // 开火点ID
    [XmlAttribute]
    public FortType type;           // 开火点类型（0-顺序,1-散射）
    [XmlAttribute]
    public int number;         // 开火点发射子弹的数量
    [XmlAttribute]
    public float cd;         // 每颗子弹发射间隔时间
    [XmlAttribute]
    public string bulletIdRange; // 子弹ID范围（逗号分隔）
    [XmlAttribute]
    public float delayTime;      // 每组延迟发射时间
}

public class FortDatas
{
    public List<FortData> fortDatas = new List<FortData>();
}