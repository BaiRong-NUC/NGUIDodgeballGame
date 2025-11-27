using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton closeButton;

    public UIScrollView rankScrollView;

    public List<RankItem> rankItemList = new List<RankItem>();

    private GameObject rankItemPrefab;

    private Vector3 initialItemPosition = new Vector3(41, 9.5f, 0);

    public override void InitPanel()
    {
        this.closeButton.onClick.Add(new EventDelegate(() =>
        {
            this.HidePanel();
        }));

        this.rankItemPrefab = Resources.Load<GameObject>("Prefabs/RankItem");
        this.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        // 获取xml中的排行榜数据,将数据赋值给rankItem上
        List<RankData> rankDataList = DataManage.instance.rankDatas.rankDataList;
        for (int i = 0; i < rankDataList.Count; i++)
        {
            // 如果面板上已经存在组合控件,则直接更新数据
            if (i < this.rankItemList.Count)
            {
                RankItem item = this.rankItemList[i];
                item.SetRankItem(i + 1, rankDataList[i].userName, rankDataList[i].time);
            }
            else
            {
                // 如果面板上不存在组合控件,则实例化一个新的组合控件
                GameObject rankItemObj = GameObject.Instantiate(this.rankItemPrefab, this.rankScrollView.transform, false);
                // 设置预制体背景大小为ScrollView宽度
                UISprite sprite = rankItemObj.GetComponent<UISprite>();
                if (sprite != null)
                {
                    sprite.width = (int)this.rankScrollView.panel.GetViewSize().x;
                }
                RankItem item = rankItemObj.GetComponent<RankItem>();
                item.SetRankItem(i + 1, rankDataList[i].userName, rankDataList[i].time);
                this.rankItemList.Add(item);
                // 设置位置
                rankItemObj.transform.localPosition = this.initialItemPosition + new Vector3(0, -(sprite.height - 5) * i, 0);
            }
        }
    }
}
