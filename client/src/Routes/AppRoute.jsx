import { Routes, Route } from "react-router-dom";
import Home from "../Pages/Home/Home";
import RootLayout from "../Layouts/RootLayout";
import Profile from "../Pages/Profile/Profile";
import Login from "../Components/Login";
import Register from "../Components/Register";
import Team from "../Pages/Team/Team";
import ProtectedRoute from "./ProtectedRoute";
import EditProfile from "../Pages/Profile/EditProfile";
import MyTeam from "../Pages/MyTeamTodo/MyTeam";
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
          <Route path="/profile-edit/:slug" element={<EditProfile />} />
          <Route path="/team-todo" element={<Team />} />
          <Route path="/my-team-todo" element={<MyTeam />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default AppRoute;
