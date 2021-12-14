import React, { useEffect, useState } from 'react';
import { Layout } from './components/Shared/Layout';
import Home from './components/Views/Home';
import PrivateRoute from './components/PrivateRoute';
import Signup from './components/Views/Signup'
import Login from './components/Views/Login'
import './styles/custom.css'
import NutritionalInformation from './components/Views/NutritionalInformation';
import ShoppingList from './components/ShoppingList/ShoppingList';
import PublicRoute from './components/PublicRoute';
import axios from 'axios';
import Profile from './components/Views/Profile';

const App = () => {

  const [auth, setAuth] = useState(false)

  useEffect(() => {
    axios.get("http://localhost:5000/api/user/validate", { withCredentials: true }).then(response => setAuth(true)).catch(err => setAuth(false))
    console.log(auth)
  })
  const authHook = { auth: auth, setAuth: setAuth }
  return (
    <Layout>
      <PublicRoute authHook={authHook} restricted={true} component={Login} path={["/Login", "/"]} exact />
      <PrivateRoute authHook={authHook} component={Home} path="/Home" exact />
      <PublicRoute authHook={authHook} restricted={true} component={Signup} path="/Signup" exact />
      <PrivateRoute authHook={authHook} component={NutritionalInformation} path="/NutritionalInformation" exact />
      <PrivateRoute authHook={authHook} component={ShoppingList} path="/ShoppingList" exact />
    </Layout>
  );
}

export default App