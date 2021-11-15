var carousel = document.getElementById("carousel-1")
var carousel_inner = document.getElementsByClassName("carousel-inner")[0]
var carousel_indicators = document.getElementsByClassName("carousel-indicators")[0]

function preloadFiles(files) {
    files = eval(files);
    for (let i = 0; i < carousel_inner.childElementCount; i++) {
        let upload_file_path = files[i];
        console.log(upload_file_path);
        let file_input = carousel_inner.children[i].getElementsByTagName('input')[0];
        let ds = new DataTransfer();
        let fn_splitted = upload_file_path.split("/");
        let filename = fn_splitted[fn_splitted.length - 1].split(".");
        let name = filename[0];
        let extension = filename[1];
        let upload_file = new File([], name, { type: `image/${extension}` });
        ds.items.add(upload_file);
        file_input.files = ds.files;
    }
}


function addToCarousel() {
    let slide_img = document.createElement("img");
    slide_img.classList.add("w-50", "d-block", "m-auto");
    slide_img.src = "/static/bungaloes/image-missing-icon.png";
    slide_img.alt = "Slide Image";

    let slide_input = document.createElement("input");
    slide_input.type = 'file';
    slide_input.name = 'bungaloImages';
    slide_input.classList.add("form-control");

    let slide_btn = document.createElement("button");
    slide_btn.setAttribute('onclick', `deleteThis(${carousel_inner.childElementCount})`);
    slide_btn.classList.add('btn', 'w-100', 'btn-danger');
    slide_btn.innerHTML = 'Удалить';

    let slide = document.createElement("div");
    slide.classList.add("carousel-item");

    slide.appendChild(slide_img);
    slide.appendChild(slide_input);
    slide.appendChild(slide_btn);
    carousel_inner.appendChild(slide);

    //var file_input = carousel_item.getElementById(`b${carousel_inner.childElementCount - 1}`);
    slide_input.onchange = showPicture;
    carousel_indicators.innerHTML = carousel_indicators.innerHTML + `<li data-target="#carousel-1" data-slide-to="${carousel_indicators.childElementCount}"></li>`
    checkForActive();
    DEBUG_printFileList();
}

function checkForActive() {
    let active_slide = carousel_inner.getElementsByClassName('active')[0];
    if (active_slide) {
        active_slide.classList.remove('active');
    }
    carousel_inner.children[carousel_inner.childElementCount - 1].classList.add('active');


    let active_indicator = carousel_indicators.getElementsByClassName('active')[0];
    if (active_indicator) {
        active_indicator.classList.remove('active');
    }
    carousel_indicators.children[carousel_inner.childElementCount - 1].classList.add('active');
}

function checkForNames() {
    
    for (let i = 0; i < carousel_inner.children.length - 1; i++) {
        var slide = carousel_inner.children[i];
        console.log(slide);
        slide.children[2].removeAttribute('onclick');
        slide.children[2].setAttribute('onclick', `deleteThis(${i})`);
        var indicator = carousel_indicators.children[i];
        indicator.removeAttribute('data-slide-to');
        indicator.setAttribute('data-slide-to', String(i));
    }
}


function deleteThis(id) {
    var slide_to_remove = carousel_inner.children[id];
    var indicator_to_remove = carousel_indicators.children[id];
    carousel_inner.removeChild(slide_to_remove);
    carousel_indicators.removeChild(indicator_to_remove);
    checkForNames();
    checkForActive();
}

function showPicture(event) {
    var target = event.target || window.event.srcElement,
        files = target.files;
    var slide = target.parentNode;
    console.log(slide);
    var image = slide.getElementsByTagName('img')[0];
    console.log(image);

    // FileReader support
    if (FileReader && files && files.length) {
        var fr = new FileReader();
        fr.onload = function () {
            image.src = fr.result;
        }
        fr.readAsDataURL(files[0]);
    }
}

function setOnChanges() {
    for (let i = 0; i < carousel_inner.childElementCount; i++) {
        carousel_inner.children[i].getElementsByTagName('input')[0].onchange = showPicture;
    }
}

function DEBUG_printFileList() {
    for (let i; i < carousel_indicators.childElementCount; i++) {
        let slide = carousel_inner.children[i];
        console.log(slide);
        let input = slide.children[1];
        console.log(input.files);
    }
}


function RatingStars(id_rating_area, rating_value) {
    let rating_area = document.getElementById(id_rating_area);
}