//using Microsoft.AspNetCore.Mvc;

//namespace Long_Term_Care.Controllers.Health
//{
//	public class HealthFormController : Controller
//	{
//		// GET: Health
//		public ActionResult Index()
//		{
//			return View();
//		}

//		[HttpPost]
//		public ActionResult Evaluate(FormCollection form)
//		{
//			// 初始化保健品分數
//			Dictionary<string, int> products = new Dictionary<string, int>
//			{
//				{ "產品C", 0 },
//				{ "產品B", 0 },
//				{ "產品UCII", 0 },
//				{ "產品鈣", 0 },
//				{ "產品魚油", 0 },
//				{ "產品D", 0 },
//				{ "產品紅麴", 0 },
//				{ "產品益生菌", 0 },
//				{ "產品酵素", 0 }
//			};

//			// 計算BMI
//			float height = float.Parse(form["bmi_height"]);
//			float weight = float.Parse(form["bmi_weight"]);

//			float bmi = weight / ((height / 100) * (height / 100));
//			// 根據BMI值設置對應的 value 和 data-product
//			string productValue;
//			string productData;

//			if (bmi < 18.5)
//			{
//				productValue = "30";
//				productData = "產品鈣";
//			}
//			else if (bmi > 24)
//			{
//				productValue = "30";
//				productData = "產品紅麴";
//			}
//			else
//			{
//				productValue = "1";
//				productData = "產品C";
//			}
//			// 添加BMI對應的產品到統計中
//			products[productData] += int.Parse(productValue);

//			// 獲取每個問題的選項分數，並添加到對應的產品上
//			for (int i = 1; i <= 18; i++)
//			{
//				string selectedValue = form[$"q{i}"];
//				string product = form[$"q{i}_product"];

//				// 如果選擇了某個產品
//				if (!string.IsNullOrEmpty(product))
//				{
//					products[product] += int.Parse(selectedValue);
//				}
//			}

//			// 按總分排序產品
//			var sortedProducts = products.OrderByDescending(kv => kv.Value).Select(kv => kv.Key).ToList();
//			var topThree = sortedProducts.Take(3).ToList();// 取前三名產品
//			var name = form["name"];// 獲取填寫的名字

//			// 使用 Session 存儲名字和前三名產品
//			Session["name"] = name;
//			Session["topThree"] = topThree;

//			return RedirectToAction("Result");
//		}

//		public ActionResult Result()
//		{
//			string name = Session["name"]?.ToString();
//			List<string> topThree = (List<string>)Session["topThree"];

//			ViewBag.Name = name;
//			ViewBag.TopThree = topThree;

//			return View();
//		}
//	}
//}
