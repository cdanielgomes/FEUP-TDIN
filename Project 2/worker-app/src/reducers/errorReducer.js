import { errorConstants } from '../actions/errorActions'

const initialState = { status: false, message: null }

const error = (state = initialState, action) => {
  switch (action.type) {
    case errorConstants.ERROR_ALERT:
      return {
        status: true,
        message: action.payload.error,
      }
    case errorConstants.ERROR_DISMISS:
      return {
        status: false,
        message: null,
      }
    default:
      return state
  }
}

export default error
