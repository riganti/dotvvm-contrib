using DotVVM.Contrib.HeroIcon.CodeGenerator;

var heroIconCSRoot = @"..\..\..\..\DotVVM.Contrib.HeroIcon\";
var outlineIconsNamespace = "DotVVM.Contrib.HeroIcon.CodeGenerator.node_modules.heroicons._24.outline";
var solidIconsNamespace = "DotVVM.Contrib.HeroIcon.CodeGenerator.node_modules.heroicons._24.solid";
var miniIconsNamespace = "DotVVM.Contrib.HeroIcon.CodeGenerator.node_modules.heroicons._20.solid";

CodeGenerator.GenerateIcons(Path.Combine(heroIconCSRoot, "HeroIcons.cs"),
    outlineIconsNamespace,
    solidIconsNamespace,
    miniIconsNamespace);
