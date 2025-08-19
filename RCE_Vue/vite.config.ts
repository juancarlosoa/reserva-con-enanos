import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { fileURLToPath, URL } from 'node:url'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  
  server: {
    host: '0.0.0.0',
    port: 5174,
    strictPort: true, 
    watch: {
      usePolling: true,
      interval: 100
    },
    hmr: {
      port: 5174
    },
    proxy: {
      '/api': {
        target: 'https://gateway:8080',
        changeOrigin: true,
        secure: false,
        rewrite: (path) => path.replace(/^\/api/, '')
      }
  
    }
  },
  optimizeDeps: {
    include: ['vue']
  },
  build: {
    target: 'esnext',
    sourcemap: true,
    rollupOptions: {
      output: {
        manualChunks: {
          vendor: ['vue']
        }
      }
    }
  }
})