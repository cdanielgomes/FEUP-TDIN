import { combineReducers } from "redux";
import auth from "./authReducer";
import error from "./errorReducer";

export default combineReducers({
  auth,
  error,
});
