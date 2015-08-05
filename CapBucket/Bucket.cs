using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transposer;

namespace CapBucket
{
    public partial class Bucket : Form
    {
        // 転送プラグインDLL読み込み起点パス
        private readonly string _dllPath = Path.Combine(Directory.GetCurrentDirectory(), "Transposers");

        // 転送プラグインクラスのリスト
        private readonly List<ITransposer> _transposers = new List<ITransposer>(); 

        public Bucket()
        {
            InitializeComponent();
        }

        private void Bucket_Load(object sender, EventArgs e)
        {
            // 転送プラグインをロード
            _transposers.AddRange(TransposerImporter.ImportTransposers(_dllPath));
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            var toolStrip = new ContextMenuStrip();
            foreach (var transposer in _transposers)
            {
                toolStrip.Items.Add(transposer.GetType().Name);
            }
            toolStrip.Show(btnSaveAs, new Point(0, btnSaveAs.Height));
        }
    }
}
