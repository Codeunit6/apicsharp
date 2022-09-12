using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ejercicio03_apis
{
    class Program
    {
        private static void pokemons(int id, int i)
        {
            //consultar api rest
            var usuario = new HttpClient();
            var url = $"https://pokeapi.co/api/v2/pokemon/{id}";
            usuario.DefaultRequestHeaders.Clear();
            var consulta = usuario.GetAsync(url).Result;
            var data = consulta.Content.ReadAsStringAsync().Result;
            try
            {
                //obj dinamico 
                dynamic response = JObject.Parse(data);
                string json = response.ToString();
                dynamic pokemon = JsonConvert.DeserializeObject<dynamic>(json);
                var nombre = pokemon.name;
                Console.WriteLine($"Pokemon No.{i}: {nombre}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"El error es: {ex}");
            }
        }

        private static void users(int id, int a)
        {
            //Consultar api rest
            var nombres = new HttpClient();
            var url = $"http://api-esp32-alexa.herokuapp.com/api/users/{id}";
            nombres.DefaultRequestHeaders.Clear();
            var conversion = nombres.GetAsync(url).Result;
            var data = conversion.Content.ReadAsStringAsync().Result;

            try
            {
                //obj dinamico
                dynamic response = JObject.Parse(data);
                string json = response.ToString();
                dynamic user = JsonConvert.DeserializeObject<dynamic>(json);
                //campos de la api
                var nombre = user.first_name;
                var apellido = user.last_name;
                Console.WriteLine($"User No.{a}: {nombre} {apellido}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"El error es: {ex}");
            }
        }

        static void hPokemon()
        {
            for (int i = 1; i <= 100; i++)
            {
                Random response = new Random();
                pokemons(response.Next(1, 905), i);
            }
        }

        static void hUser()
        {
            for (int a = 1; a <= 100; a++)
            {
                Random response = new Random();
                users(response.Next(1, 100), a);
            }
        }

        static void Main(string[] args)
        {
            Thread pokerandomH = new Thread(hPokemon);
            Thread useandomH = new Thread(hUser);
            pokerandomH.Start();
            useandomH.Start();
        }
    }
}