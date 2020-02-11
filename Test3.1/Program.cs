using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Test3._1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static Dictionary<string, string> arrayDict =
	   new Dictionary<string, string>
	   {
			{"array:entries:0", "value0"},
			{"array:entries:1", "value1"},
			{"array:entries:2", "value2"},
			{"array:entries:4", "value4"},
			{"array:entries:5", "value5"}
	   };

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				  .ConfigureHostConfiguration(config =>			     // Configuración del Host. Puede ser llamado varias veces
				  {
					  var dict = new Dictionary<string, string>
						{
							{"MemoryCollectionKey1", "value1"},
							{"MemoryCollectionKey2", "value2"}
						};

					  config.AddInMemoryCollection(dict);
				  })                                                 // Fin de Configuración del Host
				  .ConfigureAppConfiguration((hostingContext, config) =>   // Configuración de la configuración, para especifar mas proveedores que los de por defecto
				  {
					  config.Sources.Clear();		// si quisieramos borrar los proveedores por defecto

					  config.AddInMemoryCollection(arrayDict);
					  config.AddJsonFile(
						  "json_array.json", optional: false, reloadOnChange: false);
					  config.AddJsonFile(
						  "starship.json", optional: false, reloadOnChange: false);
					  config.AddXmlFile(
						  "tvshow.xml", optional: false, reloadOnChange: false);
					  config.AddIniFile(
							"config.ini", optional: true, reloadOnChange: true);
					  config.AddEnvironmentVariables(prefix: "PREFIX_");             //todas las vars con ese prefijo. el prefijo será omitido cuando se carguen
					  //config.AddEFConfiguration(
					  //options => options.UseInMemoryDatabase("InMemoryDb"));
					  config.AddCommandLine(args);   // la config con argumentos tendrás mas prioridad. 
					  //ej  dotnet run CommandLineKey1=value1 --CommandLineKey2=value2 /CommandLineKey3=value3

				  })
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();  // Le dice que use la clase startup como punto inicial
				});
	}
}
