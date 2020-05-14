import React, { useState } from "react";
import {
  FormLabel,
  FormGroup,
  FormControl,
  Button,
  Container,
  Form,
  Col,
  Row,
} from "react-bootstrap";
import { useDispatch } from "react-redux";
import { authActions } from "../../actions/authActions";

export default function Register() {
  const [fields, setFields] = useState({
    username: "",
    email: "",
    password: "",
    passwordConf: "",
  });

  const dispatch = useDispatch();

  function validateForm() {
    return (
      fields.username.length > 0 &&
      fields.email.length > 0 &&
      fields.password.length > 0 &&
      fields.password === fields.passwordConf
    );
  }

  async function handleSubmit(e) {
    e.preventDefault();
    e.stopPropagation();
    dispatch(authActions.register(fields));
  }

  function changeField(e) {
    e.preventDefault();
    setFields({ ...fields, [e.target.id]: e.target.value });
  }

  function renderForm() {
    return (
      <Container>
        <Row>
          <Col>
            <Form onSubmit={handleSubmit}>
              <FormGroup controlId="username">
                <FormLabel>Username</FormLabel>
                <FormControl
                  autoFocus
                  type="username"
                  value={fields.username}
                  onChange={changeField}
                />
              </FormGroup>
              <FormGroup controlId="email">
                <FormLabel>Email</FormLabel>
                <FormControl
                  autoFocus
                  type="email"
                  value={fields.email}
                  onChange={changeField}
                />
              </FormGroup>
              <FormGroup controlId="password">
                <FormLabel>Password</FormLabel>
                <FormControl
                  type="password"
                  value={fields.password}
                  onChange={changeField}
                />
              </FormGroup>
              <FormGroup controlId="passwordConf">
                <FormLabel>Confirm Password</FormLabel>
                <FormControl
                  type="password"
                  onChange={changeField}
                  value={fields.passwordConf}
                />
              </FormGroup>
              <FormGroup>
                <Button block type="submit" disabled={!validateForm()}>
                  Signup
                </Button>
              </FormGroup>
            </Form>
          </Col>
        </Row>
        <Row className="align-items-center">
          <Col>
            Do you not have an account? <a href="/login"> Log in </a>
          </Col>
        </Row>
      </Container>
    );
  }

  return <Container> {renderForm()}</Container>;
}
