import { UserManager } from 'oidc-client';
import { storeUserError, storeUser } from '../actions/authActions'

const config = {
  authority: "https://localhost:5001",
  client_id: "wewantdoughnuts",
  redirect_uri: "https://localhost:3000/signin-oidc",
  response_type: "id_token token doughnutapi",
  scope: "openid profile",
  post_logout_redirect_uri: "https://localhost:3000/signout-oidc",
};

const userManager = new UserManager(config)

export async function loadUserFromStorage(store) {
  try {
    let user = await userManager.getUser()
    if (!user) { return store.dispatch(storeUserError()) }
    store.dispatch(storeUser(user))
  } catch (e) {
    console.error(`User not found: ${e}`)
    store.dispatch(storeUserError())
  }
}

export function getSigninRedirectUrl() {
  return userManager.createSigninRequest({ state: { bar: 15 } }).then(req => {
    console.log('signinRedirect request', req, req.url)
    return req.url
  })
}

export function signinRedirect() {
 
  return userManager.signinRedirect()
}

export function signinRedirectCallback() {
  return userManager.signinRedirectCallback()
}

export function signoutRedirect() {
  userManager.clearStaleState()
  userManager.removeUser()
  return userManager.signoutRedirect()
}

export function signoutRedirectCallback() {
  userManager.clearStaleState()
  userManager.removeUser()
  return userManager.signoutRedirectCallback()
}

export default userManager