import './App.css'
import { Route, Routes } from 'react-router-dom'
import { Test1Comp } from './components/Test1Comp'
import { Test2Comp } from './components/Test2Comp'

function App() {

  return (

    <Routes>
      <Route path='/hello' element={<Test1Comp />}></Route>
      <Route path='/world' element={<Test2Comp />}></Route>
    </Routes>

  )
}

export default App
