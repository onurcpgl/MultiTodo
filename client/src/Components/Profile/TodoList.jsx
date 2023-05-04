import React from "react";
import todoService from "../../Service/todoService";
import { useNavigate } from "react-router-dom";

function TodoList({ myTodo, setAddModal, addModal, setMyTodo }) {
  const navigate = useNavigate();
  const deleteTodoHandler = async (todoId) => {
    const response = await todoService.deleteTodo(todoId);
    const result = myTodo.filter((item) => {
      if (item.id !== todoId) {
        return item;
      }
    });
    setMyTodo(result);
  };
  return (
    <div className="flex-1">
      <div className="flex justify-between">
        <p className="text-4xl font-semibold opacity-80">Todolar</p>
        <button
          onClick={() => setAddModal(!addModal)}
          type="button"
          className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
        >
          Todo Ekle
        </button>
      </div>
      <div>
        {myTodo &&
          myTodo?.map((todo, i) => (
            <div className="flex w-full mt-5" key={i}>
              <div className="w-full  py-4 px-6 flex items-center justify-between bg-white border border-gray-200 rounded-lg shadow hover:bg-gray-100 dark:bg-gray-800 dark:border-gray-700 dark:hover:bg-gray-700">
                <div>
                  <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                    {todo.title}
                  </h5>
                  <p className="font-normal text-gray-700 dark:text-gray-400">
                    {todo.description}
                  </p>
                </div>

                <div className="flex flex-col items-center gap-5">
                  <button
                    type="button"
                    className="ml-auto -mx-1.5 -my-1.5 bg-green-50 text-green-500 rounded-lg focus:ring-2 focus:ring-green-700 p-1.5 hover:bg-green-200 inline-flex h-8 w-8 dark:bg-gray-800 dark:text-green-400 dark:hover:bg-gray-700"
                    data-dismiss-target="#alert-3"
                    aria-label="Close"
                  >
                    <span className="sr-only">Check</span>
                    <svg
                      fill="none"
                      stroke="currentColor"
                      strokeWidth="1.5"
                      viewBox="0 0 24 24"
                      xmlns="http://www.w3.org/2000/svg"
                      aria-hidden="true"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        d="M4.5 12.75l6 6 9-13.5"
                      ></path>
                    </svg>
                  </button>
                  <button
                    type="button"
                    onClick={() => deleteTodoHandler(todo.id)}
                    className="ml-auto -mx-1.5 -my-1.5 bg-red-50 text-red-500 rounded-lg focus:ring-2 focus:ring-red-400 p-1.5 hover:bg-red-200 inline-flex h-8 w-8 dark:bg-gray-800 dark:text-red-400 dark:hover:bg-gray-700"
                    data-dismiss-target="#alert-border-2"
                    aria-label="Close"
                  >
                    <span className="sr-only">Dismiss</span>
                    <svg
                      aria-hidden="true"
                      className="w-5 h-5"
                      fill="currentColor"
                      viewBox="0 0 20 20"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <path
                        fillRule="evenodd"
                        d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                        clipRule="evenodd"
                      ></path>
                    </svg>
                  </button>
                </div>
              </div>
            </div>
          ))}
      </div>
    </div>
  );
}

export default TodoList;
