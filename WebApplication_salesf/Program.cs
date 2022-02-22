

using WebApplication_salesf;

var app = Startup.InitializeApp(args);

var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;



//SeedData.Initialize(services);

app.Run();
