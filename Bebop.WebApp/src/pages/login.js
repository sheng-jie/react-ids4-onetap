import React, { useEffect, useLayoutEffect, useState } from 'react'
import { getSigninRedirectUrl, signinRedirect } from '../services/userService'
import { Redirect } from 'react-router-dom'
import { useSelector } from 'react-redux'

function Login() {
  const user = useSelector(state => state.auth.user)
  const [redirect_uri, setRedirect_uri] = useState('');

  function login() {
    signinRedirect()
  }

  const getUrl = async () => {
    const url = await getSigninRedirectUrl()
    setRedirect_uri(url)
  }

  useEffect(() => {
    getUrl()
  }, []);

  return (
    (user) ?
      (<Redirect to={'/'} />)
      :
      (
        <div>
          <h1>Hello!</h1>
          <p>Welcome to We Want Doughnuts.</p>
          <p>A demo of using React and Identity Server 4 to authenticate a user via OpenID Connect to gain access to a web API (and some lovely doughnuts).</p>
          <p>Start by signing in.</p>
          <p>💡 <strong>Tip: </strong><em>User: 'alice', Pass: 'alice'</em></p>
          <div id="g_id_onload" data-client_id="xxxxxx-aidp32ftfirak3nrdmpsc9jjrdrtl43q.apps.googleusercontent.com"
            data-login_uri='https://localhost:5001/api/one-tap-google' data-state="cn" data-lang="en"
            data-redirect_uri={redirect_uri}
            data-cancel_on_tap_outside="false">
          </div>


          <button onClick={() => login()}>Login</button>
          <p><a target='_blank' rel='noopener noreferrer' href='https://github.com/tappyy/react-IS4-auth-demo'>Github Repo</a></p>
        </div>
      )
  )
}

export default Login
