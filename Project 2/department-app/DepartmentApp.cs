namespace Department {
    public class DepartmentApp {
        private static DepartmentApp _instance;
        private string _jwt;


        public static void Init(string address) {
            _instance ??= new DepartmentApp();
        }

        public static DepartmentApp GetInstance() {
            return _instance;
        }

        public static void setJwt(string jwt) {
            _instance._jwt = jwt;
        }

        public static string getJwt() {
            return _instance._jwt;
        }

    }
}