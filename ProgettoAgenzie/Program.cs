using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace ProgettoAgenzie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IConfigurationBuilder builder = null;
            IHost host = null;
            builder = new ConfigurationBuilder();
            BuildConfing(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                // .WriteTo.Seq("http://localhost:8081")
                .CreateLogger();

            Log.Logger.Information("Application starting up....");
            host = (IHost)Hostbuilder();

            var myServices = ActivatorUtilities
                  .CreateInstance<AgenziaAer>(host.Services);
            myServices.Init();
            

            AgenziaAer ag1 = new AgenziaAer("AgenziaPinco");
            ag1.Init();

            Biglietteria bi = new Biglietteria();
            static void menu() {
                Console.WriteLine("\n*** 1. Acquista Biglietto ***");
                Console.WriteLine("*** 2. Fai il Check In ***");
                Console.WriteLine("*** 3. Vai sul mezzo. Controllo biglietto ***");
                Console.WriteLine("*** 0. Esci ***");
            }
            menu();

            int scelta = 0;
            do
            {
                try {
                    scelta = Convert.ToInt32(Console.ReadLine());
                } catch(Exception ex){
                    Console.WriteLine(ex);
                }

                if (scelta == 1)
                {
                    Console.WriteLine("*** Benvenuto nella Biglietteria PincoPallo ***");
                    Console.WriteLine("++ Fermate principali: ++");
                    foreach (Fermate ferm in Enum.GetValues(typeof(Fermate))) {
                        Console.Write($" {ferm} ");
                    }
                    Console.WriteLine("\n++ Scrivi la città di partenza: ++");
                    string vpartenza = "";
                    try
                    {
                        vpartenza = Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Console.WriteLine("++ Scrivi la città di destinazione: ++");
                    string vdest = "";
                    try
                    {
                        vdest = Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine($"Hai scelto da " + vpartenza + " a " + vdest);

                    Console.WriteLine("Inserisci il tuo nome e cognome: ");
                    string nome = Console.ReadLine();
                    string cognome = Console.ReadLine();
                    Console.WriteLine("Benvenuto: " + nome + " " + cognome);
                    Viaggiatore viagg = new Viaggiatore(nome, cognome);

                    Console.WriteLine("** Tutti i Mezzi Disponibili: **");
                    
                    int mezzot = 0;
                    for(int i=0; i<ag1._amezzo.Length;i++)
                    {
                        Console.Write(i+" ");
                        Console.Write(ag1._amezzo[i]._mezzoID + " ");
                        for (int j=0; j< ag1._amezzo[i]._fermate.Length;j++)
                        {
                            Console.Write(ag1._amezzo[i]._fermate[j].ToString() + " ");
                            if (ag1._amezzo[i]._fermate[0].ToString() == vpartenza && ag1._amezzo[i]._fermate[2].ToString() == vdest)
                            {
                                mezzot = ag1._amezzo[i]._mezzoID;
                            }
                        }
                        Console.Write("\n");
                    }
                    Console.WriteLine($"Il mezzo n° {mezzot} risponde ai requisiti");
                    Console.WriteLine($"Confermi acquisto di questo viaggio?");
                    string confermav = Console.ReadLine();
                    DateTime today = DateTime.Now;
                    if (confermav == "si")
                    {
                        Biglietto bigl = new Biglietto(viagg, ag1, mezzot, today.AddDays(30));
                        Console.WriteLine($"Mezzo n°: {bigl.me._mezzoID}");
                        Console.WriteLine($"Mezzo fermate: {bigl.me._fermate[0]} {bigl.me._fermate[1]} {bigl.me._fermate[2]}");
                        bi.creaViaggio(ag1, mezzot, bigl);

                        bi.GetViaggio(bigl);
                    }
                    ag1.Controllo();
                    menu();

                }
                else if (scelta == 2)
                {
                    Console.WriteLine("** Benvenuto in Agenzia! Inserisci il numero del biglietto: **");
                    var numbigl = Convert.ToInt32(Console.ReadLine());

                    foreach (Viaggio vi in ag1._aviaggio) {
                        if (vi.bi.numID == numbigl) {
                            Console.WriteLine("Biglietto trovato. Check In Completato!");
                            Console.WriteLine($"Data viaggio: {vi.date} Mezzo: {vi.mez._mezzoID} viaggio n°: {vi.numViaggio}");
                            Console.WriteLine($"Biglietto n°: {vi.bi.numID} Nome: {vi.bi.viagg._nome} Cognome: {vi.bi.viagg._cognome}");
                            break;
                        } else {
                            Console.WriteLine("Biglietto non trovato!");
                            break;
                        }
                    }
                    menu();
                }
                else if (scelta == 3)
                {
                    Console.WriteLine($"** In quale mezzo vuoi salire? inserisci il n° mezzo: **");
                    int nummezz = Convert.ToInt32(Console.ReadLine());
                    Controllore controllore=null;
                    Biglietto bigli = null;
                    foreach (Mezzo i in ag1._amezzo) {
                        if(i._mezzoID == nummezz) {
                            controllore = i._controllore;
                            bigli = i.Posti[0];
                        }
                    }
                    Console.WriteLine($"** Sono {controllore._nome}, mostrami il tuo biglietto: **");
                    int numbigl = Convert.ToInt32(Console.ReadLine());
                    controllore.CheckTicket(nummezz, bigli);

                    menu();
                }
            } while (scelta != 0);
        }
        static void BuildConfing(IConfigurationBuilder builder)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var parameter = (!string.IsNullOrEmpty(env)) ? string.Concat(env, ".") : string.Empty;

            builder.SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile($"appsettings.{parameter}json", optional: false, reloadOnChange: true);
        }
        static IHost Hostbuilder()
        {
            return Host.CreateDefaultBuilder()
                 .ConfigureServices((context, services) =>
                 {
                     services.AddTransient<IAgenzia, AgenziaAer>();
                 })
                 .UseSerilog()
                 .Build();
        }
    }
}
