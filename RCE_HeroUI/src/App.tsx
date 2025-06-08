import { Route, Routes } from "react-router-dom";

import IndexPage from "@/pages/index";
import LoginPage from "@/pages/Auth/LoginPage";
import RegisterPage from "./pages/Auth/RegisterPage";
import DashboardPage from "./pages/DashboardPage";
import RoleRoute from "./routes/RoleRoute";
import { PrivateRoute } from "./routes/PrivateRoute";
import AdminPage from "./pages/AdminPage";

function App() {
  return (
    <Routes>
      <Route element={<IndexPage />} path="/" />
      <Route element={<LoginPage />} path="/login" />
      <Route element={<RegisterPage />} path="/register" />
      <Route element={
        <PrivateRoute>
          <DashboardPage />
        </PrivateRoute>
      } path="/dashboard" />
      <Route element={
        <RoleRoute allowedRoles={["admin"]} >
          <AdminPage />
        </RoleRoute>
      } path="/admin" />
    </Routes>
  );
}

export default App;
