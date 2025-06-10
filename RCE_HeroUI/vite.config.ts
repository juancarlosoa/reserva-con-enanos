import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tsconfigPaths from "vite-tsconfig-paths";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), tsconfigPaths()],
  server: {
    host: '0.0.0.0',
    port: 5173,
    strictPort: true,
    hmr: {
      host: 'localhost',
      protocol: 'ws',
      port: 5173
    },
    watch: {
      usePolling: true,
      interval: 100,
    },
    proxy: {
      '/auth/api': {
        target: 'http://gateway:8080',
        changeOrigin: true,
        secure: false,
      },
    },
  },
});