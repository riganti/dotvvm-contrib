using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class Sample1ViewModel : MasterViewModel
	{
		public string[] Htmls { get; set; } = new [] { @"
		<span style='color: red'> Hello </span>
		<img src=something onerror='alert(1)' />
		<script>alert(1)</script> 
		"
		};

		[AllowStaticCommand]
		public static string[] MoreHtmls() => new [] {
			"<style> something </style>",
			"<div style='position: absolute; top: 0; left: 0; width: 100vw; height: 100vh'> my content </div>",
			"<span> nothing bad </span>"
		};
	}
}

