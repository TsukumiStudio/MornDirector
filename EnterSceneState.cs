using System;
using Arbor;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace MornLib
{
    public class EnterSceneState : StateBehaviour
    {
        [Inject] private MornSoundVolumeCore _volumeCore;
        [SerializeField, Label("独立動作")] private bool _isExecuteAsIsolated;
        [SerializeField] private StateLink _onComplete;

        public override async void OnStateBegin()
        {
            try
            {
                var ct = _isExecuteAsIsolated ? MornApp.QuitToken : CancellationTokenOnEnd;
                var taskA = MornTransitionCore.ClearAsync(ct);
                var taskB = _volumeCore.FadeAsync(MornDirectorGlobal.I.CreateFadeInInfo(ct));
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