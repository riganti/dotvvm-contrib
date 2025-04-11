(function () {
	function removeContentAndAttributes(element) {
		element.innerHTML = "";
		for (let i = element.attributes.length - 1; i >= 0; i--) {
            const attribute = element.attributes[i];
            if (attribute.name !== "data-bind") {
                element.removeAttribute(attribute.name);
            }
		}
	}

	function setAttributesAndContent(element, svg) {
        const parser = new DOMParser();
        const doc = parser.parseFromString(svg, "image/svg+xml");
        const svgElement = doc.documentElement;
        for (let i = svgElement.attributes.length - 1; i >= 0; i--) {
            const attribute = svgElement.attributes[i];
            if (attribute.name !== "data-bind") {
                element.setAttribute(attribute.name, attribute.value);
            }
        }
        element.innerHTML = svgElement.innerHTML;
	}

	ko.bindingHandlers["dotvvm-contrib-SvgParser"] = {
		update: async function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
			const url = ko.unwrap(valueAccessor());
			try {
				const response = await fetch(url);
				const svg = await response.text();

				removeContentAndAttributes(element);
				setAttributesAndContent(element, svg);
			} catch {
				removeContentAndAttributes(element);
			}
		}
	};
})();