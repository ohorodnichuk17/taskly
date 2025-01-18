import './App.css'
import { Route, Routes } from 'react-router-dom'
import { RegisterPage } from './components/authentication/RegisterPage'

function App() {

  return (

    <Routes>
      <Route path='/authentication/'>
        <Route path='register' element={<RegisterPage />}></Route>
      </Route>
    </Routes>

  )
}

export default App
