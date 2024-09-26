namespace DataAnalysis.NET.Core
{
    /// <summary>
    /// Data frame interface
    /// </summary>
    public interface IDataFrame
    {
        /// <summary>
        /// Adds a row to the DataFrame.
        /// </summary>
        /// <param name="row">An array of doubles representing the row data.</param>
        void AddRow(double[] row);

        /// <summary>
        /// Gets a column from the DataFrame by index.
        /// </summary>
        /// <param name="index">The index of the column to retrieve.</param>
        /// <returns>An array of doubles representing the column data.</returns>
        double[] GetColumn(int index);

        /// <summary>
        /// Gets the list of column names.
        /// </summary>
        /// <returns>A list of strings representing the column names.</returns>
        List<string> GetColumns();

        /// <summary>
        /// Gets the list of rows (data) in the DataFrame.
        /// </summary>
        /// <returns>A list of double arrays representing the rows of data.</returns>
        List<double[]> GetData();

        /// <summary>
        /// Prints the DataFrame to the console.
        /// </summary>
        void Print();
    }
}
