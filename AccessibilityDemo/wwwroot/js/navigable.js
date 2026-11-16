// "Navigable" page demos.
"use strict";

// Show the current <title> so students can see it without looking at the browser tab.
document.getElementById("current-title").textContent = document.title;

// Build a heading outline from the real DOM: the same list a screen reader user
// gets from NVDA's Elements List (Insert+F7) or the VoiceOver rotor.
const button = document.getElementById("outline-button");
const outline = document.getElementById("outline");

button.addEventListener("click", () => {
    const expanded = button.getAttribute("aria-expanded") === "true";

    if (!expanded && outline.childElementCount === 0) {
        const list = document.createElement("ul");
        document.querySelectorAll("main h1, main h2, main h3").forEach(h => {
            if (outline.contains(h) || h.closest("dialog")) return;
            const level = Number(h.tagName[1]);
            const item = document.createElement("li");
            item.style.marginLeft = `${(level - 1) * 1.5}rem`;
            item.textContent = `Heading level ${level}: ${h.textContent.trim()}`;
            list.appendChild(item);
        });
        outline.appendChild(list);
    }

    button.setAttribute("aria-expanded", String(!expanded));
    button.textContent = expanded ? "Show heading outline" : "Hide heading outline";
    outline.hidden = expanded;
});
