using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace DotVVM.Contrib.HeroIcon.CodeGenerator;

public static class CodeGenerator
{
    public static void GenerateIcons(string targetNamespace, string outlineIconNamespace, string solidIconNamespace,
        string miniIconFolder)
    => File.WriteAllText(targetNamespace, GenerateIcons(outlineIconNamespace, solidIconNamespace, miniIconFolder));

    public static string GenerateIcons(string outlineIconNamespace, string solidIconNamespace, string miniIconNamespace)
    {
        var cs = new StringBuilder();
        cs.AppendLine("namespace DotVVM.Contrib.HeroIcon");
        cs.AppendLine("{");

        cs.AppendLine("    public enum HeroIcons");
        cs.AppendLine("    {");

        var assembly = Assembly.GetExecutingAssembly();
        foreach (var filename in GetIconFileNames(assembly, outlineIconNamespace))
        {
            if (filename.EndsWith(".svg") == false)
            {
                throw new InvalidOperationException("Not na svg file.");
            }

            var outlineIcon = GetResourceContent(assembly, outlineIconNamespace, filename);
            AppendHeroIconAttribute("Outline", outlineIcon, cs);
            var solidIcon = GetResourceContent(assembly, solidIconNamespace, filename);
            AppendHeroIconAttribute("Solid", solidIcon, cs);
            var miniIcon = GetResourceContent(assembly, miniIconNamespace, filename);
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

    private static IEnumerable<string> GetIconFileNames(Assembly assembly, string iconNamespace)
    {
        var icons = assembly.GetManifestResourceNames()
            .Where(x => x.StartsWith(iconNamespace))
            .Select(x => x.Replace(iconNamespace, "").Trim('.'));
        if (icons.Any() == false)
        {
            throw new InvalidOperationException("No icons loaded. Make sure you installed npm packages.");
        }
        return icons;
    }

    private static string GetResourceContent(Assembly assembly, string iconNamespace, string filename)
    {
        var resourceName = $"{iconNamespace}.{filename}";
        using Stream stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException("Resource is missing.");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
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