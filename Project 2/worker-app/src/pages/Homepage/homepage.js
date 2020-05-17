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

    issuesService.openStream((event) => {
        
      const data = JSON.parse(event.data)
      console.log("DATA ON OPENING SOURCE", data)
    })
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
