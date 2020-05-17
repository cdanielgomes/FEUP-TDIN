using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Gtk;

namespace Solver {
    public class SolverApp {
        private static SolverApp _instance;
        private string _jwt;
        private Application application;
        private HttpClient client;

        private SolverApp(Application app) {
            DotNetEnv.Env.Load();
            application = app;
            _jwt = "";
            InitHttpClient();
        }

        public static void Init(Application app) {
            _instance ??= new SolverApp(app);
        }

        public static SolverApp GetInstance() {
            return _instance;
        }

        public static void SetJwt(string jwt) {
            _instance._jwt = jwt;
        }

        public static string GetJwt() {
            return _instance._jwt;
        }

        public static Application GetApp() {
            return _instance.application;
        }

        private void InitHttpClient() {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void setJwtHeader(String accessToken) {
            _instance.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        }

        public static async Task<String> GetRequest(String endpoint) {
            String url = DotNetEnv.Env.GetString("SERVER_ADDRESS");

            try {
                var responseBody = await _instance.client.GetStringAsync(url + endpoint);

                Console.WriteLine(responseBody);
                return responseBody;
            } catch (HttpRequestException e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return null;
        }

        public static async Task<String> PostRequest(String endpoint, String body) {
            String url = DotNetEnv.Env.GetString("SERVER_ADDRESS");

            try {
                var content = new StringContent(body);
                var responseBody = await _instance.client.PostAsync(url + endpoint, content);

                var result = await responseBody.Content.ReadAsStringAsync();

                Console.WriteLine(result);
                return result;
            } catch (HttpRequestException e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return null;

        }
    }
}