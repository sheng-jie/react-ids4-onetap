import React from 'react';
import {BrowserRouter as Router, Route, Link} from 'react-router-dom'
import SecretAuth from './Pages/secretAuth'
import UserAuth from './Pages/userAuth'

function App () {
  return (
    <Router>
      <div>
        <h1>AnDy's AuTh3nTiCaTi0n PlAyGr0uNd</h1>
        <nav>
          <ul>
            <li>
              <Link to="/sharedsecret/">Shared Secret Auth</Link> - Authenticate web app based on shared secret
            </li>
            <li>
              <Link to="/usercredentials/">User Credentials Auth</Link> - Authenticate web app based on entered user credentials
            </li>
          </ul>
        </nav>
      </div>

      <Route path="/sharedsecret/" component={SecretAuth} />
      <Route path="/usercredentials/" component={UserAuth} />
    </Router>
  )
  
}

export default App;
