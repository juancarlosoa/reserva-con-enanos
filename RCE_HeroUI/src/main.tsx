import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";

import App from "./App.tsx";
import { AppProvider } from "./AppProvider.tsx";
import './styles/globals.css';
import { AuthProvider } from "./auth/AuthProvider.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <BrowserRouter>
      <AppProvider>
        <AuthProvider>
          <App />
        </AuthProvider>
      </AppProvider>
    </BrowserRouter>
  </React.StrictMode>
);
