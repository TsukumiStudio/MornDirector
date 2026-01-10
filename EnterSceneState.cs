using Arbor;
using Cysharp.Threading.Tasks;
using MornEditor;
using UnityEngine;
using VContainer;

namespace MornLib
{
    public class EnterSceneState : StateBehaviour
    {
        [Inject] private MornTransitionCtrl _transitionCtrl;
        [Inject] private MornSoundVolumeCore _volumeCore;
        [SerializeField, Label("独立動作")] private bool _isExecuteAsIsolated;
        [SerializeField] private StateLink _nextState;

        public override async void OnStateBegin()
        {
            var ct = _isExecuteAsIsolated ? MornApp.QuitToken : CancellationTokenOnEnd;
            var taskA = _transitionCtrl.ClearAsync(ct);
            var taskB = _volumeCore.FadeAsync(MornDirectorGlobal.I.CreateFadeInInfo(ct));
            await UniTask.WhenAll(taskA, taskB);
            Transition(_nextState);
        }
    }
}