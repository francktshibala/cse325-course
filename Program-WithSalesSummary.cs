using Newtonsoft.Json;
using System.Text;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

// Generate sales summary report
var salesSummary = GenerateSalesSummary(salesFiles);
Console.WriteLine(salesSummary);

// Write the summary to a file
File.WriteAllText(Path.Combine(salesTotalDir, "sales-summary.txt"), salesSummary);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }
    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

// NEW FUNCTION: Generate sales summary report
string GenerateSalesSummary(IEnumerable<string> salesFiles)
{
    StringBuilder report = new StringBuilder();
    double grandTotal = 0;

    // Build list of file totals
    List<(string fileName, double total)> fileDetails = new List<(string, double)>();

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        double fileTotal = data?.Total ?? 0;

        grandTotal += fileTotal;
        fileDetails.Add((Path.GetFileName(file), fileTotal));
    }

    // Build the report using StringBuilder
    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($" Total Sales: {grandTotal.ToString("C")}");
    report.AppendLine();
    report.AppendLine(" Details:");

    foreach (var (fileName, total) in fileDetails)
    {
        report.AppendLine($"  {fileName}: {total.ToString("C")}");
    }

    return report.ToString();
}

record SalesData(double Total);
