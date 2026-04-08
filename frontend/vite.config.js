// ==============================================================================
// INTEGRATION: Vite Development Proxy
// PURPOSE: Routes all frontend calls starting with '/api' to the local .NET 
//          backend, bypassing browser CORS restrictions during development.
// ==============================================================================

import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5211', // <-- UPDATE THIS PORT TO MATCH YOUR API
        changeOrigin: true,
        secure: false, // Required because .NET uses self-signed dev certificates
      }
    }
  }
})