# Accessibility Principles Demo

**CSCI 3110 · ASP.NET Core MVC**

This web application demonstrates each of the W3C [Accessibility Principles](https://www.w3.org/WAI/fundamentals/accessibility-principles/)
from the course document *1-CSCI3110-AccessibilityPrinciples.docx*. Every principle has its own page with:

1. **What the W3C says**: the bullet points from the principles document.
2. **Working examples** you can try with a keyboard, a screen reader, zoom, and testing tools.
3. **Do (✓) and Don't (✗) comparisons**, often with the HTML shown underneath.

The site itself is meant to be an example of accessible design. Read the Razor views, CSS, and JavaScript as
much as you use the pages.

---

## Contents

- [Running the app](#running-the-app)
- [How to use the demo](#how-to-use-the-demo)
- [The principles and where to find them](#the-principles-and-where-to-find-them)
- [Accessibility features on every page](#accessibility-features-on-every-page)
- [Project structure](#project-structure)
- [How the MVC code works](#how-the-mvc-code-works)
- [Testing accessibility yourself](#testing-accessibility-yourself)
- [Adding a sample video](#adding-a-sample-video)
- [Exercises](#exercises)
- [References](#references)

---

## Running the app

### Requirements

- [Visual Studio Code](https://code.visualstudio.com/)
- [.NET 10 SDK](https://dotnet.microsoft.com/download). Check it is installed by running `dotnet --version`
  in a terminal; it should print `10.x.x`.
- The VS Code extension **C# Dev Kit** (publisher: Microsoft). Installing it also installs the **C#** and
  **.NET Install Tool** extensions.

No database or extra NuGet packages are needed.

**Optional but recommended:** the **axe Accessibility Linter** extension (publisher: Deque Systems). It
underlines common accessibility mistakes in `.cshtml` and `.html` files while you type.

### Open the project

1. In VS Code, choose **File → Open Folder…** and select the `AccessibilityDemo` folder that contains this README
   (the folder with `AccessibilityDemo.slnx` in it).
2. If asked *"Do you trust the authors of the files in this folder?"*, choose **Yes, I trust the authors**.
3. Wait a few seconds for C# Dev Kit to load. The **Solution Explorer** appears at the bottom of the
   Explorer side bar.

### Run it from the terminal (recommended)

Open the integrated terminal with **Terminal → New Terminal** (<kbd>Ctrl</kbd>+<kbd>`</kbd>) and run:

```bash
dotnet watch --project AccessibilityDemo
```

`dotnet watch` builds the app, opens your browser at **http://localhost:5051**, and **reloads automatically**
when you save a `.cshtml`, `.css`, `.js`, or `.cs` file. That makes it easy to change an example and see the result.
Press <kbd>Ctrl</kbd>+<kbd>C</kbd> in the terminal to stop the app.

If you'd rather not use hot reload, `dotnet run --project AccessibilityDemo` also works; open
http://localhost:5051 yourself.

### Run it with the debugger

1. Open any `.cs` file (for example `Controllers/PrinciplesController.cs`).
2. Press **F5**. If VS Code asks you to choose a debugger, pick **C#**, then the **AccessibilityDemo** project,
   then the **http** launch profile.
3. Set a breakpoint by clicking to the left of a line number, for example inside the `Distinguishable` action,
   then use the contrast checker to hit it.

Press <kbd>Shift</kbd>+<kbd>F5</kbd> to stop debugging.

### Useful VS Code shortcuts for this project

| Shortcut | What it does |
|---|---|
| <kbd>Ctrl</kbd>+<kbd>P</kbd> | Quick Open: type a file name such as `Keyboard.cshtml` or `site.css` |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>F</kbd> | Search every file, e.g. for `aria-describedby` or `role="status"` |
| <kbd>F12</kbd> on a class or method | Go to its definition, e.g. `ColorContrast.Ratio` |
| <kbd>Ctrl</kbd>+<kbd>`</kbd> | Show or hide the terminal |

> **Port already in use?** If you see *"address already in use"*, an earlier copy of the app is still
> running. Stop it with <kbd>Ctrl</kbd>+<kbd>C</kbd> in the terminal where it is running (or close that
> terminal with the trash-can icon) and try again.

---

## How to use the demo

1. Start on the **Home** page. It groups the 13 principles under the four WCAG principles,
   **POUR**: **P**erceivable, **O**perable, **U**nderstandable, **R**obust.
2. Open a principle. Read the W3C summary at the top, then work through the examples.
3. **Use only the keyboard** for at least one full pass: <kbd>Tab</kbd>, <kbd>Shift</kbd>+<kbd>Tab</kbd>,
   <kbd>Enter</kbd>, <kbd>Space</kbd>, arrow keys, and <kbd>Esc</kbd>.
4. Open the matching view in VS Code to see how each example is built: press <kbd>Ctrl</kbd>+<kbd>P</kbd>
   and type the view name from the table below (for example `Keyboard.cshtml`). With `dotnet watch`
   running, save a change and the browser reloads to show it.

> **⚠ Intentionally broken examples.** The boxes marked **✗ Don't** that say *(live example)* are
> deliberately inaccessible so you can experience the problem. Accessibility checkers *should* complain about
> some of them. Everything outside those boxes is meant to be accessible.

---

## The principles and where to find them

| # | Principle (from the document) | URL | Try this | Source files |
|---|---|---|---|---|
| 1 | Text Alternatives for Non-text Content | `/Principles/TextAlternatives` | Compare good and bad `alt` text, a nameless icon button, a chart with a data table, labels vs. placeholders | `TextAlternatives.cshtml` |
| 2 | Captions and Other Alternatives for Multimedia | `/Principles/Captions` | Read the transcript; see `<track>` captions and descriptions ([add a video](#adding-a-sample-video)) | `Captions.cshtml`, `wwwroot/media/*.vtt` |
| 3 | Content can be Presented in Different Ways | `/Principles/Adaptable` | Real headings, lists, and a data table with `scope`; try reader view, dark mode, and "No Style" | `Adaptable.cshtml` |
| 4 | Content is Easier to See and Hear | `/Principles/Distinguishable` | **Contrast checker** (server-side WCAG formula), color-only status, text-spacing toggle, image of text vs. real text | `Distinguishable.cshtml`, `Infrastructure/ColorContrast.cs`, `js/distinguishable.js` |
| 5 | Functionality is Available from a Keyboard | `/Principles/Keyboard` | Try to <kbd>Tab</kbd> to the fake `<span>` button; open the native `<dialog>` and leave with <kbd>Esc</kbd> | `Keyboard.cshtml`, `js/keyboard.js` |
| 6 | Users Have Enough Time | `/Principles/EnoughTime` | 60-second **session timeout** with warning, extend, turn-off, and draft recovery; pausable ticker; postponed notifications | `EnoughTime.cshtml`, `js/enough-time.js` |
| 7 | Content Does Not Cause Seizures and Physical Reactions | `/Principles/Seizures` | Content warning pattern; pause the animation; the site-wide **Reduce motion** button; OS reduced-motion detection | `Seizures.cshtml`, `js/seizures.js`, `css/site.css` |
| 8 | Users Can Easily Navigate, Find Content, and Know Where They Are | `/Principles/Navigable` | Skip link, generated heading outline, breadcrumbs, focus visible vs. removed, link text | `Navigable.cshtml`, `js/navigable.js`, `_Layout.cshtml` |
| 9 | Users Can Use Different Input Modalities | `/Principles/InputModalities` | Reorder by drag **or** Move up/down buttons; delete with **Undo**; label-in-name; target size | `InputModalities.cshtml`, `js/input-modalities.js` |
| 10 | Text is Readable and Understandable | `/Principles/Readable` | `lang` on a Spanish quote and French phrase; `<abbr>`, `<dfn>`, glossary; plain-language rewrite | `Readable.cshtml` |
| 11 | Content Appears and Operates in Predictable Ways | `/Principles/Predictable` | Arrow through the bad `<select>` (it navigates away!) vs. the one with a **Go** button | `Predictable.cshtml`, `js/predictable.js`, `PrinciplesController.Go` |
| 12 | Users are Helped to Avoid and Correct Mistakes | `/Principles/InputAssistance` | Submit the form empty, fix the errors, **review** your answers, confirm, then **cancel** | `InputAssistance.cshtml`, `Review.cshtml`, `Registered.cshtml`, `Models/ViewModels.cs` |
| 13 | Content is Compatible with Current and Future User Tools | `/Principles/Compatible` | Custom **switch** (`role="switch"`), ARIA **tabs** with arrow keys, `role="status"` message; inspect the accessibility tree | `Compatible.cshtml`, `js/compatible.js` |

### The intentionally broken examples

| Page | Broken example | Principle it violates |
|---|---|---|
| Text alternatives | Icon button with no accessible name | Name for UI components |
| Text alternatives | Input with only a placeholder, no label | Labels for form controls |
| Easier to see & hear | Red/green dots with no text | Use of color |
| Keyboard access | "Save draft" `<span>` with a click handler | Keyboard |
| Navigation | Links with the focus outline removed | Focus visible |
| Navigation | "click here" / "Read more" links | Link purpose |
| Touch, voice & gestures | Button showing "Search" whose `aria-label` is "Find items in catalog" | Label in name |
| Touch, voice & gestures | Tiny "Prev"/"Next" buttons touching each other | Target size |
| Predictable behavior | `<select>` that navigates on `change` | On input |

---

## Accessibility features on every page

These are in the shared layout ([`Views/Shared/_Layout.cshtml`](AccessibilityDemo/Views/Shared/_Layout.cshtml)),
so every page gets them automatically:

| Feature | Why |
|---|---|
| `<html lang="en">` | Screen readers use the right pronunciation (Readable) |
| Unique `<title>`, specific part first, e.g. *"Keyboard… – Accessibility Principles Demo"* | First thing announced; identifies browser tabs (Navigable) |
| Viewport meta **without** `maximum-scale` / `user-scalable=no` | Pinch-zoom keeps working (Distinguishable) |
| **Skip to main content** link (press <kbd>Tab</kbd> once) | Bypass the header and menu (Navigable) |
| `<header>`, `<nav aria-label>`, `<main>`, `<footer>` landmarks | Jump between regions with a screen reader (Adaptable) |
| Same navigation, same order, every page, generated from one list | Consistent navigation and labels (Predictable) |
| `aria-current="page"` plus bold text and a bar on the current menu item | Where am I? Not shown by color alone (Navigable) |
| Breadcrumb trail as an ordered list in `<nav aria-label="Breadcrumb">` | Location within the site (Navigable) |
| Search box + site map + menu + previous/next links | More than one way to find pages (Navigable) |
| Strong `:focus-visible` outline | Focus is always visible (Navigable) |
| One `<h1>` per page, headings in order | Structure (Adaptable) |
| Hidden live region (`#announcer`) and `window.announce()` | Status messages for screen readers (Compatible) |
| Friendly 404 page with search and site map | Helps users recover (Avoid mistakes) |

And in [`wwwroot/css/site.css`](AccessibilityDemo/wwwroot/css/site.css):

- All sizes in `rem`/`em`; breakpoints in `em`, so zooming text also switches layouts.
- Every text color meets **4.5:1** contrast (the values are noted in comments).
- **Dark mode** via `prefers-color-scheme`.
- **Reduced motion** via `prefers-reduced-motion` **and** a site-wide toggle button.
- **Windows High Contrast** support via `@media (forced-colors: active)`.
- Buttons and menu links are at least **44 × 44 px**.
- Layout reflows to one column at 320 px wide (the WCAG 400% zoom test) with no horizontal scrolling.

---

## Project structure

```
AccessibilityDemo/                       ← solution folder (this README)
├── AccessibilityDemo.slnx
├── 1-CSCI3110-AccessibilityPrinciples.docx
└── AccessibilityDemo/                   ← the MVC project
    ├── Program.cs                       ← middleware, routing, friendly status-code pages
    ├── Controllers/
    │   ├── HomeController.cs            ← Home, Search, SiteMap, Status (404), Error
    │   └── PrinciplesController.cs      ← one action per principle + form workflow
    ├── Models/
    │   ├── Principle.cs                 ← Principle record + WcagGroup (POUR) enum
    │   ├── PrincipleCatalog.cs          ← the 13 principles (text from the course document)
    │   └── ViewModels.cs                ← Search, Contrast, and Registration view models
    ├── Infrastructure/
    │   ├── ColorContrast.cs             ← WCAG contrast-ratio formula in C#
    │   └── CodeSampleTagHelper.cs       ← <code-sample> tag helper for showing HTML
    ├── Views/
    │   ├── Shared/
    │   │   ├── _Layout.cshtml           ← skip link, landmarks, nav, title, live region
    │   │   ├── _Breadcrumbs.cshtml
    │   │   ├── _PrincipleIntro.cshtml   ← h1 + "What the W3C principles say"
    │   │   ├── _PrinciplePager.cshtml   ← previous / next links
    │   │   └── Error.cshtml
    │   ├── Home/                        ← Index, Search, SiteMap, Status
    │   └── Principles/                  ← one view per principle (+ Review, Registered)
    └── wwwroot/
        ├── css/site.css
        ├── js/site.js                   ← announce(), reduce-motion toggle, error-summary focus
        ├── js/*.js                      ← one small script per demo page
        ├── images/*.svg
        └── media/*.vtt                  ← captions and text descriptions
```

There is **no Bootstrap or jQuery**. All the markup, CSS, and JavaScript is hand-written so you can read every line.

---

## How the MVC code works

### One list drives the whole site

[`Models/PrincipleCatalog.cs`](AccessibilityDemo/Models/PrincipleCatalog.cs) holds the 13 principles with the
text from the course document. The layout's navigation menu, the home page cards, the site map, the search
results, the breadcrumbs, and the previous/next links are all generated from it. Because there is one source of
truth, the navigation *can't* become inconsistent between pages (the **Predictable** principle).

### Controller → layout

[`PrinciplesController`](AccessibilityDemo/Controllers/PrinciplesController.cs) overrides `OnActionExecuting`
to look up the principle whose name matches the current action and puts it in `ViewData["Principle"]`. The
layout uses it to build the `<title>`, breadcrumbs, intro, and pager, so each principle view only contains its
own demos.

### `<code-sample>` tag helper

Showing HTML inside HTML normally means escaping every `<` as `&lt;`. The custom tag helper in
[`Infrastructure/CodeSampleTagHelper.cs`](AccessibilityDemo/Infrastructure/CodeSampleTagHelper.cs) does that for you:

```cshtml
<code-sample kind="good" label="Good alt text">
    <img src="campus.svg" alt="Cartoon of a brick building">
</code-sample>
```

It renders a `<pre tabindex="0" aria-label="Good alt text">`. The listing is focusable because it can scroll
sideways, and keyboard users have to be able to reach scrollable areas.

### Contrast checker (server-side)

The **Distinguishable** page's checker is an ordinary `GET` form bound to `ContrastViewModel`. The controller
calls [`ColorContrast.Ratio()`](AccessibilityDemo/Infrastructure/ColorContrast.cs), which implements the WCAG
relative-luminance formula. It works without JavaScript, and results can be bookmarked.

### Accessible form workflow (Post/Redirect/Get + review step)

```
GET  /Principles/InputAssistance        → empty form
POST /Principles/InputAssistance        → invalid?  redisplay with error summary
                                         → valid?    show Review.cshtml (nothing saved yet)
POST /Principles/InputAssistance?command=edit → back to form, answers kept, no errors
POST /Principles/Confirm                → "save", then 302 redirect (PRG)
GET  /Principles/Registered             → confirmation with "Cancel my registration"
POST /Principles/CancelRegistration     → reverse the submission
```

Things to look at in [`InputAssistance.cshtml`](AccessibilityDemo/Views/Principles/InputAssistance.cshtml):

- Data-annotation messages in [`ViewModels.cs`](AccessibilityDemo/Models/ViewModels.cs) explain **how to fix**
  the problem ("…the letter E followed by 8 digits, for example E00123456").
- The **error summary** links to each field, and `site.js` moves focus to it.
- The page `<title>` starts with **"Error:"** when validation fails.
- `aria-invalid` and `aria-describedby` (hint + error) are added only when needed. The helper functions return
  `null`, and Razor then leaves the attribute out completely.
- `novalidate` turns off the browser's inconsistent error bubbles in favor of our own messages.
- Radio buttons are grouped with `<fieldset>`/`<legend>`.

---

## Testing accessibility yourself

Automated tools are useful, but **they can't find most problems**. When this project was scanned with
axe-core (WCAG 2.2 AA + best-practice rules), it flagged only **one** of the nine intentionally broken examples:
the nameless icon button. It missed the placeholder-only input, the clickable `<span>`, the mismatched voice
label, the color-only status, and the others. You have to test manually too.

### 1. Keyboard (5 minutes)

- [ ] Unplug or ignore your mouse.
- [ ] Press <kbd>Tab</kbd> on page load. Does a skip link appear?
- [ ] Can you reach and operate **everything**? Is the focus outline always visible?
- [ ] Does focus move in a logical order? Can you always get *out* of a component?

### 2. Zoom and reflow

- [ ] Zoom to 200% and then 400% (<kbd>Ctrl</kbd>+<kbd>+</kbd>). Is anything cut off? Do you have to scroll sideways?
- [ ] Turn on dark mode and Windows **Contrast themes** (Settings → Accessibility → Contrast themes).

### 3. Screen reader

| Platform | Screen reader | Start / stop | Useful keys |
|---|---|---|---|
| Windows | [NVDA](https://www.nvaccess.org/) (free) | <kbd>Ctrl</kbd>+<kbd>Alt</kbd>+<kbd>N</kbd> | <kbd>H</kbd> next heading, <kbd>D</kbd> next landmark, <kbd>Insert</kbd>+<kbd>F7</kbd> elements list |
| Windows | Narrator (built in) | <kbd>Win</kbd>+<kbd>Ctrl</kbd>+<kbd>Enter</kbd> | <kbd>H</kbd> next heading (scan mode: <kbd>Caps Lock</kbd>+<kbd>Space</kbd>) |
| macOS / iOS | VoiceOver (built in) | <kbd>Cmd</kbd>+<kbd>F5</kbd> | <kbd>Ctrl</kbd>+<kbd>Option</kbd>+<kbd>U</kbd> rotor |

### 4. Automated tools

- **axe DevTools** browser extension (Deque)
- **WAVE** browser extension (WebAIM)
- **Lighthouse** → *Accessibility* (built into Chrome/Edge DevTools)
- **Accessibility Insights for Web** (Microsoft), which includes a guided manual assessment
- **[W3C Nu HTML Checker](https://validator.w3.org/nu/)**: paste the page source (<kbd>Ctrl</kbd>+<kbd>U</kbd>) into *text input*

### 5. The accessibility tree

Open DevTools (<kbd>F12</kbd>) → Elements → **Accessibility** pane (Chrome/Edge) or the **Accessibility** tab
(Firefox). Select the switch on the *Compatible* page and toggle it to watch its role, name, and checked state.

---

## Adding a sample video

No video is included (to keep the project small). To see the captioned player on the **Captions** page:

1. Copy any short MP4 to `AccessibilityDemo/wwwroot/media/sample.mp4`.
2. Edit the timings in `wwwroot/media/sample.en.vtt` (captions) and `sample.descriptions.en.vtt`
   (text descriptions) to match your video.
3. Reload the page. The controller detects the file and shows the `<video>` element with both `<track>`s.

---

## Exercises

1. **Fix the broken examples.** Make each *(live example)* in the table above accessible, then re-test
   with the keyboard and a screen reader.
2. **Audit the default template.** In the VS Code terminal, create a new project with
   `dotnet new mvc -o TemplateAudit`, run it with `dotnet watch --project TemplateAudit`,
   and audit it with axe and the keyboard checklist. What would you change?
3. **Extend the form.** Add a required phone number field with `type="tel"`, `autocomplete="tel"`, a hint,
   and a helpful validation message. Make sure it appears in the error summary.
4. **Build a component.** Add an accessible *accordion* or *disclosure navigation menu* to the Compatible page,
   following the [ARIA Authoring Practices Guide](https://www.w3.org/WAI/ARIA/apg/patterns/).
5. **Write the tests.** Add an xUnit project that tests `ColorContrast.Ratio()` (black on white should be
   21:1; `#777777` on white should be about 4.48:1). From the VS Code terminal in the solution folder:
   ```bash
   dotnet new xunit -o AccessibilityDemo.Tests
   dotnet sln add AccessibilityDemo.Tests
   dotnet add AccessibilityDemo.Tests reference AccessibilityDemo
   dotnet test
   ```
   The tests also appear in VS Code's **Testing** panel (the flask icon in the Activity Bar).
6. **Break it on purpose.** Remove the skip link, set `outline: none` on everything, and add
   `user-scalable=no`. Which tools notice? Which don't?

---

## References

- W3C WAI, [Accessibility Principles](https://www.w3.org/WAI/fundamentals/accessibility-principles/), the source of the course document
- [WCAG 2.2 Quick Reference (How to Meet WCAG)](https://www.w3.org/WAI/WCAG22/quickref/)
- [ARIA Authoring Practices Guide (APG)](https://www.w3.org/WAI/ARIA/apg/)
- [WebAIM: Contrast Checker](https://webaim.org/resources/contrastchecker/) and [WebAIM articles](https://webaim.org/articles/)
- [MDN: Accessibility](https://developer.mozilla.org/en-US/docs/Web/Accessibility)
- [Microsoft Learn: ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core/mvc/overview)
