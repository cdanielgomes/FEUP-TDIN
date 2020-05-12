import { authService } from "../services/authService";
import { history } from "../helpers/history";

import { errorActions } from "./errorActions";

export const authConstants = {
  LOGIN_REQUEST: "USERS_LOGIN_REQUEST",
  LOGIN_SUCCESS: "USERS_LOGIN_SUCCESS",
  LOGIN_FAILURE: "USERS_LOGIN_FAILURE",
  REGISTER_REQUEST: "USER_REGISTER_REQUEST",
  REGISTER_SUCCESS: "USER_REGISTER_SUCCESS",
  REGISTER_FAILURE: "USER_REGISTER_FAILURE",

  LOGOUT: "USERS_LOGOUT",
};

export const authActions = {
  login,
  logout,
  register,
};

function login(username, password) {
  return (dispatch) => {
    dispatch(request({ username }));

    authService.login(username, password).then(
      (infos) => {
        dispatch(success(infos));
        history.push("/");
      },
      (error) => {
        dispatch(errorActions.alertError(error));
        dispatch(failure(error));
      }
    );
  };

  function request() {
    return { type: authConstants.LOGIN_REQUEST };
  }
  function success(infos) {
    return { type: authConstants.LOGIN_SUCCESS, payload: infos };
  }
  function failure(error) {
    return { type: authConstants.LOGIN_FAILURE, payload: error };
  }
}

function register(infos) {
  return (dispatch) => {
    dispatch(request());

    authService.register(infos).then(
      (response) => {
        dispatch(success(response));
        history.push("/");
      },
      (error) => {
        dispatch(failure(error));
        history.push("/register");

      }
    );
  };

  function request() {
    return { type: authConstants.REGISTER_REQUEST };
  }
  function success(infos) {
    return {
      type: authConstants.REGISTER_SUCCESS,
      payload: infos,
    };
  }
  function failure(error) {
    return { type: authConstants.REGISTER_FAILURE, payload: error };
  }
}

function logout() {
  authService.logout().then(
    (response) => {
      console.log(response)
    },
    error => {
      console.log(error)
    }
  )

  history.push("/login");
  return { type: authConstants.LOGOUT };
}
