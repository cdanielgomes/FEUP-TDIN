const axios_module = require("axios")

const axios = axios_module.create({ baseURL: "http://localhost:3000/api/auth" })

async function login(email, password) {
  console.log(axios)
  return axios.post(`/login`, { email, password })
    .then(
      ({ data, status }) => {
        if (status === 200) {
          localStorage.setItem("cookie", JSON.stringify(data));
          return data
        }
      }
    )
    .catch(error => {
      console.log("erro", error)
      return error;
    })
}

function logout() {
  const {auth_token, email, username} = JSON.parse(localStorage.getItem("cookie"))
  // remove user from local storage to log user out
  return axios.get("/logout", {session:{email}, headers: { auth_token }, params: {email} }).then(
    (status, data) => {
      console.log(data)
      console.log(status)
      if (status === 200) {
        localStorage.removeItem("cookie");
        return data
      } else return Promise.reject("Asneira")
    }
  )
  .catch(error => {
    console.log(error)
    Promise.reject(error)
  })
}

function register(infos) {
  return axios_module.post("http://localhost:3000/api/users/", { ...infos, role: "worker" }).then(response => {

    // localStorage.setItem("cookie", JSON.stringify(sensitiveInfo));
    console.log(response.session)

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
