import React from 'react';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom"

import LandingPage from './components/views/LandingPage/LandingPage'
import LoginPage from './components/views/LoginPage/LoginPage'
import RegisterPage from './components/views/RegisterPage/RegisterPage'
import Auth from './hoc/auth';

function App() {
  return (
    <Router>
      <div>
        <Switch>
          <Route exact path="/" component={ Auth(LandingPage, null) } /> {/** exact가 없으면 URL 앞부분만 대충 맞아도 찾아들어감 */}
          <Route exact path="/login" component={ Auth(LoginPage, false)} />
          <Route exact path="/register" component={ Auth(RegisterPage, false) } />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
