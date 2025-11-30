using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanel : BasePanel<GamePanel>
{
    public UIButton quitButton;
    public UILabel timeLabel;

    public Transform hp;
    private List<Transform> hpList = new List<Transform>();

    [HideInInspector]
    public float currentTime = 0f;
    public override void InitPanel()
    {
        // 初始化血量显示
        for (int i = 0; i < hp.childCount; i++)
        {
            Transform hpObj = hp.GetChild(i);
            this.hpList.Add(hpObj.GetChild(0));
        }

        // 绑定按钮事件
        this.quitButton.onClick.Add(new EventDelegate(() =>
        {
            // 显示确认退出面板
            QuitPanel.instance.ShowPanel();
        }));

        // this.UpdateHp(3);
    }

    void Update()
    {
        this.currentTime += Time.deltaTime;
        this.timeLabel.text = DataManage.instance.FormatTime((int)this.currentTime, false);

        if(Input.GetMouseButtonDown(0))
        {
            // 测试游戏结束面板
            EndPanel.instance.ShowPanel();
        }
    }

    public void UpdateHp(int hp)
    {
        for (int i = 0; i < this.hpList.Count; i++)
        {
            this.hpList[i].gameObject.SetActive(i < hp);
        }
    }
}
