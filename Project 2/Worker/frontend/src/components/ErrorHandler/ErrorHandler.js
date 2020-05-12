import React, { useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { Alert } from 'react-bootstrap'
import { useIntl } from 'react-intl'
import { useLocation } from 'react-router-dom'

import { errorActions } from '../../actions/errorActions'

import styles from './ErrorHandler.module.scss'

const ErrorHandler = () => {
  const error = useSelector(state => state.error)
  const dispatch = useDispatch()
  const { formatMessage: f } = useIntl()
  const location = useLocation()

  const handleDismiss = () => {
    dispatch(errorActions.dismissError())
  }

  useEffect(() => {
    if (error) {
      dispatch(errorActions.dismissError())
    }
    // eslint-disable-next-line
  }, [location])

  return (
    <>
      {error.status && (
        <div className={styles.absolute}>
          <Alert
            data-testid="error-alert"
            variant="danger"
            onClose={handleDismiss}
            dismissible
          >
            <Alert.Heading>{f({ id: 'error.title' })}</Alert.Heading>
            <p>{error.message}</p>
          </Alert>
        </div>
      )}
    </>
  )
}

export default ErrorHandler
