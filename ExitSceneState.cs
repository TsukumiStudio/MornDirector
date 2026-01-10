using Arbor;
using Cysharp.Threading.Tasks;
using MornEditor;
using UnityEngine;
using VContainer;

namespace MornLib
{
    public class ExitSceneState : StateBehaviour
    {
        [Inject] private MornTransitionCtrl _transitionCtrl;
        [Inject] private MornSoundVolumeCore _volumeCore;
        [SerializeField, Label("独立動作")] private bool _isExecuteAsIsolated;
        [SerializeField, Label("トランジション")] private MornTransitionType _transitionType;
        [SerializeField] private StateLink _nextState;

        public override async void OnStateBegin()
        {
            var ct = _isExecuteAsIsolated ? MornApp.QuitToken : CancellationTokenOnEnd;
            var taskA = _transitionCtrl.FillAsync(_transitionType, ct);
            var taskB = _volumeCore.FadeAsync(MornDirectorGlobal.I.CreateFadeOutInfo(ct));
            await UniTask.WhenAll(taskA, taskB);
            Transition(_nextState);
        }
    }
}