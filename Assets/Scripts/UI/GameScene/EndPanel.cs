using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : BasePanel<EndPanel>
{
    public UILabel timeLabel;
    public UIInput playerNameInput;
    public UIButton submitButton;
    public float finalTime = 0f;
    public override void InitPanel()
    {
        this.submitButton.onClick.Add(new EventDelegate(() =>
        {
            // 提交分数和玩家名称
            DataManage.instance.AddRankData(playerNameInput.value, (int)finalTime);
            // 返回开始场景
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene("BeginScene");
        }));

        this.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        // 显示游戏用时
        this.finalTime = GamePanel.instance.currentTime;
        this.timeLabel.text = GamePanel.instance.timeLabel.text;
        Time.timeScale = 0f;
    }
    public override void HidePanel()
    {
        base.HidePanel();
        Time.timeScale = 1f;
    }
}
