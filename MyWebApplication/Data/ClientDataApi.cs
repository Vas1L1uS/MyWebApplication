using MyWebApplication.AuthClientApp;
using MyWebApplication.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApplication.Data
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

        public string GetUserRole(User user)
        {
            string url = @"https://localhost:44306/api/home/GetUserRole";

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