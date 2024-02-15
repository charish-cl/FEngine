using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FlowText:MonoBehaviour
    {
        public TextMeshProUGUI textMeshPro;
        public RawImage image;
        public float dis = 100;
        public float h = 200;
        private void OnEnable()
        {
            image.color=Color.gray;
        }

        public void Flow(string str)
        {
            textMeshPro.text = str;
            Sequence sequence = DOTween.Sequence();
            Tween tween1 =  transform.DOMoveX(transform.position.x + dis, 0.2f);
            Tween tween2 = transform.DOMoveY(transform.position.y + h, 0.5f);
            Tween tween3 = image.DOFade(0, 1f);
            sequence.Append(tween1).Append(tween2).Join(tween3).onComplete= () =>
            {
                gameObject.SetActive(false);
            };
            sequence.Play();
        }
    }
}