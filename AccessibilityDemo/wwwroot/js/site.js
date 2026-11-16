// Site-wide scripts. Page-specific demos live in their own files in this folder.
"use strict";

/**
 * Announce a message to screen reader users without moving focus.
 * Uses the polite live region (#announcer) defined in _Layout.cshtml.
 * The region is cleared first so repeating the same message is announced again.
 */
window.announce = function (message) {
    const region = document.getElementById("announcer");
    if (!region) return;
    region.textContent = "";
    setTimeout(() => { region.textContent = message; }, 100);
};

// ----- "Reduce motion" toggle in the footer (Seizures & physical reactions) -----
(function () {
    const button = document.getElementById("motion-toggle");
    if (!button) return;

    const root = document.documentElement;
    const sync = () => button.setAttribute("aria-pressed", String(root.classList.contains("reduce-motion")));
    sync();

    button.addEventListener("click", () => {
        const on = root.classList.toggle("reduce-motion");
        try { localStorage.setItem("reduceMotion", String(on)); } catch (e) { /* storage may be blocked */ }
        sync();
        announce(on ? "Animations are now off." : "Animations are now on.");
    });
})();

// ----- Error summary (Avoid and correct mistakes) -----
// When a form comes back with errors, move focus to the summary so keyboard and
// screen reader users find out immediately, then they can follow its links.
(function () {
    const summary = document.getElementById("error-summary");
    if (summary) summary.focus();
})();
