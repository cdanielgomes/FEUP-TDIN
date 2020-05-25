import { caller } from '../helpers/axiosIntances'

const getIssues = () => {

  return caller.getIssueAxios().get("")

    .then(({ status, data, statusText }) => {
      if (status === 200) return data.issues
      else {
        localStorage.removeItem("cookie")
        return Promise.reject(statusText)}
    })
    .catch(error => {
      console.log(error)
      localStorage.removeItem('cookie')
      return Promise.reject(error)
    })
};

// send Issues
const sendIssue = (issue) => {

  return caller.getIssueAxios().post("", issue)
    .then(({ data, status, statusText }) => {
      return status === 200 ? data.issue : Promise.reject(statusText)
    })
    .catch(error => {
      return Promise.reject(error)
    })
};

const openStream = (onMessage) => {

  const events = caller.getStream();

  events.onmessage = (event) => {
    onMessage(event)
  }

  events.onopen = (event) => {
    console.log(event)
    console.log("openned connection")
  }
  events.onerror = (event) => {
    console.log(event)
  }

}


export const issuesService = {
  sendIssue,
  getIssues,
  openStream
};
