import axios from "axios";
import tokenService from "../Service/tokenService";
import { useNavigate } from "react-router-dom";

const axiosClient = axios.create({
  baseURL: `https://localhost:7088/`,
  withCredentials: true,
});

axiosClient.interceptors.request.use(
  function (request) {
    const token = tokenService.getToken();
    if (token) {
      request.headers["Authorization"] = "Bearer " + token;
    }
    return request;
  },
  (error) => {
    Promise.reject(error);
  }
);

axiosClient.interceptors.response.use(
  function (response) {
    return response.data;
  },
  function (error) {
    const originalRequest = error.config;

    if (
      error.response.status === 401 &&
      originalRequest.url === "https://localhost:7083/login"
    ) {
      const navigate = useNavigate();
      navigate("/login");
      return Promise.reject(error);
    }
    console.log("response");
    if (error.response.status === 401 && !originalRequest._retry) {
      localStorage.removeItem("userId");
      localStorage.removeItem("userLoggedIn");
      // originalRequest._retry = true;
      // const refreshToken = tokenService.getToken();
      // return axiosClient
      //   .post("/auth/token", {
      //     refresh_token: refreshToken,
      //   })
      //   .then((res) => {
      //     if (res.status === 201) {
      //       tokenService.setToken(res.data);
      //       axios.defaults.headers.common["Authorization"] =
      //         "Bearer " + tokenService.getToken();
      //       return axios(originalRequest);
      //     }
      //   });
    }
    return Promise.reject(error);
  }
);

export default axiosClient;
