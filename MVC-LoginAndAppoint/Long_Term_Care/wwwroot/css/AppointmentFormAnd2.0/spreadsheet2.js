var savecalculate = document.getElementById('save_calculate');
savecalculate.addEventListener('click', function () {
    var rows = document.getElementById('_table').rows;
    var data = [];
    //登入儲存 設key=id+日期
    var userid = "id";

    //儲存第0列的資料
    var firstRowData = {
        cmsLevel: document.querySelector('._ranged').selectedIndex,
        cmsLevelName: document.querySelector('._ranged').selectedOptions[0].text, //修正1
        householdType: document.querySelector('._typepay').selectedIndex,
        allprice: document.querySelector('._allprice').innerText
    };
    data.push(firstRowData);
    // 儲存B碼其他列的資料
    for (let i = 2; i < rows.length; i++) {
        let rowData = {
            product: rows[i].cells[1].querySelector('._product').selectedIndex,
            productName: rows[i].cells[1].querySelector('._product').selectedOptions[0].text, //修正2
            price: rows[i].cells[2].querySelector('._price').innerText,
            realpay: rows[i].cells[3].querySelector('._realpay').innerText,
            quantity: rows[i].cells[4].querySelector('._quantity').value,
            subtotal: rows[i].cells[5].querySelector('._subtotal').innerText
        };
        data.push(rowData);
    }
    //儲存G碼的資料
    var gData = {
        gcms: document.querySelector('.g_ranged').selectedIndex,
        gcmsName: document.querySelector('.g_ranged').selectedOptions[0].text, //修正3
        ghousetype: document.querySelector('.g_typepay').value,
        gallprice: document.querySelector('.g_allprice').innerText,
        gquantity: document.querySelector('.g_quantity').value,
        gsubtotal: document.querySelector('.g_subtotal').innerText,
        gservice :document.getElementById('g_service').innerText,
        gseven :document.getElementById('g_seven').innerText
    };
    data.push(gData);

    console.log(data);
    

    localStorage.setItem(userid, JSON.stringify(data));
    alert("資料已儲存");
});

function importsp() {
    addRow();
    var userid = "id";
    var sadata = localStorage.getItem(userid);
    
    if (sadata) {
        var data = JSON.parse(sadata);
        console.log(data);
        document.querySelector('._ranged').selectedIndex = data[0].cmsLevel;
        document.querySelector('._typepay').selectedIndex = data[0].householdType;
        document.querySelector('._allprice').innerText = data[0].allprice;
        var rows = document.getElementById('_table').rows;
        for (var i = 2; i < data.length; i++) {
            rows[i].cells[1].querySelector('._product').selectedIndex = data[i - 1].product;
            rows[i].cells[2].querySelector('._price').innerText = data[i - 1].price;
            rows[i].cells[3].querySelector('._realpay').innerText = data[i - 1].realpay;
            rows[i].cells[4].querySelector('._quantity').value = data[i - 1].quantity;
            rows[i].cells[5].querySelector('._subtotal').innerText = data[i - 1].subtotal;
            if (i != data.length - 1) {
                addRow();
            }
        }
        calculateTotal(); // 重新計算總金額

        document.querySelector('.g_ranged').selectedIndex = data[data.length - 1].gcms;
        document.querySelector('.g_typepay').value = data[data.length - 1].ghousetype;
        document.querySelector('.g_allprice').innerText = data[data.length - 1].gallprice;
        document.querySelector('.g_realpay').innerText = data[data.length - 1].ghousetype;
        document.querySelector('.g_quantity').value = data[data.length - 1].gquantity;
        document.querySelector('.g_subtotal').innerText = data[data.length - 1].gsubtotal;
        GcalculateTotal()

        var save_calculate = document.getElementById('save_calculate');
        var apply_service = document.getElementById('apply_service');
        var update_calculate = document.getElementById('update_calculate');
        apply_service.style.display = 'block';
        update_calculate.style.display = 'block';
        save_calculate.style.display = 'none';

       
            // 找到所有的輸入元素和選擇元素
            var inputs = document.getElementsByTagName('input');
            var selects = document.getElementsByTagName('select');

            // 鎖住所有的輸入元素
            for (var i = 0; i < inputs.length; i++) {
                inputs[i].disabled = true;
            }

            // 鎖住所有的選擇元素
            for (var i = 0; i < selects.length; i++) {
                selects[i].disabled = true;
            }
            var _newbtn = document.getElementById('_newbtn');
            _newbtn.style.display = 'none';
            var _delbtn = document.getElementById('_delbtn');
            _delbtn.style.display = 'none';
            
            //獲取選項的文字
            // var selectElement = rows[2].cells[1].querySelector('._product');
            // var selectedOptionText = selectElement.options[selectElement.selectedIndex].text;
            //上述兩行可簡寫成var selectedOptionText = rows[2].cells[1].querySelector('._product').selectedOptions[0].text,

            // document.getElementById('bee').innerText = selectedOptionText;

    }
}

window.onload = function () {
    importsp();
};

function deleted() {
    var userid = "id";
    localStorage.removeItem(userid);
    window.location.reload();
}

//當使用者按下儲存鍵時，顯示申請服務按鈕
var save_calculate = document.getElementById('save_calculate');
var apply_service = document.getElementById('apply_service');
var update_calculate = document.getElementById('update_calculate');
save_calculate.addEventListener('click', () => {
    apply_service.style.display = 'block';
    update_calculate.style.display = 'block';
    save_calculate.style.display = 'none';
    // 找到所有的輸入元素和選擇元素
    var inputs = document.getElementsByTagName('input');
    var selects = document.getElementsByTagName('select');

    // 鎖住所有的輸入元素
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].disabled = true;
    }

    // 鎖住所有的選擇元素
    for (var i = 0; i < selects.length; i++) {
        selects[i].disabled = true;
    }
    var _newbtn = document.getElementById('_newbtn');
    _newbtn.style.display = 'none';
    var _delbtn = document.getElementById('_delbtn');
    _delbtn.style.display = 'none';
});

//按下更新按鈕打開封印
function updatecalu() {
    var save_calculate = document.getElementById('save_calculate');
    var update_calculate = document.getElementById('update_calculate');
    var apply_service = document.getElementById('apply_service');
    update_calculate.style.display = 'none';
    apply_service.style.display = 'none';
    save_calculate.style.display = 'block';

    var inputs = document.getElementsByTagName('input');
    var selects = document.getElementsByTagName('select');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].disabled = false;
    }
    for (var i = 0; i < selects.length; i++) {
        selects[i].disabled = false;
    }
    var _newbtn = document.getElementById('_newbtn');
    _newbtn.style.display = 'block';
    var _delbtn = document.getElementById('_delbtn');
    _delbtn.style.display = 'block';
    
}



function applyservice(){
    // 轉至填寫個案基本資料
    window.location.href = 'https://www.google.com.tw/';
}


//檢查鍵是否存在
function checked() {
    var userid = "id";
    var data = localStorage.getItem(userid);
    if (data === null) {
        alert("鍵 'id' 不存在");
    } else {
        alert("鍵 'id' 存在");
    }
}


