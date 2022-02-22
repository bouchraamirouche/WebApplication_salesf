using Salesforce.Common.Models.Json;
using Salesforce.Force;
using System.Text.Json.Serialization;

namespace WebApplication_salesf
{
    public class Startup
    {

        public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;

        }



        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

            builder.Services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });



            builder.Services.AddRouting(
                Options => Options.LowercaseUrls = true);



            builder.Services.AddControllersWithViews();

        }









        public static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseRouting();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {

               endpoints.MapControllerRoute(
               name: "CandidatController1",
               pattern: "{controller=CandidatController1}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


             

            });

        }











        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////
        /// </summary>
        public static string _clientId = "3MVG9t0sl2P.pBypzkiD0_pOUMPYFHJwl8uuP9QOndYUl0YLIP.kE6PdBPUUGzHdkOkbah0ZRhFE.U6eCOoks";
        public static string _clientSecret = "6A4E71DFA91BAE8F7DDFE58D9B8B87CA8EC70DD75F540EE14E72213E217AA728";
        private static string _username = "bouchera.amr@gmail.com";
        private static string _securityToken = "qNg2LCKx1H4F0LABxn0yZC8D";
        private static string _password = "Ab15/03/1998/1998" + _securityToken;
        private static string _tokenRequestEndpointUrl = "https://login.salesforce.com/services/oauth2/token";

        static void Main(string[] args)
        {
            var auth = new Salesforce.Common.AuthenticationClient();
            auth.UsernamePasswordAsync(_clientId, _clientSecret, _username, _password).Wait();

            var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
            Task<QueryResult<dynamic>> results = client.QueryAllAsync<dynamic>("SELECT Id, Name from Account");
            results.Wait();
            var contacts = results.Result.Records;
            foreach (var contact in contacts)
            {
                Console.WriteLine(contact.Id);

                Console.Write(contact.Name);




            }

            Console.WriteLine("press any key to continue");
            Console.ReadLine();



        }





    }
}
