import React, { Fragment } from 'react';
import { Button, Collapse, Container, Navbar, NavbarBrand, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import '../../styles/NavMenu.css';

export const NavMenu = ({ authHook }) => {
  const [auth, setAuth] = authHook
  const navItems = !auth ? (
    <Fragment>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/Login">Login</NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/Signup">Signup</NavLink>
      </NavItem>
    </Fragment>
  ) : (
    <Fragment>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/Home">Home</NavLink>
      </NavItem>

      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/NutritionalInformation">Nutritional Information</NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/ShoppingList">Shopping List</NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/Profile">Profile</NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/CreateMealPlan">Create Plan</NavLink>
      </NavItem>
      <NavItem>
        <Button color="danger" onClick={()=> {
          localStorage.removeItem("jwt")
          setAuth(false)
        }}>
          Logout
        </Button>
      </NavItem>
    </Fragment>
  );


  return (
    <header>
      <Navbar className="navbar-expand-sm ng-white border-bottom box-shadow mb-3" light>
        <Container>
          <NavbarBrand tag={Link} to="/">ChamealeonApp</NavbarBrand>
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" navbar>
            <ul className="navbar-nav flex-grow">
              {navItems}
            </ul>
          </Collapse>
        </Container>
      </Navbar>
    </header>
  );
}

export default NavMenu

