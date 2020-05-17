import axios from "axios"
import EventSource from 'eventsource'

function getIssueAxios() {
    const store = localStorage.getItem("cookie")
    const { auth_token } = store ? JSON.parse(store) : { auth_token: null}
    return auth_token ? axios.create({ baseURL: "http://localhost:3000/api/worker/", headers: { auth_token} })
        : axios.create({ baseURL: "http://localhost:3000/api/worker/" })
}

function getStream() {
    const store = localStorage.getItem("cookie")
    const { auth_token } = store ? JSON.parse(store) : { auth_token: null}
    return auth_token ? new EventSource('http://localhost:3000/api/stream/', { headers: { auth_token} })
        :  new EventSource({ baseURL: "http://localhost:3000/api/stream/" })
}
export const caller = {
    getIssueAxios,
    getStream
}