import { Route, Redirect } from 'react-router';


const PrivateRoute = ({ component: Component, ...rest }) => {
    const isLogin = () => {

    }
    return (
        <Route {...rest} render={props => (
            isLogin() ?
                <Component {...props} />
                : <Redirect to="/signin" />
        )
        } />
    );
};

export default PrivateRoute