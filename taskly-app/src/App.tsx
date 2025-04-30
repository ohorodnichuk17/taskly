import './App.css'
import { Navigate, Route, Routes } from 'react-router-dom'
import { RegisterPage } from './components/authentication/RegisterPage'
import { PageNotFound } from './components/general/PageNotFound'

import { useEffect } from 'react'
import { LoginPage } from './components/authentication/LoginPage'
import { useAppDispatch, useRootState } from './redux/hooks'
import { checkTokenAsync } from './redux/actions/authenticateAction'
import { DashboardPage } from './components/user/DashboardPage'
import { ForgotPasswordPage } from './components/authentication/ForgotPasswordPage'
import { AuthenticationPage } from './components/authentication/AuthenticationPage'
import { ChangePasswordPage } from './components/authentication/ChangePasswordPage'
import MainContainer from "./components/general/MainContainer.tsx";
import AIAgent from "./components/user/AiAgent.tsx";
import { BoardsPage } from './components/boards/BoardsPage.tsx'
import { BoardPage } from './components/boards/BoardPage.tsx'
import MainPage from "./components/general/MainPage.tsx";
import { ProfilePage } from "./components/user/ProfilePage.tsx";
import TablesListPage from "./components/table/TablesListPage.tsx";
import CreateTablePage from "./components/table/CreateTablePage.tsx";
import TablePage from "./components/table/TablePage.tsx";
import EditTablePage from "./components/table/EditTablePage.tsx";
import TableFormPage from "./components/table/TableFormPage.tsx";
import { CreateTableItemPage } from "./components/table/CreateTableItemPage.tsx";
import AddMemberToTablePage from "./components/table/AddMemberToTablePage.tsx";


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

        <Route path="/" element={<DashboardPage />}>
          <Route path="" element={<MainPage />} />

          <Route path="artificial-intelligence" element={<AIAgent />} />

          {isLogin && (<>
            <Route path='/boards' element={<BoardsPage />} />
            <Route path='/boards/:boardId' element={<BoardPage />} />
            <Route path='/boards/create-new-board' element={<PageNotFound />} />
            <Route path='/edit-profile' element={<ProfilePage />} />
            <Route path='/tables' element={<TablesListPage />} />
            <Route path='/tables/create' element={<TableFormPage />} />
            <Route path="/tables/:tableId" element={<TablePage />} />
            <Route path="/tables/edit/:tableId" element={<TableFormPage />} />
            <Route path="/tables/:tableId/create" element={<CreateTableItemPage />} />
            <Route path="/tables/:tableId/add-member" element={<AddMemberToTablePage />} />
          </>)}
          <Route path='/boards' element={<Navigate to="/authentication/login" />} />


        </Route>

        <Route path='/authentication/' element={<AuthenticationPage />}>
          <Route path='register' element={<RegisterPage />}></Route>
          <Route path='login' element={<LoginPage />}></Route>
          <Route path="forgot-password" element={<ForgotPasswordPage />}></Route>
          <Route path={`change-password/:key`} element={<ChangePasswordPage />}></Route>

        </Route>

        <Route path='*' element={<PageNotFound />} />

      </Routes>
    </MainContainer>
  )
}

export default App