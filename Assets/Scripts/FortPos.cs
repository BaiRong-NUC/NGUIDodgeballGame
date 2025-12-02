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

    private Vector3 screenPosition;

    private Vector3 direction; // 炮台初始开火的方向
    void Start()
    {
        this.screenPosition = Camera.main.WorldToScreenPoint(AirPlane.instance.transform.position);
        this.UpdatePosition();
    }
    void Update()
    {
        // 分辨率自适应
        // this.UpdatePosition();
    }
    private void UpdatePosition()
    {
        // 将屏幕坐标系转化为世界坐标系
        switch (posType)
        {
            case PosType.TopLeft:
                this.screenPosition = new Vector3(0, Screen.height, this.screenPosition.z);
                this.direction = Vector3.right;
                break;
            case PosType.Top:
                this.screenPosition = new Vector3(Screen.width / 2, Screen.height, this.screenPosition.z);
                this.direction = Vector3.right;
                break;
            case PosType.TopRight:
                this.screenPosition = new Vector3(Screen.width, Screen.height, this.screenPosition.z);
                this.direction = Vector3.left;
                break;
            case PosType.Left:
                this.screenPosition = new Vector3(0, Screen.height / 2, this.screenPosition.z);
                this.direction = Vector3.right;
                break;
            case PosType.Right:
                this.screenPosition = new Vector3(Screen.width, Screen.height / 2, this.screenPosition.z);
                this.direction = Vector3.left;
                break;
            case PosType.BottomLeft:
                this.screenPosition = new Vector3(0, 0, this.screenPosition.z);
                this.direction = Vector3.right;
                break;
            case PosType.Bottom:
                this.screenPosition = new Vector3(Screen.width / 2, 0, this.screenPosition.z);
                this.direction = Vector3.right;
                break;
            case PosType.BottomRight:
                this.screenPosition = new Vector3(Screen.width, 0, this.screenPosition.z);
                this.direction = Vector3.left;
                break;
        }
        this.transform.position = Camera.main.ScreenToWorldPoint(this.screenPosition);
    }
}
