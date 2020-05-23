using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using Gtk;

namespace Solver {
    public class SolverApp {
        private static SolverApp _instance;
        private string _jwt;
        private string email;
        private Application application;
        private RestClient client;

        private SolverApp(Application app) {
            application = app;
            _jwt = "";
            client = new RestClient(DotNetEnv.Env.GetString("SERVER_ADDRESS"));
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

        public static void SetEmail(string email) {
            _instance.email = email;
        }

        public static string GetEmail() {
            return _instance.email;
        }

        public static Application GetApp() {
            return _instance.application;
        }

        public static async Task<JObject> HttpRequest(string endpoint, JObject body, Method method) {
            var request = new RestRequest(endpoint, method);
            request.AddHeader("auth_token", GetJwt());

            string requestBody = body.ToString();
            JObject result = null;

            request.AddParameter("application/json; charset=utf-8", requestBody, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            try {
                var response = await _instance.client.ExecuteAsync(request);
                result = JObject.Parse(response.Content);
            } catch (Exception e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return result;
        }

        public static async Task<JObject> PostRequest(string endpoint, JObject body) {
            return await HttpRequest(endpoint, body, Method.POST);
        }

        public static async Task<JObject> PutRequest(string endpoint, JObject body) {
            return await HttpRequest(endpoint, body, Method.PUT);
        }

        public static async Task<JObject> GetRequest(string endpoint) {
            var request = new RestRequest(endpoint, Method.GET);
            request.AddHeader("auth_token", GetJwt());

            JObject result = null;

            request.RequestFormat = DataFormat.Json;

            try {
                var response = await _instance.client.ExecuteAsync(request);
                result = JObject.Parse(response.Content);
            } catch (Exception e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return result;

        }
    }
}