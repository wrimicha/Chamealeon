import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export const Layout = ({ authHook, children, ...rest }) => {

  return (
    <div>
      <NavMenu authHook={authHook}/>
      <Container>
        {children}
      </Container>

    </div>
  )
}

export default Layout

