import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Shared/Layout';
import { Home } from './components/Views/Home';
import ShoppingList from './components/ShoppingList/ShoppingList';
import PrivateRoute from './components/PrivateRoute';
import NutritionalInformation from './components/Views/NutritionalInformation';
import Signup from './components/Views/Signup'
import Login from './components/Views/Login'
import './styles/custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <PrivateRoute exact path='/' component={Home} />
        <Route exact path='/Login' component={Login}></Route>
        <Route exact path='/Signup' component={Signup}></Route>
        <Route exact path='/ShoppingList' component={ShoppingList} />
        <Route exact path='/NutritionalInformation' component={NutritionalInformation} />
      </Layout>
    );
  }
}
