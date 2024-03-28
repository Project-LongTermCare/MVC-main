using bigproject.Models;
using Long_Term_Care.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace Long_Term_Care.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly LongTermCareContext _context;

        public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
        
        public  IActionResult CareTeam(int id)
        {

            //CareTeamTexts.Add(new CareTeamText { cID = id });
          
            return View(CareTeamTexts);
        }
        List<CareTeamText> CareTeamTexts = new List<CareTeamText>
        {
            new CareTeamText{cID=1,cName="連木棟",cLink="E001.png", cText="基本身體清潔、測量生命跡象、陪同外出", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=2,cName="連木棟",cLink="E002.png", cText="基本日常照顧、協助進食或管灌餵食、足部照護", cTime="服務時間 週二到週五上午10:00~下午3:00" },
            new CareTeamText{cID=3,cName="連木棟",cLink="E003.png", cText="基本身體清潔、翻身拍背、陪同就醫", cTime="服務時間 週四到週五上午10:00~下午3:00" },
            new CareTeamText{cID=4,cName="連木棟",cLink="E004.png", cText="基本日常照顧、代購或代領或代送服務、安全看視", cTime="服務時間 週三到週五上午10:00~下午5:00" },
            new CareTeamText{cID=5,cName="連木棟",cLink="E005.png", cText="基本身體清潔、陪伴服務、巡視服務", cTime="服務時間 週三週四上午10:00~下午5:00" },
            new CareTeamText{cID=6,cName="連木棟",cLink="E006.png", cText="基本日常照顧、協助洗頭、協助排泄", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=7,cName="連木棟",cLink="E007.png", cText="基本身體清潔、餐食照顧、肢體關節", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=8,cName="連木棟",cLink="E008.png", cText="基本日常照顧、家務協助、管路清潔", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=9,cName="連木棟",cLink="E009.png", cText="基本身體清潔、陪伴服務、巡視服務", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=10,cName="連木棟",cLink="E010.png", cText="基本日常照顧、協助上下樓梯、足部照護", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=12,cName="連木棟",cLink="E011.png", cText="基本身體清潔、協助進食或管灌、翻身拍背", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=13,cName="連木棟",cLink="E012.png", cText="基本日常照顧、血糖機驗血糖、甘油球通便", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=14,cName="連木棟",cLink="E013.png", cText="基本身體清潔、安全看視、陪同外出", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=15,cName="連木棟",cLink="E014.png", cText="基本日常照顧、測量生命跡象、巡視服務", cTime="服務時間 週六週日上午10:00~下午5:00" },
            new CareTeamText{cID=16,cName="連木棟",cLink="E015.png", cText="基本身體清潔、協助洗頭、依指示置入藥盒", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=17,cName="連木棟",cLink="E016.png", cText="基本日常照顧、管路清潔、代購服務", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=18,cName="連木棟",cLink="E017.png", cText="基本身體清潔、陪同就醫、肢體關節", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=19,cName="連木棟",cLink="E018.png", cText="基本日常照顧、足部照護、餐食照顧", cTime="服務時間 週六週日上午10:00~下午5:00" },
            new CareTeamText{cID=20,cName="連木棟",cLink="E019.png", cText="基本身體清潔、代購服務、依指示置入藥盒", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=21,cName="連木棟",cLink="E020.png", cText="基本日常照顧、陪伴服務、協助洗頭", cTime="服務時間 週六週日上午10:00~下午5:00" },
            new CareTeamText{cID=22,cName="連木棟",cLink="E021.png", cText="基本身體清潔、協助排泄、翻身拍背", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=23,cName="連木棟",cLink="E022.png", cText="基本日常照顧、協助進食或管灌、陪同就醫", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=24,cName="連木棟",cLink="E023.png", cText="基本身體清潔、陪伴服務、協助排泄", cTime="服務時間 週一到週五上午10:00~下午5:00" },
            new CareTeamText{cID=25,cName="連木棟",cLink="E024.png", cText="基本日常照顧、家務協助、陪同外出", cTime="服務時間 週六週日上午10:00~下午5:00" },
            new CareTeamText{cID=26,cName="連木棟",cLink="E025.png", cText="基本身體清潔、甘油球通便、安全看視", cTime="服務時間 週一到週五上午10:00~下午5:00" },

        };
	

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
