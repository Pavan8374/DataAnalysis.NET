namespace DataAnalysis.NET
{
    public class DataCleaner
    {
        public static void ReplaceNaNs(DataFrame df, double replacementValue)
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

        public static void DropNaNs(DataFrame df)
        {
            var data = df.GetData();
            data.RemoveAll(row => Array.Exists(row, double.IsNaN));
        }


        public static void ReplaceNaNsWithMean(DataFrame df)
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

                double mean = sum / count;

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


        public static DataFrame FilterRows(DataFrame df, Func<double[], bool> predicate)
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
