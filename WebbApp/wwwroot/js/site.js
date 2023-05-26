let validationElements = document.querySelectorAll('[data-val="true"]')

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
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*#?&]{8,}$/;

    if (!passwordRegex.test(target.value))
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `Password is invalid`
    else 
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = ``
     
}

function emailValidator(target) {
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    if (!emailRegex.test(target.value))
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `E-mail is invalid`
    else 
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = ``
}

function numberValidator(target) {
    if (isNaN(target.value) || target.value === "" || target.value < 0)
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `value is invalid`
    else
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = ``
}

function fileValidator(target) {
    const fileExtensions = ["jpg", "jpeg", "png", "gif"];

    const fileInput = target.files[0];
    const fileExtension = fileInput.name.split(".").pop();

    if (!fileExtensions.includes(fileExtension))
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = `Must be a valid image file: "jpg", "jpeg", "png" or "gif"`
    else 
        document.querySelector(`[data-valmsg-for="${target.id}"]`).innerHTML = ``
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


function ViewReviews() {
    const descriptionContainer = document.querySelector(".view-description-container")
    descriptionContainer.style.display = "none"
    const reviewContainer = document.querySelector(".view-review-container")

    if (reviewContainer.style.display === "block") {
        reviewContainer.style.display = "none";
    } else {
        reviewContainer.style.display = "block";
    }
}

function ViewDescription() {
    const reviewContainer = document.querySelector(".view-review-container")
    reviewContainer.style.display = "none"
    const descriptionContainer = document.querySelector(".view-description-container")

    if (descriptionContainer.style.display === "block") {
        descriptionContainer.style.display = "none";
    } else {
        descriptionContainer.style.display = "block";
    }
}


function ChangeProductQuantityValue(id, maxValue) {
    let increment = document.getElementById(`${id}-increment`);
    let decrement = document.getElementById(`${id}-decrement`);
    let quantityInput = document.getElementById(`${id}-quantity`);

    if (!increment.hasAttribute("data-clicked")) {
        increment.addEventListener("click", function () {
            if (parseInt(quantityInput.value) < maxValue) {
                quantityInput.value = parseInt(quantityInput.value) + 1;
            }
        });

        increment.setAttribute("data-clicked", true);
    }

    if (!decrement.hasAttribute("data-clicked")) {
        decrement.addEventListener("click", function () {
            if (parseInt(quantityInput.value) > 1) {
                quantityInput.value = parseInt(quantityInput.value) - 1;
            }
        });

        decrement.setAttribute("data-clicked", true);
    }
}
