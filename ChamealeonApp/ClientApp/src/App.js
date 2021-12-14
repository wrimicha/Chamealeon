import React, { Component } from 'react';
import { Layout } from './components/Shared/Layout';
import Home from './components/Views/Home';
import PrivateRoute from './components/PrivateRoute';
import Signup from './components/Views/Signup'
import Login from './components/Views/Login'
import './styles/custom.css'
import NutritionalInformation from './components/Views/NutritionalInformation';
import ShoppingList from './components/ShoppingList/ShoppingList';
import PublicRoute from './components/PublicRoute';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        {/* <PrivateRoute exact path='/' component={Home} />
        <Route exact path='/Login' component={Login}></Route>
        <Route exact path='/Signup' component={Signup}></Route>
        <Route exact path='/shoppingList' component={ShoppingList} />
        <Route exact path='/nutritionalInformation' component={NutritionalInformation} />
        <Route exact path='/generateMealPlan' component={CreateMealPlan} /> */}
        <PrivateRoute component={Home} path="/" exact />
        <PublicRoute restricted={true} component={Login} path="/Login" exact />
        <PublicRoute restricted={true} component={Signup} path="/Signup" exact />
        <PrivateRoute component={NutritionalInformation} path="/NutritionalInformation" exact />
        <PrivateRoute component={ShoppingList} path="/ShoppingList" exact />

      </Layout>
    );
  }
}
