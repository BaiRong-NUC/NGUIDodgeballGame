using System.Collections.Generic;
using System.Xml.Serialization;

public enum BulletMoveType
{
    Straight,   // 直线
    ParabolaLeft,   // 左抛物线
    ParabolaRight, // 右抛物线
    Tracking    // 跟踪
}

public class BulletData
{
    [XmlAttribute]
    public int id;                     // 子弹ID
    [XmlAttribute]
    public BulletMoveType moveType; // 子弹移动类型
    [XmlAttribute]
    public float forwardSpeed;    // 前进速度
    [XmlAttribute]
    public float rightSpeed;      // 右移速度
    [XmlAttribute]
    public float routeSpeed;      // 曲线速度
    [XmlAttribute]
    public string bulletPrefabPath; // 子弹预制体路径
    [XmlAttribute]
    public string hitEffectPrefabPath; // 命中效果预制体路径
    [XmlAttribute]
    public float lifeTime;        // 子弹存活时间
}

public class BulletDatas
{
    public List<BulletData> bulletDatas = new List<BulletData>();
}
