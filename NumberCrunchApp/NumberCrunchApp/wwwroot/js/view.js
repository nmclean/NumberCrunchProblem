const container = document.getElementById("group-view");
const styleSelector = document.getElementById("style-selector");

applyStyle();

styleSelector.addEventListener("change", applyStyle);

function applyStyle() {
    for (const option of styleSelector.options) {
        container.classList.remove(option.value);
    }
    container.classList.add(styleSelector.value);
}