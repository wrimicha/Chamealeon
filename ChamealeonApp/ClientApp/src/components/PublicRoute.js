import { Redirect, Route } from "react-router-dom";
const PublicRoute = ({ component: Component, restricted, authHook, ...rest }) => {
    const {auth} = authHook
    

    return (
        // restricted = false meaning public route
        // restricted = true meaning restricted route
        <Route {...rest} render={props => (
            auth && restricted ?
                <Redirect to="/Home" />
                : <Component {...props} />
        )} />
    );
};

export default PublicRoute