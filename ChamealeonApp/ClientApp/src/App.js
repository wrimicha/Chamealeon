import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import ShoppingList from './components/ShoppingList/ShoppingList';
import PrivateRoute from './PrivateRoute';
import NutritionalInformation from './components/Views/NutrtitionalInformation/NutritionalInformation';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        {/* <PrivateRoute> <Home /> </PrivateRoute> */}
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/shopping-list' component={ShoppingList} />
        <Route path='/nutritionalinformation' component={NutritionalInformation} />
      </Layout>
    );
  }
}
