
var validationElements = document.querySelectorAll('[data-val="true"]')

for (let element of validationElements) {
    element.addEventListener("keyup", function (e) {
        console.log(e.target.value.length)
        console.log(e.target.type)

        switch (e.target.type) {
            case 'text':
                textValidator(e.target, 2)
                break;
            case 'password':
                passwordValidator(e.target);
                break;
            case 'email':
                emailValidator(e.target)
                break;
            case 'checkbox':
                checkboxValidator(e.target)
                break;
            case 'number':
                numberValidator(e.target)
                break;
            case 'file':
                fileValidator(e.target)
                break;
        }
    })
}

function textValidator(target, minLength) {
    if (target.value.length < minLength)
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `${target.id} is invalid`
    else
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = ``
}

function passwordValidator(target) {
    const passwordRegex = /^(?=.*[A-Za-zÅÄÖåäö])(?=.*\d)[A-Za-zÅÄÖåäö\d@$!%*#?&]{8,}$/;

    if (!passwordRegex.test(target.value))
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `${target.id} is invalid`
    else 
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `Great ${target.id}!`
     
}

function emailValidator(target) {
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    if (!emailRegex.test(target.value))
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `${target.id} is invalid`
    else 
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `Valid ${target.id}!`
}

function checkboxValidator(target) {
    if (!target.checked)
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `${target.id} must be checked`
    else 
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `${target.id} is checked`
}

function numberValidator(target) {
    if (isNaN(target.value) || target.value === "")
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `${target.id} is invalid`
    else
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = ``
}

function fileValidator(target) {
    const fileExtensions = ["jpg", "jpeg", "png", "gif"];

    const fileInput = target.files[0];
    const fileExtension = fileInput.name.split(".").pop();

    if (!fileExtensions.includes(fileExtension))
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `${target.id} must be a valid image file: "jpg", "jpeg", "png" or "gif"`
    else 
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `Great ${target.id}!`
}


function footerPosition(element, scrollHeight, innerHeight) {
    try {
        const _element = document.querySelector(element)
        const isTallerThanScreen = scrollHeight > (innerHeight + _element.scrollHeight)

        _element.classList.toggle('position-bottom-fixed', !isTallerThanScreen)
    } catch { }
}

footerPosition('footer', document.body.scrollHeight, window.innerHeight)


function toggleMenu(attribute) {
    try {
        const toggleBtn = document.querySelector(attribute)
        toggleBtn.addEventListener('click', function () {
            const element = document.querySelector(toggleBtn.getAttribute('data-target'))

            if (!element.classList.contains('open-menu')) {
                element.classList.add('open-menu')
                toggleBtn.classList.add('btn-outline-dark')
                toggleBtn.classList.add('btn-toggle-white')
            }

            else {
                element.classList.remove('open-menu')
                toggleBtn.classList.remove('btn-outline-dark')
                toggleBtn.classList.remove('btn-toggle-white')
            }
        })
    } catch { }    
}
toggleMenu('[data-option="toggle"]')

