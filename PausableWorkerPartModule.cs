using UnityEngine;

namespace Agile.Ksp
{
    public abstract class PausableWorkerPartModule : WorkerPartModule
    {
        protected virtual string PauseWorkLabel => "Pause";
        protected virtual string ResumeWorkLabel => "Resume";
        protected virtual string PausedStatusLabel => "Paused";

        public override void OnLoad(ConfigNode node)
        {
            print("-------- ON LOOOOOOOOOOAAAAAAAAAAAD ------------");
            base.OnLoad(node);
            print(progress);
        }

        protected override bool ShouldStart()
        {
            return base.ShouldStart() && !paused;
        }

        [KSPField(isPersistant = true)]
        public bool paused;

        protected override void OnUpdateGUI()
        {
            base.OnUpdateGUI();

            var resumeWorkEvent = Events["ResumeWork"];
            resumeWorkEvent.guiActive = running && paused;
            resumeWorkEvent.guiActiveUnfocused = running && paused;
            resumeWorkEvent.active = running && paused;
            resumeWorkEvent.guiName = ResumeWorkLabel;

            var pauseWorkEvent = Events["PauseWork"];
            pauseWorkEvent.guiActive = running && !paused;
            pauseWorkEvent.guiActiveUnfocused = running && !paused;
            pauseWorkEvent.active = running && !paused;
            pauseWorkEvent.guiName = PauseWorkLabel;

            status = paused && running ? PausedStatusLabel : status;
        }

        [KSPEvent(guiName = "Pause")]
        public void PauseWork()
        {
            if (paused)
                return;

            Debug.Log("[" + GetType().Name + "] Pausing job.");
            paused = true;
            UpdateGUI();
            OnPauseWork();
            StopWorkCoroutine();
        }

        protected override void OnStopWork()
        {
            paused = false;
        }

        [KSPEvent(guiName = "Resume")]
        public void ResumeWork()
        {
            if (!paused)
                return;

            Debug.Log("[" + GetType().Name + "] Resuming job.");
            paused = false;
            UpdateGUI();
            OnResumeWork();
            StartWorkCoroutine();
        }
    }
}