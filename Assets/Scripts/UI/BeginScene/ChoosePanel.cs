using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    public UIButton quitButton;
    public UIButton leftButton;
    public UIButton rightButton;
    public UIButton startButton;
    public Transform airplaneModelParent;

    //属性
    public GameObject hp;
    private List<GameObject> hpList = new List<GameObject>();
    public GameObject speed;
    private List<GameObject> speedList = new List<GameObject>();
    public GameObject volume;
    private List<GameObject> volumeList = new List<GameObject>();

    // 当前显示的飞机模型
    [HideInInspector]
    public GameObject currentAirplaneModel;
    public override void InitPanel()
    {
        this.startButton.onClick.Add(new EventDelegate(() =>
        {
            SceneManager.LoadScene("GameScene");
        }));
        this.quitButton.onClick.Add(new EventDelegate(() =>
        {
            this.HidePanel();
            BeginPanel.instance.ShowPanel();
        }));

        this.leftButton.onClick.Add(new EventDelegate(() =>
        {
            DataManage.instance.currentAirplaneIndex--;
            if (DataManage.instance.currentAirplaneIndex < 0)
            {
                DataManage.instance.currentAirplaneIndex = DataManage.instance.airplaneDatas.airplaneDatas.Count - 1;
            }
            SwitchAirplaneModel();
        }));

        this.rightButton.onClick.Add(new EventDelegate(() =>
        {
            DataManage.instance.currentAirplaneIndex++;
            if (DataManage.instance.currentAirplaneIndex >= DataManage.instance.airplaneDatas.airplaneDatas.Count)
            {
                DataManage.instance.currentAirplaneIndex = 0;
            }
            SwitchAirplaneModel();
        }));

        // 初始化属性图标列表
        for (int i = 0; i < hp.transform.childCount; i++)
        {
            GameObject hpIcon = hp.transform.GetChild(i).gameObject;
            hpList.Add(hpIcon.transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < speed.transform.childCount; i++)
        {
            GameObject speedIcon = speed.transform.GetChild(i).gameObject;
            speedList.Add(speedIcon.transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < volume.transform.childCount; i++)
        {
            GameObject volumeIcon = volume.transform.GetChild(i).gameObject;
            volumeList.Add(volumeIcon.transform.GetChild(0).gameObject);
        }

        this.HidePanel();
    }

    // 切换选择的模型
    public void SwitchAirplaneModel()
    {
        AirplaneData airplaneData = DataManage.instance.GetCurrentAirplaneData();
        // 销毁当前模型
        this.DestroyAirplaneModel();
        // 实例化新模型
        this.currentAirplaneModel = Instantiate(Resources.Load<GameObject>(airplaneData.resPath), airplaneModelParent, false);
        this.currentAirplaneModel.transform.localScale = Vector3.one * airplaneData.modelScale;
        this.currentAirplaneModel.transform.localPosition = Vector3.zero;
        this.currentAirplaneModel.transform.localRotation = Quaternion.Euler(Vector3.zero);
        this.currentAirplaneModel.layer=LayerMask.NameToLayer("UI");
        // 更新属性显示
        for(int i = 0; i < hpList.Count; i++)
        {
            hpList[i].SetActive(i < airplaneData.hp);
        }
        for (int i = 0; i < speedList.Count; i++)
        {
            speedList[i].SetActive(i < airplaneData.speed);
        }
        for (int i = 0; i < volumeList.Count; i++)
        {
            volumeList[i].SetActive(i < airplaneData.volume);
        }
    }
    public void DestroyAirplaneModel()
    {
        if (this.currentAirplaneModel != null)
        {
            Destroy(this.currentAirplaneModel);
            this.currentAirplaneModel = null;
        }
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        DataManage.instance.currentAirplaneIndex = 0;
        // 切换到当前选择的飞机模型
        this.SwitchAirplaneModel();
    }
    public override void HidePanel()
    {
        base.HidePanel();
        // 销毁当前模型
        this.DestroyAirplaneModel();
    }


    // 让模型上下浮动
    void Update()
    {
        if (this.currentAirplaneModel != null)
        {
            float yOffset = Mathf.Sin(Time.time * 2f) * 50f;
            this.currentAirplaneModel.transform.localPosition = new Vector3(0, yOffset, 0);
        }

        // 射线检测,让飞机模型旋转
        if (Input.GetMouseButton(0))
        {
            print("Mouse Dragging");
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 1 << LayerMask.NameToLayer("UI")))
            {
                float rotationY = Input.GetAxis("Mouse X") * 5f;
                this.currentAirplaneModel.transform.Rotate(Vector3.up, -rotationY, Space.Self);
                // this.currentAirplaneModel.transform.rotation *= Quaternion.AngleAxis(-Input.GetAxis("Mouse X") * 5f, Vector3.up);
            }
        }
    }
}
