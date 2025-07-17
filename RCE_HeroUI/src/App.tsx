import { Route, Routes } from "react-router-dom";
import { PrivateRoute, AdminRoute } from "./routes/ProtectedRoutes";

import IndexPage from "@/pages/index";
import DashboardPage from "./pages/DashboardPage";
import AdminPage from "./pages/AdminPage";
import NotFoundPage from "./pages/NotFoundPage";
import { ProvidersList } from "./providers/pages/ProvidersListPage";
import { ProviderDetail } from "./providers/pages/ProviderDetailPage";

function App() {
  return (
    <Routes>
      <Route element={<IndexPage />} path="/" />
      <Route path="*" element={<NotFoundPage />} />

      <Route path="/providers" element={<ProvidersList />} />
      <Route path="/providers/:id" element={<ProviderDetail />} />

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
