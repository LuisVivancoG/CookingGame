using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField] private TimeManager _timeManager;
        [SerializeField] private TMP_Text _textDisplay;
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            _timeManager.OnTimeChanged += OnTimeChanged;
        }

        private void OnTimeChanged(object sender, TimeSpan newTime) //Updates time display based on current time span
        {
            int seconds = (int)newTime.TotalSeconds;
            _textDisplay.text = seconds.ToString();

            float normalized = Mathf.Clamp01(seconds / (float)_timeManager.MaxTime);
            _slider.value = normalized;
        }

        private void OnDestroy()
        {
            _timeManager.OnTimeChanged -= OnTimeChanged;
        }
    }
}
