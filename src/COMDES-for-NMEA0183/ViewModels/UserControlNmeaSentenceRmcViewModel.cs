using Prism.Mvvm;
using Prism.Navigation;
using System.Reactive.Disposables;

namespace COMDES_for_NMEA0183.ViewModels
{
    /// <summary>
    /// NMEAセンテンス画面クラス(RMC)
    /// </summary>
    public class UserControlNmeaSentenceRmcViewModel : BindableBase, IDestructible
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
        public UserControlNmeaSentenceRmcViewModel()
        {
            // 無処理
            // TODO:RMCデータのレイアウト作成
            // [Talker ID]:RMC:GP or GN
            // NMEA-0183 Ver.2.30未満
            // $--RMC,<1>,<2>,<3>,<4>,<5>,<6>,<7>,<8>,<9>,<10>,<11>*<Checksum><CR><LF>
            // NMEA-0183 Ver.2.30以降(測位モードの追加)
            // $--RMC,<1>,<2>,<3>,<4>,<5>,<6>,<7>,<8>,<9>,<10>,<11>,<12>*<Checksum><CR><LF>
            // ex)$GNRMC,135829.00,V,3636.3556,N,13814.6383,E,,,170822,6.8,W,N*21<CR><LF>
            // NMEA-0183 Ver.4.10以降(ナビゲーション状態の追加)
            // $--RMC,<1>,<2>,<3>,<4>,<5>,<6>,<7>,<8>,<9>,<10>,<11>,<12>,<13>*<Checksum><CR><LF>
            // ex)$GNRMC,075925.000,A,3149.2894,N,11706.9251,E,0.01,351.19,200120,,,A*75<CR><LF>
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
