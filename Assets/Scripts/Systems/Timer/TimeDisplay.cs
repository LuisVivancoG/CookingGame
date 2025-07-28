using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimeDisplay : Singleton<TimeDisplay>
    {
        [SerializeField] private TMP_Text _textDisplay;
        [SerializeField] private Slider _slider;
        [SerializeField] private SquashAndStretch _textSquash;

        [Header("Initial Countdown")]
        [SerializeField] private List<string> _countdownTexts;

        public Slider Slider => _slider;

        public void TuneEvents()
        {
            TimeManager.Instance.OnTimeChanged += OnTimeChanged;
            TimeManager.Instance.OnInitialCountDown += CountdownUpdate;
        }
        void CountdownUpdate(object sender, int currentCount)
        {
            _textDisplay.text = _countdownTexts[currentCount - 1];
            _textSquash.CheckForAndStartCoroutine();
        }

        private void OnTimeChanged(object sender, TimeSpan newTime) //Updates time display based on Stage MaxTime
        {
            int seconds = (int)newTime.TotalSeconds;
            _textDisplay.text = seconds.ToString();

            float normalized = Mathf.Clamp01(seconds / (float)TimeManager.Instance.MaxTime);
            _slider.value = normalized;
        }

        private void OnDisable()
        {
            TimeManager.Instance.OnTimeChanged -= OnTimeChanged;
            TimeManager.Instance.OnInitialCountDown -= CountdownUpdate;
        }
    }
}
