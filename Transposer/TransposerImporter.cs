using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Transposer
{
    public static class TransposerImporter
    {
        /// <summary>
        /// <see cref="ITransposer"/>を継承しているプラグインクラスを、指定のディレクトリ配下から再帰的に探索したDLLからロードします。
        /// </summary>
        /// <param name="dllDirectory">DLL探索の起点ディレクトリ</param>
        /// <returns>ロードに成功したプラグイン</returns>
        public static List<ITransposer> ImportTransposers(string dllDirectory)
        {
            var transposers = new List<ITransposer>();

            // DLLっぽいのを検索
            var dllPaths = Directory.GetFiles(dllDirectory, "*.dll", SearchOption.AllDirectories);

            foreach (var path in dllPaths)
            {
                try
                {
                    // アセンブリとしてロードしてみる
                    var assembly = Assembly.LoadFrom(path);
                    // ITransposerだけ抽出
                    transposers.AddRange(
                        assembly.GetTypes()
                            .Where(type => !type.IsInterface && !type.IsGenericType && !type.IsAbstract)
                            .Select(type => Activator.CreateInstance(type) as ITransposer)
                            .Where(transposer => transposer != null));
                }
                // 知らない知らない僕は何も知らない
                catch (IOException) {}
                catch (BadImageFormatException) {}
            }

            return transposers;
        }
        
    }
}
