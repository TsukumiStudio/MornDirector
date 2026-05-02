using System;
using Arbor;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace MornLib
{
    [Serializable]
    public class ExitSceneState : StateBehaviour
    {
        [Inject] private MornSoundVolumeCore _volumeCore;
        [SerializeField, Label("独立動作")] private bool _isExecuteAsIsolated;
        [SerializeField, Label("トランジション")] private MornTransitionType _transitionType;
        [SerializeField] private StateLink _onComplete;

        public override async void OnStateBegin()
        {
            try
            {
                var ct = _isExecuteAsIsolated ? MornApp.QuitToken : CancellationTokenOnEnd;
                var taskA = MornTransitionCore.FillAsync(_transitionType, ct);
                var taskB = _volumeCore.FadeAsync(MornDirectorGlobal.I.CreateFadeOutInfo(ct));
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