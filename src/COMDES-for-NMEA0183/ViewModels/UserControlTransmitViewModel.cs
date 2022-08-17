using Prism.Mvvm;
using Prism.Navigation;
using System.Reactive.Disposables;

namespace COMDES_for_NMEA0183.ViewModels
{
    /// <summary>
    /// 送信画面クラス
    /// </summary>
    public class UserControlTransmitViewModel : BindableBase, IDestructible
    {
        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// Dispose可能なアイテム
        /// </summary>
        private readonly CompositeDisposable _disposablesItems = new();

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserControlTransmitViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// 破棄処理
        /// </summary>
        public void Destroy()
        {
            // Dispose可能なアイテムのDispose
            _disposablesItems.Dispose();
        }
    }
}
