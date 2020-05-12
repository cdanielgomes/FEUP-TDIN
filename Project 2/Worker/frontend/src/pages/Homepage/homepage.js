import React, { useState, useEffect } from "react";
import Header from "../../components/Navbar";
import { issuesService } from "../../services/issuesService";
import IssueCard from "../../components/IssueCard";
const Homepage = () => {
  const [issues, setIssues] = useState([]);

  const sendIssue = (issue) => {
    issuesService.sendIssue({ ...issue }).then((newIssue) => {
      console.log(newIssue);
      setIssues((state) => [...state, newIssue]);
    });
  };
  useEffect(() => {
    issuesService.getIssues().then((issuesArray) => {
      setIssues(issuesArray);
    });
  }, []);

  return (
    <>
      <Header callback={sendIssue} />
      {issues.map((element) => {
        return <IssueCard key={element.id} {...element} />;
      })}
    </>
  );
};

export default Homepage;
