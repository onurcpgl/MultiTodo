import { Routes, Route } from "react-router-dom";
import Home from "../Pages/Home/Home";
import RootLayout from "../Layouts/RootLayout";
import Profile from "../Pages/Profile/Profile";
import Login from "../Components/Login";
import Register from "../Components/Register";
import Team from "../Pages/Team/Team";
import ProtectedRoute from "./ProtectedRoute";
function AppRoute() {
  return (
    <Routes>
      <Route>
        <Route path="" element={<RootLayout />}>
          <Route path="/" element={<Home />} />
          <Route path="/teams" element={<Team />} />
        </Route>
      </Route>
      <Route path="/Login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="" element={<ProtectedRoute />}>
        <Route path="" element={<RootLayout />}>
          <Route path="/profile" element={<Profile />} />
          <Route path="/team-todo" element={<Team />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default AppRoute;
