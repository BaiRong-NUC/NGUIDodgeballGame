using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    public UIButton closeButton;
    public UISlider musicSlider;
    public UISlider soundSlider;
    public UIToggle musicToggle;
    public UIToggle soundToggle;

    public override void InitPanel()
    {
        // 开始显示隐藏
        this.HidePanel();

        this.closeButton.onClick.Add(new EventDelegate(() =>
        {
            // 隐藏设置面板
            this.HidePanel();
        }));

        this.musicSlider.onChange.Add(new EventDelegate(() =>
        {
            // 调整音乐音量
            Debug.Log("Music Slider Changed: Volume = " + this.musicSlider.value);
        }));

        this.soundSlider.onChange.Add(new EventDelegate(() =>
        {
            // 调整音效音量
            Debug.Log("Sound Slider Changed: Volume = " + this.soundSlider.value);
        }));

        this.musicToggle.onChange.Add(new EventDelegate(() =>
        {
            // 切换音乐开关
            Debug.Log("Music Toggle Changed: Is On = " + this.musicToggle.value);
        }));

        this.soundToggle.onChange.Add(new EventDelegate(() =>
        {
            // 切换音效开关
            Debug.Log("Sound Toggle Changed: Is On = " + this.soundToggle.value);
        }));
    }

    override public void ShowPanel()
    {
        base.ShowPanel();
        // 初始化设置面板的状态
    }

    override public void HidePanel()
    {
        base.HidePanel();
        // 保存设置面板的状态
    }
}
