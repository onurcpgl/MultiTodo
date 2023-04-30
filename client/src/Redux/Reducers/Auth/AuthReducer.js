import { createSlice } from "@reduxjs/toolkit";
import Cookies from "js-cookie";

const initialState = {
  user: localStorage.getItem("userLoggedIn") || false,
  token: Cookies.get("jwt") || null,
  email: localStorage.getItem("userEmail") || null,
};
const authReducer = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login: (state, action) => {
      console.log(action);
      state.token = action.payload;
      state.user = true;
      localStorage.setItem("userLoggedIn", true);
      Cookies.set("jwt", action.payload);
    },
    logOut: (state, action) => {
      console.log("kapat");
      state.user = false;
      state.accessToken = null;
      state.email = null;
      localStorage.clear("usserLoggedIn");
      Cookies.remove("jwt");
    },
  },
});

export const { login, logOut } = authReducer.actions;

export default authReducer.reducer;

export const selectCurrentUser = (state) => state.auth.user;
export const selectCurrentAccesToken = (state) => state.auth.token;
export const selectCurrentUserEmail = (state) => state.auth.email;
