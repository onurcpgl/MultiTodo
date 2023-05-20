import { createSlice } from "@reduxjs/toolkit";
import Cookies from "js-cookie";
import jwt_decode from "jwt-decode";
const initialState = {
  user: localStorage.getItem("userLoggedIn") || false,
  token: Cookies.get("jwt") || null,
  userId: localStorage.getItem("userId") || null,
};
const authReducer = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login: (state, action) => {
      state.token = action.payload;
      var decoded = jwt_decode(action.payload);
      state.user = true;
      localStorage.setItem("userId", decoded.userId);
      localStorage.setItem("userLoggedIn", true);
      Cookies.set("jwt", action.payload);
    },
    logOut: (state, action) => {
      state.user = false;
      state.accessToken = null;
      state.userId = null;
      localStorage.clear("usserLoggedIn");
      localStorage.clear("userId");
      Cookies.remove("jwt");
    },
  },
});

export const { login, logOut } = authReducer.actions;

export default authReducer.reducer;

export const selectCurrentUser = (state) => state.auth.user;
export const selectCurrentAccesToken = (state) => state.auth.token;
export const selectCurrentUserId = (state) => state.auth.userId;
