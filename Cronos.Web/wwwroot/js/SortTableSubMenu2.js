
toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "100",
    "hideDuration": "800",
    "timeOut": "800",
    "extendedTimeOut": "800",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

const draggableList = document.getElementById("draggable-list");
const check = document.getElementById("check");

const orderMenus = [];



const listItems = [];

var parentId;




const saveOrderUrl = window.location.origin + "/api/submenu2/saveSubMenu2Orders"

var menus;




function saveOrder() {
    var orderedMenus = []

    listItems.map((item) => {
        //console.log("item", item);

        var Id = item.querySelector(".draggable .id").innerHTML;

        var Name = item.querySelector(".draggable .name").innerHTML;

        orderedMenus.push({ Id: Id, MenuName: Name, Order: item.getAttribute("Order") });

    })
    //refresh page so new order must be update to save

    setTimeout(() => location.reload(), 700);


    //console.log("ordered Menus", orderedMenus)






    fetch(saveOrderUrl,
        {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "POST",
            body: `${JSON.stringify(orderedMenus)}`
        })
        .then(function (res) { console.log(res) })
        .catch(function (res) { console.log(res) })
    toastr.success("Sıralama Başarıyla Kaydedildi!");

}


//Sayfa yüklendiğinde api üzerinden menüleri getir. Ş.Geyik || 29.09.22
$(document).ready(() => setTable());

var setTable = function () {
    parentId = document.getElementById("ParentId").value
    const apiUrl = window.location.origin + `/api/submenu2/getAllSubMenus2ByOrder/${parentId}`;

    //  console.log("url", apiUrl)
    fetch(apiUrl).then((response) => response.json())
        .then((data) => {


            //gelen json verisini Order propertysine göre  sırala
            data = data.sort((a, b) => {
                if (a.Order < b.Order) {
                    return -1;
                }
            });


            console.log("gelen veri", data);
            for (var i = 0; i < data.length; i++) {
                if (data[i].IsActive == true && data[i].IsDeleted == false) {
                    orderMenus.push({ "Id": data[i].Id, "name": data[i].SubMenuName, "Order": data[i].Order })


                }
            }

            /* console.log("orderMenus", orderMenus);*/
            createList();
        }

        );

}



let dragStartIndex;

function createList() {
    const newList = [...orderMenus];
    newList
        .map((menu) => ({ value: menu })) // randomize list
        .sort((a, b) => a.sort - b.sort) // generate new order
        .map((menu) => menu.value) // retrieve original strings
        .forEach((menu, index) => {
            const listItem = document.createElement("li");
            listItem.setAttribute("data-index", index);
            listItem.setAttribute("Order", menu.Order);
            listItem.innerHTML = `
          <span class="number">${index + 1}</span>
          <div class="draggable" draggable="true">
            <p  class="name">${menu.name}</p>

         <p  hidden class="id">${menu.Id}</p>
          </div>
        `;

            listItems.push(listItem);
            draggableList.appendChild(listItem);
        });
    addListeners();
}

function dragStart() {
    dragStartIndex = +this.closest("li").getAttribute("data-index");
}

function dragEnter() {
    this.classList.add("over");
}

function dragLeave() {
    this.classList.remove("over");

}

function dragOver(e) {
    e.preventDefault(); // dragDrop is not executed otherwise

}

function dragDrop() {
    const dragEndIndex = +this.getAttribute("data-index");


    this.setAttribute("Order", parseInt(this.getAttribute("data-index")) + 1)
    swapItems(dragStartIndex, dragEndIndex);
    this.classList.remove("over");
    saveOrder();
}

function swapItems(fromIndex, toIndex) {
    // Get Items
    const itemOne = listItems[fromIndex].querySelector(".draggable");
    const itemTwo = listItems[toIndex].querySelector(".draggable");
    // Swap Items
    listItems[fromIndex].appendChild(itemTwo);
    listItems[toIndex].appendChild(itemOne);
}

function checkOrder() {
    listItems.forEach((listItem, index) => {
        const menuName = listItem.querySelector(".draggable").innerText.trim();
        if (menuName !== orderMenus[index]) listItem.classList.add("wrong");
        else {
            listItem.classList.remove("wrong");
            listItem.classList.add("right");
        }
    });
}

// Event Listeners
function addListeners() {
    const draggables = document.querySelectorAll(".draggable");
    const dragListItems = document.querySelectorAll(".draggable-list li");
    draggables.forEach((draggable) => {
        draggable.addEventListener("dragstart", dragStart);
    });
    dragListItems.forEach((item) => {
        item.addEventListener("dragover", dragOver);
        item.addEventListener("drop", dragDrop);
        item.addEventListener("dragenter", dragEnter);
        item.addEventListener("dragleave", dragLeave);
    });
}

//check.addEventListener("click", checkOrder);


