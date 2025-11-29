using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel rankLabel;
    public UILabel nameLabel;
    public UILabel timeLabel;

    public void SetRankItem(int rank, string playerName, float time)
    {
        this.rankLabel.text = rank.ToString();
        this.nameLabel.text = playerName;
        // 时间转换为xxx h xxx min xxx s
        this.timeLabel.text = DataManage.instance.FormatTime((int)time);
    }
}
