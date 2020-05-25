const axios_module = require("axios")

const axios = axios_module.create({ baseURL: "http://localhost:3000/api/" })

async function login(email, password) {
  return axios.post(`auth/login`, { email, password })
    .then(
      (resp) => {
        console.log(resp)
        if (resp.status === 200) {
          // set the cookie on header by default
          console.log(resp.data)
          axios.defaults.headers.common['auth_token'] = resp.data.auth_token
          
          localStorage.setItem("cookie", JSON.stringify(resp.data));
          return resp.data
        } else {
          console.log("NOT 200", resp)
          return Promise.reject("Erro")

        }
      }
    )
    .catch(error => {
      console.log("erro", error)
      return error;
    })
}

function logout() {
  // remove user from local storage to log user out
  localStorage.removeItem("cookie");

}

function register(infos) {
  return axios.post("users/", { ...infos, role: "worker" }).then(response => {
    console.log(response.data)

  }).catch(error => {
    console.log(error)
    return Promise.resolve(error);

  })
}

export const authService = {
  login,
  logout,
  register
};
