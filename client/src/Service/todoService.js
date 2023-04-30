import axiosClient from "../Utils/axiosClient";

const getTodo = () => {
  return axiosClient
    .get("/all-todo")
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });
};

const todoService = {
  getTodo,
};
export default todoService;
