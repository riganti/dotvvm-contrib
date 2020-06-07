var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
(function (root, factory) {
    if (typeof module === "object" && typeof module.exports === "object") {
        var v = factory(require('react'), require('prop-types'), require('chartist'), exports);
        if (v !== undefined) module.exports = v;
    }
    else if (typeof define === "function" && define.amd) {
        define(["require", "exports", "react", "prop-types"], function (r, e) { return factory(r('react'), r('prop-types'), r('chartist'), e) });
    }
    else {
        root.ReactChartist = {}
        factory(root.React, root.PropTypes, root.Chartist, root.ReactChartist);
    }
})(this, function (react_1, prop_types_1, Chartist, exports) {
    "use strict";
    exports.__esModule = true;
    var ChartistGraph = (function (_super) {
        __extends(ChartistGraph, _super);
        function ChartistGraph() {
            return _super !== null && _super.apply(this, arguments) || this;
        }
        ChartistGraph.prototype.componentWillReceiveProps = function (newProps) {
            this.updateChart(newProps);
        };
        ChartistGraph.prototype.componentWillUnmount = function () {
            if (this.chartist) {
                try {
                    this.chartist.detach();
                }
                catch (err) {
                    throw new Error('Internal chartist error', err);
                }
            }
        };
        ChartistGraph.prototype.componentDidMount = function () {
            this.updateChart(this.props);
        };
        ChartistGraph.prototype.updateChart = function (config) {
            var type = config.type, data = config.data;
            var options = config.options || {};
            var responsiveOptions = config.responsiveOptions || [];
            var event;
            if (this.chartist) {
                this.chartist.update(data, options, responsiveOptions);
            }
            else {
                this.chartist = new Chartist[type](this.refs.chart, data, options, responsiveOptions);
                if (config.listener) {
                    for (event in config.listener) {
                        if (config.listener.hasOwnProperty(event)) {
                            this.chartist.on(event, config.listener[event]);
                        }
                    }
                }
            }
            return this.chartist;
        };
        ChartistGraph.prototype.render = function () {
            var _a = this.props, className = _a.className, style = _a.style, children = _a.children, data = _a.data, type = _a.type;

            // Hack: for pie chart array of array is converter to single array
            if (type === 'Pie') {
                data.series = data.series[0];
            }

            var childrenWithProps = children && react_1.Children.map(children, function (child) {
                return (react_1.cloneElement(child, {
                    type: type,
                    data: data
                }));
            });
            return (react_1.createElement("div", { className: "ct-chart " + (className || ''), ref: 'chart', style: style }, childrenWithProps));
        };
        return ChartistGraph;
    }(react_1.Component));
    ChartistGraph.propTypes = {
        type: prop_types_1.oneOf(['Line', 'Bar', 'Pie']).isRequired,
        data: prop_types_1.object.isRequired,
        className: prop_types_1.string,
        options: prop_types_1.object,
        responsiveOptions: prop_types_1.array,
        style: prop_types_1.object
    };
    exports["default"] = ChartistGraph;
});
