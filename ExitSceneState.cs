using System;
#if USE_MORNSTATE || USE_ARBOR
#if USE_MORNSTATE
using MornLib;
using StateBehaviour = MornLib.MornStateBehaviour;
#elif USE_ARBOR
using Arbor;
#endif
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace MornLib
{
    [Serializable]
    public class ExitSceneState : StateBehaviour
    {
        [SerializeField, Label("独立動作")] private bool _isExecuteAsIsolated;
        [SerializeField, NoLabel] private MornTransitionType _transitionType;
        [SerializeField] private StateLink _onComplete;

        public override async void OnStateBegin()
        {
            try
            {
                var ct = _isExecuteAsIsolated ? MornApp.QuitToken : CancellationTokenOnEnd;
                var taskA = MornTransitionCore.FillAsync(_transitionType, ct);
                var taskB = MornSoundService.I.FadeAsync(MornDirectorGlobal.I.CreateFadeOutInfo(ct));
                await UniTask.WhenAll(taskA, taskB);
                Transition(_onComplete);
            }
            catch (OperationCanceledException)
            {
                // 無視
            }
        }
    }
}
#endif // USE_MORNSTATE || USE_ARBOR
