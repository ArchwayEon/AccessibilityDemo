// "Enough time" page demos: session timeout, pausable ticker, postponed notifications.
"use strict";

// ---------------------------------------------------------------------------
// 1. Simulated session timeout
// ---------------------------------------------------------------------------
const SESSION_SECONDS = 60;
const WARN_AT_SECONDS = 20;
const DRAFT_KEY = "enoughTimeDraft";

const display = document.getElementById("session-time");
const startButton = document.getElementById("session-start");
const offButton = document.getElementById("session-off");
const draft = document.getElementById("session-draft");
const expiredBox = document.getElementById("session-expired");
const reauthButton = document.getElementById("session-reauth");
const warning = document.getElementById("session-warning");

let remaining = SESSION_SECONDS;
let timer = null;

const format = s => `${Math.floor(s / 60)}:${String(s % 60).padStart(2, "0")}`;

function saveDraft() {
    try { sessionStorage.setItem(DRAFT_KEY, draft.value); } catch (e) { /* storage blocked */ }
}
function loadDraft() {
    try { return sessionStorage.getItem(DRAFT_KEY) ?? ""; } catch (e) { return ""; }
}

function startSession() {
    clearInterval(timer);
    remaining = SESSION_SECONDS;
    display.textContent = format(remaining);
    timer = setInterval(tick, 1000);
    announce("Session started. You have 1 minute.");
}

function tick() {
    remaining--;
    display.textContent = format(remaining);
    if (remaining === WARN_AT_SECONDS) {
        // A modal dialog gets the user's attention AND is announced by screen readers.
        warning.showModal();
    }
    if (remaining <= 0) expire();
}

function extend() {
    remaining = SESSION_SECONDS;
    display.textContent = format(remaining);
    if (warning.open) warning.close();
    announce("Your session has been extended. You have 1 more minute.");
}

function expire() {
    clearInterval(timer);
    timer = null;
    if (warning.open) warning.close();
    saveDraft();                      // the work is preserved...
    draft.value = "";                 // ...even though the "signed-out" page is cleared
    draft.disabled = true;
    display.textContent = "Expired";
    expiredBox.hidden = false;
    expiredBox.focus();
}

function reauthenticate() {
    expiredBox.hidden = true;
    draft.disabled = false;
    draft.value = loadDraft();        // restore exactly what the user had typed
    display.textContent = format(SESSION_SECONDS);
    draft.focus();
    announce("You are signed in again. Your draft has been restored.");
}

startButton.addEventListener("click", startSession);
document.getElementById("session-extend").addEventListener("click", extend);
document.getElementById("session-dismiss").addEventListener("click", expire);
reauthButton.addEventListener("click", reauthenticate);

// Pressing Esc on the warning counts as "I'm still here".
warning.addEventListener("cancel", e => { e.preventDefault(); extend(); });

// Let users turn the time limit off entirely.
offButton.addEventListener("click", () => {
    const off = offButton.getAttribute("aria-pressed") !== "true";
    offButton.setAttribute("aria-pressed", String(off));
    startButton.disabled = off;
    if (off) {
        clearInterval(timer);
        timer = null;
        display.textContent = "No time limit";
        announce("The time limit is turned off.");
    } else {
        display.textContent = format(SESSION_SECONDS);
        announce("The time limit is turned on. Press Start the session to begin.");
    }
});

draft.value = loadDraft();
draft.addEventListener("input", saveDraft);

// ---------------------------------------------------------------------------
// 2. Pausable ticker
// ---------------------------------------------------------------------------
const ticker = document.getElementById("ticker");
const tickerButton = document.getElementById("ticker-toggle");

tickerButton.addEventListener("click", () => {
    const paused = ticker.classList.toggle("is-paused");
    // Here the label itself changes, so we do NOT also use aria-pressed.
    tickerButton.textContent = paused ? "Play headlines" : "Pause headlines";
});

// ---------------------------------------------------------------------------
// 3. Postponing interruptions
// ---------------------------------------------------------------------------
const postponeButton = document.getElementById("notify-postpone");
const notifyStatus = document.getElementById("notify-status");
const toast = document.getElementById("notify-toast");
const toastText = document.getElementById("notify-text");
let queued = 0;

function showToast(text) {
    toastText.textContent = text;
    toast.hidden = false;
}

document.getElementById("notify-send").addEventListener("click", () => {
    if (postponeButton.getAttribute("aria-pressed") === "true") {
        queued++;
        notifyStatus.textContent = `${queued} notification${queued === 1 ? "" : "s"} postponed. They will appear when you turn off postponing.`;
    } else {
        showToast("New message from your instructor: Lab 4 has been posted.");
        notifyStatus.textContent = "New notification: Lab 4 has been posted.";
    }
});

postponeButton.addEventListener("click", () => {
    const on = postponeButton.getAttribute("aria-pressed") !== "true";
    postponeButton.setAttribute("aria-pressed", String(on));
    if (!on && queued > 0) {
        showToast(`You have ${queued} postponed notification${queued === 1 ? "" : "s"}.`);
        notifyStatus.textContent = toastText.textContent;
        queued = 0;
    } else {
        notifyStatus.textContent = on ? "Notifications are postponed." : "Notifications are on.";
    }
});

document.getElementById("notify-close").addEventListener("click", () => {
    toast.hidden = true;
    notifyStatus.textContent = "Notification dismissed.";
    document.getElementById("notify-send").focus();
});
