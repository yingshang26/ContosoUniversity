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

        //����������ĵ����ݿ⣬�� EnsureCreated ������ִ���κβ����� ���û�����ݿ⣬�������������ݿ�ͼܹ��� 
        //EnsureCreated���������ݿ�

        //EnsureCreated �������¹���������������ģ�͸��ģ�
        //ɾ�����ݿ⡣ �κ��������ݶ�ʧ��
        //��������ģ�͡� ���磬��� EmailAddress �ֶΡ�
        //����Ӧ�á�
        //EnsureCreated ���������¼ܹ������ݿ⡣

        //�����豣�����ݵ�����£����ܹ����ٷ�չʱ���˲��������ڿ��������б������á� 
        //�����Ҫ�������������ݿ�����ݣ������������ͬ�ˡ� ����������������ʹ��Ǩ�ơ�
        //�˷������������ݿⲻ��ͨ��Ǩ�Ƶķ�ʽ���и���
        private static void CreateDbIfNotExists(IHost host)
        {
            //todo:CreateScopeʲô��˼
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
