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

const getByUser = (id) => {
  return axiosClient
    .get(`/user-profile/${id}`)
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });
};
const updateUser = (values) => {
  return axiosClient
    .put("/user-update", values)
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });
};
const userService = {
  getProfile,
  getByUser,
  updateUser,
};
export default userService;
