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
    .replaceAll("{{", "{").replaceAll("}}", "}");

function create(saved = {}, unavailable = false, ruleKeys = ["analytics_storage", "ad_storage"]) {
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
    const checkboxes = ruleKeys.map(key => ({
        checked: false,
        parentElement: { dataset: { key, titleTrue: "Allowed", titleFalse: "Disallowed" }, classList: classes() },
        addEventListener() {}
    }));
    const popup = { classList: classes() };
    const dialog = { querySelectorAll: () => checkboxes, addEventListener() {}, showModal() {}, close() {} };
    const container = { querySelector: selector => selector === "#consentDialog" ? dialog : popup };
    const window = { localStorage, dataLayer: [], location: { hostname: "www.example.com" } };
    const document = { querySelector: () => ({ style: {} }) };
    const sandbox = { window, console: { warn: (...args) => warnings.push(args) }, document };
    vm.createContext(sandbox);
    vm.runInContext(initialScript.replace(
        "{JsonSerializer.Serialize(Rules.Select(r => r.Key).ToArray())}",
        JSON.stringify(ruleKeys)), sandbox);
    vm.runInContext(moduleSource, sandbox);
    const bar = new sandbox.CookieBar({ elements: [{ parentElement: container }] });
    return { bar, window, document, storage, checkboxes, popup, warnings };
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

test("no configured rules produces no default consent keys", () => {
    const state = create({}, false, []);
    assert.deepEqual({ ...state.window.dataLayer[0][2] }, {});
});

test("default consent keys match configured rules exactly, including custom rules", () => {
    const state = create({}, false, ["analytics_storage", "clarity_storage", "custom_storage"]);
    assert.deepEqual({ ...state.window.dataLayer[0][2] }, {
        analytics_storage: "denied",
        clarity_storage: "denied",
        custom_storage: "denied"
    });
});

test("analytics-only acceptance never grants unconfigured advertising purposes", () => {
    const state = create({
        cookieconsent: "true",
        cookieconsent__analytics_storage: "granted",
        cookieconsent__ad_storage: "granted",
        cookieconsent__ad_user_data: "granted",
        cookieconsent__ad_personalization: "granted"
    }, false, ["analytics_storage", "analytics_Storage"]);
    const defaults = state.window.dataLayer[0][2];
    assert.equal(defaults.analytics_storage, "granted");
    assert.equal(defaults.analytics_Storage, "denied");
    for (const key of ["ad_storage", "ad_user_data", "ad_personalization"]) {
        assert.equal(Object.hasOwn(defaults, key), false);
    }
    state.bar.acceptAll();
    const update = state.window.dataLayer.at(-1)[2];
    assert.equal(update.analytics_storage, "granted");
    for (const key of ["ad_storage", "ad_user_data", "ad_personalization"]) {
        assert.equal(Object.hasOwn(update, key), false);
    }
});

test("explicitly configured advertising permissions can be restored and revoked", () => {
    const keys = ["analytics_storage", "ad_storage", "ad_user_data", "ad_personalization"];
    const saved = { cookieconsent: "true" };
    keys.forEach(key => { saved["cookieconsent__" + key] = "granted"; });
    const state = create(saved, false, keys);
    keys.forEach(key => { assert.equal(state.window.dataLayer[0][2][key], "granted"); });
    state.bar.disableAllUnnecessaryCookiesAndSave();
    keys.forEach(key => { assert.equal(state.window.dataLayer.at(-1)[2][key], "denied"); });
});

const rulesSource = fs.readFileSync(path.join(library, "CookieBarRule.cs"), "utf8");
test("built-in advertising purpose rules provide distinct keys and resource-backed descriptions", () => {
    const resources = fs.readFileSync(path.join(library, "Resources", "CookieTexts.resx"), "utf8");
    for (const [rule, key, resource] of [
        ["GoogleAdsPersonalizationRule", "ad_personalization", "GoogleAdsPersonalization"],
        ["GoogleAdsUserDataRule", "ad_user_data", "GoogleAdsUserData"]
    ]) {
        const start = rulesSource.indexOf(`public class ${rule} :`);
        assert.ok(start >= 0);
        const body = rulesSource.slice(start).split("[JsonConverter")[0];
        assert.ok(body.includes(`Key = "${key}";`));
        assert.ok(body.includes(`Title = CookieTexts.RuleTitle_${resource};`));
        assert.ok(body.includes(`Description = CookieTexts.Rule_${resource};`));
        for (const prefix of ["RuleTitle_", "Rule_"]) {
            assert.match(resources, new RegExp(`<data name="${prefix}${resource}"[^>]*>\\s*<value>[^<]+</value>`));
        }
    }
});

const cookieCases = [
    {
        rule: "GoogleAnalyticsRule",
        matches: ["_ga", "_ga_ABC123", "_gid", "_gat", "_gat_UA-123-1", "_gac_UA-123-1",
            "__utma", "__utmb", "__utmc", "__utmt", "__utmz", "__utmv", "_dc_gtm_UA-123-1",
            "AMP_TOKEN", "FPID", "FPLC"],
        excludes: ["__utm|", "__utmd", "_ga_", "_gat_", "_gac_", "_gcl_aw", "_fbp", "session", "prefix_ga"]
    },
    {
        rule: "GoogleAdsRule",
        matches: ["_gcl_au", "_gcl_aw", "_gcl_dc", "_gcl_gs", "_gcl_gb", "_gcl_ag",
            "_gac_UA-123-1", "_gac_gb_123", "FPGCLAW", "FPGCLGB", "FPGCLDC", "FPGCLGS", "FPAU", "FPGSID"],
        excludes: ["_gcl_", "_ga", "FPGCL", "FPGCLAW_extra", "IDE", "NID", "session"]
    },
    {
        rule: "FacebookPixelRule",
        matches: ["_fbp", "_fbc"],
        excludes: ["_fbp_extra", "prefix_fbc", "fr", "session"]
    },
    {
        rule: "SmartlookRule",
        matches: ["SL", "SL_C_23361dd035530_SID", "SL_C_23361dd035530_VID",
            "SL_C_23361dd035530_KEY", "SL_C_23361dd035530_DOMAIN"],
        excludes: ["SL_C_", "SL_C__SID", "SL_C_123_OTHER", "SL_L_23361dd035530_SID",
            "SMARTLOOK_LS_QUEUED_CHUNKS", "AWSELB", "session"]
    },
    {
        rule: "ClarityRule",
        matches: ["_clck", "_clsk"],
        excludes: ["_clck_extra", "prefix_clsk", "CLID", "MUID", "ANONCHK", "MR", "SM", "session"]
    }
];

for (const { rule, matches, excludes } of cookieCases) {
    test(`${rule} patterns match supported first-party cookies without matching unrelated names`, () => {
        const body = rulesSource.slice(rulesSource.indexOf(`public class ${rule} :`));
        const declaration = body.match(/CookieNameRegexes = new\[\] \{ ([^\r\n]+) \};/);
        assert.ok(declaration, `Missing cookie patterns for ${rule}`);
        const patterns = [...declaration[1].matchAll(/"([^"]+)"/g)].map(match => new RegExp(match[1]));
        assert.ok(patterns.length > 0);
        for (const name of matches) {
            assert.ok(patterns.some(pattern => pattern.test(name)), `${rule} must match ${name}`);
        }
        for (const name of excludes) {
            assert.ok(!patterns.some(pattern => pattern.test(name)), `${rule} must not match ${name}`);
        }
    });
}

test("cookie revocation targets host-only, current-domain and parent-domain cookies", () => {
    const state = create();
    for (const hostname of ["www.example.com", "example.com", "localhost"]) {
        state.window.location.hostname = hostname;
        const writes = [];
        Object.defineProperty(state.document, "cookie", {
            configurable: true,
            get: () => "_clck=value=with=equals; _clsk=session; unrelated=keep",
            set: value => writes.push(value)
        });
        state.bar.deleteCookie("^_clck$");
        assert.ok(writes.every(value => value.startsWith("_clck=;")));
        assert.ok(writes.some(value => value === "_clck=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;"));
        if (hostname !== "localhost") {
            assert.ok(writes.some(value => value.includes(`domain=${hostname};`)));
            assert.ok(writes.some(value => value.includes("domain=example.com;")));
        }
        assert.ok(!writes.some(value => value.includes("domain=com;")));
    }
});

test("rejecting a rule uses its cookie patterns without deleting unrelated cookies", () => {
    const state = create();
    state.checkboxes[0].parentElement.dataset.cookieNameRegexes = "^_clck$,^_clsk$";
    const writes = [];
    Object.defineProperty(state.document, "cookie", {
        get: () => "_clck=user; _clsk=session; authentication=keep",
        set: value => writes.push(value)
    });
    state.bar.disableAllUnnecessaryCookiesAndSave();
    assert.ok(writes.some(value => value.startsWith("_clck=;")));
    assert.ok(writes.some(value => value.startsWith("_clsk=;")));
    assert.ok(!writes.some(value => value.startsWith("authentication=")));
});

test("Clarity choice is independent from Google consent and restores only the new rule key", () => {
    const state = create({
        cookieconsent: "true",
        cookieconsent__clarity_storage: "granted",
        cookieconsent__analytics_Storage: "granted"
    }, false, ["clarity_storage"]);
    assert.equal(state.window.dataLayer[0][2].clarity_storage, "granted");
    assert.equal(Object.hasOwn(state.window.dataLayer[0][2], "analytics_storage"), false);
    assert.equal(Object.hasOwn(state.window.dataLayer[0][2], "ad_storage"), false);
    state.bar.disableAllUnnecessaryCookiesAndSave();
    assert.equal(state.window.dataLayer.at(-1)[2].clarity_storage, "denied");
    const legacy = create({
        cookieconsent: "true",
        cookieconsent__analytics_Storage: "granted"
    }, false, ["clarity_storage"]);
    assert.equal(legacy.window.dataLayer[0][2].clarity_storage, "denied");
});

test("new rules reopen the banner while preserving prior grants and rejections", () => {
    const saved = {
        cookieconsent: "true",
        cookieconsent__analytics_storage: "granted",
        cookieconsent__ad_storage: "denied"
    };
    const keys = ["analytics_storage", "ad_storage", "ad_user_data", "ad_personalization"];
    const state = create(saved, false, keys);
    assert.equal(state.popup.classList.contains("dotvvm-contrib-cookie-bar__pop-up--open"), true);
    assert.deepEqual(state.checkboxes.map(checkbox => checkbox.checked), [true, false, false, false]);
    assert.equal(state.window.dataLayer[0][2].ad_user_data, "denied");
    assert.equal(state.storage.has("cookieconsent__ad_user_data"), false);
    state.bar.openDialog();
    state.bar.saveAndCloseDialog();
    const reloaded = create(Object.fromEntries(state.storage), false, keys);
    assert.equal(reloaded.popup.classList.contains("dotvvm-contrib-cookie-bar__pop-up--open"), false);
    assert.deepEqual(reloaded.checkboxes.map(checkbox => checkbox.checked), [true, false, false, false]);
});

test("complete saved rejections do not reopen the banner; invalid choices do", () => {
    const saved = {
        cookieconsent: "true",
        cookieconsent__analytics_storage: "denied",
        cookieconsent__ad_storage: "denied"
    };
    assert.equal(create(saved).popup.classList.contains("dotvvm-contrib-cookie-bar__pop-up--open"), false);
    saved.cookieconsent__ad_storage = "invalid";
    assert.equal(create(saved).popup.classList.contains("dotvvm-contrib-cookie-bar__pop-up--open"), true);
});

test("removed rules do not reopen the banner or emit obsolete saved permissions", () => {
    const state = create({
        cookieconsent: "true",
        cookieconsent__analytics_storage: "granted",
        cookieconsent__ad_storage: "granted"
    }, false, ["analytics_storage"]);
    assert.equal(state.popup.classList.contains("dotvvm-contrib-cookie-bar__pop-up--open"), false);
    assert.deepEqual({ ...state.window.dataLayer[0][2] }, { analytics_storage: "granted" });
});
