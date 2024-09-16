namespace DataAnalysis.NET
{

    public class DataFrameOperations
    {
        /// <summary>
        /// Sum columns using SIMD
        /// </summary>
        /// <param name="df">Data frame</param>
        /// <returns>Sum of the columns</returns>
        public static double[] SumColumns(DataFrame df)
        {
            var data = df.GetData();
            var numColumns = df.GetColumns().Count;
            var sums = new double[numColumns];

            for (int row = 0; row < data.Count; row++)
            {
                for (int col = 0; col < numColumns; col++)
                {
                    sums[col] += data[row][col];
                }
            }

            return sums;
        }

        public static double[] MeanColumns(DataFrame df)
        {
            var sums = SumColumns(df);
            var rowCount = df.GetData().Count;

            for (int i = 0; i < sums.Length; i++)
            {
                sums[i] /= rowCount;
            }

            return sums;
        }

        public static double[] VarianceColumns(DataFrame df)
        {
            var mean = MeanColumns(df);
            var data = df.GetData();
            var variance = new double[mean.Length];

            for (int row = 0; row < data.Count; row++)
            {
                for (int col = 0; col < mean.Length; col++)
                {
                    variance[col] += Math.Pow(data[row][col] - mean[col], 2);
                }
            }

            for (int col = 0; col < variance.Length; col++)
            {
                variance[col] /= data.Count;
            }

            return variance;
        }


        public static DataFrame CreatePivotTable(DataFrame df, int groupByCol, int valueCol, Func<double[], double> aggregationFunc)
        {
            var groupedData = new Dictionary<double, List<double>>();
            var data = df.GetData();

            foreach (var row in data)
            {
                var key = row[groupByCol];
                var value = row[valueCol];

                if (!groupedData.ContainsKey(key))
                {
                    groupedData[key] = new List<double>();
                }

                groupedData[key].Add(value);
            }

            var pivotColumns = new List<string> { df.GetColumns()[groupByCol], "AggregatedValue" };
            var pivotTable = new DataFrame(pivotColumns);

            foreach (var group in groupedData)
            {
                var aggregatedValue = aggregationFunc(group.Value.ToArray());
                pivotTable.AddRow(new double[] { group.Key, aggregatedValue });
            }

            return pivotTable;
        }


    }

}
