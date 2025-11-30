using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlane : MonoBehaviour
{
    public int maxHp;
    public int currentHp;
    public int speed;
    public int routeSpeed;
    public Camera targetCamera;
    // 旋转到的目标角度
    private Quaternion targetRotation;
    public bool isDead = false;

    // 飞机上一次的位置
    private Vector3 lastPosition;

    // 飞机相对于屏幕坐标系的位置
    private Vector3 screenPosition;

    private static AirPlane _instance;
    public static AirPlane instance => AirPlane._instance;


    private void Awake()
    {
        _instance = this;
    }

    // 飞机死亡
    public void Dead()
    {
        this.isDead = true;
        EndPanel.instance.ShowPanel(); //游戏结束
    }

    // 飞机受伤
    public void TakeDamage(int damage = 1)
    {
        if (isDead) return;

        this.currentHp -= damage;
        // 更新面板
        GamePanel.instance.UpdateHp(this.currentHp);
        if (this.currentHp <= 0)
        {
            this.currentHp = 0;
            this.isDead = true;
            this.Dead();
        }
    }

    void Update()
    {
        if (this.isDead == true)
        {
            return;
        }

        // 检测输入
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        this.lastPosition = this.transform.position;
        // 飞机移动(左右移动)修改x轴坐标
        this.transform.Translate(Vector3.right * h * speed * Time.deltaTime, Space.World);
        // 飞机移动(上下移动)修改z轴坐标
        this.transform.Translate(Vector3.forward * v * speed * Time.deltaTime, Space.World);

        // 限制飞机移动范围,不能飞出屏幕外
        this.screenPosition = targetCamera.WorldToScreenPoint(this.transform.position);
        // 溢出判断
        if (this.screenPosition.x < 0 || this.screenPosition.x > Screen.width)
        {
            // 飞机位置还原
            this.transform.position = new Vector3(this.lastPosition.x, this.transform.position.y, this.transform.position.z);
        }
        if (this.screenPosition.y < 0 || this.screenPosition.y > Screen.height)
        {
            // 飞机位置还原
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.lastPosition.z);
        }

        // 飞机旋转
        // 计算目标角度，假设最大倾斜角度为 30 度，向左移动(h<0)向左倾斜(z>0)
        targetRotation = Quaternion.Euler(0, 0, -h * 30);
        // 平滑旋转到目标角度
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, routeSpeed * Time.deltaTime);
    }
}
