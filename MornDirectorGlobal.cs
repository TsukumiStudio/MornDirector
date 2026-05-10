using System.Threading;
using UnityEngine;

namespace MornLib
{
    [CreateAssetMenu(fileName = nameof(MornDirectorGlobal), menuName = "Morn/" + nameof(MornDirectorGlobal))]
    public sealed class MornDirectorGlobal : MornGlobalBase<MornDirectorGlobal>
    {
        protected override string ModuleName => "MornDirector";
        [Header("音量")]
        [Label("フェードタイプ"), SerializeField] private MornSoundMixerType _volumeFadeType;
        [Label("フェードイン(s)"), SerializeField] private float _volumeFadeInDuration = 0.5f;
        [Label("フェードインEase"), SerializeField] private MornEaseType _volumeFadeInEase = MornEaseType.EaseInQuad;
        [Label("フェードアウト(s)"), SerializeField] private float _volumeFadeOutDuration = 0.5f;
        [Label("フェードアウトEase"), SerializeField] private MornEaseType _volumeFadeOutEase = MornEaseType.EaseInQuad;

        public MornSoundFadeInfo CreateFadeInInfo(CancellationToken ct)
        {
            return new MornSoundFadeInfo
            {
                SoundVolumeType = _volumeFadeType,
                IsFadeIn = true,
                Duration = _volumeFadeInDuration,
                EaseType = _volumeFadeInEase,
                CancellationToken = ct,
            };
        }

        public MornSoundFadeInfo CreateFadeOutInfo(CancellationToken ct)
        {
            return new MornSoundFadeInfo
            {
                SoundVolumeType = _volumeFadeType,
                IsFadeIn = false,
                Duration = _volumeFadeOutDuration,
                EaseType = _volumeFadeOutEase,
                CancellationToken = ct,
            };
        }
    }
}