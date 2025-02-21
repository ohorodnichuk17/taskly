import './App.css'
import { Navigate, Route, Routes } from 'react-router-dom'
import { RegisterPage } from './components/authentication/RegisterPage'
import { PageNotFound } from './components/general/PageNotFound'

import { useEffect, useState } from 'react'
import { LoginPage } from './components/authentication/LoginPage'
import { useAppDispatch, useRootState } from './redux/hooks'
import { checkTokenAsync } from './redux/actions/authenticateAction'
import { DashboardPage } from './components/user/DashboardPage'
import { ForgotPasswordPage } from './components/authentication/ForgotPasswordPage'
import { AuthenticationPage } from './components/authentication/AuthenticationPage'
import { ChangePasswordPage } from './components/authentication/ChangePasswordPage'
<<<<<<< HEAD
import { ProfilePage } from './components/user/ProfilePage'
=======
import MainContainer from "./components/general/MainContainer.tsx";
>>>>>>> 961184718f5adf4c989118eff31f73c3094e4b3c


function App() {

  const dispatch = useAppDispatch();

  const isLogin = useRootState(s => s.authenticate.isLogin);

  const checkUserToken = async () => {
    await dispatch(checkTokenAsync());
  }
  useEffect(() => {
    checkUserToken();
  }, [])
  useEffect(() => {
    console.log(isLogin)
  }, [isLogin])



  return (

  <MainContainer>
      <Routes>
          <Route path='/authentication/' element={<AuthenticationPage />}>
              <Route path='register' element={<RegisterPage />}></Route>
              <Route path='login' element={<LoginPage />}></Route>
              <Route path="forgot-password" element={<ForgotPasswordPage />}></Route>
              <Route path={`change-password/:key`} element={<ChangePasswordPage />}></Route>

<<<<<<< HEAD
      </Route>
      <Route path='/dashboard/' element={<DashboardPage />}>
        {isLogin &&
          <Route path='profile' element={<ProfilePage />}></Route>
        }
        <Route path='*' element={<PageNotFound />} />
      </Route>

    </Routes>
=======
          </Route>
>>>>>>> 961184718f5adf4c989118eff31f73c3094e4b3c

          {isLogin &&
              <Route path='/dashboard' element={<DashboardPage />}>
              </Route>
          }
          <Route path='/dashboard' element={<Navigate to={"/authentication/login"} />}>
          </Route>
          <Route path='*' element={<PageNotFound />} />
      </Routes>
  </MainContainer>
  )
}

export default App
/*
<Route path='/dashboard' element={<Navigate to={"/authentication/login"} />}>
      </Route>
      <Route path='/profile' element={<ProfilePage />}></Route>
*/