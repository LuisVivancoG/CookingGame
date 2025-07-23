using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textDisplay;
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            StartCoroutine(DelayedAwake());
        }

        void CountdownUpdate(object sender, int currentCount)
        {
            _textDisplay.text = new string("Ready?\n" + currentCount);
        }

        private void OnTimeChanged(object sender, TimeSpan newTime) //Updates time display based on current time span
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

        IEnumerator DelayedAwake()
        {
            yield return new WaitForSeconds(1f);
            TimeManager.Instance.OnTimeChanged += OnTimeChanged;
            TimeManager.Instance.OnInitialCountDown += CountdownUpdate;
        }
    }
}
