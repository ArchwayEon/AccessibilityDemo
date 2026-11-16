// "Predictable" page: the intentionally BAD example.
// Navigating as soon as a <select> changes is a "change of context on input".
// Keyboard users pressing the arrow keys trigger "change" on every option, so
// they get sent away before they can reach the one they wanted.
"use strict";

document.getElementById("jump-bad").addEventListener("change", e => {
    if (e.target.value) window.location = e.target.value;
});
