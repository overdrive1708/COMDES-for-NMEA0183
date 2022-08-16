using Prism.Mvvm;

namespace COMDES_for_NMEA0183.ViewModels
{
    /// <summary>
    /// メイン画面クラス
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        //--------------------------------------------------
        // バインディングデータ(スニペット:propp)
        //--------------------------------------------------
        /// <summary>
        /// タイトル
        /// </summary>
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        //--------------------------------------------------
        // メソッド
        //--------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {

        }
    }
}
