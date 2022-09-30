let slideIndex = 0;
const bookContainer = [...document.querySelectorAll('.book-container')];
const swiperslide =[...document.querySelectorAll('.swiper-slide')];
const nxtBtn = [...document.querySelectorAll('.next-btn')];
const preBtn = [...document.querySelectorAll('.pre-btn')];
const dots = document.getElementsByClassName("dot");
bookContainer.forEach((item, i) => {
    /*getBoundingRect returns item's size and location*/
    let containerDimensions = item.getBoundingClientRect();
    let containerWidth = containerDimensions.width;

    nxtBtn[i].addEventListener('click', () => {
        item.scrollLeft += containerWidth;
    })

    preBtn[i].addEventListener('click', () => {
        item.scrollLeft -= containerWidth;
    })
})

//const slides = document.getElementsByClassName("mySlides");
//const dots = document.getElementsByClassName("dot");

showSlides(slideIndex);


function nextSlide() {
    //  [] [] [] [x] which means we need to go first element 
    if (slideIndex + 1 === swiperslide.length) {
        slideIndex = 0;
    } else {
        slideIndex++
    }

    showSlides(slideIndex);
}
function showSlides(index) {
    // when we use getElementsByClassName it returns an element collection array which does not have .forEach method by built-in,
    // thats why we spread into an array by using [...slides]
    [...slides].forEach(slide => {
        swiperslide.style.display = 'none';
    });

    [...dots].forEach(dot => {
        dot.classList.remove('active')
    });

    const activeSlide = swiperslide[index]
    const activeDot = dots[index]


    activeSlide.style.display = 'block'
    activeDot.classList.add('active')
}
