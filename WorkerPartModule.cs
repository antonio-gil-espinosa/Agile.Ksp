using System.Collections;
using UnityEngine;

namespace Agile.Ksp
{
    public abstract class WorkerPartModule : PartModule
    {
        [KSPField(guiActive = true, guiActiveEditor = false, isPersistant = false, guiName = "Status")]
        public string status;

        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            if (state != StartState.Editor && state != StartState.None)
            {
                if (ShouldStart())
                {
                    running = false;
                    StartWork();
                }
            }
            UpdateGUI();
        }

        protected virtual bool ShouldStart()
        {
            return running;
        }

        [KSPField]
        public bool canStart = true;

        [KSPField]
        public bool canCancel = true;

        [KSPField(isPersistant = true, guiUnits = "%", guiFormat = "F", guiName = "Progress")]
        public double progress = -1;

        [KSPField(isPersistant = true)]
        public bool running;

        private Coroutine _coroutine;
        private double _lastFixedUpdate;
        public string overrideStatus;

        [KSPEvent(guiName = "Cancel")]
        public void CancelWork()
        {
            if (!running)
                return;

            Debug.Log("[" + GetType().Name + "] Canceling job.");
            UpdateGUI();
            StopWork();
            OnCancelWork();
        }

        protected virtual string CancelWorkLabel => "Cancel";
        protected virtual string StartWorkLabel => "Start";

        protected virtual string IdleStatusLabel => "Idle";
        protected virtual string WorkingStatusLabel => "Working";

        protected void UpdateGUI()
        {
            OnUpdateGUI();
            status = string.IsNullOrEmpty(overrideStatus) ? status : overrideStatus;
        }

        protected virtual void OnUpdateGUI()
        {
            status = running ? WorkingStatusLabel : IdleStatusLabel;

            var cancelWorkEvent = Events["CancelWork"];
            cancelWorkEvent.guiActive = running && canCancel;
            cancelWorkEvent.guiActiveUnfocused = running && canCancel;
            cancelWorkEvent.active = running && canCancel;
            cancelWorkEvent.guiName = CancelWorkLabel;

            var startWorkEvent = Events["StartWork"];
            startWorkEvent.guiActive = !running && canStart;
            startWorkEvent.guiActiveUnfocused = !running && canStart;
            startWorkEvent.active = !running && canStart;
            startWorkEvent.guiName = StartWorkLabel;

            var progressField = Fields["progress"];
            progressField.guiActive = progress != -1;
        }

        [KSPEvent(guiName = "Start")]
        public void StartWork()
        {
            Debug.Log("[" + GetType().Name + "] Starting job.");
            if (running)
                return;

            running = true;
            UpdateGUI();
            OnStartWork();
            StartWorkCoroutine();
        }

        protected virtual double GetProgressPercentage()
        {
            return -1;
        }

        protected virtual void OnCancelWork()
        {
        }

        protected virtual void OnCompleteWork()
        {
        }

        protected virtual void OnPauseWork()
        {
        }

        protected virtual void OnResumeWork()
        {
        }

        protected virtual void OnStartWork()
        {
        }

        protected abstract void OnWork(double deltaTime);

        private void StopWorkCoroutine(Part data)
        {
            if (data != part)
                return;

            StopWorkCoroutine();
        }

        private IEnumerator Run()
        {
            while (GetProgressPercentage() < 100)
            {
                var time = Planetarium.GetUniversalTime();

                if (_lastFixedUpdate == 0)
                {
                    // Just started running
                    _lastFixedUpdate = time;
                    yield return new WaitForFixedUpdate();
                }
                overrideStatus = null;
                double deltaTime = time - _lastFixedUpdate;
                OnWork(deltaTime);

                progress = GetProgressPercentage();

                _lastFixedUpdate = time;

                UpdateGUI();
                yield return new WaitForFixedUpdate();
            }

            OnCompleteWork();
            StopWork();
        }

        protected void StartWorkCoroutine()
        {
            _lastFixedUpdate = 0;
            GameEvents.onPartDie.Add(StopWorkCoroutine);
            GameEvents.onPartDestroyed.Add(StopWorkCoroutine);

            if (_coroutine == null)
            {
                Debug.Log("[" + GetType().Name + "] Starting coroutine.");
                _coroutine = StartCoroutine(Run());
            }
            else
            {
                Debug.LogError("[" + GetType().Name + "] Coroutine but already started.");
            }
        }

        protected void StopWorkCoroutine()
        {
            GameEvents.onPartDie.Remove(StopWorkCoroutine);
            GameEvents.onPartDestroyed.Remove(StopWorkCoroutine);
            if (_coroutine != null)
            {
                Debug.Log("[" + GetType().Name + "] Stoping coroutine.");
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            else
            {
                Debug.LogError("[" + GetType().Name + "] Coroutine but already stoped.");
            }
        }

        private void StopWork()
        {
            Debug.Log("[" + GetType().Name + "] Stoping job.");
            if (!running)
                return;

            progress = -1;
            running = false;
            UpdateGUI();
            StopWorkCoroutine();
            OnStopWork();
        }

        protected virtual void OnStopWork()
        {
        }
    }
}