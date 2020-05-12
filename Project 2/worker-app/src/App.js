import React from "react";
import { useSelector } from "react-redux";
import { Router, Route, Switch, Redirect } from "react-router-dom";
import { history } from "./helpers/history";

import PrivateRoute from "./components/PrivateRoute/PrivateRoute";
import Login from "./pages/Login";
import Homepage from "./pages/Homepage/homepage";
import Register from "./pages/Register";

const App = () => {
  const loggedIn = useSelector((state) => state.auth.loggedIn);
  const cookie = useSelector((state) => state.auth.auth_token);
  return (
    <Router history={history}>
      <Switch>
        <PrivateRoute exact path="/" component={Homepage} />
        <Route path="/register">
          {cookie && loggedIn ? <Redirect to="/" /> : <Register />}
        </Route>

        <Route exact path="/login">
          {cookie && loggedIn ? <Redirect to="/" /> : <Login />}
        </Route>
        <Redirect from="*" to="/" />
      </Switch>
    </Router>
  );
};

export default App;
