using DataAnalysis.NET.Core;

namespace DataAnalysis.NET.Abstractions
{
    /// <summary>
    /// Excel reader
    /// </summary>
    public class ExcelReader
    {
        public static IDataFrame ReadCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var columns = new List<string>(lines[0].Split(','));
            var dataFrame = new DataFrame(columns);

            for (int i = 1; i < lines.Length; i++)
            {
                var values = Array.ConvertAll(lines[i].Split(','), double.Parse);
                dataFrame.AddRow(values);
            }

            return dataFrame;
        }

        public static void WriteCsv(string filePath, IDataFrame df)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(string.Join(",", df.GetColumns()));
                foreach (var row in df.GetData())
                {
                    writer.WriteLine(string.Join(",", row));
                }
            }
        }
    }

}
