using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Timer
{
    public class TimeObserver : MonoBehaviour
    {
        [SerializeField] private List<Schedule> _schedules;

        [Serializable]
        private class Schedule
        {
            public int Seconds;
            public UnityEvent Action;
        }

        private TimeManager _levelTime;

        private void Awake()
        {
            _levelTime = FindAnyObjectByType<TimeManager>();
        }

        private void Start()
        {
            _levelTime.OnTimeChanged += CheckShedules;
        }

        private void OnDestroy()
        {
            _levelTime.OnTimeChanged -= CheckShedules;
        }

        private void CheckShedules(object sender, TimeSpan newTime)
        {
            var schedule = _schedules.FirstOrDefault(s => s.Seconds == newTime.Seconds);
            schedule?.Action?.Invoke();
        }
    }
}
