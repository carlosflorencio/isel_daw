import { UserManager, WebStorageStateStore } from 'oidc-client'

/*
 |--------------------------------------------------------------------------
 | OpenID Connect Helpers
 |--------------------------------------------------------------------------
 */

export function createUserManager () {
  const port = window.location.port ? `:${window.location.port}` : ''
  const currentUrl = `${window.location.protocol}//${window.location.hostname}${port}`

  return new UserManager({
    client_id: 'daw-app',
    authority: 'http://localhost:5000',
    redirect_uri: `${currentUrl}/auth/signin-oidc`,
    post_logout_redirect_uri: `${currentUrl}/auth/signout-oidc`,
    silent_redirect_uri: `${currentUrl}/auth/silent-renew-oidc`,
    response_type: 'token id_token',
    scope: 'openid profile user daw_api',
    automaticSilentRenew: true,
    filterProtocolClaims: true,
    loadUserInfo: true,
    userStore: new WebStorageStateStore({store: window.localStorage}),
    popupWindowFeatures: popupConfiguration(1000, 700)
  })
}

/**
 * Helper function to center the popup
 * Works on single/dual MONITOR
 * @param w popup width
 * @param h popup height
 * @returns {string}
 */
function popupConfiguration (w, h) {
  // Fixes dual-screen position                         Most browsers      Firefox
  var dualScreenLeft = window.screenLeft !== undefined ? window.screenLeft : window.screen.left
  var dualScreenTop = window.screenTop !== undefined ? window.screenTop : window.screen.top

  let auxWidth = document.documentElement.clientWidth ? document.documentElement.clientWidth : window.screen.width
  let width = window.innerWidth ? window.innerWidth : auxWidth
  let auxHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : window.screen.height
  let height = window.innerHeight ? window.innerHeight : auxHeight

  var left = ((width / 2) - (w / 2)) + dualScreenLeft
  var top = ((height / 2) - (h / 2)) + dualScreenTop

  return 'scrollbars=location=no,toolbar=no,width=' + w + ', height=' + h + ', top=' + top + ', left=' + left
}
