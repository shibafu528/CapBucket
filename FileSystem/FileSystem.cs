using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transposer;

namespace FileSystem
{
    /// <summary>
    /// ファイルシステムへの出力をサポートするプラグインです。
    /// </summary>
    public class FileSystem : ITransposer
    {
        public void TransferBitmaps(IEnumerable<Bitmap> bitmaps)
        {
            // 出力先を問い合わせるためのダイアログ作る
            var folderPicker = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                Description = "出力先を選択してください"
            };

            // 問い合わせてみてキャンセルされたら諦める
            if (folderPicker.ShowDialog() != DialogResult.OK) return;

            // 保存ファイル名作る
            var fileNamePrefix = DateTime.Now.ToString("yyyyMMddHHmmss");

            // ひたすら保存
            foreach (var obj in bitmaps.Select((bitmap, i) => new {bitmap, i}))
            {
                obj.bitmap.Save($"{fileNamePrefix}{obj.i:0000}", ImageFormat.Png);
            }
        }
    }
}
