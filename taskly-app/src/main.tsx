import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { Provider } from 'react-redux'
import { store } from './redux/store.ts'
import { BrowserRouter } from 'react-router-dom'


createRoot(document.getElementById('root')!).render(

  <BrowserRouter
    future={{
      v7_startTransition: true,
      v7_relativeSplatPath: true
    }}
  >
    <Provider store={store}>
      <App />
    </Provider>
  </BrowserRouter>
)
/*<PersistGate loading={null} persistor={persistor}>
</PersistGate>*/