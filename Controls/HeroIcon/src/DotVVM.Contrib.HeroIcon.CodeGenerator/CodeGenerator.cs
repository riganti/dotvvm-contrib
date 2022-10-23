using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace DotVVM.Contrib.HeroIcon.CodeGenerator;

public static class CodeGenerator
{
    public static void GenerateIcons(string targetPath, string outlineIconFolder, string solidIconFolder,
        string miniIconFolder)
    => File.WriteAllText(targetPath, GenerateIcons(outlineIconFolder, solidIconFolder, miniIconFolder));

    public static string GenerateIcons(string outlineIconFolder, string solidIconFolder, string miniIconFolder)
    {
        var cs = new StringBuilder();
        cs.AppendLine("namespace DotVVM.Contrib.HeroIcon");
        cs.AppendLine("{");

        cs.AppendLine("    public enum HeroIcons");
        cs.AppendLine("    {");

        foreach (var iconPath in Directory.GetFiles(outlineIconFolder))
        {
            var filename = Path.GetFileName(iconPath);
            if (filename.EndsWith(".svg") == false)
            {
                throw new InvalidOperationException("Not na svg file.");
            }

            var outlineIcon = File.ReadAllText(Path.Combine(outlineIconFolder, filename));
            AppendHeroIconAttribute("Outline", outlineIcon, cs);
            var solidIcon = File.ReadAllText(Path.Combine(solidIconFolder, filename));
            AppendHeroIconAttribute("Solid", solidIcon, cs);
            var miniIcon = File.ReadAllText(Path.Combine(miniIconFolder, filename));
            AppendHeroIconAttribute("Mini", miniIcon, cs);
            cs.AppendLine($"        {NormalizeFilename(filename)},");
            cs.AppendLine();
        }

        cs.AppendLine("    }");
        
        cs.AppendLine("}");

        return cs.ToString();
    }

    private static void AppendHeroIconAttribute(string iconType, string fileContent, StringBuilder cs)
    {
        var html = new HtmlDocument();
        html.LoadHtml(fileContent);
        var svg = html.DocumentNode.SelectSingleNode("//svg");
        var pathPart = svg.InnerHtml.Replace("\n", "").Replace("\r", ""); ;
        
        cs.AppendLine($"        [HeroIcon(VisualStyle.{iconType}, \"{EscapeString(pathPart)}\")]");
    }

    private static string NormalizeFilename(string filename)
    {
        return filename.Replace(".svg", "").Replace("-", "_");
    }

    private static string EscapeString(string value)
    {
        return value.Replace("\"", @"\""");
    }
}