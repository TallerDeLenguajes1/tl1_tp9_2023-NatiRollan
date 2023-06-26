// See https://aka.ms/new-console-template for more information

using EspacioClaseJson;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        Get();
    }

    private static void Get()
    {
        var url = "https://api.coindesk.com/v1/bpi/currentprice.json";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader != null)
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            Root moneda = JsonSerializer.Deserialize<Root>(responseBody);

                            Console.WriteLine("PRECIOS:");
                            Console.WriteLine(" -"+moneda.bpi.EUR.code+": "+moneda.bpi.EUR.rate_float);
                            Console.WriteLine(" -"+moneda.bpi.GBP.code+": "+moneda.bpi.GBP.rate_float);
                            Console.WriteLine(" -"+moneda.bpi.USD.code+": "+moneda.bpi.USD.rate_float);
                            Console.WriteLine();
                            Console.WriteLine("Seleccione una moneda: EUR - GBP - USD");
                            string? respuesta;
                            respuesta = Console.ReadLine();
                            switch (respuesta)
                            {
                                case "EUR":
                                    Console.WriteLine("Código: " + moneda.bpi.EUR.code);
                                    Console.WriteLine("Simbolo: " + moneda.bpi.EUR.symbol);
                                    Console.WriteLine("Tasa: " + moneda.bpi.EUR.rate);
                                    Console.WriteLine("Descripción: " + moneda.bpi.EUR.description);
                                    break;
                                case "GBP":
                                    Console.WriteLine("Código: " + moneda.bpi.GBP.code);
                                    Console.WriteLine("Simbolo: " + moneda.bpi.GBP.symbol);
                                    Console.WriteLine("Tasa: " + moneda.bpi.GBP.rate);
                                    Console.WriteLine("Descripción: " + moneda.bpi.GBP.description);
                                    break;
                                case "USD":
                                    Console.WriteLine("Código: " + moneda.bpi.USD.code);
                                    Console.WriteLine("Simbolo: " + moneda.bpi.USD.symbol);
                                    Console.WriteLine("Tasa: " + moneda.bpi.USD.rate);
                                    Console.WriteLine("Descripción: " + moneda.bpi.USD.description);
                                    break;
                                default:
                                    Console.WriteLine("No ingresó una moneda correcta");
                                    break;
                            }
                        }
                    }
                }
            }
        }
        catch (WebException)
        {
            Console.WriteLine("Problemas en el acceso a la API");
        }
    }
}



