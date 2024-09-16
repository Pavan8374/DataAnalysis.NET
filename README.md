# DataAnalysis.NET

DataAnalysis.NET is a powerful .NET library designed for efficient data analysis and manipulation. It provides a flexible DataFrame structure and a suite of tools for reading, writing, cleaning, and analyzing data, with a focus on Excel and CSV file formats.

## Features

- **DataFrame Structure**: A versatile data structure for holding and manipulating tabular data.
- **File I/O**: 
  - Read and write CSV files
  - Read and write Excel (.xlsx) files
- **Data Cleaning**:
  - Replace NaN values with a specified value or column mean
  - Drop rows with NaN values
  - Filter rows based on custom predicates
- **Data Analysis**:
  - Column-wise operations: Sum, Mean, Variance
  - Create pivot tables with custom aggregation functions
- **Performance**: Utilizes SIMD (Single Instruction, Multiple Data) for efficient column summation

## Installation

Install DataAnalysis.NET via NuGet Package Manager:

```
Install-Package DataAnalysis.NET
```

Or via .NET CLI:

```
dotnet add package DataAnalysis.NET
```

## Quick Start

```csharp
using DataAnalysis.NET;

// Read a CSV file
var df = ExcelReader.ReadCsv("data.csv");

// Clean the data
DataCleaner.ReplaceNaNsWithMean(df);

// Perform analysis
var columnMeans = DataFrameOperations.MeanColumns(df);

// Create a pivot table
var pivotTable = DataFrameOperations.CreatePivotTable(df, 0, 1, arr => arr.Average());

// Write results back to Excel
DataFrame.WriteXlsx("results.xlsx", pivotTable);
```

## Key Components

### DataFrame

The core data structure for holding tabular data. It supports:
- Adding rows
- Retrieving columns
- Getting column names
- Printing data to console

### ExcelReader

Provides methods for reading and writing CSV files:
- `ReadCsv(string filePath)`: Read data from a CSV file into a DataFrame
- `WriteCsv(string filePath, DataFrame df)`: Write a DataFrame to a CSV file

### DataFrameOperations

Offers various data analysis operations:
- `SumColumns(DataFrame df)`: Calculate the sum of each column
- `MeanColumns(DataFrame df)`: Calculate the mean of each column
- `VarianceColumns(DataFrame df)`: Calculate the variance of each column
- `CreatePivotTable(DataFrame df, int groupByCol, int valueCol, Func<double[], double> aggregationFunc)`: Create a pivot table with custom aggregation

### DataCleaner

Provides methods for cleaning and filtering data:
- `ReplaceNaNs(DataFrame df, double replacementValue)`: Replace NaN values with a specified value
- `DropNaNs(DataFrame df)`: Remove rows containing NaN values
- `ReplaceNaNsWithMean(DataFrame df)`: Replace NaN values with the column mean
- `FilterRows(DataFrame df, Func<double[], bool> predicate)`: Filter rows based on a custom predicate

## Excel Support

DataAnalysis.NET includes built-in support for Excel files:
- `DataFrame.ReadXlsx(string filePath)`: Read data from an Excel file into a DataFrame
- `DataFrame.WriteXlsx(string filePath, DataFrame df)`: Write a DataFrame to an Excel file

## Use Cases

1. **Financial Analysis**: Process and analyze financial data from CSV or Excel files.
2. **Data Cleaning**: Easily handle missing values and filter datasets.
3. **Statistical Analysis**: Compute basic statistical measures like mean and variance.
4. **Data Aggregation**: Create pivot tables for summarizing large datasets.
5. **Data Transformation**: Filter and manipulate data using custom predicates.

## Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for more details.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

If you encounter any issues or have questions, please file an issue on the GitHub repository.

---

Happy Data Analyzing with DataAnalysis.NET!