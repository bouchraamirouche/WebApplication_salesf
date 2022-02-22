using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Salesforce.Force;
using WebApplication_salesf.Models;

namespace WebApplication_salesf.Controllers
{
    public class CandidatController1 : Controller
    {

        //authentification credentials
        public static string _clientId = "3MVG9I9urWjeUW051PumYX1mbS5HkS3kpZsbCEzYWjgivRyDno1MjvM08EfVf2be52s0vrthHamsgMpQCrm5Z";
        public static string _clientSecret = "EC97DAFBF9F6F2399DE5E7BADA2E9BBEF6B3B6E832DC435668AA452940AD9501";
        private static string _username = "soljit_algeria2@soljit.com";
        private static string _securityToken = "zoLmTaUDLiouUaOAN6WhOQPi";
        private static string _password = "entretient_1235" + _securityToken;
        private static string _tokenRequestEndpointUrl = "https://login.salesforce.com/services/oauth2/token";


        // GET: CandidatController1
        public ActionResult Index()
        {
            return View();
        }



        // GET: CandidatController1/Create
        [Route("candidatcontroller1/createasync1")]
        public ActionResult CreateAsync1()
        {
            return View();
        }

        ///Q2 
        [Route("candidatcontroller1/createasync1")]
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync1(string First_Name__c, string Last_Name__c, object Year__c, double Year_Of_Experience__c)
        {
            var auth = new Salesforce.Common.AuthenticationClient();

            auth.UsernamePasswordAsync(_clientId, _clientSecret, _username, _password).Wait();

            var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
            
            var new_rec = new Candidature__c() { First_Name__c = First_Name__c, Last_Name__c = Last_Name__c, Year__c = 0, Year_Of_Experience__c = Year_Of_Experience__c };
            var idc = await client.CreateAsync("Candidature__c", new_rec);
            return View();
        }

        //edit candidature Q3

        // GET: CandidatController1/Edit/5
        public ActionResult Edit(string id)
        {
           
            return View();
        }

        // POST: CandidatController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, string First_Name__c, string Last_Name__c, object Year__c, double Year_Of_Experience__c)
        {
            var auth = new Salesforce.Common.AuthenticationClient();

            auth.UsernamePasswordAsync(_clientId, _clientSecret, _username, _password).Wait();

            var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
            var new_edit = new Candidature__c() { First_Name__c = First_Name__c, Last_Name__c = Last_Name__c, Year__c = 0, Year_Of_Experience__c = Year_Of_Experience__c };
            var id_new = await client.CreateAsync("Candidature__c", new_edit);
            var id_n = id;


            var success = await client.UpdateAsync("Candidature__c", id_n, new_edit);
            return View();
        }

        // GET: CandidatController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CandidatController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
