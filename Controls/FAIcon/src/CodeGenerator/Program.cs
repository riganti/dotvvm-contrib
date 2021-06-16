namespace FontAwesomeCS.CodeGenerator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var faCSRoot = @"..\..\DotVVM.Contrib\";
            var iconJsonPath = "Assets/icons.json";
            if (args.Length == 1)
            {
                iconJsonPath = args[0];
            }

            // Generate mappings
            CodeGenerator.GenerateMappings(faCSRoot + "FontAwesomeIcon.cs", iconJsonPath);
        }
    }
}