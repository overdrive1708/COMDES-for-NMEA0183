using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;

namespace COMDES_for_NMEA0183.ViewModels
{
    /// <summary>
    /// メイン画面クラス
    /// </summary>
    public class MainWindowViewModel : BindableBase, IDisposable
    {
        //--------------------------------------------------
        // バインディングデータ
        //--------------------------------------------------
        /// <summary>
        /// タイトル
        /// </summary>
        public ReactivePropertySlim<string> Title { get; }

        //--------------------------------------------------
        // バインディングコマンド
        //--------------------------------------------------
        /// <summary>
        /// 画面遷移コマンド
        /// </summary>
        public ReactiveCommand<string> CommandTransitionScreen { get; }

        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// Dispose可能なアイテム
        /// </summary>
        private readonly CompositeDisposable _disposablesItems = new();

        /// <summary>
        /// Dispose実行管理フラグ
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// 画面遷移管理情報
        /// </summary>
        private readonly IRegionManager _regionManager;

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ(XAMLデザイナー用)
        /// </summary>
        public MainWindowViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="regionManager">画面遷移管理情報</param>
        public MainWindowViewModel(IRegionManager regionManager)
        {
            // バインディングデータの初期化とDispose可能なアイテムへの登録
            Title = new ReactivePropertySlim<string>(Properties.Resources.ApplicationTitle).AddTo(_disposablesItems);

            // バインディングコマンドの初期化とDispose可能なアイテムへの登録
            CommandTransitionScreen = new ReactiveCommand<string>().WithSubscribe(screenName => ExecuteCommandTransitionScreen(screenName)).AddTo(_disposablesItems);

            // 画面遷移管理情報を設定する
            _regionManager = regionManager;

            // 初期画面を設定する
            _ = _regionManager.RegisterViewWithRegion("MainContentRegion", typeof(Views.UserControlTransmit));
            Title.Value = $"{Properties.Resources.ApplicationTitle} | {Properties.Resources.ScreenTitleTransmit}";
        }

        /// <summary>
        /// Dispose処理(処理はこちらに追加)
        /// </summary>
        /// <param name="disposing">Dispose要否</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose可能なアイテムのDispose
                    _disposablesItems.Dispose();

                    // 画面を破棄する
                    foreach (IRegion region in _regionManager.Regions)
                    {
                        region.RemoveAll();
                    }
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose処理
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
        }

        /// <summary>
        /// 画面遷移コマンド実行処理
        /// </summary>
        /// <param name="screenName">画面遷移先のユーザーコントロール名</param>
        private void ExecuteCommandTransitionScreen(string screenName)
        {
            // 指定された画面に遷移する
            _regionManager.RequestNavigate("MainContentRegion", screenName);

            // タイトルを更新する
            switch (screenName)
            {
                case "UserControlTransmit":
                    Title.Value = $"{Properties.Resources.ApplicationTitle} | {Properties.Resources.ScreenTitleTransmit}";
                    break;
                default:
                    Title.Value = Properties.Resources.ApplicationTitle;
                    break;
            }
        }
    }
}
