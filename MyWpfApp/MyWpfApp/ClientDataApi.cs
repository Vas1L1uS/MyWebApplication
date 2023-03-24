using MyWpfApp.Entities;
using MyWpfApp.AuthClientApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyWpfApp.Data
{
    public class ClientDataApi
    {

        private HttpClient httpClient { get; set; }

        public ClientDataApi()
        {
            httpClient = new HttpClient();
        }
        public void AddClient(Client client)
        {
            string url = @"https://localhost:44306/api/home";

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8,
                mediaType: "application/json")
            ).Result;
        }

        public IEnumerable<Client> GetAll()
        {
            string url = @"https://localhost:44306/api/home";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<Client>>(json);
        }

        public Client Get(int id)
        {
            string url = $"https://localhost:44306/api/home/GetClient/{id}";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<Client>(json);
        }

        public void Edit(Client client)
        {
            string url = @"https://localhost:44306/api/home";

            var r = httpClient.PutAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8,
                mediaType: "application/json")
            ).Result;

        }

        public void Delete(int id)
        {
            string url = $"https://localhost:44306/api/home/{id}";

            var r = httpClient.DeleteAsync(
                requestUri: url
            ).Result;

        }

        public async Task<User> Login(UserLogin user)
        {
            string url = @"https://localhost:44306/api/home/Login";

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,
                mediaType: "application/json")
            ).Result;

            if (r.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await r.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            string url = @"https://localhost:44306/api/home/GetAllUsers";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<User>>(json);
        }

        public bool AddUser(UserRegistration user)
        {
            string url = @"https://localhost:44306/api/home/AddUser";

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,
                mediaType: "application/json")
            ).Result;

            if (r.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetUserRole(string id)
        {
            string url = $"https://localhost:44306/api/home/GetUserRole/{id}";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<string>(json);
        }

        public void DeleteUser(string id)
        {
            string url = $"https://localhost:44306/api/home/DeleteUser/{id}";

            var r = httpClient.DeleteAsync(
                requestUri: url
            ).Result;

        }
    }
}