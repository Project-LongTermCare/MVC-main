// 開始評估 button
document.getElementById('startButton').addEventListener('click', function () {
    document.getElementById('question').style.display = 'block'; // 點擊按鈕後顯示問卷部分
});

document.getElementById('fb_form78').addEventListener('submit', function (event) {
    event.preventDefault(); // 防止表單被提交

    // 初始化保健品分數
    var products = {
        '產品C': 0,
        '產品B': 0,
        '產品UCII': 0,
        '產品鈣': 0,
        '產品魚油': 0,
        '產品D': 0,
        '產品紅麴': 0,
        '產品益生菌': 0,
        '產品酵素': 0,
    };

    var name = document.getElementById('name').value;// 獲取填寫的名字

    // 計算BMI
    var height = parseFloat(document.getElementById('bmi_height').value);
    var weight = parseFloat(document.getElementById('bmi_weight').value);

    var bmi = weight / ((height / 100) * (height / 100));
    console.log(bmi);
    // 根據BMI值設置對應的 value 和 data-product
    var productValue;
    var productData;

    if (bmi < 18.5) {
        productValue = "30";
        productData = "產品鈣";
    } else if (bmi > 24) {
        productValue = "30";
        productData = "產品紅麴";
    } else {
        productValue = "1";
        productData = "產品C";
    }
    // 添加BMI對應的產品到統計中
    products[productData] += parseInt(productValue);

    // 獲取每個問題的選項分數，並添加到對應的產品上
    for (var i = 1; i <= 18; i++) {
        var selectedValue = document.querySelector('input[name="q' + i + '"]:checked').value;
        var product = document.querySelector('input[name="q' + i + '"]:checked').dataset.product;

        // 如果選擇了某個產品
        if (product) {
            products[product] += parseInt(selectedValue);
        }
    }

    // 按總分排序產品
    var sortedProducts = Object.keys(products).sort(function (a, b) {
        return products[b] - products[a];
    });
    // 取前三名產品
    var topThree = sortedProducts.slice(0, 3);

    var product_detail = {
        '產品C': { name_detail: '【抗氧專科】法國西印度櫻桃萃取維他命C', image_source: '/css/health_items/img/health_item_1.png' },
        '產品B': { name_detail: '【美妍專科】歐洲天然酵母維生素B群+Q10+專利鐵', image_source: '/css/health_items/img/health_item_2.png' },
        '產品UCII': { name_detail: '【關鍵專科】美國專利UC2+日本高效葡萄糖胺+美國專利C3薑黃', image_source: '/css/health_items/img/health_item_3.png' },
        '產品鈣': { name_detail: '【挺固專科】愛爾蘭高比例海藻鈣+海洋鎂+維生素D+K2', image_source: '/css/health_items/img/health_item_4.png' },
        '產品魚油': { name_detail: '【純淨專科】挪威 85% rTG 高濃度魚油 Omega-3', image_source: '/css/health_items/img/health_item_5.png' },
        '產品D': { name_detail: '【陽光專科】歐洲天然酵母維生素D', image_source: '/css/health_items/img/health_item_6.png' },
        '產品紅麴': { name_detail: '【舒活專科】日本專利紅麴+Q10+天然蕎麥B12', image_source: '/css/health_items/img/health_item_7.png' },
        '產品益生菌': { name_detail: '【順暢專科】LP28 超有感複合 300 億益生菌+美國綜合消化酵素', image_source: '/css/health_items/img/health_item_8.png' },
        '產品酵素': { name_detail: '【消化專科】美國代謝19種活性超級酵素', image_source: '/css/health_items/img/health_item_9.png' },
    };

    // 更新推荐产品部分的显示，显示新的产品名称和图像
    var recommendationsDiv = document.getElementById('recommendations');
    recommendationsDiv.innerHTML = '<h3>' + '親愛的' + name + ' 先生/小姐</h3>' +
        '<h3>感謝您的用心填寫</h3>' +
        '<h3>以下是推薦給您的保健品：</h3>';

    // 遍历前三名产品，并显示产品名称和图像
    for (var i = 0; i < topThree.length; i++) {
        var productName = topThree[i];
        var newProductName = product_detail[productName].name_detail;
        var productImageSrc = product_detail[productName].image_source;

        // 创建包含产品名称和图像的 div
        var productDiv = document.createElement('div');
        productDiv.innerHTML = '<p>' + newProductName + '</p>';

        // 创建图像元素并设置图像路径
        var imageElement = document.createElement('img');
        imageElement.src = productImageSrc;

        // 将图像元素添加到 div 中
        productDiv.appendChild(imageElement);

        // 将包含产品名称和图像的 div 添加到推荐产品部分
        recommendationsDiv.appendChild(productDiv);
    }


    //// 生成推薦產品列表，包括排名
    //for (var i = 0; i < topThree.length; i++) {
    //    var ranking = i + 1;
    //    topThreeProducts += '<p>' + '第 ' + ranking + ' 名：' + topThree[i] + '</p>';
    //}

    //// 顯示推薦產品
    //var recommendationsDiv = document.getElementById('recommendations');
    //recommendationsDiv.innerHTML = '<h3>' + '親愛的' + name + ' 先生/小姐</h3>' +
    //    '<h3>感謝您的用心填寫</h3>' +
    //    '<h3>以下是推薦給您的保健品：</h3>' +
    //    topThreeProducts;
});

    //// 取前三名產品
    //var topThree = sortedProducts.slice(0, 3);
    //var topThreeProducts = topThree.join('\n'); // 將前三名產品以換行符號連接成字串

    //// 顯示推薦產品
    //var recommendationsDiv = document.getElementById('recommendations');
    //recommendationsDiv.innerHTML = '<h3>' + '親愛的' + name + ' 先生/小姐</h3>' +
    //    '<h3>感謝您的用心填寫</h3>' +
    //    '<h3>以下是推薦給您的保健品：</h3>' +
    //    topThreeProducts;
