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
const getUserTodos = () => {
  return axiosClient
    .get("/get-user-todos")
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });
};
const saveTodo = async (values) => {
  return await axiosClient
    .post("/save-todo", values)
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });
};
const deleteTodo = async (todoId) => {
  return await axiosClient
    .delete(`/delete-todo/${todoId}`)
    .then((response) => {
      return response;
    })
    .catch((error) => {
      return error.response;
    });
};
const todoService = {
  getTodo,
  getUserTodos,
  saveTodo,
  deleteTodo,
};
export default todoService;
