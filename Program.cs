using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SortColors
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            var file = new FileInfo($"{dir.FullName}\\color-keywords.csv");

            using var fs = new StreamReader(file.FullName);

            var colors = new CsvHelper
                .CsvReader(fs, CultureInfo.InvariantCulture)
                .GetRecords<CsvColor>()
                .Select(c => new ColorData(c))
                .OrderBy(c => c.Color.GetHue())
                    .ThenBy(c => c.Color.GetSaturation())
                        .ThenBy(c => c.Color.GetBrightness())
                .ToList();
                
            var doc = new StringBuilder();
            doc.AppendLine("<style>");
            doc.AppendLine("table img {");
            doc.AppendLine("\tbox-shadow: 1px 1px 8px rgba(0,0,0,.67);");
            doc.AppendLine("\tborder: 1px solid rgba(0,0,0,.67);");
            doc.AppendLine("}");
            doc.AppendLine("</style>");
            doc.AppendLine();
            doc.AppendLine("Keyword | RGB Hex Value | Color");
            doc.AppendLine("--------|---------------|------");
            
            foreach (var color in colors)
            {
                doc.AppendLine($"{color.Name} | {color.Hex} | {color.Preview}");
            }

            using var sw = new StreamWriter($"{dir.FullName}\\color-keywords.md");
            sw.Write(doc.ToString());
        }
    }
}