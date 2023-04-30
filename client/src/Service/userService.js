import axiosClient from "../Utils/axiosClient";

const getProfile = (id) => {
  return axiosClient
    .get(`/user-profile/${id}`)
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });
};

const userService = {
  getProfile,
};
export default userService;
