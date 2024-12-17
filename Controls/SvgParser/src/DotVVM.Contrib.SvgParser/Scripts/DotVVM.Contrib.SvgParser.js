ko.bindingHandlers["dotvvm-contrib-SvgParser"] = {
	update: async function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
		try {
			const url = ko.unwrap(valueAccessor());
			const response = await fetch(url);
			const svg = await response.text();
			element.outerHTML = svg;
		} catch {
			element.outerHTML = "<svg></svg>";
		}
    }
};