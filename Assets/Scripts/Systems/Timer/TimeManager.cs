using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Timer
{
    public class TimeManager : Singleton<TimeManager>
    {
        [Header("Initial Countdown")]
        private int countdown;
        public EventHandler<int> OnInitialCountDown;
        public EventHandler OnCountdownCompleted;

        [Header ("Stage Timer")]
        private GameManager _gameManager;
        private TimeSpan CurrentTime;
        private float _timerSpan = 1;
        private int _maxTime;
        private StageHandler _stageHandler;
        public int MaxTime => _maxTime;
        public EventHandler<TimeSpan> OnTimeChanged;
        public UnityEvent OnTimerFinished;

        public void SetUp(GameManager gameManager, int time, StageHandler handler)
        {
            TimeDisplay.Instance.TuneEvents();
            _gameManager = gameManager;
            _maxTime = time;
            _stageHandler = handler;

            CurrentTime = TimeSpan.FromSeconds(_maxTime);

            StartCoroutine(InitialCountdown());
        }

        IEnumerator InitialCountdown()
        {
            countdown = 3;
            while (countdown > 0)
            {
                OnInitialCountDown?.Invoke(this, countdown);
                //Debug.Log(countdown);

                yield return new WaitForSeconds(1f);

                countdown--;
            }
            //Debug.Log("Countdown finished");
            OnCountdownCompleted?.Invoke(this, null);
            UIManager.Instance.ShowSlider();
            _stageHandler.FetchNextIngredient();
            StartCoroutine(Timer());
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
    }
}
