using HomeExpenses.DTO;
using HomeExpenses.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace HomeExpenses
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddSingleton<IMapperService, MapperService>();
            services.AddSingleton<IValidatorService, ValidatorService>();
            services.AddSingleton<IStoreService, StoreService>();
            services.AddSingleton<IReceiptService, ReceiptService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IUriService>(x =>
            {
                IUriService service = new UriService("base url here through httpansestor");
                return service;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeExpenses API v1.0");
                options.RoutePrefix = "";
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitDB();
        }

        public void InitDB()
        {
            using (var context = new HomeExpensesContext())
            {
                bool created = context.Database.EnsureCreated();

                if (!created)
                    return;

                var store = new StoreDTO
                {
                    Name = "Biedronka",
                    Address = "ul. Uniwersytecka 12, 40-007 Katowice",
                    NIP = "779-10-11-327"
                };
                context.Stores.Add(store);

                var receipt = new ReceiptDTO
                {
                    Id = Guid.NewGuid(),
                    DateTime = DateTime.Parse("2021-10-01 12:08"),
                    PaymentMethod = "Visa Card",
                    Store = store
                };
                context.Receipts.Add(receipt);

                var product = new ProductDTO
                {
                    Id = Guid.NewGuid(),
                    Category = Category.Jedzenie,
                    Name = "Cukier Bia³y 1kg",
                    ItemPrice = 2.49,
                    Ammount = 1f,
                    Discount = 0f,
                    Receipt = receipt
                };
                context.Products.Add(product);

                context.SaveChanges();
            }
        }
    }
}
