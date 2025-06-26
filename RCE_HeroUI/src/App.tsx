import { Route, Routes } from "react-router-dom";
import { PrivateRoute, PublicRoute, AdminRoute } from "./routes/ProtectedRoutes";

import IndexPage from "@/pages/index";
import DashboardPage from "./pages/DashboardPage";
import AdminPage from "./pages/AdminPage";
import NotFoundPage from "./pages/NotFoundPage";
import Callback from "./components/Auth/CallbackAuth";
import { ProvidersList } from "./pages/Providers/ProvidersListPage";
import { ProviderDetail } from "./pages/Providers/ProviderDetailPage";

function App() {
  return (
    <Routes>
      <Route element={<IndexPage />} path="/" />
      <Route path="*" element={<NotFoundPage />} />

      <Route element={<PublicRoute />}>
        <Route path="/auth/callback" element={<Callback />} />
        <Route path="/providers" element={<ProvidersList />} />
        <Route path="/providers/:id" element={<ProviderDetail />} />
      </Route>

      <Route element={<PrivateRoute />}>
        <Route path="/dashboard" element={<DashboardPage />} />
      </Route>

      <Route element={<AdminRoute />}>
        <Route path="/admin" element={<AdminPage />} />
      </Route>
    </Routes>
  );
}

export default App;
