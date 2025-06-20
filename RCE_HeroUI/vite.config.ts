import fs from 'fs';
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tsconfigPaths from "vite-tsconfig-paths";

export default defineConfig({
  plugins: [react(), tsconfigPaths()],
  server: {
    host: '0.0.0.0',
    port: 5173,
    strictPort: true,
    https: {
      key: fs.readFileSync('./cert/key.pem'),
      cert: fs.readFileSync('./cert/cert.pem'),
    },
    hmr: {
      host: 'localhost',
      protocol: 'wss',
      port: 5173
    },
    watch: {
      usePolling: true,
      interval: 100,
    },
    proxy: {
      '/auth': {
        target: 'https://gateway:8080',
        changeOrigin: true,
        secure: false,
      },
    },
  },
});