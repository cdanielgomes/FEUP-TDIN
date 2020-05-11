function login(username, password) {
  const cookie = "cookie";
  const email = "email";
  const sensitiveInfo = {
    username,
    email,
    cookie,
  };
  localStorage.setItem("cookie", JSON.stringify(sensitiveInfo));

  return Promise.resolve(sensitiveInfo);
}

function logout() {
  // remove user from local storage to log user out
  localStorage.removeItem("cookie");
}

function register(username, password, email) {
  const cookie = "cookie";
  const sensitiveInfo = {
    username,
    email,
    cookie,
  };
  localStorage.setItem("cookie", JSON.stringify(sensitiveInfo));
  return Promise.resolve(sensitiveInfo);
}

export const authService = {
  login,
  logout,
  register,
};
