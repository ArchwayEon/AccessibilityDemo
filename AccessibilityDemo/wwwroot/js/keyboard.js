// Keyboard page demos.
"use strict";

const now = () => new Date().toLocaleTimeString([], { hour: "numeric", minute: "2-digit", second: "2-digit" });

// The native <button> works with mouse, Enter, and Space with just a click handler.
document.getElementById("kb-good-button").addEventListener("click", () => {
    document.getElementById("kb-good-status").textContent = `Draft saved at ${now()}.`;
});

// The <span> "button" only ever receives mouse clicks: it is not focusable,
// and it would not respond to Enter or Space even if it were.
document.getElementById("kb-bad-button").addEventListener("click", () => {
    document.getElementById("kb-bad-status").textContent = `Draft saved at ${now()} (mouse users only!).`;
});

// Native <dialog>: showModal() moves focus inside, keeps Tab within the dialog,
// closes on Esc, and makes the rest of the page inert.
const dialog = document.getElementById("kb-dialog");
const opener = document.getElementById("open-dialog");

opener.addEventListener("click", () => dialog.showModal());
dialog.querySelectorAll("[data-close-dialog]").forEach(b =>
    b.addEventListener("click", () => dialog.close()));

// Return focus to the button that opened the dialog, so keyboard users
// continue from where they were.
dialog.addEventListener("close", () => opener.focus());
