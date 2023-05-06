/*
const toggleClassName = (element, className, expression) => {
    document.querySelector(element).classList.toggle(className, expression)
}
 
const toggleMenuButton = (attribute) => {
    const btn = document.querySelector(attribute)

    btn.addEventListener('click', function () {
        const element = document.querySelector(btn.getAttribute('data-target'))
        const contains = element.classList.contains('open-menu')

        element.classList.toggle('open-menu', !contains)
        btn.classList.toggle('btn-outline-dark', !contains)
        btn.classList.toggle('btn-toggle-white', !contains)
    })
}
*/

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

