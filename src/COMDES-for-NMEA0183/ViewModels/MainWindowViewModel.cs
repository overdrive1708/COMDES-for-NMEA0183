using Prism.Mvvm;
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

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            // バインディングデータの初期化とDispose可能なアイテムへの登録
            Title = new ReactivePropertySlim<string>(Properties.Resources.ApplicationTitle).AddTo(_disposablesItems);
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
                    _disposablesItems.Dispose();
                    // TODO:regionManager.Regionsの要素をすべてRemoveAllする｡regionはIDestructibleを継承してDestroy処理を実装すること｡IDisposableも継承すること｡
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
    }
}
