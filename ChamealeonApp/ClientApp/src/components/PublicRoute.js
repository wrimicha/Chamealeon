import { Redirect, Route } from "react-router-dom";
const PublicRoute = ({ component: Component, restricted, authHook, ...rest }) => {
    const [auth] = authHook
    
    console.log(auth)
    return (
        // restricted = false meaning public route
        // restricted = true meaning restricted route
        <Route {...rest} render={props => (
            auth && restricted ?
                <Redirect to="/Home" />
                : <Component authHook={authHook} {...props} />
        )} />
    );
};

export default PublicRoute