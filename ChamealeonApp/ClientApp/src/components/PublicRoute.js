import { Redirect, Route } from "react-router-dom";

const PublicRoute = ({ component: Component, restricted, ...rest }) => {

    const isLogin = () => { }

    return (
        // restricted = false meaning public route
        // restricted = true meaning restricted route
        <Route {...rest} render={props => (
            isLogin() && restricted ?
                <Redirect to="/dashboard" />
                : <Component {...props} />
        )} />
    );
};

export default PublicRoute