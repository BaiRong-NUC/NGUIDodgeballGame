using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public UIButton yesButton;
    public UIButton noButton;
    public override void InitPanel()
    {
        this.yesButton.onClick.Add(new EventDelegate(() =>
        {
            // 退出游戏,返回开始场景
            Time.timeScale = 1f;
            SceneManager.LoadScene("BeginScene");
        }));
        this.noButton.onClick.Add(new EventDelegate(() =>
        {
            // 关闭面板
            this.HidePanel();
        }));
        this.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        Time.timeScale = 0f;
    }

    public override void HidePanel()
    {
        base.HidePanel();
        Time.timeScale = 1f;
    }
}
