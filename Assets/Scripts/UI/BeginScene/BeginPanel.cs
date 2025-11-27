using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton startButton;
    public UIButton rankButton;
    public UIButton settingButton;
    public UIButton quitButton;
    public override void InitPanel()
    {
        this.startButton.onClick.Add(new EventDelegate(() =>
        {
            // 显示选择面板
            Debug.Log("Start Button Clicked: Show Select Panel");
            // 隐藏当前面板
            this.HidePanel();
        }));

        this.rankButton.onClick.Add(new EventDelegate(() =>
        {
            // 显示排行榜面板
            // Debug.Log("Rank Button Clicked: Show Rank Panel");
            RankPanel.instance.ShowPanel();
        }));

        this.settingButton.onClick.Add(new EventDelegate(() =>
        {
            // 显示设置面板
            // Debug.Log("Setting Button Clicked: Show Setting Panel");
            SettingPanel.instance.ShowPanel();
        }));

        this.quitButton.onClick.Add(new EventDelegate(() =>
        {
            // 退出游戏
            Debug.Log("Quit Button Clicked: Quit Game");
            Application.Quit();
        }));
    }
}
