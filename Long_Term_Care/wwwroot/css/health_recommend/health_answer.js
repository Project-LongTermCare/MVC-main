document.addEventListener('DOMContentLoaded', function () {
    //// 呼叫localStorage中的name
    //var name = localStorage.getItem('name');
    //var nameDisplay = document.getElementById('nameDisplay');
    //nameDisplay.innerHTML = `<h3>親愛的 ${name} 先生/小姐</h3>`;

    var u_name = sessionStorage.getItem('name');
    var nameDisplay = document.getElementById('nameDisplay');
    console.log(u_name);
    if (u_name) {
        nameDisplay.innerHTML = `<h3>親愛的 ${u_name} 先生/小姐</h3>`;
    } else {
        nameDisplay.innerHTML = "<h3>親愛的用戶</h3>";
    }

    //呼叫sessionStorage中的topThree
    var topthree_answer = JSON.parse(sessionStorage.getItem('topThree'));
    console.log(topthree_answer);
    var product_detail = {
        '產品C': { name_detail: '【抗氧專科】法國西印度櫻桃萃取維他命C', image_source: 'health_item_1.png' },
        '產品B': { name_detail: '【美妍專科】歐洲天然酵母維生素B群+Q10+專利鐵', image_source: 'health_item_2.png' },
        '產品UCII': { name_detail: '【關鍵專科】美國專利UC2+日本高效葡萄糖胺+美國專利C3薑黃', image_source: 'health_item_3.png' },
        '產品鈣': { name_detail: '【挺固專科】愛爾蘭高比例海藻鈣+海洋鎂+維生素D+K2', image_source: 'health_item_4.png' },
        '產品魚油': { name_detail: '【純淨專科】挪威 85% rTG 高濃度魚油 Omega-3', image_source: 'health_item_5.png' },
        '產品D': { name_detail: '【陽光專科】歐洲天然酵母維生素D', image_source: 'health_item_6.png' },
        '產品紅麴': { name_detail: '【舒活專科】日本專利紅麴+Q10+天然蕎麥B12', image_source: 'health_item_7.png' },
        '產品益生菌': { name_detail: '【順暢專科】LP28 超有感複合 300 億益生菌+美國綜合消化酵素', image_source: 'health_item_8.png' },
        '產品酵素': { name_detail: '【消化專科】美國代謝19種活性超級酵素', image_source: 'health_item_9.png' },
    };

    for (var i = 0; i < topthree_answer.length; i++) {
        var productName = topthree_answer[i];
        var p_name = product_detail[productName].name_detail;
        var img = product_detail[productName].image_source;

        // 將產品名稱和圖片路徑存入 window 物件中
        window['name_' + (i + 1)] = p_name;
        window['img_' + (i + 1)] = img;
    }

    // 定義一個陣列，包含所有產品的資訊
    var productsInfo = [
        { p_name: name_1, img: img_1 },
        { p_name: name_2, img: img_2 },
        { p_name: name_3, img: img_3 }
    ];

    // 創建一個空字串，用於存儲所有卡片的 HTML 內容
    var cardsHTML = '';

    // 遍歷產品資訊陣列，並生成對應的卡片 HTML
    productsInfo.forEach(function (item, index) {

        // 將每個產品的資訊插入到卡片的 HTML 模板中
        var cardHTML = `
        <div class="card mb-4" style="max-width: 70%;">
            <div class="row g-0">
                <div class="col-md-4">
                    <img src="/css/health_items/img/${item.img}" class="img-fluid rounded-start">
                </div>
                <div class="col-md-8">
                    <div class="card-body">
                        <h5 class="card-title">${item.p_name}</h5>
                        <p class="card-text">This is a wider card with supporting text below as a natural
                            lead-in to additional content. This content is a little bit longer.</p>
                        <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
                    </div>
                </div>
            </div>
        </div><hr>
    `;
        // 將當前卡片的 HTML 添加到總的 HTML 字串中
        cardsHTML += cardHTML;
    });

    // 將所有卡片的 HTML 內容插入到頁面中的容器元素中
    document.getElementById('cardContainer').innerHTML = cardsHTML;

    document.getElementById('back').addEventListener('click', function (event) {
        history.back();
        sessionStorage.clear();
    })
})