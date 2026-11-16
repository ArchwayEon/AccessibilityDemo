// "Distinguishable" page: toggle the WCAG text-spacing test values on a sample box.
"use strict";

const spacingButton = document.getElementById("spacing-toggle");
const sample = document.getElementById("spacing-sample");

spacingButton.addEventListener("click", () => {
    const on = sample.classList.toggle("wcag-spacing");
    spacingButton.setAttribute("aria-pressed", String(on));
});
