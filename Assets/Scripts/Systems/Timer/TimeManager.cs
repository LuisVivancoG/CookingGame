using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Timer
{
    public class TimeManager : MonoBehaviour
    {
        private int _maxTime;
        private GameManager _gameManager;
        private TimeSpan CurrentTime;
        private float _timerSpan = 1;

        public int MaxTime => _maxTime;
        public EventHandler<TimeSpan> OnTimeChanged;
        public UnityEvent OnTimeConsumed;
        public UnityEvent OnTimerFinished;

        public void SetUp(GameManager gameManager, int time)
        {
            _gameManager = gameManager;
            _maxTime = time;

            CurrentTime = TimeSpan.FromSeconds(_maxTime);
        }

        public void InitiateTimer()
        {
            StartCoroutine(Countdown());
        }

        public void TimeConsume(int timeRested)
        {
            StopAllCoroutines();
            CurrentTime -= TimeSpan.FromSeconds(timeRested);
            if (CurrentTime < TimeSpan.Zero) CurrentTime = TimeSpan.Zero;

            OnTimeConsumed.Invoke();
            OnTimeChanged?.Invoke(this, CurrentTime);

            if (CurrentTime.TotalSeconds > 0)
                StartCoroutine(Countdown());
            else
                OnTimerFinished?.Invoke();
        }

        public void FrezeTime()
        {
            StopAllCoroutines();
        }

        IEnumerator Countdown()
        {
            while (CurrentTime.TotalSeconds > 0)
            {
                yield return new WaitForSeconds(_timerSpan);
                CurrentTime -= TimeSpan.FromSeconds(1);

                if (CurrentTime < TimeSpan.Zero)
                    CurrentTime = TimeSpan.Zero;

                OnTimeChanged?.Invoke(this, CurrentTime);
            }
            OnTimerFinished?.Invoke();
        }
    }
}
