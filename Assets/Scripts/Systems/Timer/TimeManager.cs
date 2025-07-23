using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Timer
{
    public class TimeManager : Singleton<TimeManager>
    {
        private int countdown;
        public EventHandler<int> OnInitialCountDown;

        private int _maxTime;
        private GameManager _gameManager;
        private TimeSpan CurrentTime;
        private float _timerSpan = 1;

        public int MaxTime => _maxTime;
        public EventHandler<TimeSpan> OnTimeChanged;
        public UnityEvent OnTimerFinished;

        public void SetUp(GameManager gameManager, int time)
        {
            _gameManager = gameManager;
            _maxTime = time;

            CurrentTime = TimeSpan.FromSeconds(_maxTime);
        }

        public void InitiateTimer()
        {
            StartCoroutine(Timer());
        }

        public void TimeConsume(int timeRested)
        {
            StopAllCoroutines();
            CurrentTime -= TimeSpan.FromSeconds(timeRested);
            if (CurrentTime < TimeSpan.Zero) CurrentTime = TimeSpan.Zero;

            OnTimeChanged?.Invoke(this, CurrentTime);

            if (CurrentTime.TotalSeconds > 0)
                StartCoroutine(Timer());
            else
                OnTimerFinished?.Invoke();
        }

        public void FrezeTime()
        {
            StopAllCoroutines();
        }

        public void InitiateCountdown()
        {
            StartCoroutine(InitialCountdown());
        }

        IEnumerator InitialCountdown()
        {
            /*for (int i = -1; countdown < i; countdown--)
            {
                OnInitialCountDown?.Invoke(this, countdown);
            }*/

            countdown = 3;
            while (countdown > -1)
            {
                OnInitialCountDown?.Invoke(this, countdown);
                Debug.Log(countdown);
                countdown--;
            }
            yield return null;
        }

        IEnumerator Timer()
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
