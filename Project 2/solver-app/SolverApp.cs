using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using Gtk;

namespace Solver {
    public class SolverApp {
        private static SolverApp _instance;
        private string _jwt;
        private Application application;
        private RestClient client;

        private SolverApp(Application app) {
            DotNetEnv.Env.Load();
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

        public static Application GetApp() {
            return _instance.application;
        }

        public static async Task<String> PostRequest(string endpoint, JObject body) {
            var request = new RestRequest(endpoint, Method.POST);

            string requestBody = body.ToString();
            string result = null;

            request.AddParameter("application/json; charset=utf-8", requestBody, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            try {
                var response = await _instance.client.ExecuteAsync(request);
                result = response.Content;
            } catch (Exception e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return result;

        }
    }
}