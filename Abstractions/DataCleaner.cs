using DataAnalysis.NET.Core;

namespace DataAnalysis.NET.Abstractions
{
    /// <summary>
    /// Class for cleaning data within a DataFrame.
    /// </summary>
    internal class DataCleaner : IDataCleaner
    {
        /// <summary>
        /// Replaces all NaN values in the DataFrame with a specified replacement value.
        /// </summary>
        /// <param name="df">The DataFrame to clean.</param>
        /// <param name="replacementValue">The value to replace NaNs with.</param>
        public void ReplaceNaNs(IDataFrame df, double replacementValue)
        {
            var data = df.GetData();
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (double.IsNaN(data[i][j]))
                        data[i][j] = replacementValue;
                }
            }
        }

        /// <summary>
        /// Drops all rows from the DataFrame that contain any NaN values.
        /// </summary>
        /// <param name="df">The DataFrame to clean.</param>
        public void DropNaNs(IDataFrame df)
        {
            var data = df.GetData();
            // Remove rows with any NaN values
            data.RemoveAll(row => Array.Exists(row, double.IsNaN));
        }

        /// <summary>
        /// Replaces all NaN values in each column of the DataFrame with the mean of that column.
        /// </summary>
        /// <param name="df">The DataFrame to clean.</param>
        public void ReplaceNaNsWithMean(IDataFrame df)
        {
            var data = df.GetData();
            var columns = df.GetColumns();

            for (int col = 0; col < columns.Count; col++)
            {
                double sum = 0;
                int count = 0;

                // Calculate the mean for the column
                for (int row = 0; row < data.Count; row++)
                {
                    if (!double.IsNaN(data[row][col]))
                    {
                        sum += data[row][col];
                        count++;
                    }
                }

                // Avoid division by zero
                double mean = count > 0 ? sum / count : 0;

                // Replace NaN values with the column mean
                for (int row = 0; row < data.Count; row++)
                {
                    if (double.IsNaN(data[row][col]))
                    {
                        data[row][col] = mean;
                    }
                }
            }
        }

        /// <summary>
        /// Filters rows in the DataFrame based on a specified predicate function.
        /// </summary>
        /// <param name="df">The DataFrame to filter.</param>
        /// <param name="predicate">A function that defines the filtering criteria.</param>
        /// <returns>A new DataFrame containing only the rows that satisfy the predicate.</returns>
        public IDataFrame FilterRows(IDataFrame df, Func<double[], bool> predicate)
        {
            var filteredData = new DataFrame(df.GetColumns());

            foreach (var row in df.GetData())
            {
                if (predicate(row))
                {
                    filteredData.AddRow(row);
                }
            }

            return filteredData;
        }
    }
}
