let fontPreview;
let fontOptionSelect;
let fontSizeSelect;
let selectedFontSizeId;
let selectedFontOptionsId;

window.addEventListener('load', (event) => {
    setVariables();
    setEventListeners();
});

function setVariables() {
    fontPreview = document.getElementById("fontPreview");
    fontOptionSelect = document.getElementById("fontOptions");
    fontSizeSelect = document.getElementById("fontSizes");
    selectedFontSizeId = document.getElementById("selectedFontSizeId");
    selectedFontOptionsId = document.getElementById("selectedFontOptionsId");
}

function setEventListeners() {
    fontOptionSelect.addEventListener("change", setFont);
    fontSizeSelect.addEventListener("change", setSize);
}

function setFont() {
    var selected = fontOptionSelect.options[fontOptionSelect.selectedIndex];
    let family = selected.value.substring(0, selected.value.indexOf('-') - 1);
    let id = selected.value.substring(selected.value.indexOf('-') + 2);

    fontPreview.style.fontFamily = family;
    selectedFontOptionsId.value = id;
}

function setSize() {
    var selected = fontSizeSelect.options[fontSizeSelect.selectedIndex];
    fontPreview.style.fontSize = selected.text;
    selectedFontSizeId.value = selected.value;
}