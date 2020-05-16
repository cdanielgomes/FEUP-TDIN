import React from "react";
import { Card, Container, Row, Col } from "react-bootstrap"
import moment from "moment"
import styles from "./IssueCard.module.scss";

const IssueCard = ({ title, createdAt, state, description }) => {

  const x = () => {
    return moment(createdAt).format('LLLL')
  }
  
  return (
    <Card className={styles["issueCard"]}>
      <Card.Header as="h5" className={styles[`issue-${state}`]}>
        <Container fluid>
          <Row className={styles.issueCardHeader}>
            <Col>{title}</Col>
            <Col className={styles.state}>{state}</Col>
          </Row>
        </Container>
      </Card.Header>
      <Card.Body>
        <Card.Text>{description}</Card.Text>
      </Card.Body>
      <Card.Footer> {x()} </Card.Footer>
    </Card>
  );
};

export default IssueCard;
