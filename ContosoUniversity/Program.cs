using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ContosoUniversity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //如果有上下文的数据库，则 EnsureCreated 方法不执行任何操作。 如果没有数据库，则它将创建数据库和架构。 
        //EnsureCreated创建空数据库

        //EnsureCreated 启用以下工作流来处理数据模型更改：
        //删除数据库。 任何现有数据丢失。
        //更改数据模型。 例如，添加 EmailAddress 字段。
        //运行应用。
        //EnsureCreated 创建具有新架构的数据库。

        //在无需保存数据的情况下，当架构快速发展时，此操作在早期开发过程中表现良好。 
        //如果需要保存已输入数据库的数据，情况就有所不同了。 如果是这种情况，请使用迁移。
        //此方法创建的数据库不能通过迁移的方式进行更新
        private static void CreateDbIfNotExists(IHost host)
        {
            //todo:CreateScope什么意思
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SchoolContext>();
                    //context.Database.EnsureCreated();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
