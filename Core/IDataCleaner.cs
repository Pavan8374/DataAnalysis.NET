namespace DataAnalysis.NET.Core
{
    /// <summary>
    /// Data Cleaner interface
    /// </summary>
    public interface IDataCleaner
    {
        /// <summary>
        /// Replaces all NaN values in the DataFrame with a specified replacement value.
        /// </summary>
        /// <param name="df">The DataFrame to clean.</param>
        /// <param name="replacementValue">The value to replace NaNs with.</param>
        void ReplaceNaNs(IDataFrame df, double replacementValue);

        /// <summary>
        /// Drops all rows from the DataFrame that contain any NaN values.
        /// </summary>
        /// <param name="df">The DataFrame to clean.</param>
        void DropNaNs(IDataFrame df);

        /// <summary>
        /// Replaces all NaN values in each column of the DataFrame with the mean of that column.
        /// </summary>
        /// <param name="df">The DataFrame to clean.</param>
        void ReplaceNaNsWithMean(IDataFrame df);

        /// <summary>
        /// Filters rows in the DataFrame based on a specified predicate function.
        /// </summary>
        /// <param name="df">The DataFrame to filter.</param>
        /// <param name="predicate">A function that defines the filtering criteria.</param>
        /// <returns>A new DataFrame containing only the rows that satisfy the predicate.</returns>
        IDataFrame FilterRows(IDataFrame df, Func<double[], bool> predicate);
    }
}
