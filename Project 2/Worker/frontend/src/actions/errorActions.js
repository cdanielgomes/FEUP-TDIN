export const errorConstants = {
  ERROR_ALERT: 'ERROR_ALERT',
  ERROR_DISMISS: 'ERROR_DISMISS',
}

export const errorActions = {
  alertError,
  dismissError,
}

function alertError(error) {
  return { type: errorConstants.ERROR_ALERT, payload: { error } }
}

function dismissError() {
  return { type: errorConstants.ERROR_DISMISS }
}
