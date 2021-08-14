using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace MainPanel
{
    public class FliesPanel : MonoBehaviour
    {
        [SerializeField] private int _eatID;
        [SerializeField] private GameObject _flyPrefab;
        [SerializeField] private Slider _slider;
        [SerializeField] private RectTransform _fliesContainer;
        [SerializeField] private int _flyCount;

        public int EatID => _eatID;

        public void SetValueWithoutNotify(float value) => _slider.SetValueWithoutNotify(value);
        public TweenerCore<float, float, FloatOptions> DOValue(float endValue, float duration) => _slider.DOValue(endValue, duration);

        private void Awake()
        {
            for (var i = 0; i < _flyCount; i++)
            {
                Instantiate(_flyPrefab, _fliesContainer);
            }
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
            for (var i = 0; i < _fliesContainer.childCount; i++)
            {
                var child = _fliesContainer.GetChild(i);
                child.gameObject.SetActive(active);
                child.GetComponent<Image>().enabled = true;
            }
        }
    }
}