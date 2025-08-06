import { Route, Routes } from "react-router-dom";
import { AdminRoute } from "./routes/ProtectedRoutes";

import IndexPage from "@/pages/index";
import AdminPage from "./pages/AdminPage";
import NotFoundPage from "./pages/NotFoundPage";
import { ProvidersList } from "./providers/pages/ProvidersListPage";
import { ProviderDetail } from "./providers/pages/ProviderDetailPage";
import Layout from "./layouts/default";

function App() {
  return (
    <Routes>
      <Route
        index
        element={
          <Layout>
            <IndexPage />
          </Layout>
        }
      />
      <Route path="*" element={<NotFoundPage />} />
      <Route
        path="providers"
        element={
          <Layout>
            <ProvidersList />
          </Layout>
        }
      />
      <Route
        path="providers/:id"
        element={
          <Layout>
            <ProviderDetail />
          </Layout>
        }
      />
      <Route element={<AdminRoute />}>
        <Route
          path="admin"
          element={
            <Layout>
              <AdminPage />
            </Layout>
          }
        />
      </Route>
    </Routes>
  );
}

export default App;
