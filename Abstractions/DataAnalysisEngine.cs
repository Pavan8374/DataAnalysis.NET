using CsvHelper;
using DataAnalysis.NET.Core;
using OfficeOpenXml;
using System.Globalization;

namespace DataAnalysis.NET.Abstractions
{
    internal sealed class DataAnalysisEngine : IDataAnalysis
    {
        private List<Dictionary<string, string>> data; // Simple DataFrame-like structure

        public DataAnalysisEngine()
        {
            data = new List<Dictionary<string, string>>();
        }

        public void LoadData(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            if (extension == ".csv")
            {
                LoadDataFromCsv(filePath);
            }
            else if (extension == ".xlsx" || extension == ".xls")
            {
                LoadDataFromExcel(filePath);
            }
            else
            {
                throw new NotSupportedException($"File type '{extension}' is not supported.");
            }
        }

        public void LoadData(Stream stream)
        {
            // This method can be implemented similarly to handle streams,
            // but for simplicity, we'll focus on file paths.
            throw new NotImplementedException("Loading from stream is not implemented.");
        }

        public void AnalyzeData()
        {
            // Example analysis: Count occurrences of each unique value in the first column
            if (data.Count == 0)
                throw new InvalidOperationException("No data loaded for analysis.");

            var firstColumn = data.Select(row => row.Values.First()).ToList();
            var analysisResult = firstColumn.GroupBy(x => x)
                                            .Select(g => new { Value = g.Key, Count = g.Count() })
                                            .ToList();

            Console.WriteLine("Analysis Results:");
            foreach (var result in analysisResult)
            {
                Console.WriteLine($"Value: {result.Value}, Count: {result.Count}");
            }
        }

        public void ExportResults(string filePath, string format)
        {
            if (data.Count == 0)
                throw new InvalidOperationException("No data available to export.");

            if (format.ToLower() == "csv")
            {
                ExportToCsv(filePath);
            }
            else if (format.ToLower() == "xlsx")
            {
                ExportToExcel(filePath);
            }
            else
            {
                throw new NotSupportedException($"Export format '{format}' is not supported.");
            }
        }

        private void LoadDataFromCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Read the header
                csv.Read();
                csv.ReadHeader();

                // Get headers
                var headers = csv.HeaderRecord;

                while (csv.Read())
                {
                    var record = new Dictionary<string, string>();
                    foreach (var header in headers)
                    {
                        record[header] = csv.GetField(header);
                    }
                    data.Add(record);
                }
            }
        }

        private void LoadDataFromExcel(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Get the first worksheet
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++) // Assuming first row is header
                {
                    var record = new Dictionary<string, string>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        string header = worksheet.Cells[1, col].Text; // Get header from first row
                        record[header] = worksheet.Cells[row, col].Text; // Get cell value
                    }
                    data.Add(record);
                }
            }
        }

        private void ExportToCsv(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Write header
                foreach (var key in data[0].Keys)
                {
                    csv.WriteField(key);
                }
                csv.NextRecord();

                // Write records
                foreach (var record in data)
                {
                    foreach (var value in record.Values)
                    {
                        csv.WriteField(value);
                    }
                    csv.NextRecord();
                }
            }
        }

        private void ExportToExcel(string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Results");

                // Write header
                int colIndex = 1;
                foreach (var key in data[0].Keys)
                {
                    worksheet.Cells[1, colIndex++].Value = key;
                }

                // Write records
                int rowIndex = 2;
                foreach (var record in data)
                {
                    colIndex = 1;
                    foreach (var value in record.Values)
                    {
                        worksheet.Cells[rowIndex, colIndex++].Value = value;
                    }
                    rowIndex++;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }
    }
}
