using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MainPanel
{
    public class FlyPanelsController : MonoBehaviour
    {
        [SerializeField] private List<FliesPanel> _flyPanels;
        [SerializeField] private List<FrogController> _frogControllers;

        private int _currentPanelIndex;

        // Start is called before the first frame update
        private void Start()
        {
            foreach (var panel in _flyPanels)
            {
                panel.SetActive(false);
                panel.SetValueWithoutNotify(0f);
            }

            _currentPanelIndex = _flyPanels.Count - 1;
            CreateTween();
        }

        private void CreateTween()
        {
            _flyPanels[_currentPanelIndex].SetActive(false);

            _currentPanelIndex++;
            if (_currentPanelIndex > _flyPanels.Count - 1)
            {
                _currentPanelIndex = 0;
            }

            var currentPanel = _flyPanels[_currentPanelIndex];
            currentPanel.SetValueWithoutNotify(0f);
            currentPanel.SetActive(true);
            SetEatStatus(currentPanel.EatID);

            var tween = currentPanel
                .DOValue(1f, 5f)
                .SetEase(Ease.Linear)
                .OnComplete(CreateTween)
                .Play();
        }

        private void SetEatStatus(int eatID)
        {
            foreach (var frog in _frogControllers)
            {
                frog.ShouldEatAll = frog.EatID == eatID;
            }
        }
    }
}
