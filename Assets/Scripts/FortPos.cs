using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PosType
{
    TopLeft,
    Top,
    TopRight,
    Left,
    Right,
    BottomLeft,
    Bottom,
    BottomRight
}

public class FortPos : MonoBehaviour
{
    public PosType posType;

    private Vector3 screenPosition; //屏幕上的点

    private Vector3 beginDirection; // 炮台初始开火的方向
    private Vector3 curDirection; // 当前炮台开火的方向

    // 散射时,每颗子弹的间隔角度
    public float changeAngle;

    // 炮台开火的信息
    public FortData fortData = null;

    // 当前炮台的CD,发射的子弹数量
    public float currentCD = 0;
    public int currentNum = 0;
    // 当前炮台的延迟时间
    public float currentDelay = 0;

    // 炮台要发射的子弹信息
    public BulletData bulletData = null;

    // 测试用数据
    Vector3 testPos;

    void Start()
    {
        this.screenPosition = Camera.main.WorldToScreenPoint(AirPlane.instance.transform.position);
        this.UpdatePosition();
        this.testPos = AirPlane.instance.transform.position;
    }
    void Update()
    {
        // 分辨率自适应
        // this.UpdatePosition();
        // 初始化炮台数据
        this.InitFortData();
        // 检测开火
        this.UpdateFire();
    }
    // 分辨率自适应
    private void UpdatePosition()
    {
        // 将屏幕坐标系转化为世界坐标系
        switch (posType)
        {
            case PosType.TopLeft:
                this.screenPosition = new Vector3(0, Screen.height, this.screenPosition.z);
                this.beginDirection = Vector3.right;
                break;
            case PosType.Top:
                this.screenPosition = new Vector3(Screen.width / 2, Screen.height, this.screenPosition.z);
                this.beginDirection = Vector3.right;
                break;
            case PosType.TopRight:
                this.screenPosition = new Vector3(Screen.width, Screen.height, this.screenPosition.z);
                this.beginDirection = Vector3.left;
                break;
            case PosType.Left:
                this.screenPosition = new Vector3(0, Screen.height / 2, this.screenPosition.z);
                this.beginDirection = Vector3.right;
                break;
            case PosType.Right:
                this.screenPosition = new Vector3(Screen.width, Screen.height / 2, this.screenPosition.z);
                this.beginDirection = Vector3.left;
                break;
            case PosType.BottomLeft:
                this.screenPosition = new Vector3(0, 0, this.screenPosition.z);
                this.beginDirection = Vector3.right;
                break;
            case PosType.Bottom:
                this.screenPosition = new Vector3(Screen.width / 2, 0, this.screenPosition.z);
                this.beginDirection = Vector3.right;
                break;
            case PosType.BottomRight:
                this.screenPosition = new Vector3(Screen.width, 0, this.screenPosition.z);
                this.beginDirection = Vector3.left;
                break;
        }
        this.transform.position = Camera.main.ScreenToWorldPoint(this.screenPosition);
    }

    // 初始化炮台数据
    private void InitFortData()
    {
        this.currentDelay -= Time.deltaTime;
        if ((this.currentCD != 0 && this.currentNum != 0) || this.currentDelay > 0)
        {
            // 已经初始化过了,或处于炮台冷却期
            return;
        }

        // 初始化一个炮台数据
        this.fortData = DataManage.instance.fortDatas.fortDatas[Random.Range(0, DataManage.instance.fortDatas.fortDatas.Count)];

        // 记录当前炮台的数据
        this.currentCD = this.fortData.cd;
        this.currentNum = this.fortData.number;
        this.currentDelay = this.fortData.delayTime;

        // 初始化一个子弹数据
        // 获取子弹ID随机范围
        string rangeStr = this.fortData.bulletIdRange;
        string[] ranges = rangeStr.Split(',');
        this.bulletData = DataManage.instance.bulletDatas.bulletDatas[Random.Range(int.Parse(ranges[0]), int.Parse(ranges[1]) + 1) - 1];

        // 初始化角度差值
        switch (this.fortData.type)
        {
            // 直线子弹
            case FortType.Sequence:
                this.changeAngle = 0;
                break;
            // 散射子弹
            case FortType.Scatter:
                // 根据初始位置的不同计算角度差值
                switch (this.posType)
                {
                    case PosType.TopLeft:
                    case PosType.TopRight:
                    case PosType.BottomLeft:
                    case PosType.BottomRight:
                        // 90度拆分 
                        this.changeAngle = 90f / (this.currentNum + 1);
                        break;
                    case PosType.Top:
                    case PosType.Bottom:
                    case PosType.Left:
                    case PosType.Right:
                        // 180度拆分
                        this.changeAngle = 180f / (this.currentNum + 1);
                        break;
                }
                break;
        }
        // print("角度差值:" + this.changeAngle);
    }

    // 检测开火
    private void UpdateFire()
    {
        if (this.currentNum == 0 && this.currentCD == 0)
        {
            // 当前状态 是不需要发射子弹的
            return;
        }
        // 炮台CD
        this.currentCD -= Time.deltaTime;
        if (this.currentCD > 0)
        {
            // 还在CD时间内
            return;
        }
        // 发射子弹
        GameObject bullet = null;
        switch (this.fortData.type)
        {
            case FortType.Sequence:
                // 直线子弹
                bullet = Instantiate(Resources.Load<GameObject>(this.bulletData.bulletPrefabPath));
                bullet.AddComponent<Bullet>().InitBullet(this.bulletData);

                // 初始化位置与朝向
                bullet.transform.position = this.transform.position;
                // bullet.transform.rotation = Quaternion.LookRotation(AirPlane.instance.transform.position - this.transform.position);
                bullet.transform.rotation = Quaternion.LookRotation(this.testPos - this.transform.position);

                // 记录发射数量与重置CD
                this.currentNum -= 1;
                this.currentCD = this.currentNum == 0 ? 0 : this.fortData.cd;
                break;
            case FortType.Scatter:
                // 散射子弹
                if (this.currentCD == 0)
                {
                    // 无cd 一瞬间 发射所有的散弹
                    for (int i = 0; i < this.currentNum; i++)
                    {
                        bullet = Instantiate(Resources.Load<GameObject>(this.bulletData.bulletPrefabPath));
                        bullet.AddComponent<Bullet>().InitBullet(this.bulletData);

                        // 计算每颗子弹的朝向与位置
                        bullet.transform.position = this.transform.position;

                        this.curDirection = Quaternion.AngleAxis(this.changeAngle * (i + 1), Vector3.up) * this.beginDirection;
                        // print("当前子弹方向:" + this.curDirection);
                    }
                    this.currentNum = 0;
                }
                else
                {
                    // 有cd 分批发射散弹
                    bullet = Instantiate(Resources.Load<GameObject>(this.bulletData.bulletPrefabPath));
                    bullet.AddComponent<Bullet>().InitBullet(this.bulletData);
                    // print("子弹移动类型:" + this.bulletData.moveType);

                    // 计算每颗子弹的朝向与位置
                    bullet.transform.position = this.transform.position;

                    this.curDirection = Quaternion.AngleAxis(this.changeAngle * (this.fortData.number - this.currentNum + 1), Vector3.up) * this.beginDirection;
                    bullet.transform.rotation = Quaternion.LookRotation(this.curDirection);

                    // 记录发射数量与重置CD
                    this.currentNum -= 1;
                    this.currentCD = this.currentNum == 0 ? 0 : this.fortData.cd;

                }
                break;
        }
    }
}