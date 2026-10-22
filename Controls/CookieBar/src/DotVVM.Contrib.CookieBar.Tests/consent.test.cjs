const { test } = require("node:test");
const assert = require("node:assert/strict");
const fs = require("node:fs");
const path = require("node:path");
const vm = require("node:vm");

const library = path.join(__dirname, "..", "DotVVM.Contrib.CookieBar");
const moduleSource = fs.readFileSync(path.join(library, "Scripts", "DotVVM.Contrib.CookieBar.js"), "utf8")
    .replace("export default context => new CookieBar(context);", "") + "\nglobalThis.CookieBar = CookieBar;";
const controlSource = fs.readFileSync(path.join(library, "CookieBar.cs"), "utf8");
const scriptStart = controlSource.indexOf("window.dataLayer =");
const scriptEnd = controlSource.indexOf('";', scriptStart);
const initialScript = controlSource.slice(scriptStart, scriptEnd)
    .replace("{JsonSerializer.Serialize(Rules.Select(r => r.Key).ToArray())}", '["analytics_storage","ad_storage"]')
    .replaceAll("{{", "{").replaceAll("}}", "}");

function create(saved = {}, unavailable = false) {
    const storage = new Map(Object.entries(saved));
    const warnings = [];
    const localStorage = {
        getItem(key) {
            if (unavailable === true || unavailable === key) throw new Error("Storage unavailable");
            return storage.get(key) ?? null;
        },
        setItem: (key, value) => storage.set(key, String(value)),
        removeItem: key => storage.delete(key)
    };
    function classes() {
        const values = new Set();
        return {
            add: key => values.add(key),
            remove: key => values.delete(key),
            toggle: (key, enabled) => enabled ? values.add(key) : values.delete(key),
            contains: key => values.has(key)
        };
    }
    const checkboxes = ["analytics_storage", "ad_storage"].map(key => ({
        checked: false,
        parentElement: { dataset: { key, titleTrue: "Allowed", titleFalse: "Disallowed" }, classList: classes() },
        addEventListener() {}
    }));
    const popup = { classList: classes() };
    const dialog = { querySelectorAll: () => checkboxes, addEventListener() {}, showModal() {}, close() {} };
    const container = { querySelector: selector => selector === "#consentDialog" ? dialog : popup };
    const window = { localStorage, dataLayer: [] };
    const sandbox = { window, console: { warn: (...args) => warnings.push(args) }, document: { querySelector: () => ({ style: {} }) } };
    vm.createContext(sandbox);
    vm.runInContext(initialScript, sandbox);
    vm.runInContext(moduleSource, sandbox);
    const bar = new sandbox.CookieBar({ elements: [{ parentElement: container }] });
    return { bar, window, storage, checkboxes, popup, warnings };
}

test("first visit defaults to denied before module initialization and keeps optional controls off", () => {
    const state = create();
    assert.equal(state.window.dataLayer[0][1], "default");
    assert.equal(state.window.dataLayer[0][2].analytics_storage, "denied");
    assert.equal(state.checkboxes[0].checked, false);
    assert.equal(state.popup.classList.contains("dotvvm-contrib-cookie-bar__pop-up--open"), true);
});

test("saved acceptance is in the first consent command, not just the later update", () => {
    const state = create({ cookieconsent: "true", cookieconsent__analytics_storage: "granted" });
    assert.equal(state.window.dataLayer[0][2].analytics_storage, "granted");
    assert.equal(state.window.dataLayer[0][2].ad_storage, "denied");
    assert.equal(state.checkboxes[0].checked, true);
    assert.equal(state.checkboxes[0].parentElement.title, "Allowed");
});

test("saved rejection remains rejected when preferences are saved unchanged", () => {
    const state = create({ cookieconsent: "true", cookieconsent__analytics_storage: "denied" });
    state.bar.openDialog();
    state.bar.saveAndCloseDialog();
    assert.equal(state.storage.get("cookieconsent__analytics_storage"), "denied");
    assert.equal(state.window.dataLayer.at(-1)[2].analytics_storage, "denied");
});

test("unconfirmed or invalid stored values never grant consent", () => {
    assert.equal(create({ cookieconsent__analytics_storage: "granted" }).window.dataLayer[0][2].analytics_storage, "denied");
    assert.equal(create({ cookieconsent: "true", cookieconsent__analytics_storage: "invalid" }).window.dataLayer[0][2].analytics_storage, "denied");
});

test("accept, reject and reset synchronize storage, Google consent and controls", () => {
    const state = create();
    state.bar.acceptAll();
    assert.equal(state.storage.get("cookieconsent__analytics_storage"), "granted");
    assert.equal(state.checkboxes[0].checked, true);
    state.bar.disableAllUnnecessaryCookiesAndSave();
    assert.equal(state.checkboxes[0].parentElement.title, "Disallowed");
    assert.equal(state.window.dataLayer.at(-1)[2].analytics_storage, "denied");
    state.bar.acceptAll();
    state.window.DotVVM.Contrib.CookieBar.resetConsent(true);
    assert.equal(state.checkboxes[0].checked, false);
    assert.equal(state.storage.has("cookieconsent"), false);
    assert.equal(state.window.dataLayer.at(-1)[2].analytics_storage, "denied");
});

test("cancelled unsaved choices are discarded when preferences reopen", () => {
    const state = create();
    state.checkboxes[0].checked = true;
    state.bar.openDialog();
    assert.equal(state.checkboxes[0].checked, false);
});

test("unavailable storage retains denied defaults and reports the problem", () => {
    const state = create({}, true);
    assert.equal(state.window.dataLayer[0][2].analytics_storage, "denied");
    assert.equal(state.checkboxes[0].checked, false);
    assert.equal(state.warnings.length, 2);
});

test("a partial storage read failure discards all restored grants", () => {
    const state = create({ cookieconsent: "true", cookieconsent__analytics_storage: "granted" }, "cookieconsent__ad_storage");
    assert.equal(state.window.dataLayer[0][2].analytics_storage, "denied");
    assert.equal(state.checkboxes[0].checked, false);
    assert.equal(state.warnings.length, 2);
});
