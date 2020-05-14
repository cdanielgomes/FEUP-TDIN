import React, { useState } from "react";
import { Navbar, Nav, Button, Modal, Form } from "react-bootstrap";
import { useSelector } from "react-redux";
import { useDispatch } from "react-redux";
import { authActions } from "../../actions/authActions";
const Header = ({ callback }) => {
  // const login = useSelector((state) => state.auth.loggedIn);
  const [showModal, setShowModal] = useState(false);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const name = useSelector((state) => state.auth.username);

  const dispatch = useDispatch();

  const validation = () => {
    return title.length > 0 && description.length > 0;
  };

  const handleForm = (e) => {
    setShowModal(true);
  };
  const closeForm = (e) => {
    //   e.preventDefault();
    setTitle("");
    setDescription("");
    setShowModal(false);
  };

  const changeTitle = (e) => {
    setTitle(e.target.value);
  };

  const changeDescription = (e) => {
    setDescription(e.target.value);
  };

  const sendTicket = (e) => {
    e.preventDefault();
    callback({title, description });
    closeForm();
  };

  const logout = (e) => {
    e.preventDefault();
    e.stopPropagation();
    dispatch(dispatch(authActions.logout));
  };

  return (
    <>
      <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
        <Navbar.Brand href="#">{name}</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav>
            <Nav.Link>
              <Button onClick={handleForm}> New Issue </Button>{" "}
            </Nav.Link>
            <Nav.Link>
              <Button onClick={logout}>Logout</Button>
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>

      <Modal show={showModal} onHide={closeForm}>
        <Modal.Header closeButton>
          <Modal.Title>New Ticket</Modal.Title>
        </Modal.Header>

        <Modal.Body>
          <Form>
            <Form.Group controlId="modalFormTitle">
              <Form.Label>Title</Form.Label>
              <Form.Control value={title} onChange={changeTitle} />
            </Form.Group>
            <Form.Group controlId="modalFormDescription">
              <Form.Label>Description</Form.Label>
              <Form.Control
                as="textarea"
                value={description}
                onChange={changeDescription}
              ></Form.Control>
            </Form.Group>
          </Form>
        </Modal.Body>

        <Modal.Footer>
          <Button variant="secondary" onClick={closeForm}>
            Close
          </Button>
          <Button
            disabled={!validation()}
            variant="primary"
            onClick={sendTicket}
          >
            Save changes
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default Header;
