using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transposer
{
    /// <summary>
    /// ビットマップを受け取り処理するプラグイン機能を実装します。
    /// </summary>
    public interface ITransposer
    {
        /// <summary>
        /// ビットマップを転送する処理を実行します。この操作は同期的に行われます。
        /// </summary>
        /// <param name="bitmaps">ビットマップ</param>
        void TransferBitmaps(IEnumerable<Bitmap> bitmaps);
    }
}
