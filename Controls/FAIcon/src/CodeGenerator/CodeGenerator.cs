using FontAwesomeCS.CodeGenerator.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FontAwesomeCS.CodeGenerator
{
    public static class CodeGenerator
    {
        public static void GenerateMappings(string targetPath) => File.WriteAllText(targetPath, GenerateMappings());

        public static string GenerateMappings()
        {
            var icons = GetIcons();

            var cs = new StringBuilder();
            cs.AppendLine("namespace DotVVM.Contrib");
            cs.AppendLine("{");

            // FAStyles
            cs.AppendLine("    public enum FAStyle");
            cs.AppendLine("    {");

            foreach (var style in icons.Values.SelectMany(icon => icon.Styles).Distinct().OrderBy(s => s))
            {
                cs.AppendLine($"        [FAStyle(\"{style}\")] {FormatName(style)},");
            }

            cs.AppendLine("    }");

            // FAIcons
            cs.AppendLine("    public enum FAIcons");
            cs.AppendLine("    {");

            foreach (var icon in icons)
            {
                foreach (var style in icon.Value.Styles)
                {
                    cs.AppendLine($"        /// <summary>{icon.Value.Label} - {icon.Key} - {icon.Value.Unicode}</summary>");
                    cs.AppendLine($"        [FAIcon(\"{icon.Key}\", \"{icon.Value.Label}\", FAStyle.{FormatName(style)}, \"\\u{icon.Value.Unicode}\")] {FormatName(icon.Key)}_{FormatName(style)},");
                }
            }

            cs.AppendLine("    }");

            cs.AppendLine("}");

            return cs.ToString();
        }
        
        private static Dictionary<string, IconDto> GetIcons()
        {
            var json = File.ReadAllText("Assets/icons.json");
            return JsonConvert.DeserializeObject<Dictionary<string, IconDto>>(json);
        }

        internal static string FormatName(string input)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i].ToString();

                if (i == 0 && int.TryParse(c, out var num))
                {
                    sb.Append("_");
                }

                sb.Append(c == "-" ? "_" : c);
            }

            return sb.ToString();
        }
    }
}