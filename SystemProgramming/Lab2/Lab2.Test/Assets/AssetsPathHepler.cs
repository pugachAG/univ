using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Test.Assets
{
    public static class AssetsPathHepler
    {
        const string AssetsFolerPrefix = "Assets/";
        const string AssetsTestsSuffix = "Tests";
        const string AssetsDefinitionsSuffix = "Definition";
        const string AssetsAutomatonTestsPrefix = "Automaton";
        const int AssetsAutomatonTestsCount = 3;

        public static string[] GetAssetsAutomatonTestsPaths()
        {
            return GenerateFilePaths(AssetsAutomatonTestsPrefix, AssetsTestsSuffix, AssetsAutomatonTestsCount);
        }

        public static string[] GetAssetsAutomatonDefinitionsPaths()
        {
            return GenerateFilePaths(AssetsAutomatonTestsPrefix, AssetsDefinitionsSuffix, AssetsAutomatonTestsCount);
        }

        private static string[] GenerateFilePaths(string prefix, string suffix, int count, string extension = ".txt")
        {
            List<string> result = new List<string>();
            for (int i = 0; i <= count; i++)
                result.Add(string.Format("{0}{1}{2}{3}{4}", AssetsFolerPrefix, prefix, i, suffix, extension));
            return result.ToArray();
        }

    }
}
