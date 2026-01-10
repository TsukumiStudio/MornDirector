using System.Threading;
using MornEditor;
using UnityEngine;

namespace MornLib
{
    [CreateAssetMenu(fileName = nameof(MornDirectorGlobal), menuName = nameof(MornDirectorGlobal))]
    public sealed class MornDirectorGlobal : MornGlobalBase<MornDirectorGlobal>
    {
        public override string ModuleName => "MornDirector";
        [Header("音量")]
        [Label("音量フェードタイプ"), SerializeField] private MornSoundVolumeType _volumeFadeType;
        [Label("音量フェードイン(s)"), SerializeField] private float _volumeFadeInDuration = 0.5f;
        [Label("音量フェードイン(s)"), SerializeField] private float _volumeFadeOutDuration = 0.5f;

        public MornSoundVolumeFadeInfo CreateFadeInInfo(CancellationToken ct)
        {
            return new MornSoundVolumeFadeInfo { SoundVolumeType = _volumeFadeType, IsFadeIn = true, Duration = _volumeFadeInDuration, CancellationToken = ct };
        }

        public MornSoundVolumeFadeInfo CreateFadeOutInfo(CancellationToken ct)
        {
            return new MornSoundVolumeFadeInfo { SoundVolumeType = _volumeFadeType, IsFadeIn = false, Duration = _volumeFadeOutDuration, CancellationToken = ct };
        }
    }
}