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
        '產品葉酸': 0,
        '產品D': 0,
        '產品紅麴': 0,
        '產品益生菌': 0,
        '產品苦瓜': 0,
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

    // 獲取每個問題的選項分數，並添加到對應的產品上
    for (var i = 1; i <= 19; i++) {
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
    var topThreeProducts = topThree.join('\n'); // 將前三名產品以換行符號連接成字串

    // 顯示推薦產品
    var recommendationsDiv = document.getElementById('recommendations');
    recommendationsDiv.innerHTML = '<h3>' + '親愛的' + name + ' 先生/小姐</h3>' +
        '<h3>感謝您的用心填寫</h3>' +
        '<h3>以下是推薦給您的保健品：</h3>' +
        topThreeProducts;
    // alert('親愛的'+ name + '先生/小姐\n感謝您的用心填寫\n以下是推薦給您的保健品：\n' + topThreeProducts);

});