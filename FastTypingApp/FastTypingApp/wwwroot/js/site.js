let exercise_text = document.getElementById("exercise_text");
let passed_text = document.getElementById("passed_text");
let error_num = document.getElementById("error_num");
let timer = document.getElementById("timer");
let errors = 0;
let time = 0;
let end = true;
let source_text_length = 0;

let errorNumHiddenInput = document.getElementById("errorNumHiddenInput");
let accuracyHiddenInput = document.getElementById("accuracyHiddenInput");
let timeHiddenInput = document.getElementById("timeHiddenInput");
let succefullHiddenInput = document.getElementById("succefullHiddenInput");

let start_button = document.getElementById("startButton");
let save_button = document.getElementById("buttonSave");

let accuracy_span = document.getElementById("accuracy_span");
let max_time_span = document.getElementById("MaxTime");
let max_errors_span = document.getElementById("MaxErrors");
let exercise_params = document.getElementById("exercise_params");

let all_clicked = 0;
let accuracy = 0;

function check_text(char) {
    console.log(char);
    all_clicked++;
    if (char === exercise_text.innerText[0]) {
        if (char != " ") {
            passed_text.innerText += exercise_text.innerText[0];
            exercise_text.innerText = exercise_text.innerText.substring(1);
        }
        else {
            passed_text.innerText += " ";
            exercise_text.innerText = exercise_text.innerText.trim();
        }

        unHighlightButtons(char);
        if (exercise_text.innerText.length === 0) {
            stop();
            end = true;
        }
        highlightButtons(exercise_text.innerText[0]);

        if (passed_text.innerText.length > 10 && exercise_text.innerText.length > 20) {
            passed_text.innerText = passed_text.innerText.substring(1);
        }
    } else {
        errors++;
    }
    error_num.innerText = errors.toString();
    accuracy = 100 - errors / all_clicked * 100;
    accuracy_span.innerText = accuracy.toString() + "%";
}

function changeKeyboard() {
    let lang = document.querySelector('input[name="lang"]:checked').value;
    let eng_keyboard = document.getElementById("eng_keyboard");
    let rus_keyboard = document.getElementById("rus_keyboard");
    if (lang === 'rus') {
        eng_keyboard.classList.remove("active_keyboard");
        rus_keyboard.classList.add("active_keyboard");
    } else if (lang === "eng") {
        rus_keyboard.classList.remove("active_keyboard");
        eng_keyboard.classList.add("active_keyboard");
    }
}

function myKeyPress(e) {
    let lang = document.querySelector('input[name="lang"]:checked').value;
    if (!end) {
        let keynum;

        if (window.event) { // IE
            keynum = e.keyCode;
        } else if (e.which) { // Netscape/Firefox/Opera
            keynum = e.which;
        }

        // console.log(keynum);
        // console.log(String.fromCharCode(keynum));
        check_text(String.fromCharCode(keynum));
    }
}


function setTimer() {
    let timerId = setTimeout(function tick() {
        if (!end) {
            timerId = setTimeout(tick, 25);
            time += 25;
            let time_str = (time / 1000).toFixed(2).toString();
            timer.innerText = time_str;
        }
    }, 25);
}

function start() {
    end = false;
    exercise_params.style.display = "block";
    start_button.style.display = "none";
    source_text_length = exercise_text.innerText.length;
    highlightButtons(exercise_text.innerText[0]);
    setTimer();
    error_num.innerText = errors.toString();
    accuracy_span.innerText = "100%";
}

function stop() {
    if (error_num.innerText) {
        errorNumHiddenInput.value = error_num.innerText;
    } else {
        errorNumHiddenInput.value = 0;
    }
    accuracyHiddenInput.value = accuracy.toString().replace(".", ",");
    if (timer.innerText) {
        timeHiddenInput.value = timer.innerText.toString().replace(".", ",");
    }
    else {
        timeHiddenInput.value = 0;
    }

    if (parseInt(timer.innerText) <= parseFloat(max_time_span.innerText) && errors <= parseInt(max_errors_span.innerText)) {
        succefullHiddenInput.value = "true";
    } else {
        succefullHiddenInput.value = "false";
    }

    end = true;
    save_button.style.display = "block";
}