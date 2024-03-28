﻿using Long_Term_Care.Models.health;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;


namespace Long_Term_Care.Controllers

{
	public class HealthController : Controller
	{
		//推薦入口
		public IActionResult health_recommend()
		{
			return View();
		}
		//推薦表單   // GET 方法用於顯示表單
		public IActionResult health_form()
		{
			return View();
		}
		[HttpPost] // POST 方法用於處理提交的表單數據
		public IActionResult HealthAnswer()
		{
			return View("health_answer");
		}

		//保健品總覽
		public IActionResult Health_items()
		{

            return View(health_items_texts);
		}
		List<Health_items_text> health_items_texts = new List<Health_items_text>
		{
			new Health_items_text{Items_Id=1, Items_img="health_item_1.png", Items_title="原力維生素C+鋅粉",
                Items_context="●增強體力、健康發育，調整體質、滋補強身\r\n●符合每日所需攝取量\r\n●採用國際知名維生素原料大廠 Quali-C\r\n●維生素C：鋅 = 10：1 比例，讓您輕鬆補充必需營養素，享受健康生活的原力\"\r\n",
				Items_link="https://www.yohopower.tw/products/bpower-2"},
			new Health_items_text{Items_Id=2, Items_img="health_item_2.png", Items_title="綜合維生素B群 緩釋膜衣錠",
                Items_context="●增強體力、健康發育，調整體質、滋補強身\r\n●符合每日所需攝取量\r\n",
                Items_link="https://www.yohopower.tw/products/bpower-2"},
			new Health_items_text{Items_Id=3, Items_img="health_item_3.png", Items_title="悠活好代謝 苦瓜胜肽",
                Items_context="●增強體力、健康發育，調整體質、滋補強身\r\n●符合每日所需攝取量\r\n",
                Items_link="https://www.yohopower.tw/products/bpower-2"},
			new Health_items_text{Items_Id=4, Items_img="health_item_4.png", Items_title="原力維生素D3",
                Items_context="●增強體力、健康發育，調整體質、滋補強身\r\n●符合每日所需攝取量\r\n",
                Items_link="https://www.yohopower.tw/products/bpower-2"}
		};
		public IActionResult Health_info()
		{
			return View(health_info_texts);
		}
		List<Health_info_text> health_info_texts = new List<Health_info_text>
		{
			new Health_info_text{Info_Id=1, Info_img="health_info_1.jpg", Info_title="老年人飲食：解決四大挑戰，享受美味健康！",
				Info_context="\n如果:假牙摩擦影響咀嚼功能?\n建議您:選擇質地較柔軟、易於咀嚼和吞嚥的食物。\n" +
				"如果:唾液分泌不足影響消化功能?\n建議您:在烹飪時適時添加水分，或使用水煮或蒸煮的烹調方式。\n" +
				"如果:味覺及嗅覺衰退影響食慾?\n建議您:建議減少鹽和醬油的使用，並適量使用香料以增加食物的風味。\n" +
				"如果:消化液分泌減少影響消化功能?\n建議您:建議補充酵素和益生菌的營養補充品，以促進消化和吸收。" },

			new Health_info_text{Info_Id=2, Info_img="health_info_3.jpg", Info_title="老年人營養攝取指南：保持活力，關鍵營養一覽(上)",
				Info_context="\n1. 蛋白質\n許多銀髮族擔心影響身體而不敢攝取肉類，但身體最主要的架構就是蛋白質。" +
				"\n不能忽略優質蛋白質的攝取，" +
				"\n例如：魚肉、雞蛋、雞肉、牛肉、豬肉、黃豆製品、乳品類等。" +
				"\n搭配充足日曬與規律運動，才能維持健康活力。" +
				"\n2. 鈣質" +
				"\n鈣有助於維持骨骼與牙齒的正常發育及健康，對於銀髮族是不可缺少的營養素。" +
				"\n依據老年期營養手冊指出 65 歲以上銀髮族一日所需的鈣質為 1000 毫克，" +
				"\n建議平常可以多補充牛奶、起司、奶粉、傳統豆腐、豆干、芥藍等食物攝取。" +
				"\n無法從飲食中攝取足夠的鈣質，亦可適度補充鈣片。" +
				"\n3. 維生素D" +
				"\n與鈣質相同，維生素D可幫助骨骼與牙齒的生長發育之外，" +
				"\n還能幫增進鈣質吸收。建議要多多出門活動，適度的曬太陽使身體產生維生素D。" },

			new Health_info_text{Info_Id=3, Info_img="health_info_3.jpg", Info_title="老年人營養攝取指南：保持活力，關鍵營養一覽(下)",
				Info_context="\n4. 葉酸" +
				"\n2005～2008 年營養調查結果，顯示老人葉酸不足率（血清濃度 <6 ng/mL）。" +
				"\n尤其是 80 歲以上的男性老人，其葉酸不足率高達 40%。" +
				"\n葉酸與老年人健康有相關性，\n故建議多攝取含葉酸之食物，例如：蘆筍、花椰菜、蛋黃等。" +
				"\n5. 碘\n擁有足夠的碘，才能有助維持神經肌肉的功能。" +
				"\n建議老年人應選擇添加碘之鹽取代一般鹽。\n並適量攝取含碘食物，例如：海帶、紫菜等海藻類食物，" +
				"\n額外補充身體所需之碘量。" +
				"\n6. 益生菌\n除了平常選擇多纖維、植物性食物為基底外，" +
				"\n也可適量補充益生菌達到幫助維持消化道機能、改變細菌叢生態等等。" },

			new Health_info_text{Info_Id=4, Info_img="health_info_4.jpg", Info_title="長者保健美食大揭密：挑選4大營養補充，維護健康生活(上)",
				Info_context="\n1. 鈣保健品" +
				"\n鈣是老人必備保健食品，建議挑選時可以注意以下幾點：" +
				"\n▼天然鈣質來源：" +
				"\n挑選天然鈣質來源，例如：「骨鈣素」或是「海藻鈣」。才能維持吸收與利用率。" +
				"\n若挑選如「碳酸鈣」等人工合成的鈣質，雖然鈣質含量高，" +
				"\n但是影響吸收狀況，反而事倍功半。" +
				"\n▼添加維生素D3：" +
				"\n若長輩的日常活動範圍以室內為主，更需要注意維生素D3 與鈣質的攝取搭配。" +
				"\n足量維生素D3 攝取可以增進鈣質吸收。" +
				"\n▼粉狀劑型：許多長者有吞嚥上的困擾，粉狀劑型可以讓長輩攝取較方便。" +
				"\n\n2. 維生素D" +
				"\n雖然維生素D可透過陽光照射取得，" +
				"\n但現代人的生活模式不易照射充足陽光，造成約 9 成的人缺乏維生素D。" +
				"\n▼選擇液態維生素D：" +
				"\n對於吞嚥不方便的老年人，可以使用液態形式的維生素D，方便使用且吸收效果佳。" +
				"\n▼若家中長輩茹素：" +
				"\n則須考慮市售維生素D3 多為動物來源，需謹慎挑選素食來源維生素D。" },

			new Health_info_text{Info_Id=5, Info_img="health_info_5.jpg", Info_title="長者保健美食大揭密：挑選4大營養補充，維護健康生活(下)",
				Info_context="\n3. 維生素B(葉酸)" +
				"\n葉酸不只是孕婦需要的營養素，中老年人也需要葉酸。" +
				"\n挑選老年人的葉酸保健食品時，可注意以下幾點：" +
				"\n▼挑選第4代活性葉酸，相較於食物中的葉酸，不需轉換，進入人體就能直接吸收。" +
				"\n▼添加營養素「鐵」，幫助氧氣的運輸與利用。" +
				"\n▼可與維生素B12搭配使用，增進神經系統的健康。" +
				"\n\n4. 銀髮族益生菌" +
				"\n可能是壓力，或是因為各種因素影響老人的消化道機能。建議老年人可從益生菌的保健食品著手，來調節生理機能、幫助維持消化道機能。" +
				"\n▼補充無添加任何糖及香料的保健品，避免不必要的糖類攝取量，解決年長者的困擾。" +
				"\n▼根據補充益生菌的目的，可以挑選對應的菌株。維持消化道機能與排便順暢：可以選擇長雙岐桿菌、兩歧雙歧桿菌、乾酪乳桿菌。過敏調節、增加免疫力：可以選擇植物乳桿菌、鼠李糖乳桿菌。" +
				"\n避免添加酵素的益生菌產品，因為酵素主要的功能是幫助餐後食物分解消化，但酵素與益生菌兩者共存時，反而會影響益生菌的死亡速度，補充效果也大打折扣" },

			new Health_info_text{Info_Id=6, Info_img="health_info_6.jpg", Info_title="老人補鈣食品：5大好處，3點推薦一次掌握",
				Info_context="\n5大補鈣益處" +
				"\n維持骨骼與牙齒的正常發育及健康" +
				"\n幫助血液正常的凝固功能" +
				"\n有助於肌肉與心臟的正常收縮及神經的感應性" +
				"\n活化凝血酶元轉變為凝血酶，幫助血液凝固" +
				"\n調控細胞的通透性" +
				"\n\n1. 選擇「天然鈣質」" +
				"\n營養師建議民眾可選擇天然來源的鈣質，例如骨鈣素、海藻鈣，吸收率較好，更適合作為補鈣來源。" +
				"\n化學合成的鈣例如碳酸鈣、檸檬酸鈣等，這類化學合成的鈣分子量大，吸收率差，作用也會打折扣。" +
				"\n2. 注意「鈣含量」" +
				"\n衛生福利部建議一天要攝取 1000 毫克的鈣質，選購時也要注意鈣含量標示是否完整。" +
				"\n鈣質補充建議單次劑量不要超過 500 毫克，營養師建議可挑選每包約含有 400 毫克的鈣質產品較合適。" +
				"\n3. 添加複方，效果加乘" +
				"\n挑選有添加維生素K、維生素D3、酪蛋白水解物（CPP）的產品。補鈣還要鎖鈣，添加複方成分的產品才能聰明補鈣。" },

			new Health_info_text{Info_Id=7, Info_img="health_info_7.jpg", Info_title="老人補鈣Q&A，補鈣眉角報你知",
				Info_context="\nQ1. 老年人額外補充鈣質，要挑選錠劑、膠囊、還是粉包好呢？" +
				"\n鈣的吸濕性強，因此建議挑選有獨立包裝的產品。市售的鈣質產品大多為錠劑，在開開關關的情況下，就容易受潮，可以選擇獨立包裝的粉包，一次一包衛生安全。粉包也不用擔心膠囊、錠劑過大的困擾，更方便老年人食用。" +
				"\nQ2. 什麼時間點吃鈣最好？一次要吃多少量呢？" +
				"\n無論飯前及飯後食用皆可。那什麼時間點吃鈣最好呢？營養師建議『睡前吃最好』！睡前補充鈣質可以幫助入睡，另外也可以減少鈣質與其他食物（例如鐵、磷）的交互作用影響吸收率喔。" +
				"\nQ3. 從幾歲開始需要積極補鈣呢？" +
				"\n衛生福利部建議各年齡層鈣質建議攝取量如下，13~18 歲為發育中的孩童需求量會增加至 1200 毫克，一般成人每日需求量為 1000 毫克，如有特殊情況可依照醫師指示增加攝取量。" },
		};
	}
}
