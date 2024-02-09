using CsvHelper.Configuration;
using System.Globalization;

namespace INF11207.TP1.CsvFileHandling;

public static class CsvReader
{
    //private static CsvConfiguration _config = new CsvConfiguration(CultureInfo.CurrentCulture)
    //{
    //    Delimiter = ";",
    //};

    public static string[] ReadAttributes(string path)
    {
        string[] headerRow = Array.Empty<string>();

        using (var reader = new StreamReader(path))
        using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();
            headerRow = csv.HeaderRecord;
        }

        return headerRow;
    }

    public static string[] ReadLines(string path)
    {
        List<string> lines = new List<string>();

        using (var reader = new StreamReader(path))
        using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                string line = csv.Context.Reader.Parser.RawRecord;
                lines.Add(line.Replace("\r\n", ""));
            }
        }

        return lines.ToArray();
    }
}
