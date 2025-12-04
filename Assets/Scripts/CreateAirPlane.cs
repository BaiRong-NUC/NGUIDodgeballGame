using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAirPlane : MonoBehaviour
{
    public Camera AirplaneCamera;
    void Awake()
    {
        // 创建玩家飞机
        AirplaneData airplaneData = DataManage.instance.GetCurrentAirplaneData();
        GameObject airPlanePrefab = Instantiate(Resources.Load<GameObject>(airplaneData.resPath));
        if (airPlanePrefab != null)
        {

            AirPlane airPlane = airPlanePrefab.AddComponent<AirPlane>();
            airPlane.speed = airplaneData.speed * 10;
            airPlane.maxHp = airplaneData.hp;
            airPlane.currentHp = airplaneData.hp;
            airPlane.routeSpeed = 10;

            airPlane.tag = "Player";
            airPlane.gameObject.layer = LayerMask.NameToLayer("Player");
            airPlane.transform.position = new Vector3(0, 0, 0);

            airPlane.targetCamera = AirplaneCamera;
        }
    }
    
    void Start()
    {
        GamePanel.instance.UpdateHp(DataManage.instance.GetCurrentAirplaneData().hp);
    }
}
