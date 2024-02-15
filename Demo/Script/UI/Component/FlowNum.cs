using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class FlowNum:MonoBehaviour
    {
        public TextMeshProUGUI textMeshProUGUI;
        public float h;
        private void OnEnable()
        {
            transform.localScale=Vector3.one;
            textMeshProUGUI.DOFade(1, 0);
        }

        public void Close()
        {
            swingTween.Kill();
            gameObject.SetActive(false);
        }
        public Tween swingTween;
        public void Swing(string text)
        {
            textMeshProUGUI.text = text;
            Tween tween = transform.DOScale(transform.localScale * 1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            tween.Play();
        }
        
        [Button]
        public void Flow(string tex,bool autohide=true)
        {
            textMeshProUGUI.text = tex;
            Sequence sequence = DOTween.Sequence();
            Tween tween1 =   textMeshProUGUI.transform.DOMoveY(transform.position.y+h, 0.5f);
            Tween tween2 = transform.DOScale(transform.localScale*1.2f,0.5f);
            Tween tween3 = textMeshProUGUI.DOFade(0, 1f);
            sequence.Append(tween1).Join(tween2).Join(tween3);
            if (autohide)
            {
                sequence.onComplete= () =>
                {
                    gameObject.SetActive(false);
                };
            }
      
            sequence.Play();
        }
    }
}