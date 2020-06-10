using System.Drawing;

namespace SortColors
{
    public class ColorData
    {
        public ColorData(CsvColor color)
        {
            Name = color.Name;
            Hex = color.Hex;
            Preview = color.Preview;
            Color = ColorTranslator.FromHtml(color.Hex.Trim('`'));
        }

        public string Name { get; set; }
        public string Hex { get; set; }
        public string Preview { get; set; }
        public Color Color { get; set; }
    }
}