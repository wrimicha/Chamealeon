import { Route, Redirect } from 'react-router';


const privateRoute = ({ component, ...rest }) => {
    // get auth some how from identity core
    let auth = true
    return (
        auth ?
            <Route {...rest} component={component}/>
            :
            <Redirect
                to={{
                    pathname: "/Login",
                }} />
    )

}

export default privateRoute