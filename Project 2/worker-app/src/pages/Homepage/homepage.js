import React, { useState, useEffect, useRef } from "react";
import Header from "../../components/Navbar";
import { issuesService } from "../../services/issuesService";
import IssueCard from "../../components/IssueCard";
import { useDispatch } from "react-redux";
import { authActions } from "../../actions/authActions";

const Homepage = () => {

  const [allIssues, _setAllIssues] = useState([]);
  const allIssuesRef = useRef(allIssues)

  const setAllIssues = data => {
    allIssuesRef.current = data;
    _setAllIssues(data)
  }

  const dispatch = useDispatch();


  const sendIssue = (issue) => {
    issuesService.sendIssue({ ...issue }).then((newIssue) => {
      setAllIssues([...allIssuesRef.current, newIssue]);
    }).catch(() =>
      dispatch(dispatch(authActions.logout)))
  };

  useEffect(() => {

    issuesService.getIssues().then((issues) => {
      setAllIssues(issues);

      issuesService.openStream((event) => {
        const issueUpdated = JSON.parse(event.data)

        const index = allIssuesRef.current.findIndex(element => {
          return element._id === issueUpdated._id
        })

        const tmp = [...allIssuesRef.current]
        tmp[index] = issueUpdated

        setAllIssues(tmp)
      })
    }).catch(error =>
      dispatch(dispatch(authActions.logout))
    )

  }, [dispatch]);

  return (
    <>
      <Header callback={sendIssue} />
      {allIssues.map((element) => {
        return <IssueCard key={element._id} {...element} />;
      })}
    </>
  );
};

export default Homepage;
