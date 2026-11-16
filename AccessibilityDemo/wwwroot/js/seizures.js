// "Seizures and physical reactions" page demos.
"use strict";

// ----- Warning before (hypothetical) flashing content -----
const result = document.getElementById("flash-result");

function showResult(html) {
    result.innerHTML = html;
    result.hidden = false;
    result.focus();   // move focus to the new content so keyboard users find it
}

document.getElementById("flash-alt").addEventListener("click", () => showResult(
    "<h4>Text description</h4>" +
    "<p>A rock band plays on an outdoor stage at night. The crowd waves their hands while white strobe lights " +
    "pulse in time with the drums. The song ends with a burst of confetti.</p>"));

document.getElementById("flash-continue").addEventListener("click", () => showResult(
    "<p><strong>Demo note:</strong> no real video is included, because this site contains no flashing content. " +
    "In a real site the video would only start now, after the user chose to continue.</p>"));

// ----- Report the operating system's reduced-motion setting -----
const osStatus = document.getElementById("motion-os-status");
const query = window.matchMedia("(prefers-reduced-motion: reduce)");

function reportOsSetting() {
    osStatus.innerHTML = query.matches
        ? "Your operating system asks for <strong>reduced motion</strong>, so the animation below is already stopped."
        : "Your operating system does <strong>not</strong> ask for reduced motion, so the animation below is running.";
}
reportOsSetting();
query.addEventListener("change", reportOsSetting);

// ----- Pause button for this page's animation -----
const stage = document.getElementById("motion-stage");
const pause = document.getElementById("motion-pause");

pause.addEventListener("click", () => {
    const paused = stage.classList.toggle("is-paused");
    pause.textContent = paused ? "Play animation" : "Pause animation";
});
