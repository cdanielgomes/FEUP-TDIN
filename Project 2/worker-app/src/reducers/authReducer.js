import { authConstants } from "../actions/authActions";

const cookie = JSON.parse(localStorage.getItem("cookie"));
const initialState = cookie
  ? { loggedIn: true, ...cookie }
  : { loggedIn: false, auth_token: null };

const auth = (state = initialState, action) => {
  switch (action.type) {
    case authConstants.LOGIN_REQUEST:
      return {
        loggingIn: true,
      };
    case authConstants.LOGIN_SUCCESS:
      return {
        loggedIn: true,
        ...action.payload,
      };
    case authConstants.LOGIN_FAILURE:
      return { loggedIn: false, auth_token: null };
    case authConstants.LOGOUT:
      return { loggedIn: false, auth_token: null };
    case authConstants.REGISTER_REQUEST:
      return {
        loggingIn: true,
      };
    case authConstants.REGISTER_SUCCESS:
      return {
        loggedIn: true,
        ...action.payload,
      };
    case authConstants.REGISTER_FAILURE:
      return { loggedIn: false, auth_token: null };
    default:
      return state;
  }
};

export default auth;
