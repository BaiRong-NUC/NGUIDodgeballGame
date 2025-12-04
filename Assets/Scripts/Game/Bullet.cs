using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData = null;


    // 初始化
    public void InitBullet(BulletData data)
    {
        this.bulletData = data;
        // 设置销毁时间
        Invoke("DelayDestory", this.bulletData.lifeTime); // 对象被移除,延迟函数不会进行,避免报错
        this.Update();
    }

    // 延时销毁
    private void DelayDestory()
    {
        this.Dead();
    }

    public void Dead()
    {
        // 创建特效
        if(this.bulletData.hitEffectPrefabPath != null)
        {
            GameObject hitEffectPrefab = Resources.Load<GameObject>(this.bulletData.hitEffectPrefabPath);
            if(hitEffectPrefab != null)
            {
                GameObject hitEffectInstance = Instantiate(hitEffectPrefab, this.transform.position, Quaternion.identity);
                hitEffectInstance.transform.position = this.transform.position;
                Destroy(hitEffectInstance, 0.5f); // 0.5秒后销毁特效实例
            }
        }

        // 销毁子弹
        Destroy(this.gameObject);
    }

    // 碰撞检测
    private void OnTriggerEnter(Collider collision)
    {
        // 检测是否碰撞到敌人
        if (collision.gameObject.CompareTag("Player"))
        {
            // print("子弹碰撞到玩家");
            AirPlane airPlane = collision.gameObject.GetComponent<AirPlane>();
            if (airPlane != null)
            {
                // 造成伤害
                airPlane.TakeDamage();
            }
            // 子弹销毁
            this.Dead();
        }
    }

    // 子弹移动
    void Update()
    {
        if(this.bulletData == null)
        {
            // 未初始化数据 直接返回
            Debug.LogError("子弹未初始化数据");
            return;
        }
        this.transform.Translate(Vector3.forward * this.bulletData.forwardSpeed * Time.deltaTime);
        //横向移动
        switch (this.bulletData.moveType)
        {
            case BulletMoveType.Straight:
                // 直线不变
                break;
            case BulletMoveType.Sin:
                // Sin曲线移动
                // 这里规定rightSpeed为频率，routeSpeed为振幅
                this.transform.Translate(Vector3.right * Mathf.Sin(Time.time * this.bulletData.rightSpeed) * Time.deltaTime * this.bulletData.routeSpeed);
                break;
            case BulletMoveType.Parabola:
                this.transform.rotation *= Quaternion.AngleAxis(this.bulletData.routeSpeed * Time.deltaTime, Vector3.up);
                break;
            case BulletMoveType.Tracking:
                // 面向玩家移动
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(AirPlane.instance.transform.position - this.transform.position), this.bulletData.routeSpeed * Time.deltaTime);
                break;
        }
    }
}

