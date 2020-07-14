using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Com.Ctrip.Framework.Apollo;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace testDotNetCore22
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(LoadAppSettingsAndApollo)
                .UseStartup<Startup>();
        }
        public static void LoadAppSettingsAndApollo(IConfigurationBuilder builder)
        {
            var japx = builder.Build().GetSection("Japx");
            var appId = japx["AppId"];
            var metaServer = japx["Apollo.MetaServer"];
            var _outerConsulAddress = japx["OuterConsulAddress"];
            var _currentIp = japx["CurrentIp"];
            var currentPort = japx["CurrentPort"];
            var _currentPort = !string.IsNullOrEmpty(currentPort)
                ? int.Parse(currentPort)
                : 80;

            var apolloBuilder = builder
                .AddApollo(new ApolloOptions { AppId = appId, MetaServer = metaServer })
                .AddDefault()
                .AddNamespace("japx.public")
                .AddNamespace("japx.protected")
                .AddNamespace("japx.db")
                .AddNamespace("japx.redis")
                .AddNamespace("japx.appsecret")
                .AddNamespace("japx.except")
                .AddNamespace("japx.grpc")
                .AddNamespace("japx.route")
                .AddNamespace("japx.manage")
                .AddNamespace("japx.RocketMq");
            Console.WriteLine("ApolloMetaServer:" + metaServer);
            //Request03_ByGet(metaServer);
            var configuration = builder.Build();

            foreach (var i in configuration.GetChildren())
            {
                Console.WriteLine(i.Key + ":" + i.Value);
            }
            Console.WriteLine("-----------------------------------------------");
        }


        public static void Request03_ByGet(string url)
        {

            string address = url; //拼接数据提交的网址和经过中文编码后的中文参数

            HttpWebRequest httpWebRequest = WebRequest.Create(address) as HttpWebRequest;
            httpWebRequest.Method = "GET";

            HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse; // 获取响应
            if (httpWebResponse != null)
            {
                using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string content = sr.ReadToEnd();
                    Console.WriteLine("geturl-" + url + ":" + content);
                }

                httpWebResponse.Close();
            }
        }
    }
}
