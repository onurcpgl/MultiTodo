import Cookies from "js-cookie";
const setToken = (token) => {
  Cookies.set("jwt", token);
};

const getToken = () => {
  return Cookies.get("jwt");
};

const tokenService = { setToken, getToken };

export default tokenService;
