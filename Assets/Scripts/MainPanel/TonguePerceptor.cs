using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPanel
{
    public class TonguePerceptor : MonoBehaviour
    {
        public event Action OnFlyCaught;
        // public bool ShouldEatAll { get; set; }

        private List<GameObject> _caughtFlies = new List<GameObject>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<Image>().enabled = false;
            _caughtFlies.Add(other.gameObject);
            OnFlyCaught?.Invoke();
            // if (!ShouldEatAll)
            // {
            //     OnFlyCaught?.Invoke();
            // }
        }

        public int RemoveCaught()
        {
            var count = _caughtFlies.Count;
            foreach (var fly in _caughtFlies)
            {
                fly.SetActive(false);
            }
            _caughtFlies.Clear();
            return count;
        }
    }
}