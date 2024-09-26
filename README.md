# DataAnalysis.NET
[![NuGet Downloads](https://img.shields.io/nuget/dt/Pavan.DataAnalysis.NET.svg)](https://www.nuget.org/packages/Pavan.DataAnalysis.NET)

Pavan.DataAnalysis.NET is a powerful .NET library designed for efficient data analysis and manipulation. It provides a flexible DataFrame structure and a suite of tools for reading, writing, cleaning, and analyzing data, with a focus on Excel and CSV file formats.
Here is the link: https://www.nuget.org/packages/Pavan.DataAnalysis.NET

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

Install Pavan.DataAnalysis.NET via NuGet Package Manager:

```
Install-Package Pavan.DataAnalysis.NET
```

Or via .NET CLI:

```
dotnet add package Pavan.DataAnalysis.NET
```

## Quick Start

### Using Dependency Injection

1. First, add the following using statements to your file:

```csharp
using Pavan.DataAnalysis.NET;
using Pavan.DataAnalysis.NET.Abstractions;
```

2. In your `Program.cs` or startup file, use one of the extension methods provided to register the `IDataAnalysis` interface with the dependency injection container:

```csharp
// For singleton service (recommended for most cases)
builder.Services.AddDataAnalysis();

// Or for scoped service
// builder.Services.AddDataAnalysisScoped();

// Or for transient service
// builder.Services.AddDataAnalysisTransient();
```

3. Then, you can inject and use the `IDataAnalysis` interface in your classes:

```csharp
public class MyAnalysisService
{
    private readonly IDataAnalysis _dataAnalysis;

    public MyAnalysisService(IDataAnalysis dataAnalysis)
    {
        _dataAnalysis = dataAnalysis;
    }

    public void PerformAnalysis(string filePath)
    {
        _dataAnalysis.LoadData(filePath);
        _dataAnalysis.AnalyzeData();
        _dataAnalysis.ExportResults("results.csv", "csv");
    }
}
```

### Basic Usage

```csharp
using Pavan.DataAnalysis.NET;

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

### IDataAnalysis Interface

The `IDataAnalysis` interface provides a high-level API for data analysis operations:
- `LoadData(string filePath)`: Load data from a CSV or Excel file
- `LoadData(Stream stream)`: Load data from a stream (not implemented in the current version)
- `AnalyzeData()`: Perform data analysis (implementation may vary)
- `ExportResults(string filePath, string format)`: Export analysis results to CSV or Excel

## Excel Support

Pavan.DataAnalysis.NET includes built-in support for Excel files:
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

## Building a Data Science Community in .NET

As C# and .NET developers, we have a unique opportunity to contribute to the growing field of data science. While languages like Python and R have traditionally dominated this space, the power and versatility of C# make it an excellent choice for data analysis and machine learning tasks.

### Why C# for Data Science?

1. **Performance**: C# offers superior performance, crucial for handling large datasets and complex computations.
2. **Type Safety**: The strong typing system in C# helps catch errors early and improves code reliability.
3. **Integration**: Seamlessly integrate data science workflows into existing .NET applications and ecosystems.
4. **Tooling**: Leverage the robust IDE support and debugging tools available in the .NET ecosystem.
5. **Cross-Platform**: With .NET Core, run your data science applications on Windows, Linux, and macOS.

### Join the Movement

By contributing to projects like DataAnalysis.NET and other data science initiatives in the .NET ecosystem, you can:

- **Innovate**: Bring fresh perspectives and ideas from the .NET world to data science challenges.
- **Learn**: Expand your skillset by applying C# knowledge to new domains.
- **Collaborate**: Connect with like-minded developers and data scientists, bridging the gap between software engineering and data analysis.
- **Impact**: Help shape the future of data science in the .NET ecosystem and create tools that empower developers worldwide.

### Get Involved

- Contribute to open-source data science libraries for .NET.
- Share your experiences and learnings through blog posts, talks, or tutorials.
- Participate in data science hackathons and competitions using C#.
- Engage with the community on forums, social media, and at conferences.

Together, we can build a thriving data science community in C# and .NET, creating powerful tools and pushing the boundaries of what's possible in data analysis and machine learning.

Let's code, analyze, and innovate – the .NET way!

---

Happy Data Analyzing with Pavan.DataAnalysis.NET!