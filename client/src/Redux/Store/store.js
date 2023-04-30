import { configureStore, combineReducers } from "@reduxjs/toolkit";
import authReducer from "../Reducers/Auth/AuthReducer";

// Store'u yapılandır
const rootReducer = combineReducers({
  auth: authReducer,
});

const store = configureStore({
  reducer: rootReducer,
});
export default store;
