using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace COMDES_for_NMEA0183.ViewModels
{
    /// <summary>
    /// 送信画面クラス
    /// </summary>
    public class UserControlTransmitViewModel : BindableBase, IDestructible
    {
        //--------------------------------------------------
        // バインディングデータ
        //--------------------------------------------------
        /// <summary>
        /// NMEAセンテンスリスト
        /// </summary>
        private ObservableCollection<string> _nmeaSentenceList = new() { "RMC" };
        public ObservableCollection<string> NmeaSentenceList
        {
            get { return _nmeaSentenceList; }
            set { SetProperty(ref _nmeaSentenceList, value); }
        }

        /// <summary>
        /// 選択中NMEAセンテンス
        /// </summary>
        public ReactivePropertySlim<string> SelectedNmeaSentence { get; }

        //--------------------------------------------------
        // 内部変数
        //--------------------------------------------------
        /// <summary>
        /// Dispose可能なアイテム
        /// </summary>
        private readonly CompositeDisposable _disposablesItems = new();

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
        public UserControlTransmitViewModel()
        {
            // 無処理
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="regionManager">画面遷移管理情報</param>
        public UserControlTransmitViewModel(IRegionManager regionManager)
        {
            // バインディングデータの初期化とDispose可能なアイテムへの登録
            SelectedNmeaSentence = new ReactivePropertySlim<string>("RMC").AddTo(_disposablesItems);

            // 画面遷移管理情報を設定する
            _regionManager = regionManager;

            // 初期画面を設定する
            _ = _regionManager.RegisterViewWithRegion("SentenceContentRegion", typeof(Views.UserControlNmeaSentenceRmc));

            // バインディングデータの変更イベントを購読(購読時の通知は不要であるためSkip(1)をする)
            _ = SelectedNmeaSentence.Skip(1).Subscribe(changeNmeaSentenceName => ChangeNmeaSentence(changeNmeaSentenceName));
        }

        /// <summary>
        /// 破棄処理
        /// </summary>
        public void Destroy()
        {
            // Dispose可能なアイテムのDispose
            _disposablesItems.Dispose();

            // 画面の破棄はRegionはMainWindowViewModelですべて実施する
        }

        /// <summary>
        /// 選択中NMEAセンテンス変更処理
        /// </summary>
        /// <param name="changeNmeaSentenceName">変更後のNMEAセンテンス</param>
        private void ChangeNmeaSentence(string changeNmeaSentenceName)
        {
            // 変更後のNMEAセンテンスに対応する画面に遷移する
            string screenName = changeNmeaSentenceName switch
            {
                "RMC" => "UserControlNmeaSentenceRmc",
                _ => throw new NotImplementedException()
            };
            _regionManager.RequestNavigate("SentenceContentRegion", screenName);
        }
    }
}
