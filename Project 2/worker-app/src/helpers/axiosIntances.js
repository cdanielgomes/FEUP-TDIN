import axios from "axios"


function getIssueAxios() {
    const store = localStorage.getItem("cookie")
    const { auth_token } = store ? JSON.parse(store) : { auth_token: null}
    return auth_token ? axios.create({ baseURL: "http://localhost:3000/api/worker/", headers: { auth_token} })
        : axios.create({ baseURL: "http://localhost:3000/api/worker/" })
}

export const caller = {
    getIssueAxios
}