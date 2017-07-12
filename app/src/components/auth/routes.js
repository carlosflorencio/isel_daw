import OIDCCallback from "./OIDCCallback";
import * as oidcHelpers from "../../helpers/OIDCHelpers";
import * as React from "react";

const BASE = "/auth";

export default [
  {
    path: BASE + "/signin-oidc",
    exact: true,
    render: props =>
      <OIDCCallback
        callback={() => oidcHelpers.createUserManager().signinPopupCallback()}
      />
  },
  {
    path: BASE + "/signout-oidc",
    exact: true,
    render: props =>
      <OIDCCallback
        callback={() => oidcHelpers.createUserManager().signoutPopupCallback()}
      />
  },
  {
    path: BASE + "/silent-renew-oidc",
    exact: true,
    render: props =>
      <OIDCCallback
        callback={() => oidcHelpers.createUserManager().signinSilentCallback()}
      />
  }
];
