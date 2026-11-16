// "Compatible" page demos: name, role, and value for custom widgets.
"use strict";

// ---------------------------------------------------------------------------
// 1. Toggle switch: role="switch" + aria-checked
// ---------------------------------------------------------------------------
const toggle = document.getElementById("notify-switch");

toggle.addEventListener("click", () => {
    const on = toggle.getAttribute("aria-checked") !== "true";
    toggle.setAttribute("aria-checked", String(on));                  // the VALUE assistive tech reads
    toggle.querySelector(".switch-state").textContent = on ? "On" : "Off"; // the visible state
});

// ---------------------------------------------------------------------------
// 2. Tabs, following the ARIA Authoring Practices "Tabs" pattern
//    https://www.w3.org/WAI/ARIA/apg/patterns/tabs/
// ---------------------------------------------------------------------------
const tabs = [...document.querySelectorAll('#tabs-demo [role="tab"]')];

function selectTab(tab) {
    tabs.forEach(t => {
        const selected = t === tab;
        t.setAttribute("aria-selected", String(selected));
        t.tabIndex = selected ? 0 : -1;       // "roving tabindex": only the selected tab is in the Tab order
        document.getElementById(t.getAttribute("aria-controls")).hidden = !selected;
    });
    tab.focus();
}

tabs.forEach((tab, i) => {
    tab.addEventListener("click", () => selectTab(tab));
    tab.addEventListener("keydown", e => {
        let next = null;
        if (e.key === "ArrowRight") next = tabs[(i + 1) % tabs.length];
        else if (e.key === "ArrowLeft") next = tabs[(i - 1 + tabs.length) % tabs.length];
        else if (e.key === "Home") next = tabs[0];
        else if (e.key === "End") next = tabs[tabs.length - 1];
        if (next) {
            e.preventDefault();
            selectTab(next);
        }
    });
});

// ---------------------------------------------------------------------------
// 3. Status message in a role="status" live region
// ---------------------------------------------------------------------------
document.getElementById("save-button").addEventListener("click", () => {
    const time = new Date().toLocaleTimeString([], { hour: "numeric", minute: "2-digit", second: "2-digit" });
    document.getElementById("save-status").textContent = `Settings saved at ${time}.`;
});
