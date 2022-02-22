using Microsoft.AspNetCore.Mvc;
using Salesforce.Common.Models.Json;
using Salesforce.Force;
using System.Diagnostics;
using WebApplication_salesf.Models;

namespace WebApplication_salesf.Controllers
{
    
    public class HomeController : Controller
    {
        //authentification credentials
        public static string _clientId = "3MVG9I9urWjeUW051PumYX1mbS5HkS3kpZsbCEzYWjgivRyDno1MjvM08EfVf2be52s0vrthHamsgMpQCrm5Z";
        public static string _clientSecret = "EC97DAFBF9F6F2399DE5E7BADA2E9BBEF6B3B6E832DC435668AA452940AD9501";
        private static string _username = "soljit_algeria2@soljit.com";
        private static string _securityToken = "zoLmTaUDLiouUaOAN6WhOQPi";
        private static string _password = "entretient_1235" + _securityToken;
        private static string _tokenRequestEndpointUrl = "https://login.salesforce.com/services/oauth2/token";




        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       //display candidature using the id
       ///Q1 answer : display candidature By specifying the ID
        [HttpGet("{id}")]
        public IActionResult Index(string id)

        {
            var auth = new Salesforce.Common.AuthenticationClient();

            auth.UsernamePasswordAsync(_clientId, _clientSecret, _username, _password).Wait();

            var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
            //, Year_Of_Experience__c 
            Task<QueryResult<dynamic>> results = client.QueryAllAsync<dynamic>("SELECT id,First_Name__c,Last_Name__c,Year__c, Year_Of_Experience__c from Candidature__c where id='" + id + "'");

            results.Wait();


            string c;
            var candidatures = results.Result.Records;
            foreach (var candidature in candidatures)
            {
                ViewBag.id=candidature.Id;
                ViewBag.firstname= candidature.First_Name__c;
                ViewBag.lastname= candidature.Last_Name__c;
                ViewBag.year_of_experience= candidature.Year_Of_Experience__c;
                ViewBag.year_c= candidature.Year__c;

            }


            return View();
        }

        //get all candidature + display all condidatures on a GUI interface   (Q4+Q6)
        [HttpGet("/")]
        [HttpGet("/DisplayAllCandidature")]
        [HttpGet("home/DisplayAllCandidature")]
        public async Task<IActionResult> Display_All_CandidatureAsync()

        {
            var auth = new Salesforce.Common.AuthenticationClient();

            auth.UsernamePasswordAsync(_clientId, _clientSecret, _username, _password).Wait();

            var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
           
            Task<QueryResult<dynamic>> results = client.QueryAllAsync<dynamic>("SELECT Id,First_Name__c,Last_Name__c,Year__c, Year_Of_Experience__c from Candidature__c");

            results.Wait();
           
            List<Candidature__c> c = new List<Candidature__c>();
            var candidatures = results.Result.Records;
            foreach (var candidature in candidatures)
            {
                string id_c=candidature.Id;
                string firstname = candidature.First_Name__c;
                string lastname = candidature.Last_Name__c;
                double year_of_experience = candidature.Year_Of_Experience__c;
                var year = candidature.Year__c;
               
                c.Add(new Candidature__c(id_c,firstname, lastname, year, year_of_experience));
              

            }
            

            return View(c);
        }



        //Post une candidature 
        [HttpPost]
        
        public async Task<ActionResult> CreateAsync()
        {
            var auth = new Salesforce.Common.AuthenticationClient();

            auth.UsernamePasswordAsync(_clientId, _clientSecret, _username, _password).Wait();

            var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
            var new_rec = new Candidature__c() { First_Name__c = "bouchra", Last_Name__c = "amirouche", Year__c = 0, Year_Of_Experience__c = 0 };
            var idc = await client.CreateAsync("Candidature__c", new_rec);
            return View();

        }


       





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}