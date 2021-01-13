using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HCSchemaFirst
{
    public class Startup
    {
        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
        }
        
        public class Query
        {
            public Book GetBook() => new Book { Title  = "C# in depth", Author = "Jon Skeet" };
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddDocumentFromString(@"
                    type Query {
                      book: Book
                    }

                    type Book {
                      title: String
                      author: String
                    }
                ")
                .BindComplexType<Query>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}