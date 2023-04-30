import axiosClient from "../Utils/axiosClient";
import Cookies from "js-cookie";
import { useDispatch, useSelector } from "react-redux";
import { selectCurrentAccesToken } from "../Redux/Reducers/Auth/AuthReducer";
const loginService = (mail, password) => {
  return axiosClient
    .post("/login", {
      mail,
      password,
    })
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });
};
const logoutService = () => {
  Cookies.remove("jwt");
};
const getCurrentUser = () => {
  const jwtToken = Cookies.get("jwt");
  return jwtToken;
};
const GetProfile = (mail, token) => {
  const response = axiosClient
    .post(
      "/user-profile",
      {
        mail,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });

  return response;
};

const authService = {
  loginService,
  getCurrentUser,
  logoutService,
  GetProfile,
};

export default authService;
