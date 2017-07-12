/**
 * Middleware to make async calls to an api
 * Helps reducing the boilerplate
 *
 * Example:
 *
 * function loadPostsAction(userId) {
 *   return {
 *    types: ['LOAD_POSTS_REQUEST', 'LOAD_POSTS_SUCCESS', 'LOAD_POSTS_FAILURE'],
 *    shouldCallAPI: (state) => !state.posts[userId],
 *    callAPI: () => fetch(`http://myapi.com/users/${userId}/posts`),
 *    payload: { userId }
 *  }
 * }
 *
 * More info:
 * http://redux.js.org/docs/recipes/ReducingBoilerplate.html#userinfojs
 *
 * @param dispatch
 * @param getState
 * @returns {function(*): function(*=)}
 */
export default function callAPIMiddleware({ dispatch, getState }) {
  return next => action => {
    const { types, callAPI, shouldCallAPI = () => true, payload = {} } = action;

    if (!types) {
      // Normal action: pass it on
      return next(action);
    }

    if (
      !Array.isArray(types) ||
      types.length !== 3 ||
      !types.every(type => typeof type === "string")
    ) {
      throw new Error("Expected an array of three string types.");
    }

    if (typeof callAPI !== "function") {
      throw new Error("Expected callAPI to be a function.");
    }

    if (!shouldCallAPI(getState())) {
      return;
    }

    const [requestType, successType, failureType] = types;

    dispatch(
      Object.assign({}, payload, {
        type: requestType
      })
    );

    return (
      callAPI()
        // .then(x => new Promise(resolve => setTimeout(() => resolve(x), 2000))) // delay for testing
        .then(
          response =>
            dispatch(
              Object.assign({}, payload, {
                response,
                type: successType
              })
            ),
          error =>
            dispatch(
              Object.assign({}, payload, {
                error,
                type: failureType
              })
            )
        )
    );
  };
}
