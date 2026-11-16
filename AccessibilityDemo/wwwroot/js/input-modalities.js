// "Input modalities" page demos.
"use strict";

// ---------------------------------------------------------------------------
// 1. Reorderable list: drag and drop, PLUS Move up / Move down buttons
// ---------------------------------------------------------------------------
const rankList = document.getElementById("rank-list");

function announcePosition(item) {
    const items = [...rankList.children];
    announce(`${item.dataset.name} moved to position ${items.indexOf(item) + 1} of ${items.length}.`);
}

// Single-pointer alternative: works with tap, click, keyboard, voice, and switch access.
rankList.addEventListener("click", e => {
    const button = e.target.closest("button[data-move]");
    if (!button) return;

    const item = button.closest("li");
    const up = button.dataset.move === "up";
    const sibling = up ? item.previousElementSibling : item.nextElementSibling;

    if (!sibling) {
        announce(`${item.dataset.name} is already ${up ? "first" : "last"}.`);
        return;
    }
    if (up) rankList.insertBefore(item, sibling);
    else rankList.insertBefore(sibling, item);

    button.focus();             // keep focus on the button the user just pressed
    announcePosition(item);
});

// Drag and drop for mouse users who prefer it.
let dragged = null;

rankList.addEventListener("dragstart", e => {
    dragged = e.target.closest("li");
    dragged.classList.add("dragging");
    e.dataTransfer.effectAllowed = "move";
});

rankList.addEventListener("dragover", e => {
    e.preventDefault();
    const over = e.target.closest("li");
    if (!over || over === dragged) return;
    const rect = over.getBoundingClientRect();
    const after = e.clientY > rect.top + rect.height / 2;
    rankList.insertBefore(dragged, after ? over.nextElementSibling : over);
});

rankList.addEventListener("dragend", () => {
    dragged.classList.remove("dragging");
    announcePosition(dragged);
    dragged = null;
});

// ---------------------------------------------------------------------------
// 2. Delete with undo
// ---------------------------------------------------------------------------
const fileList = document.getElementById("file-list");
const toast = document.getElementById("undo-toast");
const toastText = document.getElementById("undo-text");
const undoButton = document.getElementById("undo-button");
let lastDeleted = null;   // { item, nextSibling }

// Note: "click" fires on release, so sliding a finger or pointer off the
// button before letting go cancels the action (pointer cancellation).
fileList.addEventListener("click", e => {
    const button = e.target.closest("button[data-delete]");
    if (!button) return;

    const item = button.closest("li");
    const name = item.querySelector("span").textContent;
    lastDeleted = { item, nextSibling: item.nextElementSibling };
    item.remove();

    toastText.textContent = `Deleted ${name}.`;
    toast.hidden = false;
    undoButton.focus();
    announce(`${name} deleted. Press Undo to restore it.`);
});

undoButton.addEventListener("click", () => {
    if (!lastDeleted) return;
    const { item, nextSibling } = lastDeleted;
    fileList.insertBefore(item, nextSibling && nextSibling.isConnected ? nextSibling : null);
    lastDeleted = null;
    toast.hidden = true;
    item.querySelector("button").focus();
    announce(`${item.querySelector("span").textContent} restored.`);
});
