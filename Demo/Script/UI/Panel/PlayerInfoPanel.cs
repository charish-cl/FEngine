using FEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace DefaultNamespace
{
    public class PlayerInfoPanel : UIWindow
    {
        public TextMeshProUGUI MyCoin;
        public TextMeshProUGUI HpInfo;
        public Image ExperienceBar;
        public Image HpBar;
        public TextMeshProUGUI ExperienceBarInfo;
        public TextMeshProUGUI PlayerName;

        public float RawWidth;
        public void Awake()
        {
            MyCoin = transform.FindChildComponent<TextMeshProUGUI>("CoinInfo/Text (TMP)_MyCoin");
            HpInfo = transform.FindChildComponent<TextMeshProUGUI>("Image/Text (TMP)_HpInfo");
            ExperienceBar = transform.FindChildComponent<Image>("Image (1)/Image_ExperienceBar");
            HpBar = transform.FindChildComponent<Image>("Image/Image_HpBar");
            ExperienceBarInfo = transform.FindChildComponent<TextMeshProUGUI>("Image (1)/Text (TMP)_ExperienceBarInfo");
            PlayerName = transform.FindChildComponent<TextMeshProUGUI>("Text (TMP)_PlayerName");
            RawWidth = HpBar.GetComponent<RectTransform>().rect.width;
        }

    }
}