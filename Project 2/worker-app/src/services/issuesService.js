import { caller } from '../helpers/axiosIntances'

// get Issues


const getIssues = () => {

  return caller.getIssueAxios().get("")

    .then(({ status, data, statusText }) => {
      if (status === 200) return data.issues
      else return Promise.reject(statusText)
    })
    .catch(error => {
      console.log(error)
      //  localStorage.removeItem('cookie')
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
  console.log(events)
  events.onmessage = (event) => {
    console.log(event)
    onMessage(event)}

  events.onopen = (event) => {
    console.log(event)
    console.log("openned connection")
  }
  events.onerror = (err) => {

    console.log(err)
  }

}


export const issuesService = {
  sendIssue,
  getIssues,
  openStream
};
