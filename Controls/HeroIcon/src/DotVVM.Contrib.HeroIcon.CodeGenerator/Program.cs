using DotVVM.Contrib.HeroIcon.CodeGenerator;

var heroIconCSRoot = @"..\..\..\..\DotVVM.Contrib.HeroIcon\";
var outlineIconsPath = @"Assets\24\outline";
var solidIconsPath = @"Assets\24\solid";
var miniIconsPath = @"Assets\20\solid";

CodeGenerator.GenerateIcons(Path.Combine(heroIconCSRoot, "HeroIcons.cs"),
    outlineIconsPath,
    solidIconsPath,
    miniIconsPath);
