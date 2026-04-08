// ==============================================================================
// INTEGRATION: Vite Development Proxy
// PURPOSE: Routes all frontend calls starting with '/api' to the local .NET 
//          backend, bypassing browser CORS restrictions during development.
// ==============================================================================

import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
  plugins: [
    react(),
    tailwindcss(),
  ],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5211',
        changeOrigin: true,
        secure: false, // Required because .NET uses self-signed dev certificates
      }
    }
  }
})