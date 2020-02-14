using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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

		public static Dictionary<string, string> arrayDict =   // Ejemplo Diccionario para ser incluido in-memory en la configuración del Host
		
		new Dictionary<string, string>
		{
			{"array:entries:0", "value0"},
			{"array:entries:1", "value1"},
			{"array:entries:2", "value2"},
			{"array:entries:4", "value4"},
			{"array:entries:5", "value5"}
		};
	
		 public static readonly Dictionary<string, string> _switchMappings =
		 new Dictionary<string, string>				// Diccionario para mapeos de linea de commandos, son como abreviaturas
		 {
			{ "-CLKey1", "CommandLineKey1" },		// Si se recibe -CLkey1 se interpretará como CommandLineKey1
			{ "-CLKey2", "CommandLineKey2" }
		 };

	 
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				  .ConfigureHostConfiguration(config =>              // Configuración del Host. Puede ser llamado varias veces
				  {
					  var dict = new Dictionary<string, string>
						{
							{"MemoryCollectionKey1", "value1"},
							{"MemoryCollectionKey2", "value2"}
						};

					  config.AddInMemoryCollection(dict);
				  })                                               // Fin de Configuración del Host

				.ConfigureAppConfiguration((hostingContext, config) =>   // Configuración de la configuración, para especifar mas proveedores que los de por defecto
				{
					//config.Sources.Clear();     // si quisieramos borrar los proveedores por defecto

					config.AddInMemoryCollection(arrayDict);
					config.AddJsonFile(
					 "json_array.json", optional: false, reloadOnChange: false);
					config.AddJsonFile(
					 "starship.json", optional: false, reloadOnChange: false);
					config.AddXmlFile(
					 "tvshow.xml", optional: false, reloadOnChange: false);
					config.AddIniFile(
					   "config.ini", optional: true, reloadOnChange: true);
					config.AddEnvironmentVariables(prefix: "PREFIX_");             // todas las vars con ese prefijo. el prefijo será omitido cuando se carguen
					
					//config.AddEFConfiguration(									// Config EF
					//options => options.UseInMemoryDatabase("InMemoryDb"));

					config.AddCommandLine(args);   // la config con argumentos tendrás mas prioridad que lo anterior. 
												   //ej  dotnet run CommandLineKey1=value1 --CommandLineKey2=value2 /CommandLineKey3=value3

					config.AddCommandLine(args, _switchMappings);  // tambien pueden usarse switchMappings, ver diccionario arriba. 
																   // dotnet run -CLKey1=value1 -CLKey2=value2 daria value1 a la key CommandLineKey1

				})
				.ConfigureWebHostDefaults(webBuilder =>
				{

					//webBuilder.ConfigureKestrel(serverOptions =>
					//{
					//	serverOptions.Limits.MaxConcurrentConnections = 100;          //Max conexiones concurrentes
					//	serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
					//	serverOptions.Limits.MaxRequestBodySize = 10 * 1024;          // Tamaño maximo de paquete
					//	serverOptions.Limits.MinRequestBodyDataRate =
					//		new MinDataRate(bytesPerSecond: 100,
					//			gracePeriod: TimeSpan.FromSeconds(10));
					//	serverOptions.Limits.MinResponseDataRate =
					//		new MinDataRate(bytesPerSecond: 100,
					//			gracePeriod: TimeSpan.FromSeconds(10));
					//	serverOptions.Listen(IPAddress.Loopback, 5000);        // que puerto ha de escuchar el servidor
					//	serverOptions.Listen(IPAddress.Loopback, 5001,
					//		listenOptions =>
					//		{
					//			//listenOptions.UseHttps("testCert.pfx", "testPassword"); // Uso de HTTPS con cert.
					//		});
					//	serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
					//	serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
					//})
					//.UseStartup<Startup2>();  // Le dice que use la clase startup como punto inicial

					webBuilder.UseStartup<Startup>();  // Le dice que use la clase startup como punto inicial

		});
	}
}
