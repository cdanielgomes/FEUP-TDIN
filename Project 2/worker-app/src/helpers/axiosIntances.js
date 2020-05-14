import axios from "axios"


function getIssueAxios() {
    const store = localStorage.getItem("cookie")
    const { auth_token, email } = store ? JSON.parse(store) : { auth_token: null, email: null }
    return auth_token && email ? axios.create({ baseURL: "http://localhost:3000/api/users/issue", headers: { auth_token, email } })
        : axios.create({ baseURL: "http://localhost:3000/api/users/issue" })
}


export const caller = {
    getIssueAxios
}