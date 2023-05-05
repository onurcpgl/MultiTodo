import axiosClient from "../Utils/axiosClient";

const getTeam = () => {
  return axiosClient
    .get("all-team")
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });
};

const userService = {
  getTeam,
};
export default userService;
