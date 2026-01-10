# MornDirector

シーン遷移演出（トランジション + 音量フェード）を一括管理するArbor用ステートライブラリ

## 依存関係

- UniTask
- Arbor
- VContainer
- MornGlobal
- MornSound
- MornTransition

## セットアップ

1. `Project`を右クリック → `MornDirectorGlobal`を作成
2. 音量フェード設定（タイプ、フェードイン/アウト時間）を設定

## Arborステート

| ステート | 説明 |
|---|---|
| `EnterSceneState` | シーン入場：トランジションクリア + 音量フェードイン |
| `ExitSceneState` | シーン退場：トランジションフィル + 音量フェードアウト |

## 設定項目

- **独立動作**: ステート終了時にキャンセルせず、アプリ終了まで継続
- **トランジション**: ExitSceneStateで使用するトランジションタイプ
