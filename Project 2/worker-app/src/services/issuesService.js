import {caller} from '../helpers/axiosIntances'
// get Issues


const getIssues = () => {

  return caller.getIssueAxios().get("")

    .then(({ status, data, statusText }) => {
      if (status === 200) return data.issues
      else return Promise.reject(statusText)
    })
    .catch(error => {
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

export const issuesService = {
  sendIssue,
  getIssues,
};
