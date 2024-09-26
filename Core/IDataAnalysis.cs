namespace DataAnalysis.NET.Core
{
    /// <summary>
    /// Provides methods for analyzing textual data from various sources such as CSV, Excel, and SQL tables.
    /// </summary>
    public interface IDataAnalysis
    {
        /// <summary>
        /// Loads data from a specified file path into a data structure for analysis.
        /// Supports file types: CSV and Excel (both .xlsx and .xls).
        /// </summary>
        /// <param name="filePath">The path to the file from which to load data.</param>
        /// <exception cref="NotSupportedException">Thrown when the file type is not supported.</exception>
        void LoadData(string filePath);

        /// <summary>
        /// Loads data from a provided stream into a data structure for analysis.
        /// This method is not yet implemented.
        /// </summary>
        /// <param name="stream">The stream from which to load data.</param>
        /// <exception cref="NotImplementedException">Thrown when this method is called since it is not implemented.</exception>
        void LoadData(Stream stream);

        /// <summary>
        /// Analyzes the loaded data and performs basic statistical operations.
        /// This method currently counts occurrences of unique values in the first column.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when no data has been loaded before analysis.</exception>
        void AnalyzeData();

        /// <summary>
        /// Exports the analyzed results to a specified file path in the desired format.
        /// Supported formats include CSV and Excel.
        /// </summary>
        /// <param name="filePath">The path where the results will be saved.</param>
        /// <param name="format">The format in which to save the results (e.g., "csv" or "xlsx").</param>
        /// <exception cref="InvalidOperationException">Thrown when no data is available to export.</exception>
        /// <exception cref="NotSupportedException">Thrown when an unsupported export format is specified.</exception>
        void ExportResults(string filePath, string format);
    }
}
