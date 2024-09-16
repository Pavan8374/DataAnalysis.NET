using OfficeOpenXml;

public class DataFrame
{
    private readonly List<string> _columns;
    private readonly List<double[]> _data;

    public DataFrame(List<string> columns)
    {
        _columns = columns;
        _data = new List<double[]>();
    }

    public void AddRow(double[] row)
    {
        if (row.Length != _columns.Count)
            throw new ArgumentException("Row length must match column count.");
        _data.Add(row);
    }

    public double[] GetColumn(int index)
    {
        double[] column = new double[_data.Count];
        for (int i = 0; i < _data.Count; i++)
        {
            column[i] = _data[i][index];
        }
        return column;
    }

    public List<string> GetColumns()
    {
        return _columns;
    }

    public List<double[]> GetData() => _data;

    public void Print()
    {
        // Print column headers
        foreach (var column in _columns)
        {
            Console.Write(column + "\t");
        }
        Console.WriteLine();

        // Print rows of data
        foreach (var row in _data)
        {
            Console.WriteLine(string.Join("\t", row));
        }
    }

    public static DataFrame ReadXlsx(string filePath)
    {
        var columns = new List<string>();
        var df = new DataFrame(columns);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Required for non-commercial use
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            // Read headers
            for (int col = 1; col <= worksheet.Dimension.Columns; col++)
            {
                columns.Add(worksheet.Cells[1, col].Text);
            }

            // Read rows
            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
            {
                var rowData = new double[columns.Count];
                for (int col = 1; col <= columns.Count; col++)
                {
                    rowData[col - 1] = double.Parse(worksheet.Cells[row, col].Text);
                }
                df.AddRow(rowData);
            }
        }

        return df;
    }

    public static void WriteXlsx(string filePath, DataFrame df)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage())
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

            // Write headers
            var columns = df.GetColumns();
            for (int col = 1; col <= columns.Count; col++)
            {
                worksheet.Cells[1, col].Value = columns[col - 1];
            }

            // Write rows
            var data = df.GetData();
            for (int row = 0; row < data.Count; row++)
            {
                for (int col = 0; col < columns.Count; col++)
                {
                    worksheet.Cells[row + 2, col + 1].Value = data[row][col];
                }
            }

            package.SaveAs(new FileInfo(filePath));
        }
    }

}
