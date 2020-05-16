import React, { useState, useEffect } from "react";
import Header from "../../components/Navbar";
import { issuesService } from "../../services/issuesService";
import IssueCard from "../../components/IssueCard";
const Homepage = () => {
  const [issues, setIssues] = useState([]);

  const sendIssue = (issue) => {
    issuesService.sendIssue({ ...issue }).then((newIssue) => {
      setIssues((state) => [...state, newIssue]);
    });
  };

  useEffect(() => {
    issuesService.getIssues().then((issues) => {

      setIssues(issues);
    });
  }, []);

  return (
    <>
      <Header callback={sendIssue} />
      {issues.map((element) => {
        return <IssueCard key={element._id} {...element} />;
      })}
    </>
  );
};

export default Homepage;
