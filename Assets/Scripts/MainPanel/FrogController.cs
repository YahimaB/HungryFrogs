using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MainPanel
{
    public class FrogController : MonoBehaviour
    {
        private const int MinTongueLength = 50;

        [SerializeField] private int _eatID;
        [SerializeField] private Button _frogButton;
        [SerializeField] private RectTransform _tongue;
        [SerializeField] private TonguePerceptor _perceptor;
        [SerializeField] private int _maxTongueLength = 1050;
        [SerializeField] private int _pixelsPerSecond = 1000;

        public int EatID => _eatID;
        public bool ShouldEatAll { get; set; }

        private Tween _catchAnimation;

        private void Awake()
        {
            _frogButton.onClick.AddListener(TryCatchFly);
            _perceptor.OnFlyCaught += OnFlyCaught;

            ReturnTongue();
        }

        private void OnFlyCaught()
        {
            if (ShouldEatAll)
            {
                return;
            }
            _catchAnimation?.Kill();
            ReturnTongue();
        }

        private void TryCatchFly()
        {
            _frogButton.interactable = false;
            _perceptor.gameObject.SetActive(true);

            var size = _tongue.sizeDelta;
            var duration = (_maxTongueLength - size.y) / _pixelsPerSecond;
            var endValue = new Vector2(size.x, _maxTongueLength);

            _catchAnimation = DOTween.Sequence()
                .SetEase(Ease.Linear)
                .Append(_tongue.DOSizeDelta(endValue, duration))
                .OnComplete(ReturnTongue)
                .Play();
        }

        private void ReturnTongue()
        {
            var count = _perceptor.RemoveCaught();
            _perceptor.gameObject.SetActive(false);

            var size = _tongue.sizeDelta;
            var duration = (size.y - MinTongueLength) / _pixelsPerSecond;
            var endValue = new Vector2(size.x, MinTongueLength);

            var mySequence = DOTween.Sequence()
                .SetEase(Ease.Linear)
                .Append(_tongue.DOSizeDelta(endValue, duration))
                .OnComplete(() =>
                {
                    _frogButton.interactable = true;
                })
                .Play();
        }
    }
}