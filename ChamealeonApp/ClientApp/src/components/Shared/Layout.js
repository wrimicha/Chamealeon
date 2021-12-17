import React from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export const Layout = ({ authHook, children, ...rest }) => {
// TODO fix navmenu
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

